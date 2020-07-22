using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using Microsoft.VisualBasic;
using System.IO;
using TwainLib;
using TwainGui;
using GdiPlusLib;
using System.Globalization;
using System.Diagnostics;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;

namespace Arenda
{
	public partial class frmScannedDocs : System.Windows.Forms.Form, IMessageFilter
	{
		readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
		ImageClass IC = new ImageClass();

		private bool msgfilter;
		public Twain tw;

		Image image = null;
		int id_Fines;
		string PaymentName;
		DateTime date;
		string Sum;		

		DataTable dtIm;
		string AppPath = Path.GetDirectoryName(Application.ExecutablePath);
		string ImagePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\images";
		public static string sFileName = "";

		public frmScannedDocs(int _id_Fines, string _PaymentName, DateTime _date, string _Sum)
		{
			InitializeComponent();
			id_Fines = _id_Fines;
			PaymentName = _PaymentName;
			date = _date;
			Sum = _Sum;

			tw = new Twain();
			tw.Init(this.Handle);


            

        }

		private void frmScannedDocs_Load(object sender, EventArgs e)
		{
			timer1.Tick += new EventHandler(timer1_Tick); // Everytime timer ticks, timer_Tick will be called
			timer1.Interval = (10) * (1);              // Timer will tick evert second
			timer1.Enabled = true;                       // Enable the timer
			timer1.Start();                              // Start the timer

			if (TempData.Rezhim == "ПР")
			{
				this.Text = "Просмотр изображения";

				btnUpload.Visible = false;
				btnScan.Visible = false;
				btnDel.Visible = false;
			}
			InputLanguage.CurrentInputLanguage = GetInputLanguageByName("ru");
			txtPaymentName.Text = PaymentName;
			dtpDate.Value = date;
			txtSum.Text = Sum;

			GetImagesList();
		}

		private void GetImagesList()
		{
			dtIm = new DataTable();
			dtIm = _proc.GetPictures(id_Fines);

			dgImages.DataSource = null;
			dgImages.AutoGenerateColumns = false;
			dgImages.DataSource = dtIm;

			GetImage();
		}

		private void GetImage()
		{
			if (dgImages.CurrentRow != null)
			{
				image = null;
				pbPhoto.Image = null;

				if ((dtIm != null) && (dtIm.Rows.Count > 0))
				{
					byte[] ImageBytes = (byte[])dtIm.Select("id = " + dgImages.CurrentRow.Cells["id"].Value.ToString())[0]["DocScan"];
					image = IC.ByteArrayToImage(ImageBytes);
				}

				pbPhoto.Image = image;

				pbPhoto.Location = new Point(1, 1);
				pbPhoto.Size = new Size(panel1.Width - 2, panel1.Height - 2);


				if (pbPhoto.Image != null)
				{
					pbPhoto.SizeMode = PictureBoxSizeMode.Zoom;
				}

				btPrint.Enabled = true;
				btnDel.Enabled = true;
				btnZoomIn.Enabled = true;
				btnZoomOut.Enabled = true;
				btnLeft.Enabled = !(dgImages.CurrentRow.Index == 0);
				btnRight.Enabled = !(dgImages.CurrentRow.Index == dgImages.Rows.Count - 1);
			}
			else
			{
				image = null;
				pbPhoto.Image = null;

				btPrint.Enabled = false;
				btnDel.Enabled = false;
				btnLeft.Enabled = false;
				btnRight.Enabled = false;
				btnZoomIn.Enabled = false;
				btnZoomOut.Enabled = false;
			}
		}

		private void btnUpload_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.InitialDirectory = Environment.SpecialFolder.MyComputer.ToString();
			//dlg.InitialDirectory = "D:";
			dlg.Filter = "Все рисунки (*.jpg, *.jpeg, *.jpe, *.png)|*.jpg; *.jpeg; *.jpe; *.png*";
			dlg.FilterIndex = 1;
			dlg.Multiselect = false;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				foreach (String filepath in dlg.FileNames)
				{
					Image picForCheck = Image.FromFile(filepath);

					if (picForCheck != null)
					{
						System.IO.FileInfo file = new System.IO.FileInfo(filepath);
						CheckAndSaveImage(picForCheck, file);
					}
				}

				GetImagesList();
			}
		}

		private void CheckAndSaveImage(Image pictureForCheck, FileInfo file)
		{
			decimal sizeBytes = Math.Round(decimal.Parse(file.Length.ToString()), 0);

			decimal maxSizeBytes = _proc.GetSettings("psof", 1000000);

			if (sizeBytes > maxSizeBytes)
			{
				MessageBox.Show("Размер файла ("
						+ sizeBytes.ToString()
						+ " байт) превышает \nдопустимый размер (" + maxSizeBytes.ToString()
						+ " байт) \nСохранение файла невозможно.", "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			int picWidth = pictureForCheck.Width;
			int picHeight = pictureForCheck.Height;

			decimal maxPicWidth = _proc.GetSettings("pxlx", 800);
			decimal maxPicHeight = _proc.GetSettings("pxly", 600);
			
			if ((picHeight > maxPicHeight)
					||
				 (picWidth > maxPicWidth))
			{
				MessageBox.Show("Разрешение файла ("
						+ picWidth.ToString() + "x" + picHeight.ToString() + ") превышает"
						+ "\nдопустимый размер ("
						+ maxPicWidth.ToString() + "x" + maxPicHeight.ToString() + ")."
						+ "\nСохранение файла невозможно.", "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Error);

				TempData.NeedToRefresh = true;
				return;
			}
			
			byte[] pic = IC.ImageToByteArray(pictureForCheck);

			int id_CheckScan = _proc.AddPicture(id_Fines, file.Name, pic);

            Logging.StartFirstLevel(823);
            Logging.Comment("Данные договора, к которому был загружен внешний файл");
            Logging.Comment("№ договора: " + numDoc);
            Logging.Comment("Арендатор ID: " + idArend + " ; Наименование: " + nameArend);

            Logging.Comment("Данные оплаты");
            Logging.Comment("ID: " + id_Fines);
            Logging.Comment("Наименвоание доп.оплаты: " + PaymentName);
            Logging.Comment("Дата выписки: " + date.ToShortDateString());
            Logging.Comment("Сумма оплаты: " + Sum);

            Logging.Comment("Данные прикрепенного файла");
            Logging.Comment("ID: " + id_CheckScan);
            Logging.Comment("Файл наименование: " + file.Name + " ; расширение: " + file.Extension);

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
            + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            TempData.NeedToRefresh = true;
		}


		private void dgImages_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				GetImage();
			}
			catch { }
		}

		private void btExit_Click(object sender, EventArgs e)
		{
            if (Directory.Exists(ImagePath))
            {
                DelGarbage();
            }			
			this.Close();
		}

		private void btnScan_Click(object sender, EventArgs e)
		{
			try
			{
				if (TempData.isFirstScan == 0)
				{
					TempData.isFirstScan = 1;
					tw.Select();
				}
				if (!msgfilter)
				{
					msgfilter = true;
					Application.AddMessageFilter(this);
				}
				if (!tw.Acquire())
				{
					MessageBox.Show("Операция отменена, \nлибо необходимо проверить \nподключение сканера", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);


					//if (tw.getSRCT()== 0)
					//{
					//    MessageBox.Show("Не найдено подключенных сканеров", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
					//}
					//this.Close();
				}
				//this.Activate();
			}
			catch
			{
				MessageBox.Show("Сканер недоступен");
				tw.CloseSrc();
				EndingScan();
			}			
		}

		/// <summary>
		/// Выбор языка из установленых
		/// </summary>
		/// <param name="inputName">язык</param>
		/// <returns></returns>
		public static InputLanguage GetInputLanguageByName(string inputName)
		{
			foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
			{
				if (lang.Culture.EnglishName.ToLower().StartsWith(inputName))
				{
					return lang;
				}
			}
			return null;
		}

		bool IMessageFilter.PreFilterMessage(ref Message m)
		{
			string sFileImage = "";
			TwainCommand cmd = tw.PassMessage(ref m);
			if (cmd == TwainCommand.Null)
			{
				//EndingScan();
				return false;
			}
			if (cmd == TwainCommand.Not)
				return false;

			switch (cmd)
			{
				case TwainCommand.CloseRequest:
					EndingScan();
					tw.CloseSrc();
					break;
				case TwainCommand.CloseOk:
					EndingScan();
					tw.CloseSrc();
					break;
				case TwainCommand.DeviceEvent:
					break;
				case TwainCommand.TransferReady:
					IntPtr img;
					ArrayList pics = tw.TransferPictures();
					EndingScan();
					tw.CloseSrc();
					for (int i = 0; i < pics.Count; i++)
					{
						img = (IntPtr)pics[i];
						PicForm newpic = new PicForm(img);
						int picnum = i + 1;
					}
					if (pics.Count > 0)
					{
						sFileImage = getFileImage().Trim();

						try
						{
							if (!Directory.Exists(ImagePath))
							{
								DirectoryInfo di = Directory.CreateDirectory(ImagePath);
							}

							string filepath = ImagePath + @"\" + sFileImage.Trim();

							Gdip.SaveDIBAsFile(filepath, PicForm.bmpptr, PicForm.pixptr);

							Image picForCheck = Image.FromFile(filepath);

							if (picForCheck != null)
							{
								System.IO.FileInfo file = new System.IO.FileInfo(filepath);
								CheckAndSaveImage(picForCheck, file);
							}
						}
						catch { }
					}
					else
					{
						MessageBox.Show("Ошибка сканирования!");
					}
					break;
			}

			return true;
		}

		private void DelGarbage()
		{
			string[] aFiles = Directory.GetFiles(ImagePath, "*.jpg", SearchOption.TopDirectoryOnly);

			for (int i = 0; i < aFiles.Length; i++)
			{
				try
				{
					File.Delete(aFiles[i].ToString());
				}
				catch { }
			}
		}

		//формирует имя временного файла 
		private string getFileImage()
		{
			/*
			string[] aFiles = Directory.GetFiles(ImagePath, "*.jpg", SearchOption.TopDirectoryOnly);
			string cF = "";
			string c0 = "";
			int iDoc = 0;
			int j = 0;
			DataTable dt = new DataTable();
			dt.Columns.Add("num", typeof(int));
			dt.AcceptChanges();

			for (int i = 0; i < aFiles.Length; i++)
			{
				int t = 0;


				if (aFiles[i].Contains("Img"))
				{
					j = aFiles[i].LastIndexOf(@"\") + 5;
					cF = aFiles[i].Substring(j, 6);
					if (cF.IndexOf(".") > 0)
					{
						cF = cF.Substring(0, cF.IndexOf(".") - 1);
					}

					int.TryParse(cF, out t);

					if (t > 0)
					{
						dt.Rows.Add(t);
						dt.AcceptChanges();
					}
				}
			}

			int maxNum = 0;

			if (dt.Rows.Count > 0)
			{
				for (int r = 0; dt.Rows.Count > r; r++)
				{
					if (int.Parse(dt.Rows[r]["num"].ToString()) > maxNum)
					{
						maxNum = int.Parse(dt.Rows[r]["num"].ToString());
					}
				}
			}

			if (maxNum > iDoc)
			{
				iDoc = maxNum;
			}

			iDoc = iDoc + 1;

			while (c0.Length < 7)
			{
				if (c0.Length == 0)
				{
					c0 = iDoc.ToString();
				}
				else
				{
					c0 = "0" + c0;
				}
			}

			sFileName = "Img" + c0 + ".jpg";
			*/
			frmScannedFileName frmName = new frmScannedFileName();
			frmName.ShowDialog();

			if (TempData.ScannedFileName.Length > 0)
			{
				sFileName = TempData.ScannedFileName + ".jpg";
				TempData.ScannedFileName = "";
			}
			else
			{
				sFileName = "temp" + ".jpg";
				TempData.ScannedFileName = "";
			}
			return sFileName;
		}

		private void EndingScan()
		{
			if (msgfilter)
			{
				Application.RemoveMessageFilter(this);
				msgfilter = false;
				//this.Enabled = true;
				//this.Activate();
			}
		}

		private void btnLeft_Click(object sender, EventArgs e)
		{
			try
			{
				dgImages.CurrentCell = dgImages.Rows[dgImages.CurrentRow.Index - 1].Cells[0];
				dgImages.CurrentCell.Selected = true;
				GetImage();
			}
			catch { }
		}

		private void btnRight_Click(object sender, EventArgs e)
		{
			try
			{
				dgImages.CurrentCell = dgImages.Rows[dgImages.CurrentRow.Index + 1].Cells[0];
				dgImages.CurrentCell.Selected = true;
				GetImage();
			}
			catch { }
		}

		private void btnZoomOut_Click(object sender, EventArgs e)
		{
			pbPhoto.Height -= 20;
			pbPhoto.Width -= 20;
		}

		private void btnZoomIn_Click(object sender, EventArgs e)
		{
			pbPhoto.Height += 20;
			pbPhoto.Width += 20;
		}

		private void btnDel_Click(object sender, EventArgs e)
		{
			if (dgImages.CurrentRow != null)
			{
				DialogResult d = MessageBox.Show("Удалить выбранное \nизображение?", "Удаление изображения", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (d == DialogResult.Yes)
				{
					int idImage = int.Parse(dgImages.CurrentRow.Cells["id"].Value.ToString());
                    string _cName = dgImages.CurrentRow.Cells["cName"].Value.ToString();

                    _proc.DelPicture(idImage);

                    Logging.StartFirstLevel(822);
                    Logging.Comment("Данные договора, к которому был загружен внешний файл");
                    Logging.Comment("№ договора: " + numDoc);
                    Logging.Comment("Арендатор ID: " + idArend + " ; Наименование: " + nameArend);

                    Logging.Comment("Данные оплаты");
                    Logging.Comment("ID: " + id_Fines);
                    Logging.Comment("Наименвоание доп.оплаты: " + PaymentName);
                    Logging.Comment("Дата выписки: " + date.ToShortDateString());
                    Logging.Comment("Сумма оплаты: " + Sum);

                    Logging.Comment("Данные прикрепенного файла");
                    Logging.Comment("ID: " + idImage);
                    Logging.Comment("Файл наименование: " + _cName);

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                    GetImagesList();
				}
			}


		}

		private void btPrint_Click(object sender, EventArgs e)
		{
			try
			{
				string nameImage = "temp.jpg";
				string path = Application.StartupPath + @"\" + nameImage;

				if (File.Exists(path))
				{
					File.Delete(path);
				}

				pbPhoto.Image.Save(path);

                int idImage = int.Parse(dgImages.CurrentRow.Cells["id"].Value.ToString());
                string _cName = dgImages.CurrentRow.Cells["cName"].Value.ToString();
                Logging.StartFirstLevel(221);
                Logging.Comment("Данные договора, к которому был загружен внешний файл");
                Logging.Comment("№ договора: " + numDoc);
                Logging.Comment("Арендатор ID: " + idArend + " ; Наименование: " + nameArend);

                Logging.Comment("Данные оплаты");
                Logging.Comment("ID: " + id_Fines);
                Logging.Comment("Наименвоание доп.оплаты: " + PaymentName);
                Logging.Comment("Дата выписки: " + date.ToShortDateString());
                Logging.Comment("Сумма оплаты: " + Sum);

                Logging.Comment("Данные прикрепенного файла");
                Logging.Comment("ID: " + idImage);
                Logging.Comment("Файл наименование: " + _cName);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();


                System.Diagnostics.Process.Start(path);
			}
			catch { MessageBox.Show("Ошибка открытия файла для печати!"); }

			//PrintDocument DocPrn = new PrintDocument();
			//DocPrn.PrintPage += new PrintPageEventHandler(PRDImages);

			//PrintPreviewDialog dlg = new PrintPreviewDialog();
			//dlg.WindowState = FormWindowState.Maximized;
			//dlg.Location = new System.Drawing.Point(this.Top - 4, this.Left - 4);
			//dlg.Document = DocPrn;
			//dlg.MinimumSize = new System.Drawing.Size(375, 250);
			//dlg.UseAntiAlias = true;
			//dlg.ShowDialog();
		}

		void PRDImages(object sender, PrintPageEventArgs e)
		{
			e.Graphics.DrawImage(pbPhoto.Image, 29, 29, 800, 1200);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (TempData.NeedToRefresh)
			{
				TempData.NeedToRefresh = false;
				GetImagesList();
			}
		}

        private string dateDoc, numDoc, nameArend, position, dateStartDoc, dateEndDoc, idArend;

        public void setData(string dateDoc, string numDoc, string nameArend, string position, string dateStartDoc, string dateEndDoc, string idArend)
        {
            this.dateDoc = dateDoc;
            this.numDoc = numDoc;
            this.nameArend = nameArend;
            this.position = position;
            this.dateStartDoc = dateStartDoc;
            this.dateEndDoc = dateEndDoc;
            this.idArend = idArend;
        }

    }
}
