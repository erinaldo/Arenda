using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using System.Text.RegularExpressions;

namespace Arenda
{
    public partial class Looktenant : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        DataTable Lookten;

        int tenant, idcp;
        public Looktenant(int p, int id_cp)
        {
            InitializeComponent();

            tenant = p;
            idcp = id_cp;

            if (tenant == 1) 
            {
              this.Text = "Список арендаторов";
              aren.HeaderText = "Арендатор";
              Address_trade_premises.Visible = false;
            }
            else 
            {
              this.Text = "Список арендодателей";
              aren.HeaderText = "Арендодатель";
              Address_trade_premises.Visible = true; 
            } 
            
            ini();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            dataTen.ClearDataTen();
            DialogResult = DialogResult.Cancel;
        }

        private void ini()
        {            
            Lookten = _proc.FillCbTen(tenant);
            if (Lookten.Rows.Count != 0)
            {
                foreach (DataRow dr in Lookten.Rows)
                {
                    dr["Contact_Lastname"] = dr["Contact_Lastname"].ToString()  
                        + ((dr["Contact_Firstname"].ToString().Length !=0) ? " " + dr["Contact_Firstname"].ToString().Substring(0, 1) + "." : " -.")
                        + ((dr["Contact_Middlename"].ToString().Length != 0) ? " " + dr["Contact_Middlename"].ToString().Substring(0, 1) + "." : " -.");
                }
            }

            bds.DataSource = Lookten;
            
            dglookten.DataSource = bds;
            id.DataPropertyName = "id";
            aren.DataPropertyName = "aren";
            fam.DataPropertyName = "Contact_Lastname";
            name.DataPropertyName = "Contact_Firstname";
            midname.DataPropertyName = "Contact_Middlename";
            inn.DataPropertyName = "INN";

            if (Lookten == null || Lookten.Rows.Count == 0)
              tbFIO.Enabled = tbINN.Enabled = tbTenant.Enabled = false;
        }

        private void btChoose_Click(object sender, EventArgs e)
        {
            Choose();
        }

        private void Choose()
        {
          if (dglookten != null && dglookten.SelectedRows != null && dglookten.SelectedRows.Count > 0)
          {
            if(idcp != 0)
            {
              DataTable dtp = _proc.CheckParentChildTenant(Convert.ToInt32(dglookten.SelectedRows[0].Cells["id"].Value),
                1);
              if(dtp != null && dtp.Rows.Count > 0)
              {
                MessageBox.Show("Арендатор " + dtp.Rows[0]["CurrentTenant"].ToString() + " уже имеет связь в\nкачестве ребенка с арендатором " + dtp.Rows[0]["ConTenant"].ToString() + ".\n                         Добавление арендатора для связи\n                                   невозможно.",
                  "Добавление связи арендаторов", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                return;
              }
              DataTable dtc = _proc.CheckParentChildTenant(Convert.ToInt32(dglookten.SelectedRows[0].Cells["id"].Value),
                0);
              if (dtc != null && dtc.Rows.Count > 0)
              {
                MessageBox.Show("Арендатор " + dtc.Rows[0]["CurrentTenant"].ToString() + " уже имеет связь в\nкачестве родителя с арендатором " + dtc.Rows[0]["ConTenant"].ToString() + ".\n                         Добавление арендатора для связи\n                                   невозможно.",
                  "Добавление связи арендаторов", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                return;
              }
            }
            long inn;
            int id_Obj;
            dataTen.id = Convert.ToInt32(dglookten.SelectedRows[0].Cells["id"].Value);
            dataTen.aren = dglookten.SelectedRows[0].Cells["aren"].Value.ToString();
            dataTen.fam = dglookten.SelectedRows[0].Cells["fam"].Value.ToString();
            dataTen.name = dglookten.SelectedRows[0].Cells["name"].Value.ToString();
            dataTen.midname = dglookten.SelectedRows[0].Cells["midname"].Value.ToString();
            if (long.TryParse(dglookten.SelectedRows[0].Cells["INN"].Value.ToString(), out inn))
              dataTen.inn = inn;
            if (int.TryParse(dglookten.SelectedRows[0].Cells["id_ObjectLease"].Value.ToString(), out id_Obj))
              dataTen.id_Obj = id_Obj;
            DialogResult = DialogResult.Cancel;
          }
        }

        private void dglookten_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Choose();
            }
        }

        private void dglookten_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            { Choose(); }
        }

      private void Filter()
      {
        if(Lookten != null && Lookten.Rows.Count > 0)
        {
          string filter = "";
          if (tbINN.Text.Length > 0)
            filter += "INN like '%" + tbINN.Text + "%'";
          if (tbTenant.Text.Length > 0)
            filter += (filter.Length > 0 ? " and " : "") + "aren like '%"
              + tbTenant.Text + "%'";
          if (tbFIO.Text.Length > 0)
            filter += (filter.Length > 0 ? " and " : "") + "Contact_Lastname like '%"
              + tbFIO.Text + "%'";
          if (idcp != 0)
            filter += (filter.Length > 0 ? " and " : "")
              + "id_TenantParent is null and id_TenantChild is null and id <> "
              + idcp.ToString();
          Lookten.DefaultView.RowFilter = filter;
        }
      }

      private void tbINN_TextChanged(object sender, EventArgs e)
      {
        Filter();
      }

      private void tbTenant_TextChanged(object sender, EventArgs e)
      {
        Filter();
      }

      private void tbFIO_TextChanged(object sender, EventArgs e)
      {
        Filter();
      }

      private void Looktenant_Load(object sender, EventArgs e)
      {
        Filter();
      }

      private void dglookten_Paint(object sender, PaintEventArgs e)
      {
        int X = dglookten.Location.X;
        tbINN.Location = new Point(X, tbINN.Location.Y);
        tbINN.Width = dglookten.Columns["inn"].Width;
        X += tbINN.Width;
        tbTenant.Location = new Point(X, tbTenant.Location.Y);
        tbTenant.Width = dglookten.Columns["aren"].Width;
        X += tbTenant.Width;
        tbFIO.Location = new Point(X, tbFIO.Location.Y);
        tbFIO.Width = dglookten.Columns["fam"].Width;
      }

      private void tb_KeyPress(object sender, KeyPressEventArgs e)
      {
        Regex pat = new Regex(@"[\b]|[0-9]");
        bool b = pat.IsMatch(e.KeyChar.ToString());
        if (b == false)
        {
          e.Handled = true;
        }
      }

      private void tb_KeyPress2(object sender, KeyPressEventArgs e)
      {
        Regex pat = new Regex(@"[\b]|[0-9]|[A-Za-z]|[А-Яа-я]");
        bool b = pat.IsMatch(e.KeyChar.ToString());
        if (b == false)
        {
          e.Handled = true;
        }
      }
    }
}
