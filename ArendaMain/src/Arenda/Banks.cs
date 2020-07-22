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
using System.Reflection;
using Exc = Microsoft.Office.Interop.Excel;
using System.IO;
using Nwuram.Framework.Logging;

namespace Arenda
{
    public partial class Banks : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        DataTable Bank,bank_old;
        DataTable _Bank;
        int count = 0;
        int mode = 3;
        string _cName, _cA, _bIk;
        DataView view = new DataView();
        int selected;
        bool flag = false;
        bool saving = false;
        int _choose;
        int edit;
        int editp;
        bool add;

        public Banks(int choose)
        {
            InitializeComponent();

            if (Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("пр"))
            {
                Logging.StartFirstLevel(1394);
                Logging.Comment("Открыта форма просмотра справочника банков");

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }

            _choose = choose;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            if (TempData.Rezhim == "ПР")
            {
                this.Close();
                return;
            }

            if (saving == false)
                DialogResult = DialogResult.Cancel;
            else
            {
                if (MessageBox.Show("Есть несохраненные данные. \n Вернуться к редактированию", "Внимание", MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    save();
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void Banks_Load(object sender, EventArgs e)
        {
            if (TempData.Rezhim == "ПР")
            {
                btAdd.Visible =
                    btEdit.Visible =
                    btChoose.Visible =
                    false;

                dgBanks.ReadOnly = true;
            }

            if (TempData.Rezhim == "МН")
            {
              btAdd.Visible = btEdit.Visible = false;
            }


            cbBanks.Checked = true;
            ini();
            enable();
            if (_choose == 1)
            {
                btChoose.Enabled = true;
            }
            else btChoose.Enabled = false;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            // dgBanks.ColumnHeadersVisible = false;
            cbBanks.Enabled = false;////////////////         

            foreach (DataGridViewColumn currDgvColumn in dgBanks.Columns)
            {
                currDgvColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (saving == false)
            {

                if (sBanks.Text.Length == 0)
                {

                    mode = 1;
                    if (cbBanks.Checked == true)
                    {
                        _Bank.Rows.Add(1, "Введите наименование", 0, 0);
                    }
                    else
                    {
                        Bank.Rows.Add(1, "Введите наименование", 0, 0);
                    }
                    dgBanks.Update();
                    int count, mark = 0;


                    count = dgBanks.Rows.Count;

                    for (int i = 0; i < count; i++)

                        if (dgBanks.Rows[i].Cells[1].Value.ToString() == "Введите наименование")
                            mark = i;

                    dgBanks.Rows[mark].Selected = true;
                    dgBanks.FirstDisplayedScrollingRowIndex = mark;
                    selected = mark;
                    flag = true;
                    saving = true;
                    add = true;
                }
                else
                {
                    MessageBox.Show("Очистите поле \"Фильтр\"", "Внимание");
                    foreach (DataGridViewColumn currDgvColumn in dgBanks.Columns)
                    {
                        currDgvColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                    }
                }
            }

        }

        private void dgBanks_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (saving)
            {
                dgBanks.ReadOnly = false;
                btEdit.Enabled = false;
            }
            else
            {
                dgBanks.ReadOnly = true;
                btEdit.Enabled = true;
            }
        }

        private void dgBanks_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {

            }
            catch (Exception) { }
        }

        private int chkadd()
        {
            count++;

            return count;
        }

        private void ini()
        {
            if (cbBanks.Checked == true)
            {
                Bank = _proc.getBank();
                DataRow[] results = Bank.Select("isActive = 1");
                _Bank = Bank.Clone();
                foreach (DataRow dr in results)
                    _Bank.ImportRow(dr);
                bds.DataSource = _Bank;
                bank_old = _Bank.Copy();
            }

            else
            {
                Bank = _proc.getBank();
                bank_old = Bank.Copy();
                bds.DataSource = Bank;
            }
            dgBanks.DataSource = bds;
            id.DataPropertyName = "id";
            cName.DataPropertyName = "cName";
            CorrespondentAccount.DataPropertyName = "CorrespondentAccount";
            BIK.DataPropertyName = "BIC";
            isActive.DataPropertyName = "isActive";
            //FilterDataView();

        }

        private void dgBanks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TempData.Rezhim != "ПР")
            {
                if (e.RowIndex != -1)
                {
                    //   dgBanks.ColumnHeadersVisible = false;
                    cbBanks.Enabled = false;
                    foreach (DataGridViewColumn currDgvColumn in dgBanks.Columns)
                    {
                        currDgvColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                else
                {

                    // dgBanks.ColumnHeadersVisible = true;

                    cbBanks.Enabled = true;
                    if (!saving)
                        foreach (DataGridViewColumn currDgvColumn in dgBanks.Columns)
                        {
                            currDgvColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                        }
                }

                try
                {
                    if (e.RowIndex != -1)
                    {
                        // dgBanks.ColumnHeadersVisible = false;
                        cbBanks.Enabled = false;
                        if (add)
                        {
                            mode = 1;

                        }

                        else mode = 0;
                        dgBanks.ReadOnly = false;

                        _cName = dgBanks.SelectedRows[0].Cells[1].Value.ToString();
                        _cA = dgBanks.SelectedRows[0].Cells[2].Value.ToString();
                        _bIk = dgBanks.SelectedRows[0].Cells[3].Value.ToString();
                        //selected =Convert.ToInt32(dgBanks.SelectedRows[0].Cells[0].Value);
                        saving = true;
                        selected = e.RowIndex;
                    }
                    else
                    {

                    }
                }
                catch (Exception) { }
            }            
        }

        private void cbBanks_CheckedChanged(object sender, EventArgs e)
        {
            if (saving == false)
            {
                ini();
                enable();
                FilterDataView();
            }
            else { MessageBox.Show("Завершите редактирование", "Внимание"); }
        }

        public void save()
        {
            bool gonext;

            string answer = String.Format("Данные были изменены. Сохранить изменения?");
            if (MessageBox.Show(answer, "Внимание", MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if ("Введите наименование" == dgBanks.Rows[selected].Cells[1].Value.ToString() ||
                "0" == dgBanks.Rows[selected].Cells[2].Value.ToString() ||
                 "0" == dgBanks.Rows[selected].Cells[3].Value.ToString() ||
                 "" == dgBanks.Rows[selected].Cells[1].Value.ToString() ||
                "" == dgBanks.Rows[selected].Cells[2].Value.ToString() ||
                 "" == dgBanks.Rows[selected].Cells[3].Value.ToString())
                {
                    MessageBox.Show("Некорректные значения в колонках. Сохранение не возможно");
                    dgBanks.Rows[selected].Selected = true;
                    saving = true;
                    gonext = false;
                }
                else gonext = true;




                if (gonext != false)
                {
                    if (mode == 0)//Редактирование
                    {
                        if (uniq(dgBanks.Rows[selected].Cells[1].Value.ToString(), dgBanks.Rows[selected].Cells[2].Value.ToString(), dgBanks.Rows[selected].Cells[3].Value.ToString()))
                        {
                            _proc.addeditBank(Convert.ToInt32(dgBanks.Rows[selected].Cells[0].Value), dgBanks.Rows[selected].Cells[1].Value.ToString(), dgBanks.Rows[selected].Cells[2].Value.ToString(), dgBanks.Rows[selected].Cells[3].Value.ToString(), mode, 1);

                            EnumerableRowCollection<DataRow> rowCollect = bank_old.AsEnumerable().Where(rq => rq.Field<int>("id") == Convert.ToInt32(dgBanks.Rows[selected].Cells[0].Value));


                            foreach (DataRow r in rowCollect)
                            {

                                Logging.StartFirstLevel(1386);
                                Logging.Comment("ID: " + Convert.ToInt32(dgBanks.Rows[selected].Cells[0].Value));
                                Logging.VariableChange("Наименование банка: ", dgBanks.Rows[selected].Cells[1].Value.ToString(), r["cName"].ToString());
                                Logging.VariableChange("Корр. счет банка: ", dgBanks.Rows[selected].Cells[2].Value.ToString(), r["CorrespondentAccount"].ToString());
                                Logging.VariableChange("БИК банка: ", dgBanks.Rows[selected].Cells[3].Value.ToString(), r["BIC"].ToString());

                                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                                Logging.StopFirstLevel();

                            }

                            saving = false;
                            mode = 3;
                            ini();
                            // dgBanks.ColumnHeadersVisible = true;
                            cbBanks.Enabled = true;
                            foreach (DataGridViewColumn currDgvColumn in dgBanks.Columns)
                            {
                                currDgvColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                            }
                        }
                        else
                        {
                            CheakUniqrec();
                        }

                    }
                    if (mode == 1)//Добавление
                    {
                        if (uniq(dgBanks.Rows[selected].Cells[1].Value.ToString(), dgBanks.Rows[selected].Cells[2].Value.ToString(), dgBanks.Rows[selected].Cells[3].Value.ToString()))
                        {

                            Logging.StartFirstLevel(1385);
                            Logging.Comment("Наименование банка: " + dgBanks.Rows[selected].Cells[1].Value.ToString());
                            Logging.Comment("Корр. счет банка: " + dgBanks.Rows[selected].Cells[2].Value.ToString());
                            Logging.Comment("БИК банка: " + dgBanks.Rows[selected].Cells[3].Value.ToString());

                            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                            + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                            Logging.StopFirstLevel();


                            _proc.addeditBank(Convert.ToInt32(dgBanks.Rows[selected].Cells[0].Value), dgBanks.Rows[selected].Cells[1].Value.ToString(), dgBanks.Rows[selected].Cells[2].Value.ToString(), dgBanks.Rows[selected].Cells[3].Value.ToString(), mode, 1);
                            saving = false;
                            add = false;
                            mode = 3;
                            ini();
                            cbBanks.Enabled = true;
                            foreach (DataGridViewColumn currDgvColumn in dgBanks.Columns)
                            {
                                currDgvColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                            }
                        }
                        else
                        {
                            CheakUniqrec();
                        }
                    }
                }
            }
            else
            {
                saving = false;
                mode = 3;
                ini();
                add = false;
                foreach (DataGridViewColumn currDgvColumn in dgBanks.Columns)
                {
                    currDgvColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                }
                cbBanks.Enabled = true;
            }

            dgBanks.ReadOnly = true;
        }

        private void CheakUniqrec()
        {
            int id = Convert.ToInt32(_proc.CheakBK(dgBanks.Rows[selected].Cells[1].Value.ToString(), dgBanks.Rows[selected].Cells[2].Value.ToString(), dgBanks.Rows[selected].Cells[3].Value.ToString()).Rows[0][0].ToString());
            string bik = _proc.CheakBK(dgBanks.Rows[selected].Cells[1].Value.ToString(), dgBanks.Rows[selected].Cells[2].Value.ToString(), dgBanks.Rows[selected].Cells[3].Value.ToString()).Rows[0][3].ToString();

            string mess1 = String.Format("В справочнике найдены НЕдействующие записи,\nу которых идентичны\nследующие параметры \n{0} ,{1}\n\n  Да - Сделать найденную запись активной \n(редактируемая запись будет удалена) \n\n Нет - Вернуться к редактированию.", dgBanks.Rows[selected].Cells[1].Value.ToString(), dgBanks.Rows[selected].Cells[2].Value.ToString());
            if (_proc.CheakBK(dgBanks.Rows[selected].Cells[1].Value.ToString(), dgBanks.Rows[selected].Cells[2].Value.ToString(), dgBanks.Rows[selected].Cells[3].Value.ToString()).Rows[0][4].ToString() == "False")
            {
                if (MessageBox.Show(mess1, "Внимание", MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    _proc.addeditBank(id, dgBanks.Rows[selected].Cells[1].Value.ToString(), dgBanks.Rows[selected].Cells[2].Value.ToString(), dgBanks.Rows[selected].Cells[3].Value.ToString(), 0, 1);
                    _proc.delBank(Convert.ToInt32(dgBanks.Rows[selected].Cells[0].Value));

                    Logging.StartFirstLevel(1387);
                    Logging.Comment("ID: " + Convert.ToInt32(dgBanks.Rows[selected].Cells[0].Value));
                    Logging.Comment("Наименование банка: " + dgBanks.Rows[selected].Cells[1].Value.ToString());
                    Logging.Comment("Корр. счет банка: " + dgBanks.Rows[selected].Cells[2].Value.ToString());
                    Logging.Comment("БИК банка: " + dgBanks.Rows[selected].Cells[3].Value.ToString());

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();


                    Logging.StartFirstLevel(540);
                    Logging.Comment("Произведена смена статуса на активный у банка");
                    Logging.Comment("ID: " + Convert.ToInt32(dgBanks.Rows[selected].Cells[0].Value));
                    Logging.Comment("Наименование банка: " + dgBanks.Rows[selected].Cells[1].Value.ToString());
                    Logging.Comment("Корр. счет банка: " + dgBanks.Rows[selected].Cells[2].Value.ToString());
                    Logging.Comment("БИК банка: " + dgBanks.Rows[selected].Cells[3].Value.ToString());

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();


                    saving = false;
                    ini();
                    // dgBanks.ColumnHeadersVisible = true;
                    cbBanks.Enabled = true;
                }
                else { dgBanks.Rows[selected].Selected = true; }
            }
            else
            {
                string mess = String.Format("В справочнике найдены записи,\nу которых идентичны\nследующие параметры \n{0},{1}\n Сохранение невозможно.", dgBanks.Rows[selected].Cells[1].Value.ToString(), dgBanks.Rows[selected].Cells[2].Value.ToString());
                MessageBox.Show(mess, "Сохранение изменений");
                dgBanks.Rows[selected].Selected = true;
                saving = true;
            }
        }

        private void FilterDataView()
        {
            try
            {
                DataTable dt;
                string Fstring;
                //if (sBanks.Text == "")
                //{ Fstring = "*"; }
                //else 
                Fstring = sBanks.Text;

                if (cbBanks.Checked == true)
                {
                    dt = _Bank;
                }

                else
                {
                    dt = Bank;
                }

                view = dt.DefaultView;
                StringBuilder sb = new StringBuilder();
                sb.Append("cName like '%" + Fstring + "%'");
                view.RowFilter = sb.ToString();
            }
            catch (Exception) { }

        }

        private void sBanks_TextChanged(object sender, EventArgs e)
        {
            if (saving == false)
            {
                FilterDataView();
                enable();
            }
            { }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgBanks.SelectedRows[0].Cells[4].Value.ToString() == "True")
            {
                if (MessageBox.Show("Изменить признак на \" Недействующий \"", "Внимание", MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Logging.StartFirstLevel(540);
                    Logging.Comment("Произведена смена статуса на неактивный у банка");
                    Logging.Comment("ID: " + Convert.ToInt32(dgBanks.SelectedRows[0].Cells[0].Value));
                    Logging.Comment("Наименование банка: " + dgBanks.SelectedRows[0].Cells[1].Value.ToString());
                    Logging.Comment("Корр. счет банка: " + dgBanks.SelectedRows[0].Cells[2].Value.ToString());
                    Logging.Comment("БИК банка: " + dgBanks.SelectedRows[0].Cells[3].Value.ToString());

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                    _proc.addeditBank(Convert.ToInt32(dgBanks.SelectedRows[0].Cells[0].Value), dgBanks.SelectedRows[0].Cells[1].Value.ToString(), dgBanks.SelectedRows[0].Cells[2].Value.ToString(), dgBanks.SelectedRows[0].Cells[3].Value.ToString(), 0, 0);
                    ini();
                }
            }
            else
            {
                if (MessageBox.Show("Изменить признак на \" Действующий \"", "Внимание", MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Logging.StartFirstLevel(540);
                    Logging.Comment("Произведена смена статуса на активный у банка");
                    Logging.Comment("ID: " + Convert.ToInt32(dgBanks.SelectedRows[0].Cells[0].Value));
                    Logging.Comment("Наименование банка: " + dgBanks.SelectedRows[0].Cells[1].Value.ToString());
                    Logging.Comment("Корр. счет банка: " + dgBanks.SelectedRows[0].Cells[2].Value.ToString());
                    Logging.Comment("БИК банка: " + dgBanks.SelectedRows[0].Cells[3].Value.ToString());

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();

                    _proc.addeditBank(Convert.ToInt32(dgBanks.SelectedRows[0].Cells[0].Value), dgBanks.SelectedRows[0].Cells[1].Value.ToString(), dgBanks.SelectedRows[0].Cells[2].Value.ToString(), dgBanks.SelectedRows[0].Cells[3].Value.ToString(), 0, 1);
                    ini();
                }
            }

            enable();
        }

        private void dgBanks_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (!cbBanks.Checked)
            {
                if (Bank.DefaultView[e.RowIndex]["isActive"].ToString() == "False")
                {
                    dgBanks.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(146, 74, 74);
                }
            }

        }

        private void enable()
        {
            if (dgBanks.Rows.Count == 0)
            {
                btEdit.Enabled = false;
                btExel.Enabled = false;
            }
            else
            {
                btEdit.Enabled = true;
                btExel.Enabled = true;
            }
        }

        private void dgBanks_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            enable();
        }

        private bool uniq(string cName, string cA, string bIc)
        {
            DataRow[] dr;
            dr = Bank.Select("cName ='" + cName + "' and CorrespondentAccount = '" + cA + "'");

            int u = _proc.CheakBK(cName, cA, bIc).Rows.Count;
            int i = dr.Count();

            if (u != 0)
                return false;
            else return true;
        }

        private void dgBanks_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (TempData.Rezhim == "ПР")
            {
                return;
            }

            dgBanks.ReadOnly = true;
            try
            {                
                if ((e.Row.Index != -1) 
                    && (dgBanks.SelectedRows[0].Cells[0].Value.ToString() != dgBanks.Rows[selected].Cells[0].Value.ToString()))
                {
                    if (saving)
                    {
                        if (mode == 0)
                        {
                            save();
                        }

                        if (mode == 1 && flag)
                        {
                            save();
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void dgBanks_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (TempData.Rezhim == "ПР")
            {
                return;
            }

            if (this.dgBanks.Columns[1].SortMode == DataGridViewColumnSortMode.Automatic)
            { 
                saving = false; 
            }
            else saving = true;
        }

        private void dgBanks_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (TempData.Rezhim == "ПР")
            {
                return;
            }

            int a, b, c;
            if (_cName == dgBanks.Rows[selected].Cells[1].Value.ToString())
                a = 0;
            else a = 1;
            if (_cA == dgBanks.Rows[selected].Cells[2].Value.ToString())
                b = 0;
            else b = 1;
            if (_bIk == dgBanks.Rows[selected].Cells[3].Value.ToString())
                c = 0;
            else c = 1;

            if ((a + b + c) == 0 && !add)
            {
                saving = false;
                foreach (DataGridViewColumn currDgvColumn in dgBanks.Columns)
                {
                    currDgvColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                }

            }
            else saving = true;


        }

        private void dgBanks_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (TempData.Rezhim == "ПР")
            {
                return;
            }

            if (this.dgBanks.Columns[1].SortMode == DataGridViewColumnSortMode.Automatic)
            {
                saving = false;
                cbBanks.Enabled = true;
            }
            else
            {
                saving = true;
                cbBanks.Enabled = false;
            }

        }

        private void dgBanks_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (TempData.Rezhim == "ПР")
            {
                return;
            }

            if (this.dgBanks.Columns[1].SortMode == DataGridViewColumnSortMode.Automatic)
            {
                saving = false;
                cbBanks.Enabled = true;
            }
            else
            {
                saving = true;
                cbBanks.Enabled = false;
            }

        }

        private void dgBanks_KeyDown(object sender, KeyEventArgs e)
        {
            if (TempData.Rezhim == "ПР")
            {
                return;
            }

            if (saving == true)
                if (e.KeyCode == Keys.Enter)
                {
                    save();
                }

        }

        private void lockSimbols(KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[\w]|[\s]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private void sBanks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (saving == true)
            {
                Regex pat = new Regex(@"[/b]");
                bool b = pat.IsMatch(e.KeyChar.ToString());
                if (b == false)
                {
                    e.Handled = true;
                }
            }

            else { lockSimbols(e); }
        }

        private void btChoose_Click(object sender, EventArgs e)
        {
            if (saving == false)
            {
                dataBank.id = Convert.ToInt32(dgBanks.SelectedRows[0].Cells[0].Value);
                dataBank.cName = dgBanks.SelectedRows[0].Cells[1].Value.ToString();
                dataBank.cA = dgBanks.SelectedRows[0].Cells[2].Value.ToString();
                dataBank.BIK = dgBanks.SelectedRows[0].Cells[3].Value.ToString();
                DialogResult = DialogResult.Cancel;
            }
            else { MessageBox.Show("Завершите редактирование"); }

        }

        string _fileName;
        frmLoad frmWait = new frmLoad();

        private void btExel_Click(object sender, EventArgs e)
        {
            var fd = new SaveFileDialog { Filter = @"Файлы Excel|*.xls" };
            fd.ShowDialog();
            if (fd.FileName.Trim().Length == 0)
            {
                return;
            }

            /*if (!File.Exists(Application.StartupPath + "\\Templates\\Sections.xls"))
            {
                MessageBox.Show(@"Не обнаружен файл шаблона " + "\\Templates\\Sections.xls", @"Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }*/

            _fileName = fd.FileName.Trim();
            if (!_fileName.EndsWith(".xls", StringComparison.CurrentCultureIgnoreCase))
            {
                _fileName += ".xls";
            }

            this.Enabled = false;

            frmWait = new frmLoad();
            frmWait.TextWait = "ЖДИТЕ. ИДЁТ ВЫГРУЗКА";
            frmWait.Show();

            backgroundWorker1.RunWorkerAsync(new object[] { "\\Templates\\Sections.xls" });
        }

        private void dgBanks_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            editp = edit;
            edit = e.ColumnIndex;
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
          frmWait.Dispose();

            Logging.StartFirstLevel(472);
            Logging.Comment("Выгрузка отчета со списком банков в Excel файл");
            
            Logging.Comment("Наименование Excel файла: "+ System.IO.Path.GetFileName(_fileName));
            Logging.Comment("Путь выгрузки Excel файла: " + System.IO.Path.GetDirectoryName(_fileName));

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
            + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            this.Enabled = true;
        }

        private void dgBanks_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dgBanks.EditingControl.KeyPress += new KeyPressEventHandler(EditingControl_KeyPress);
        }

        void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (edit == 1)
            {
                if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsSeparator(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != '"' || e.KeyChar == '!' || e.KeyChar == '@' || e.KeyChar == '$' || e.KeyChar == '%' || e.KeyChar == '^' || e.KeyChar == '&' || e.KeyChar == '*' || e.KeyChar == '(' || e.KeyChar == ')' || e.KeyChar == '+' || e.KeyChar == '=' || e.KeyChar == '|' || e.KeyChar == '№' || e.KeyChar == ';' || e.KeyChar == '?' || e.KeyChar == '#' || e.KeyChar == ':') //|| e.KeyChar == '-') // &&!(e.KeyChar == System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0]) && !char.IsControl(e.KeyChar) )//&& e.KeyChar == '-')
                {
                    e.Handled = true;
                } 
            }
            else
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Exc.Application appExc = new Exc.Application();
            appExc.DisplayAlerts = false;
            appExc.Visible = false;
            appExc.SheetsInNewWorkbook = 1;
            Exc.Workbook book = appExc.Workbooks.Add(1);
            Exc.Worksheet sheet = (Exc.Worksheet)book.Worksheets[1];
            DataTable dt;

            sheet.Cells[1, 1] = "Справочник банков";
            sheet.get_Range("A1", "C1").Merge();
            sheet.get_Range("A1", "C1").BorderAround();
            sheet.get_Range("A1", "A1").HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
            sheet.get_Range("A1", "A1").Font.Bold = true;
            sheet.get_Range("A1", "A1").Font.Size = 16;

            sheet.Cells[3, 1] = "Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername;
            sheet.get_Range("A3", "C3").Merge();

            sheet.Cells[4, 1] = "Дата выгрузки: " + DateTime.Now;
            sheet.get_Range("A4", "C4").Merge();

            sheet.Cells[6, 1] = "Наименование банка";
            sheet.Cells[6, 2] = "Корреспондентский счет";
            sheet.Cells[6, 3] = "БИК";

            sheet.get_Range("A6", "A6").ColumnWidth = 25;
            sheet.get_Range("B6", "B6").ColumnWidth = 23;
            sheet.get_Range("C6", "C6").ColumnWidth = 8;


            sheet.get_Range("A6", "A6").Font.Bold = true;
            sheet.get_Range("B6", "B6").Font.Bold = true;
            sheet.get_Range("C6", "C6").Font.Bold = true;

            sheet.get_Range("A6", "C6").HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
            sheet.get_Range("A6", "C6").Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            sheet.get_Range("A6", "C6").WrapText = true;


            if (cbBanks.Checked == true)
            {
                dt = _Bank;
            }

            else
            {
                dt = Bank;
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string A, G;
                    A = "A" + (i + 7);
                    G = "C" + (i + 7);


                    sheet.Cells[i + 7, 1] = dt.DefaultView[i]["cName"];
                    sheet.Cells[i + 7, 2] = dt.DefaultView[i]["CorrespondentAccount"];
                    sheet.Cells[i + 7, 3] = dt.DefaultView[i]["BIC"];

                    sheet.get_Range(A, G).WrapText = true;
                    sheet.get_Range(A, G).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop;
                    sheet.get_Range(A, G).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    sheet.PageSetup.PrintArea = "A1:" + G;
                    sheet.get_Range("B6", G).NumberFormat = 0;
                }
            }
            sheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            sheet.PageSetup.LeftMargin = 13.88;
            sheet.PageSetup.RightMargin = 13.88;
            sheet.PageSetup.TopMargin = 13.88;
            sheet.PageSetup.BottomMargin = 13.88;
            sheet.PageSetup.HeaderMargin = 0;
            sheet.PageSetup.FooterMargin = 0;
            appExc.Visible = true;
            object[] args = new object[2];
            args[0] = @_fileName;
            args[1] = 39;
            book.GetType().InvokeMember("SaveAs", BindingFlags.InvokeMethod, null, book, args);
        }

    }



}

