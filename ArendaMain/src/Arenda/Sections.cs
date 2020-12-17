using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using System.IO;
using Exc = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Text.RegularExpressions;
using Nwuram.Framework.Logging;
using Nwuram.Framework.ToExcelNew;

namespace Arenda
{
    public partial class Sections : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        DataTable tSec = null;
        DataTable tEqVsSec = null;
        DataView view = new DataView();
        int mod = 0;        
        string build, floor, obj;
        System.IO.FileInfo file;

        public Sections()
        {
            InitializeComponent(); 
            dgvDevices.AutoGenerateColumns = false;
            dgEqVsSec.AutoGenerateColumns = false;
            tbSections1.AutoGenerateColumns = false;


        }

        private void Sections_Load(object sender, EventArgs e)
        {
            //if (TempData.Rezhim == "ПР")
            if (new List<string> { "СОА", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim))
            {
                btAddSec.Visible =
                btEditSec.Visible =
                btDelSec.Visible =
                //btAddEq.Visible =
                //btEditEq.Visible =
                //btDelEq.Visible =
                //btnAddDeviceToSection.Visible =
                //btnEditDevice.Visible =
                //btnDeleteDeviceFromSection.Visible =
                false;


                btAddEq.Visible =
                btEditEq.Visible =
                btDelEq.Visible =
                btnAddDeviceToSection.Visible =
                btnEditDevice.Visible =
                btnDeleteDeviceFromSection.Visible = new List<string> { "СОА" }.Contains(TempData.Rezhim);
            }
            else
            {
                btAddEq.Visible =
                    btEditEq.Visible =
                    btDelEq.Visible = new List<string> { "РКВ" }.Contains(TempData.Rezhim);
                //_proc.SuperUserMode();
            }
            

            FillCbZloor();
            FillCbZdan();
            FillCmbObject();

            ini();            
            iniclick();
            FillDevices();
        }

        private void FillDevices()
        {
            dgvDevices.AutoGenerateColumns = false;
            if (tbSections1.CurrentRow != null)
            {
                dgvDevices.DataSource = _proc.GetSectionDevices(Convert.ToInt32(tbSections1.CurrentRow.Cells["sid"].Value));
            }
            FilterDevices();
            dgvDevices.Columns["id_device"].Visible = false;
        }

        private void FillCbZloor()
        {
            DataTable dtFloor = new DataTable();

            dtFloor = _proc.FillCbZdFl(1);

            dtFloor.DefaultView.RowFilter = "isActive";

            if ((dtFloor != null) && (dtFloor.Rows.Count > 0))
            {
                cbZloor.DataSource = dtFloor;
                cbZloor.DisplayMember = "cName";
                cbZloor.ValueMember = "id";
            }

            DataRow all = dtFloor.NewRow();
            all["cName"] = "Все этажи";
            all["id"] = 0;
            all["isActive"] = 1;
            dtFloor.Rows.InsertAt(all, 0);

            cbZloor.SelectedValue = 0;
        }

        private void FillCbZdan()
        {
            DataTable dtZdan = new DataTable();

            dtZdan = _proc.FillCbZdFl(0);

            dtZdan.DefaultView.RowFilter = "isActive = 1";

            if ((dtZdan != null) && (dtZdan.Rows.Count > 0))
            {
                cbZdan.DataSource = dtZdan;
                cbZdan.DisplayMember = "cName";
                cbZdan.ValueMember = "id";
            }

            DataRow all = dtZdan.NewRow();
            all["cName"] = "Все здания";
            all["id"] = 0;
            all["isActive"] = 1;
            dtZdan.Rows.InsertAt(all, 0);

            cbZdan.SelectedValue = 0;
        }
      
      private void FillCmbObject()
      {
        object tmp = 0;
        if (cmbObject.SelectedValue != null)
          tmp = cmbObject.SelectedValue;
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("cName");
        dt.Columns.Add("Comment");
        dt.Columns.Add("isActive");
        //dt.Columns.Add("Used");
        DataTable dtObj = _proc.GetObjects();
        DataRow dr = dt.Rows.Add();
        dr["id"] = 0;
        dr["cName"] = "Все объекты";
        dr["isActive"] = 1;
        foreach (DataRow r in dtObj.Rows)
        {
          DataRow dro = dt.Rows.Add();
          dro["id"] = r["id"];
          dro["cName"] = r["cName"];
          dro["Comment"] = r["Comment"];
          dro["isActive"] = r["isActive"];
          //dro["Used"] = r["Used"];
        }
        dt.DefaultView.RowFilter = "isActive = 1";
        //dtObj.DefaultView.Sort = "cName";
        cmbObject.DataSource = dt;
        cmbObject.SelectedValue = tmp;
      }

        private void btAddEq_Click(object sender, EventArgs e)
        {
            try
            {
                var AddEqSec = new AddEqVsSec(
                    tbSections1.SelectedRows[0].Cells["sSec"].Value.ToString(), 
                    tbSections1.SelectedRows[0].Cells["sid"].Value.ToString());
                AddEqSec.ShowDialog();
                iniclick();
                isactive();
            }
            catch (Exception) { MessageBox.Show("Нет выбранной секции","Ошибка"); }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btAddSec_Click(object sender, EventArgs e)
        {
            var AddSec = new AddSec();
            AddSec.ShowDialog();
            ini();            
         }
        
        private void ini()
        {
            System.Windows.Forms.SortOrder sort;
            if (checSec.Checked == true)
            {
                tSec = _proc.GetSec(0);
            }
            else
                tSec = _proc.GetSec(1);

            sort = tbSections1.SortOrder;

            bds.DataSource = tSec;
            tbSections1.DataSource = bds;

            FilterDataView();
            iniclick();            
        }


        private void cantediteq()
        {
            if (dgEqVsSec.Rows.Count == 0)
            {
                btEditEq.Enabled = false;
                btDelEq.Enabled = false;
            }
            else
            {
                btEditEq.Enabled = true;
                btDelEq.Enabled = true;
            }
        }

        private void checEq_CheckedChanged(object sender, EventArgs e)
        {
            mod = (checEq.Checked) ? 1 : 0;
            iniclick();
            isactive();           
        }

        private void iniclick()
        {
            if (tbSections1.Rows.Count != 0)
                try
                {
                    tEqVsSec = _proc.GetEqVsSec(
                        tbSections1.SelectedRows[0].Cells["sid"].Value.ToString(), 
                        (mod == 1) ? 1 : 0);

                    bds2.DataSource = tEqVsSec;
                    dgEqVsSec.AutoGenerateColumns = false;
                    dgEqVsSec.DataSource = bds2;
                    Equipment.DataPropertyName = "cName";
                    Col.DataPropertyName = "Quantity";
                    isActive.DataPropertyName = "isActive";
                    idEqVsSec.DataPropertyName = "id";
                    FilterDataView1();
                }
                catch {}
            else { deleqvsec(); }
        }

        private void dgEqVsSec_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {            
            if (tEqVsSec.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
            {
                dgEqVsSec.Rows[e.RowIndex].DefaultCellStyle.BackColor = dgEqVsSec.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 251, 149); 
            }
        }

        private void btEditSec_Click(object sender, EventArgs e)
        {            
            try
            {
                int idSec = int.Parse(tbSections1.SelectedRows[0].Cells["sid"].Value.ToString());
                AddSec Add = new AddSec(idSec);
                Add.ShowDialog();
                ini();
            }
            catch (Exception) { MessageBox.Show("Нет записи для редактирования", "Внимание!"); }
        }

        private void checSec_CheckedChanged(object sender, EventArgs e)
        {
            ini();
            deleqvsec();
            isactive();
        }

        private void tbSections_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (tSec.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
            {
                tbSections1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Coral;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FilterDataView();
            deleqvsec();
            iniclick();            

            if (tbSections1.Rows.Count != 0)
            {
                tbSections1.ClearSelection();
                tbSections1.Rows[0].Selected = true;                
            }

            isactive();
            
        }

        private void deleqvsec()
        {
            try
            {
                if (tbSections1.Rows.Count == 0)
                {
                    if (tEqVsSec == null) return;

                    while (tEqVsSec.Rows.Count != 0)
                        tEqVsSec.Rows.Remove(tEqVsSec.Rows[tEqVsSec.Rows.Count - 1]);
                }
            }
            catch { }
        }
      
      private void btDelSec_Click(object sender, EventArgs e)
      {
        try
        {
          int? tl;
          int? lm;
          string ph;
          decimal ta, ath;

          if (tbSections1.SelectedRows[0].Cells["sTelephone_lines"].Value.ToString() == "")
            tl = null;
          else
            tl = Convert.ToInt32(tbSections1.SelectedRows[0].Cells["sTelephone_lines"].Value);

          if (tbSections1.SelectedRows[0].Cells["sLamps"].Value.ToString() == "")
            lm = null;
          else
            lm = Convert.ToInt32(tbSections1.SelectedRows[0].Cells["sLamps"].Value);

          if (tbSections1.SelectedRows[0].Cells["sPhone_number"].Value.ToString() == "")
            ph = null;
          else
            ph = tbSections1.SelectedRows[0].Cells["sPhone_number"].Value.ToString();

          if (tbSections1.SelectedRows[0].Cells["sTotal_Area"].Value.ToString() == "")
            ta = 0;
          else
            ta = decimal.Parse(tbSections1.SelectedRows[0].Cells["sTotal_Area"].Value.ToString());

          if (tbSections1.SelectedRows[0].Cells["sArea_of_Trading_Hall"].Value.ToString() == "")
            ath = 0;
          else
            ath = decimal.Parse(tbSections1.SelectedRows[0].Cells["sArea_of_Trading_Hall"].Value.ToString());

          int idSec = Convert.ToInt32(tbSections1.SelectedRows[0].Cells["sid"].Value.ToString());

          string rez = _proc.isActiveSec(Convert.ToInt32(tbSections1.SelectedRows[0].Cells["sid"].Value.ToString())).Rows[0][0].ToString();
          if (rez == "False")
          {
            {
              //if (MessageBox.Show("Сделать запись снова активной?", "Внимание", MessageBoxButtons.YesNo,
                //MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              if (MessageBox.Show("Сделать выбранную запись действующей?",
                "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                string logEvent = "Смена статуса секции";

                Logging.StartFirstLevel(765);
                Logging.Comment(logEvent);
                Logging.Comment("id = " + idSec.ToString());
                Logging.Comment("Наименование секции: \"" + tbSections1.SelectedRows[0].Cells["sSec"].Value.ToString() + "\"");
                Logging.Comment("Статус изменен на активный");

                Logging.Comment("Список приборов в секции");
                foreach (DataGridViewRow r in dgvDevices.Rows)
                {
                  Logging.Comment("Тип прибора ID: " + r.Cells["id_device"].Value + " ;Наименование: " + r.Cells["device_name"].Value.ToString());
                  Logging.Comment("Количество: " + r.Cells["device_quantity"].Value);
                }

                Logging.Comment("Список оборудования в секции");
                foreach (DataGridViewRow r in dgEqVsSec.Rows)
                {
                  Logging.Comment("Тип оборудования ID: " + r.Cells["idEqVsSec"].Value + " ;Наименование: " + r.Cells["Equipment"].Value.ToString());
                  Logging.Comment("Количество: " + r.Cells["Col"].Value);
                }
                
                Logging.Comment("Завершение операции \"" + logEvent + "\"");
                Logging.StopFirstLevel();

                _proc.AddEditSec(
                  tbSections1.SelectedRows[0].Cells["sSec"].Value.ToString(),
                  tbSections1.SelectedRows[0].Cells["sBuild"].Value.ToString(),
                  tbSections1.SelectedRows[0].Cells["sFloo"].Value.ToString(),
                  0,
                  idSec,
                  1,
                  lm,
                  tl,
                  ph,
                  ta,
                  ath,
                  int.Parse(tbSections1.SelectedRows[0].Cells["cIdObj"].Value.ToString())
                  );
              }
            }
          }
          else
          {
            //if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo,
            //                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            string cheas = _proc.BefSec(idSec).Rows[0][0].ToString();
            if (cheas == "0")
            {
              if (MessageBox.Show("Удалить выбранную запись?", "Удаление записи",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                DataTable dtGetSecInfo = new DataTable();
                dtGetSecInfo = _proc.GetSecInfo(idSec);

                if ((dtGetSecInfo != null) && (dtGetSecInfo.Rows.Count > 0))
                {
                  string logEvent = "Удаление секции из справочника";

                  Logging.StartFirstLevel(759);
                  Logging.Comment(logEvent);
                  Logging.Comment("id = " + idSec.ToString()
                    + ", Наименование секции: \"" + dtGetSecInfo.Rows[0]["Sec"].ToString() + "\"");
                  Logging.Comment("id здания = " + dtGetSecInfo.Rows[0]["id_Build"].ToString()
                    + ", Наименование здания: \"" + dtGetSecInfo.Rows[0]["Build"].ToString() + "\"");
                  Logging.Comment("id этажа = " + dtGetSecInfo.Rows[0]["id_Floo"].ToString()
                    + ", Наименование этажа: \"" + dtGetSecInfo.Rows[0]["Floo"].ToString() + "\"");
                  Logging.Comment("Количество телефонных линий: " + dtGetSecInfo.Rows[0]["Telephone_lines"].ToString());
                  Logging.Comment("Количество светильников: " + dtGetSecInfo.Rows[0]["Lamps"].ToString());
                  Logging.Comment("Номер телефона: " + dtGetSecInfo.Rows[0]["Phone_number"].ToString());
                  Logging.Comment("Общая площадь: " + dtGetSecInfo.Rows[0]["Total_Area"].ToString());
                  Logging.Comment("Площадь торгового зала: " + dtGetSecInfo.Rows[0]["Area_of_Trading_Hall"].ToString());
                  Logging.Comment("Признак АППЗ: " + ((bool.Parse(dtGetSecInfo.Rows[0]["isAPPZ"].ToString())) ? "ДА" : "НЕТ"));

                  Logging.Comment("Список приборов в секции");
                  foreach (DataGridViewRow r in dgvDevices.Rows)
                  {
                    Logging.Comment("Тип прибора ID: " + r.Cells["id_device"].Value + " ;Наименование: " + r.Cells["device_name"].Value.ToString());
                    Logging.Comment("Количество: " + r.Cells["device_quantity"].Value);
                  }

                  Logging.Comment("Список оборудования в секции");
                  foreach (DataGridViewRow r in dgEqVsSec.Rows)
                  {
                    Logging.Comment("Тип оборудования ID: " + r.Cells["idEqVsSec"].Value + " ;Наименование: " + r.Cells["Equipment"].Value.ToString());
                    Logging.Comment("Количество: " + r.Cells["Col"].Value);
                  }

                  Logging.Comment("Завершение операции \"" + logEvent + "\"");
                  Logging.StopFirstLevel();
                }

                _proc.delSec(idSec);
              }
            }
            else
            {
              //if (MessageBox.Show("Удаляемая запись используется и ее невозможно удалить. Сделать ее неактивной?", "Ошибка", MessageBoxButtons.YesNo,
              //MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              if (MessageBox.Show("Выбранная для удаления запись\n    используется в программе.\nСделать запись недействующей?",
                "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                string logEvent = "Смена статуса секции";

                Logging.StartFirstLevel(765);
                Logging.Comment(logEvent);
                Logging.Comment("id = " + idSec.ToString());
                Logging.Comment("Наименование секции: \"" + tbSections1.SelectedRows[0].Cells["sSec"].Value.ToString() + "\"");
                Logging.Comment("Статус изменен на неактивный");

                Logging.Comment("Список приборов в секции");
                foreach (DataGridViewRow r in dgvDevices.Rows)
                {
                  Logging.Comment("Тип прибора ID: " + r.Cells["id_device"].Value + " ;Наименование: " + r.Cells["device_name"].Value.ToString());
                  Logging.Comment("Количество: " + r.Cells["device_quantity"].Value);
                }

                Logging.Comment("Список оборудования в секции");
                foreach (DataGridViewRow r in dgEqVsSec.Rows)
                {
                  Logging.Comment("Тип оборудования ID: " + r.Cells["idEqVsSec"].Value + " ;Наименование: " + r.Cells["Equipment"].Value.ToString());
                  Logging.Comment("Количество: " + r.Cells["Col"].Value);
                }

                Logging.Comment("Завершение операции \"" + logEvent + "\"");
                Logging.StopFirstLevel();

                _proc.AddEditSec(
                  tbSections1.SelectedRows[0].Cells["sSec"].Value.ToString(),
                  tbSections1.SelectedRows[0].Cells["sBuild"].Value.ToString(),
                  tbSections1.SelectedRows[0].Cells["sFloo"].Value.ToString(),
                  0,
                  idSec,
                  0,
                  lm,
                  tl,
                  ph,
                  ta,
                  ath,
                  int.Parse(tbSections1.SelectedRows[0].Cells["cIdObj"].Value.ToString())
                  );
              }
            }
          }
          
          ini();
          iniclick();
          deleqvsec();
        }
        catch (Exception r) { MessageBox.Show("Нет записей для удаления." + "\n" + r.ToString(), "Ошибка"); }
      }

        private void btEditEq_Click(object sender, EventArgs e)
        {
            try
            {
                int idEqVsSec = int.Parse(dgEqVsSec.SelectedRows[0].Cells["idEqVsSec"].Value.ToString());

                var addedd = new AddEqVsSec(idEqVsSec);
                addedd.ShowDialog();
                iniclick();
                isactive();
            }

            catch (Exception r) { MessageBox.Show("Нельзя редактировать данную запись." + "\n" + r.ToString(), "Ошибка"); }
        }
        
      private void FilterDataView()
      {
            if (tSec == null) return;
        try
        {
          string Fstring, Fstring1, Fstring2;
          Fstring = Fstring1 = Fstring2 = "";
          if (cbZloor.Text == "Все этажи")
          {
            Fstring = "";
          }
          else
            Fstring = "Floo = '" + cbZloor.Text + "'";

          if (cbZdan.Text == "Все здания")
          {
            Fstring1 = "";
          }
          else
          {
            if (Fstring.Length > 0)
              Fstring1 = " AND ";
            Fstring1 += "Build = '" + cbZdan.Text + "'";
          }

          if (textBox1.Text == "")
          {
            Fstring2 = "";
          }
          else
          {
            if (Fstring.Length > 0 || Fstring1.Length > 0)
              Fstring2 = " AND ";
            Fstring2 += "Sec like '%" + textBox1.Text + "%'";
          }

          DataTable dt = tSec;
          view = dt.DefaultView;

          StringBuilder sb = new StringBuilder();

          /*sb.Append("Floo like '%" + Fstring + "%'");
           * sb.Append("and Build like '%" + Fstring1 + "%'");
           * sb.Append("and Sec like '%" + Fstring2 + "%'");*/
          sb.Append(Fstring);
          sb.Append(Fstring1);
          sb.Append(Fstring2);

          if (cmbObject.SelectedValue != null
            && int.Parse(cmbObject.SelectedValue.ToString()) != 0)
          {
            if (sb.Length != 0)
              sb.Append(" and ");
            sb.Append("id_ObjectLease = " + cmbObject.SelectedValue.ToString());
          }

          view.RowFilter = sb.ToString();
        }
        catch (Exception) { }

        if (tbSections1.Rows.Count == 0)
        {
          btEditSec.Enabled = false;
          btDelSec.Enabled = false;
        }
        else
        {
          btEditSec.Enabled = true;
          btDelSec.Enabled = true;
        }
      }

        private void btDelEq_Click(object sender, EventArgs e)
        {
            if (dgEqVsSec.Rows.Count != 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    int idEqVsSec = Convert.ToInt32(dgEqVsSec.SelectedRows[0].Cells["idEqVsSec"].Value.ToString());

                    DataTable dtGetEq_vs_Sec = new DataTable();
                    dtGetEq_vs_Sec = _proc.GetEquipment_vs_Sections(idEqVsSec);

                    if ((dtGetEq_vs_Sec != null) && (dtGetEq_vs_Sec.Rows.Count > 0))
                    {
                        string logEvent = "Удалить оборудование из секции";

                        Logging.StartFirstLevel(762);
                        Logging.Comment(logEvent);
                        Logging.Comment("id записи = " + idEqVsSec.ToString());
                        Logging.Comment("id секции = " + dtGetEq_vs_Sec.Rows[0]["id_Sections"].ToString()
                            + ", Наименование секции: \"" + dtGetEq_vs_Sec.Rows[0]["name_Sections"].ToString() + "\"");
                        Logging.Comment("id оборудования = " + dtGetEq_vs_Sec.Rows[0]["id_Equipment"].ToString()
                            + ", Наименование оборудования: \"" + dtGetEq_vs_Sec.Rows[0]["name_Equipment"].ToString() + "\"");
                        Logging.Comment("Количество удаляемого оборудования = " + dtGetEq_vs_Sec.Rows[0]["Quantity"].ToString());
                        Logging.Comment("Завершение операции \"" + logEvent + "\"");
                        Logging.StopFirstLevel();
                    }

                    _proc.DelEVS(idEqVsSec);
                    iniclick();
                    isactive();
                }
            }
            else { MessageBox.Show("Нет записей для удаления!","Внимание");
            isactive();
            } 
        }
       
        private void cbZloor_TextChanged(object sender, EventArgs e)
        {
                FilterDataView();
                iniclick();
                deleqvsec();
        }

        private void cbZdan_TextChanged(object sender, EventArgs e)
        {
                FilterDataView();
                iniclick();
                deleqvsec();            
        }

        private void tbFEq_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterDataView1();
                isactive();
            }
            catch { }        
        }

        private void FilterDataView1()
        {
            DataTable dt = tEqVsSec;
            DataView view = new DataView();

            view = dt.DefaultView;

            StringBuilder sb = new StringBuilder();

            sb.Append("cName like '%" + tbFEq.Text + "%'");

            view.RowFilter = sb.ToString();

            if (dgEqVsSec.Rows.Count == 0)
            {
                btDelEq.Enabled = false;
                btEditEq.Enabled = false;
            }
            else
            {
                btDelEq.Enabled = true;
                btEditEq.Enabled = true;
            }        
        }

        private void dgEqVsSec_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            isactive();
        }

        private void isactive()
        {
            try
            {
                if (dgEqVsSec.SelectedRows[0].Cells[2].Value.ToString() == "False")
                {
                    btEditEq.Enabled = false;
                }
                else
                {
                    btEditEq.Enabled = true;
                }
            }
            catch {} 
        }

        private void tbSections_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            iniclick();
            isactiveSec();
        }

        private void dgEqVsSec_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            isactive();
        }

        private void dgEqVsSec_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           isactive();
        }

        private void isactiveSec()
        {
            try
            {
                if (tbSections1.SelectedRows[0].Cells["sisActive"].Value.ToString() == "False")
                {
                    btEditSec.Enabled = false;
                }
                else
                {
                    btEditSec.Enabled = true;
                }
            }
            catch {}
        }

        private void tbSections_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                iniclick();
                isactiveSec();
                FillDevices();
            }
            catch {}
        }

        private void dgEqVsSec_SelectionChanged(object sender, EventArgs e)
        {
            isactive();
        }
        
        string _fileName;
        frmLoad frmWait = new frmLoad();

        private void btExel_Click(object sender, EventArgs e)
        {
          
            this.Enabled = false;
            frmWait = new frmLoad();
            frmWait.TextWait = "ЖДИТЕ. ИДЁТ ВЫГРУЗКА";
            frmWait.Show();

            backgroundWorker1.RunWorkerAsync(new object[] {"\\Templates\\Sections.xls" });
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
          frmWait.Dispose();
          this.Enabled = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ExcelUnLoad rep = new ExcelUnLoad("Отчет по секциям");
         
            rep.AddSingleValue("Справочник секций",1,1);
            rep.Merge(1, 1, 1, 9);
            rep.SetBordersToBottom(1, 1, 9, true);
            rep.SetBordersToBottom(1, 2, 9, true);
            rep.SetBordersToBottom(1, 3, 9, true);
            rep.SetBordersToBottom(1, 4, 9, true);
            rep.SetBordersToBottom(1, 5, 9, true);
            rep.SetBordersToBottom(1, 6, 9, true);
            rep.SetBordersToBottom(1, 7, 9, true);
            rep.SetBordersToBottom(1, 8, 9, true);
            rep.SetBordersToBottom(1, 9, 9, true);

            // rep.SetBorders(1, 1, 1, 9);
            rep.SetFontBold(1, 1, 1, 1);
            rep.SetFontSize(1, 1, 1, 1, 16);
            rep.SetCellAlignmentToCenter(1, 1, 1, 1);
            int crow = 3;
            rep.AddSingleValue($"Объект: {obj}", crow, 1);
            crow++;
            rep.AddSingleValue($"Здание: {build}", crow, 1);
            crow++;
            rep.AddSingleValue($"Этаж: {floor}", crow, 1);
            crow += 2;
            rep.AddSingleValue($"Выгрузил: {Nwuram.Framework.Settings.User.UserSettings.User.FullUsername}", crow, 1);
            crow++;
            rep.AddSingleValue($"Дата выгрузки: {DateTime.Now}", crow, 1);
            crow += 2;
            int startRow = crow;
            #region Шапка
            rep.AddSingleValue("Секция", crow, 1);
            rep.AddSingleValue("Объект", crow, 2);
            rep.AddSingleValue("Здание", crow, 3);
            rep.AddSingleValue("Этаж", crow, 4);
            rep.AddSingleValue("Кол-во телефонных линий", crow, 5);
            rep.AddSingleValue("Кол-во светильников", crow, 6);
            rep.AddSingleValue("Номер телефона", crow, 7);
            rep.AddSingleValue("Оборудование и кол-во", crow, 8);
            rep.AddSingleValue("Приборы/части системы", crow, 9);

            #endregion

            #region ширина столбцов
            rep.SetColumnWidth(1, 1, 1, 1, 9);
            rep.SetColumnWidth(1, 2, 1, 2, 9);
            rep.SetColumnWidth(1, 3, 1, 3, 17);
            rep.SetColumnWidth(1, 4, 1, 4, 9);
            rep.SetColumnWidth(1, 5, 1, 5, 13);
            rep.SetColumnWidth(1, 6, 1, 6, 14);
            rep.SetColumnWidth(1, 7, 1, 7, 10);
            rep.SetColumnWidth(1, 8, 1, 8, 20);
            rep.SetColumnWidth(1, 9, 1, 9, 20);
            #endregion

            rep.SetFontBold(crow, 1, crow, 9);
            rep.SetCellAlignmentToCenter(crow, 1, crow, 9);
            rep.SetCellAlignmentToJustify(crow, 1, crow, 9);
            rep.SetWrapText(crow, 1, crow, 9);
            crow++;

          
                                             
            if (tSec.Rows.Count > 0)
            {
                for (int i = 0; i < view.Count; i++)
                {
                    string A, I,sEq="";
                    string sDevices = "";
                    A = "A" + (i + 11);
                    I = "I" + (i + 11);

                    int isActive = 0;

                    if (checEq.Checked == true)
                    { isActive = 1; }
                    else isActive = 0;

                    foreach (DataRow dr in _proc.GetEqVsSec(tSec.DefaultView[i]["id"].ToString(), isActive).Rows)
                    {
                      if (sEq.Length > 0)
                        sEq += "\n";
                      sEq += dr["cName"] + " " + dr["Quantity"] + " шт.";
                    }

                    DataTable dtDevices = _proc.GetSectionDevices(Convert.ToInt32(tSec.DefaultView[i]["id"]));
                    foreach (DataRow dr in dtDevices.Rows)
                    {
                      if (sDevices.Length > 0)
                        sDevices += "\n";
                      sDevices += dr["cname"].ToString() + " " + dr["quantity"].ToString() + " " + dr["unit"].ToString();
                    }

                    rep.AddSingleValue(tSec.DefaultView[i]["Sec"].ToString(), crow, 1);
                    rep.AddSingleValue(tSec.DefaultView[i]["Obj"].ToString(), crow, 2);
                    rep.AddSingleValue(tSec.DefaultView[i]["Build"].ToString(), crow, 3);
                    rep.AddSingleValue(tSec.DefaultView[i]["Floo"].ToString(), crow, 4);
                    rep.AddSingleValue(tSec.DefaultView[i]["Telephone_lines"].ToString(), crow, 5);
                    rep.AddSingleValue(tSec.DefaultView[i]["Lamps"].ToString(), crow, 6);
                    rep.AddSingleValue(tSec.DefaultView[i]["Phone_number"].ToString(), crow, 7);
                    rep.AddSingleValue(sEq, crow,8);
                    rep.AddSingleValue(sDevices, crow, 9);
                    rep.SetWrapText(crow, 1, crow, 9);
                    rep.SetCellAlignmentToTop(crow, 1, crow, 9);
                    crow++;
                }
            }

            rep.SetBorders(startRow, 1, crow - 1, 9);
            rep.SetPageOrientationToLandscape();                   
            rep.Show();
            string logEvent = "Выгрузка справочника секций в Excel";

            string BuildName = "";
            cbZdan.Invoke((MethodInvoker)delegate
            {
                BuildName = cbZdan.Text;
            });

            string FloorName = "";

            cbZloor.Invoke((MethodInvoker)delegate
            {
                FloorName = cbZloor.Text;
            });
            
            Logging.StartFirstLevel(763);
            Logging.Comment(logEvent);
            Logging.Comment("Имя выгруженного excel файла \"" + file.Name + "\"");
            Logging.Comment("Выгрузка произведена для здания: \"" + BuildName + "\" и этажа: \"" + FloorName + "\". Выгружено " + view.Count.ToString() + " строк.");
            Logging.Comment("Завершение операции \"" + logEvent + "\"");
            Logging.StopFirstLevel();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[0-9]|[\s]|[\w]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private void tbFEq_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[0-9]|[\s]|[\w]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private void cbZdan_SelectedIndexChanged(object sender, EventArgs e)
        {
            build = cbZdan.Text;
        }

        private void cbZloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            floor = cbZloor.Text;
        }

        private void dgSec_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                iniclick();
                isactiveSec();
                FillDevices();
            }
            catch {}
        }

        private void dgSec_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (tSec.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
            {
                tbSections1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Coral;
            }
        }

        private void dgSec_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            iniclick();
            isactiveSec();
        }

        private void btnAddDeviceToSection_Click(object sender, EventArgs e)
        {
            if (tbSections1.CurrentRow != null)
            {
                frmAddEditDeviceToEquipment frmAdd = new frmAddEditDeviceToEquipment(new Section { Id = Convert.ToInt32(tbSections1.CurrentRow.Cells["sid"].Value), Name = tbSections1.CurrentRow.Cells["sSec"].Value.ToString() },
                                                                                     new Device { Id = 0, Quantity = 0, Name = "" });
                if (frmAdd.ShowDialog() == DialogResult.OK)
                {
                    FillDevices();
                }
            }
        }

        private void btnEditDevice_Click(object sender, EventArgs e)
        {
            if (tbSections1.CurrentRow != null && dgvDevices.CurrentRow != null)
            {
                frmAddEditDeviceToEquipment frmEdit = new frmAddEditDeviceToEquipment(new Section { Id = Convert.ToInt32(tbSections1.CurrentRow.Cells["sid"].Value), Name = tbSections1.CurrentRow.Cells["sSec"].Value.ToString() },
                                                                                     new Device { Id = Convert.ToInt32(dgvDevices.CurrentRow.Cells["id_device"].Value), Quantity = Convert.ToInt32(dgvDevices.CurrentRow.Cells["device_quantity"].Value), Name = dgvDevices.CurrentRow.Cells["device_name"].Value.ToString() });
                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    FillDevices();
                }
            }
        }

        private void btnDeleteDeviceFromSection_Click(object sender, EventArgs e)
        {
            if (tbSections1.CurrentRow != null && dgvDevices.CurrentRow !=  null && MessageBox.Show("Вы уверены, что хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                Logging.StartFirstLevel(1397);
                Logging.Comment("Секция ID: " + tbSections1.CurrentRow.Cells["sid"].Value.ToString() + " ;Наименование: " + tbSections1.CurrentRow.Cells["sSec"].Value.ToString());
                Logging.Comment("Тип прибора ID: " + dgvDevices.CurrentRow.Cells["id_device"].Value + " ;Наименование: " + dgvDevices.CurrentRow.Cells["device_name"].Value.ToString());
                Logging.Comment("Количество: " + dgvDevices.CurrentRow.Cells["device_quantity"].Value);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

                _proc.RemoveDeviceFromSection(Convert.ToInt32(tbSections1.CurrentRow.Cells["sid"].Value), Convert.ToInt32(dgvDevices.CurrentRow.Cells["id_device"].Value));
                FillDevices();
            }
        }

        private void SetDevicesButtonsEnabled()
        {
            btnEditDevice.Enabled = btnDeleteDeviceFromSection.Enabled = dgvDevices.RowCount > 0;
        }

        private void dgvDevices_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (!Convert.ToBoolean(dgvDevices.Rows[e.RowIndex].Cells["device_active"].Value))
                {
                    dgvDevices.Rows[e.RowIndex].DefaultCellStyle.BackColor = dgvDevices.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = pbInactiveDevices.BackColor;
                }
            }
        }

        private void FilterDevices()
        {
            if (dgvDevices.DataSource != null)
            {
                string filter = "cname like '%" + txtDeviceName.Text + "%'";
                if (!cbAllDevices.Checked)
                {
                    filter += " and is_active = true";
                }
                (dgvDevices.DataSource as DataTable).DefaultView.RowFilter = filter;
                SetDevicesButtonsEnabled();
            }
        }

        private void txtDeviceName_TextChanged(object sender, EventArgs e)
        {
            FilterDevices();
        }

        private void cbAllDevices_CheckedChanged(object sender, EventArgs e)
        {
            FilterDevices();
        }

        private void btReportArendSection_Click(object sender, EventArgs e)
        {
            new ArendaViewSection.frmView().ShowDialog();
        }

        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void cmbObject_SelectedIndexChanged(object sender, EventArgs e)
        {
          obj = cmbObject.Text;
          FilterDataView();
          iniclick();
          deleqvsec();
        }
    }
}
