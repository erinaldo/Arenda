using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;

namespace Arenda
{   
    public partial class Post : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        DataTable tPost = null;
        public Post()
        {
            InitializeComponent();

            if (Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("пр"))
            {
                Logging.StartFirstLevel(1394);
                Logging.Comment("Открыта форма просмотра справочника должностей");

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
        }

        private void Post_Load(object sender, EventArgs e)
        {            
            ini();            
        }

        private void ini()
        {
            //if (TempData.Rezhim == "ПР")
            if (new List<string> { "СОА", "МНД", "ПР", "КНТ" }.Contains(TempData.Rezhim))
            {
                button1.Visible =
                    button2.Visible =
                    button3.Visible =
                    false;
            }

            tPost = _proc.GetArendaPosts();
            if (tPost != null)
            {
                if (!checAll.Checked)
                {
                    tPost.DefaultView.RowFilter = "isActive = True";
                }

                tPost.DefaultView.Sort = "isActive desc";
                bds1.DataSource = tPost;
                dgPosts.DataSource = bds1;
                id.DataPropertyName = "id";
                cName.DataPropertyName = "cName";
                isActive.DataPropertyName = "isActive";
                Used.DataPropertyName = "Used";
            }

            if (dgPosts.Rows.Count == 0)
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var aepost = new AddEditPost("add");
            aepost.ShowDialog();
            ini();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var aepost = new AddEditPost(int.Parse(dgPosts.SelectedRows[0].Cells["id"].Value.ToString()), 
                                         dgPosts.SelectedRows[0].Cells["cName"].Value.ToString(),
                                         bool.Parse(dgPosts.SelectedRows[0].Cells["isActive"].Value.ToString()) ? 1 : 0,
                                         dgPosts.SelectedRows[0].Cells["Dative_case"].Value.ToString());
            aepost.ShowDialog();
            ini();  
        }

      private void button3_Click(object sender, EventArgs e)
      {
        string _cName = dgPosts.SelectedRows[0].Cells["cName"].Value.ToString();
        string _Abbr = dgPosts.SelectedRows[0].Cells["Dative_case"].Value.ToString();
        int zid = Convert.ToInt32(dgPosts.SelectedRows[0].Cells["id"].Value);

        bool used = Convert.ToBoolean(dgPosts.CurrentRow.Cells["Used"].Value);
        if (!used)
        {
          //if (_proc.delPos(int.Parse(dgPosts.SelectedRows[0].Cells["id"].Value.ToString())).Rows.Count == 0)
          if (MessageBox.Show("Удалить выбранную запись?", "Удаление записи",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
          {
            Logging.StartFirstLevel(1384);
            Logging.Comment("ID: " + zid);
            Logging.Comment("Наименование должности: " + _cName);
            Logging.Comment("Наименование должности в дательном падеже: " + _Abbr);

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
              + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            _proc.delPos(int.Parse(dgPosts.SelectedRows[0].Cells["id"].Value.ToString()));

            MessageBox.Show("Запись удалена", "Внимание");
            ini();
          }
        }
        else
        {
          if (dgPosts.SelectedRows[0].Cells["isActive"].Value.ToString() == "True")
          {
            //if (MessageBox.Show("Данная запись уже используется. Сделать ее неактивной?", "Внимание", MessageBoxButtons.YesNo,
            //                      MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            if (MessageBox.Show("Выбранная для удаления запись\n    используется в программе.\nСделать запись недействующей?",
              "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
              _proc.ActiveSprav("pos", int.Parse(dgPosts.SelectedRows[0].Cells["id"].Value.ToString()), 0);

              Logging.StartFirstLevel(540);
              Logging.Comment("Произведена смена статуса на неактивный у типа должности");
              Logging.Comment("ID: " + zid);
              Logging.Comment("Наименование должности: " + _cName);
              Logging.Comment("Наименование должности в дательном падеже: " + _Abbr);

              Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
              Logging.StopFirstLevel();

              ini();
            }
          }
          else
          {
            //if (MessageBox.Show("Сделать запись активной?", "Внимание", MessageBoxButtons.YesNo,
            //                         MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            if (MessageBox.Show("Сделать выбранную запись действующей?", "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
              _proc.ActiveSprav("pos", int.Parse(dgPosts.SelectedRows[0].Cells["id"].Value.ToString()), 1);

              Logging.StartFirstLevel(540);
              Logging.Comment("Произведена смена статуса на активный у должности");
              Logging.Comment("ID: " + zid);
              Logging.Comment("Наименование должности: " + _cName);
              Logging.Comment("Наименование должности в дательном падеже: " + _Abbr);

              Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
              Logging.StopFirstLevel();

              ini();
            }
          }
        }   
      }

        private void dgPosts_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (tPost.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
            {
                dgPosts.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Coral;
            }
        }

        private void checAll_CheckedChanged(object sender, EventArgs e)
        {
            ini();
        }



    }
}
