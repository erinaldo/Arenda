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
using Nwuram.Framework.Logging;

namespace Arenda
{
    public partial class AddSec : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        int mode, _id;

        string _Name, _zdan, _floor, _Ktl, _tbkl, _tbphone, _total_area, _area_trade, _obj;
        int _idZdan, _idFloor, _idObj;
        bool _APPZ;
        DataTable dtGetSecInfo;
        DataTable dtFloor;
        DataTable dtZdan;
        DataTable dtObj;

        public AddSec()
        {
            InitializeComponent();
            mode = 1;
            ini();
            chbIsAPPZ.Enabled = new List<string> { "РКВ" }.Contains(TempData.Rezhim);
                //= _proc.SuperUserMode();
            check();
            _Name = "";
            _zdan = "";
            _floor = "";
            _Ktl = "";
            _tbkl = "";
            _tbphone = "";
            _total_area = "";
            _area_trade = "";
            _obj = "";
            _APPZ = chbIsAPPZ.Checked = false;
        }
      
      public AddSec(int id)
      {
        InitializeComponent();
        mode = 0;
        ini();
        chbIsAPPZ.Enabled = new List<string> { "РКВ" }.Contains(TempData.Rezhim);
            //= _proc.SuperUserMode();

            _id = id;

        dtGetSecInfo = new DataTable();
        dtGetSecInfo = _proc.GetSecInfo(_id);

        string msg = "";

        if ((dtGetSecInfo !=null) && (dtGetSecInfo.Rows.Count>0))
        {
          _Name
            = tbcName.Text
            = dtGetSecInfo.Rows[0]["Sec"].ToString();
          
          _zdan = dtGetSecInfo.Rows[0]["Build"].ToString();
          _idZdan = int.Parse(dtGetSecInfo.Rows[0]["id_Build"].ToString());

          _floor = dtGetSecInfo.Rows[0]["Floo"].ToString();
          _idFloor = int.Parse(dtGetSecInfo.Rows[0]["id_Floo"].ToString());

          if (dtGetSecInfo.Rows[0]["id_ObjectLease"] != null)
          {
            _idObj = int.Parse(dtGetSecInfo.Rows[0]["id_ObjectLease"].ToString());
            _obj = dtGetSecInfo.Rows[0]["Obj"].ToString();
          }

          _Ktl
            = tbKtl.Text
            = dtGetSecInfo.Rows[0]["Telephone_lines"].ToString();
          
          _tbkl
            = tbkl.Text
            = dtGetSecInfo.Rows[0]["Lamps"].ToString();

          _tbphone
            = tbphone.Text
            = dtGetSecInfo.Rows[0]["Phone_number"].ToString();

          _total_area
            = textBox2.Text
            = dtGetSecInfo.Rows[0]["Total_Area"].ToString();

          _area_trade
            = textBox1.Text
            = dtGetSecInfo.Rows[0]["Area_of_Trading_Hall"].ToString();

          _APPZ
            = chbIsAPPZ.Checked
            = bool.Parse(dtGetSecInfo.Rows[0]["isAPPZ"].ToString());

          int idZd = int.Parse(dtGetSecInfo.Rows[0]["id_Build"].ToString());

          try
          {
            if (dtZdan.Select("isActive = 1 AND id = " + idZd.ToString()).Count() > 0)
            {
              cbZdan.SelectedValue = idZd;
            }
            else
            {
              dtZdan.DefaultView.RowFilter = "isActive = 1 OR id = " + idZd.ToString();
              cbZdan.SelectedValue = idZd;
              //cbZdan.SelectedIndex = -1;
              //MessageBox.Show("В записи используется неактивное здание, пожалуйста измените его", "Внимание");
              msg += "\n- неактивное здание";
            }                    
          }
          catch
          {
            cbZdan.SelectedIndex = -1;
            //MessageBox.Show("В записи используется неактивное здание, пожалуйста измените его", "Внимание");
            msg += "\n- неактивное здание";
          }

          int idFl = int.Parse(dtGetSecInfo.Rows[0]["id_Floo"].ToString());

          try
          {
            if (dtFloor.Select("isActive = 1 AND id = " + idFl.ToString()).Count() > 0)
            {
              cbFloor.SelectedValue = idFl;
            }
            else
            {
              dtFloor.DefaultView.RowFilter = "isActive = 1 OR id = " + idFl.ToString();
              cbFloor.SelectedValue = idFl;
              //cbFloor.SelectedIndex = -1;
              //MessageBox.Show("В записи используется неактивный этаж, пожалуйста измените его", "Внимание");
              msg += "\n- неактивный этаж";
            }
          }
          catch
          {
            cbFloor.SelectedIndex = -1;
            //MessageBox.Show("В записи используется неактивный этаж, пожалуйста измените его", "Внимание");
            msg += "\n- неактивный этаж";
          }

          int idObj = int.Parse(dtGetSecInfo.Rows[0]["id_ObjectLease"].ToString());

          try
          {
            if (dtObj.Select("isActive = 1 AND id = " + idObj.ToString()).Count() > 0)
            {
              cmbObject.SelectedValue = idObj;
            }
            else
            {
              //cmbObject.SelectedIndex = -1;
              dtObj.DefaultView.RowFilter = "isActive = 1 OR id = " + idObj.ToString();
              cmbObject.SelectedValue = idObj;
              //MessageBox.Show("В записи используется неактивный объект, пожалуйста измените его", "Внимание");
              msg += "\n- неактивный объект";
            }
          }
          catch
          {
            cmbObject.SelectedIndex = -1;
            //MessageBox.Show("В записи используется неактивный объект, пожалуйста измените его", "Внимание");
            msg += "\n- неактивный объект";
          }
          if (msg != "")
            MessageBox.Show("В записи используется:" + msg, "Внимание");
        }
        else
        {
          this.Close();
        }
      }         

        private void btExit_Click(object sender, EventArgs e)
        {
            if (_Name != tbcName.Text
                || _zdan != cbZdan.Text
                || _floor != cbFloor.Text   
                || _Ktl != tbKtl.Text
                || _tbkl != tbkl.Text
                || _tbphone != tbphone.Text
                || _total_area != textBox2.Text
                || _area_trade != textBox1.Text
                || (_APPZ != chbIsAPPZ.Checked)
              || _obj != cmbObject.Text)
            {
                //if (MessageBox.Show("Были внесены изменения. Выйти без сохранения?",
                  //                  "Внимание",
                  //                  MessageBoxButtons.YesNo,
                  //                  MessageBoxIcon.Question,
                  //                  MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              if (MessageBox.Show(" На форме есть несохранённые данные.\nЗакрыть форму без сохранения данных?",
                "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
              {
                this.DialogResult = DialogResult.Cancel;
              }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btAddEq_Click(object sender, EventArgs e)
        {
            if (!CheckNumericValues())
            {
                return;
            }
            
          if(cmbObject.SelectedValue == null)
          {
            MessageBox.Show("Не выбран объект аренды.\nСохранение невозможно.",
              "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }

            int LogId = 0;

            int? tl = 0;
            int? lm = 0;
            string ph;

            if (tbKtl.Text == "")
            {
                tl = null;
            }
            else
                tl = Convert.ToInt32(tbKtl.Text);

            if (tbkl.Text == "")
            {
                lm = null;
            }
            else
                lm = Convert.ToInt32(tbkl.Text);

            if (tbphone.Text == "")
            {
                ph = null;
            }
            else
                ph = tbphone.Text;

            //изменить активацию записей, проверка на изменение данных в секции
            int uniqRec;

            DataTable dtCheakSec = _proc.CheakSec(tbcName.Text, /*cbZdan.Text, cbFloor.Text,*/ (int)cmbObject.SelectedValue);

            if ((dtCheakSec != null) && (dtCheakSec.Rows.Count > 0))
            {
                uniqRec = int.Parse(dtCheakSec.Rows[0][0].ToString());

                if (mode == 1)
                {
                    if (_proc.CheakAll(tbcName.Text, "sc", 0, (int)cmbObject.SelectedValue).Rows.Count != 0)
                    {
                        string rez = _proc.isActiveSec(uniqRec).Rows[0][0].ToString();
                        if (rez == "False")
                        {
                            if (MessageBox.Show("Уже существует неактивная запись с таким наименованием\nдля выбранного объекта аренды!\nСделать запись активной?", "Внимание", MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {

                                string logEvent = "Смена статуса секции";

                                Logging.StartFirstLevel(765);
                                Logging.Comment(logEvent);
                                Logging.Comment("id = " + uniqRec.ToString());
                                Logging.Comment("Наименование секции: \"" + tbcName.Text + "\"");
                                Logging.Comment("Имя объекта: \"" + cmbObject.Text + "\"");
                                Logging.Comment("Статус изменен на активный");
                                Logging.Comment("Завершение операции \"" + logEvent + "\"");
                                Logging.StopFirstLevel();

                                _proc.ActiveSprav("sections", uniqRec, 1, (int)cmbObject.SelectedValue);
                                DialogResult = DialogResult.Cancel;
                            }
                        }
                        else
                        {
                            //MessageBox.Show("Запись с такими параметрами уже существует", "Внимание!");
                          MessageBox.Show("         В справочнике уже\nприсутствует секция с таким\n        наименованием для\nвыбранного объекта аренды.",
                            "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        LogId  = _proc.AddEditSec2(
                            tbcName.Text,
                            cbZdan.Text,
                            cbFloor.Text,
                            mode,
                            0,
                            1,
                            lm,
                            tl,
                            ph,
                            (textBox2.Text != "" ? decimal.Parse(textBox2.Text) : 0),
                            (textBox1.Text != "" ? decimal.Parse(textBox1.Text) : 0),
                            chbIsAPPZ.Checked,
                            (int)cmbObject.SelectedValue);

                        string logEvent = "Добавление секции в справочник ";

                        Logging.StartFirstLevel(757);
                        Logging.Comment(logEvent);
                        Logging.Comment("id = " + LogId.ToString() 
                            + ", Наименование секции: \"" + tbcName.Text + "\"");
                        Logging.Comment("id объекта = " + cmbObject.SelectedValue.ToString()
                          + ", Наименование объекта: \"" + cmbObject.Text + "\"");
                        Logging.Comment("id здания = " + cbZdan.SelectedValue.ToString() 
                            + ", Наименование здания: \"" + cbZdan.Text + "\"");
                        Logging.Comment("id этажа = " + cbFloor.SelectedValue.ToString() 
                            + ", Наименование этажа: \"" + cbFloor.Text + "\"");
                        Logging.Comment("Количество телефонных линий: " + tbKtl.Text);
                        Logging.Comment("Количество светильников: " + tbkl.Text);
                        Logging.Comment("Номер телефона: " + tbphone.Text);
                        Logging.Comment("Общая площадь: " + textBox2.Text);
                        Logging.Comment("Площадь торгового зала: " + textBox1.Text);
                        Logging.Comment("Признак АППЗ: " + ((chbIsAPPZ.Checked) ? "ДА" : "НЕТ"));
                        Logging.Comment("Завершение операции \"" + logEvent + "\"");
                        Logging.StopFirstLevel();

                        MessageBox.Show("Запись добавлена", "Сообщение");
                        DialogResult = DialogResult.Cancel;
                    }
                }
                if (mode == 0)
                {
                    if (_Name == tbcName.Text && _zdan == cbZdan.Text && _floor == cbFloor.Text
                      && _obj == cmbObject.Text)
                    {
                        LogId  = _proc.AddEditSec2(
                            tbcName.Text,
                            cbZdan.Text,
                            cbFloor.Text,
                            mode,
                            _id,
                            1,
                            lm,
                            tl,
                            ph,
                            (textBox2.Text != "" ? decimal.Parse(textBox2.Text) : 0),
                            (textBox1.Text != "" ? decimal.Parse(textBox1.Text) : 0),
                            chbIsAPPZ.Checked,
                            (int)cmbObject.SelectedValue);


                        string logEvent = "Сохранение отредактированной секции в справочник";

                        Logging.StartFirstLevel(758);
                        Logging.Comment(logEvent);
                        Logging.Comment("id = " + LogId.ToString()
                            + ", Наименование секции: \"" + tbcName.Text + "\"");
                        Logging.Comment("");
                        Logging.VariableChange("Наименование секции", tbcName.Text, _Name);
                        Logging.VariableChange("Объект",
                            "id объекта = " + cmbObject.SelectedValue.ToString() + ", Наименование объекта: \"" + cmbObject.Text + "\"",
                            "id объекта = " + _idObj.ToString() + ", Наименование объекта: \"" + _obj + "\"");
                        Logging.VariableChange("Здание",
                            "id здания = " + cbZdan.SelectedValue.ToString() + ", Наименование здания: \"" + cbZdan.Text + "\"",
                            "id здания = " + _idZdan.ToString() + ", Наименование здания: \"" + _zdan + "\"");
                        Logging.VariableChange("Этаж",
                            "id этажа = " + cbFloor.SelectedValue.ToString() + ", Наименование этажа: \"" + cbFloor.Text + "\"",
                            "id этажа = " + _idFloor.ToString() + ", Наименование этажа: \"" + _floor + "\"");
                        Logging.VariableChange("Количество телефонных линий", tbKtl.Text, _Ktl);
                        Logging.VariableChange("Количество светильников", tbkl.Text, _tbkl);
                        Logging.VariableChange("Номер телефона", tbphone.Text, _tbphone);
                        Logging.VariableChange("Общая площадь", textBox2.Text, _total_area);
                        Logging.VariableChange("Площадь торгового зала", textBox1.Text, _area_trade);
                        Logging.VariableChange("Признак АППЗ", 
                            ((chbIsAPPZ.Checked) ? "ДА" : "НЕТ"),
                            ((_APPZ) ? "ДА" : "НЕТ"));
                        Logging.Comment("Завершение операции \"" + logEvent + "\"");
                        Logging.StopFirstLevel();


                        MessageBox.Show("Запись изменена", "Сообщение");
                        DialogResult = DialogResult.Cancel;
                    }
                    else
                    {

                        if (uniqRec == 0)
                        {
                            LogId = _proc.AddEditSec2(
                                tbcName.Text,
                                cbZdan.Text,
                                cbFloor.Text,
                                mode,
                                _id,
                                1,
                                lm,
                                tl,
                                ph,
                                (textBox2.Text != "" ? decimal.Parse(textBox2.Text) : 0),
                                (textBox1.Text != "" ? decimal.Parse(textBox1.Text) : 0),
                                chbIsAPPZ.Checked,
                                (int)cmbObject.SelectedValue);

                            string logEvent = "Сохранение отредактированной секции в справочник";

                            Logging.StartFirstLevel(758);
                            Logging.Comment(logEvent);
                            Logging.Comment("id = " + LogId.ToString()
                                + ", Наименование секции: \"" + tbcName.Text + "\"");
                            Logging.Comment("");
                            Logging.VariableChange("Наименование секции", tbcName.Text, _Name);
                            Logging.VariableChange("Объект",
                                "id объекта = " + cmbObject.SelectedValue.ToString() + ", Наименование объекта: \"" + cmbObject.Text + "\"",
                                "id объекта = " + _idObj.ToString() + ", Наименование объекта: \"" + _obj + "\"");
                            Logging.VariableChange("",
                                "id здания = " + cbZdan.SelectedValue.ToString() + ", Наименование здания: \"" + cbZdan.Text + "\"",
                                "id здания = " + _idZdan.ToString() + ", Наименование здания: \"" + _zdan + "\"");
                            Logging.VariableChange("",
                                "id этажа = " + cbFloor.SelectedValue.ToString() + ", Наименование этажа: \"" + cbFloor.Text + "\"",
                                "id этажа = " + _idFloor.ToString() + ", Наименование этажа: \"" + _floor + "\"");
                            Logging.VariableChange("Количество телефонных линий", tbKtl.Text, _Ktl);
                            Logging.VariableChange("Количество светильников", tbkl.Text, _tbkl);
                            Logging.VariableChange("Номер телефона", tbphone.Text, _tbphone);
                            Logging.VariableChange("Общая площадь", textBox2.Text, _total_area);
                            Logging.VariableChange("Площадь торгового зала", textBox1.Text, _area_trade);
                            Logging.VariableChange("Признак АППЗ",
                                ((chbIsAPPZ.Checked) ? "ДА" : "НЕТ"),
                                ((_APPZ) ? "ДА" : "НЕТ"));
                            Logging.Comment("Завершение операции \"" + logEvent + "\"");
                            Logging.StopFirstLevel();

                            MessageBox.Show("Запись изменена", "Сообщение");
                            DialogResult = DialogResult.Cancel;
                        }
                        else
                        {
                          //MessageBox.Show("Запись с такими параметрами уже существует", "Внимание!");
                          MessageBox.Show("         В справочнике уже\nприсутствует секция с таким\n        наименованием для\nвыбранного объекта аренды.",
                            "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                if (mode == 1)
                {
                    LogId  = _proc.AddEditSec2(
                        tbcName.Text,
                        cbZdan.Text,
                        cbFloor.Text,
                        mode,
                        0,
                        1,
                        lm,
                        tl,
                        ph,
                        (textBox2.Text != "" ? decimal.Parse(textBox2.Text) : 0),
                        (textBox1.Text != "" ? decimal.Parse(textBox1.Text) : 0),
                        chbIsAPPZ.Checked,
                        (int)cmbObject.SelectedValue);


                    string logEvent = "Добавление секции в справочник ";

                    Logging.StartFirstLevel(757);
                    Logging.Comment(logEvent);
                    Logging.Comment("id = " + LogId.ToString()
                        + ", Наименование секции: \"" + tbcName.Text + "\"");
                    Logging.Comment("id объекта = " + cmbObject.SelectedValue.ToString()
                      + ", Наименование объекта: \"" + cmbObject.Text + "\"");
                    Logging.Comment("id здания = " + cbZdan.SelectedValue.ToString()
                        + ", Наименование здания: \"" + cbZdan.Text + "\"");
                    Logging.Comment("id этажа = " + cbFloor.SelectedValue.ToString()
                        + ", Наименование этажа: \"" + cbFloor.Text + "\"");
                    Logging.Comment("Количество телефонных линий: " + tbKtl.Text);
                    Logging.Comment("Количество светильников: " + tbkl.Text);
                    Logging.Comment("Номер телефона: " + tbphone.Text);
                    Logging.Comment("Общая площадь: " + textBox2.Text);
                    Logging.Comment("Площадь торгового зала: " + textBox1.Text);
                    Logging.Comment("Признак АППЗ: " + ((chbIsAPPZ.Checked) ? "ДА" : "НЕТ"));
                    Logging.Comment("Завершение операции \"" + logEvent + "\"");
                    Logging.StopFirstLevel();

                    //MessageBox.Show("Запись добавлена", "Сообщение");
                    MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.Cancel;
                }

                if (mode == 0)
                {
                    LogId  = _proc.AddEditSec2(
                        tbcName.Text,
                        cbZdan.Text,
                        cbFloor.Text,
                        mode,
                        _id,
                        1,
                        lm,
                        tl,
                        ph,
                        (textBox2.Text != "" ? decimal.Parse(textBox2.Text) : 0),
                        (textBox1.Text != "" ? decimal.Parse(textBox1.Text) : 0),
                        chbIsAPPZ.Checked,
                        (int)cmbObject.SelectedValue);

                    string logEvent = "Сохранение отредактированной секции в справочник";

                    Logging.StartFirstLevel(758);
                    Logging.Comment(logEvent);
                    Logging.Comment("id = " + LogId.ToString()
                        + ", Наименование секции: \"" + tbcName.Text + "\"");
                    Logging.Comment("");
                    Logging.VariableChange("Наименование секции", tbcName.Text, _Name);
                    Logging.VariableChange("Объект",
                        "id объекта = " + cmbObject.SelectedValue.ToString() + ", Наименование объекта: \"" + cmbObject.Text + "\"",
                        "id объекта = " + _idObj.ToString() + ", Наименование объекта: \"" + _obj + "\"");
                    Logging.VariableChange("Здание",
                        "id здания = " + cbZdan.SelectedValue.ToString() + ", Наименование здания: \"" + cbZdan.Text + "\"",
                        "id здания = " + _idZdan.ToString() + ", Наименование здания: \"" + _zdan + "\"");
                    Logging.VariableChange("Этаж",
                        "id этажа = " + cbFloor.SelectedValue.ToString() + ", Наименование этажа: \"" + cbFloor.Text + "\"",
                        "id этажа = " + _idFloor.ToString() + ", Наименование этажа: \"" + _floor + "\"");
                    Logging.VariableChange("Количество телефонных линий", tbKtl.Text, _Ktl);
                    Logging.VariableChange("Количество светильников", tbkl.Text, _tbkl);
                    Logging.VariableChange("Номер телефона", tbphone.Text, _tbphone);
                    Logging.VariableChange("Общая площадь", textBox2.Text, _total_area);
                    Logging.VariableChange("Площадь торгового зала", textBox1.Text, _area_trade);
                    Logging.VariableChange("Признак АППЗ",
                        ((chbIsAPPZ.Checked) ? "ДА" : "НЕТ"),
                        ((_APPZ) ? "ДА" : "НЕТ"));
                    Logging.Comment("Завершение операции \"" + logEvent + "\"");
                    Logging.StopFirstLevel();

                    //MessageBox.Show("Запись изменена", "Сообщение");
                    MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.Cancel;
                }
            }
        }

        private bool CheckNumericValues()
        {
            if (textBox2.Text.Length != 0 && !NumericValueIsCorrect(textBox2.Text))
            {
                MessageBox.Show("Значение в поле Общая площадь имеет неверный формат. Число не может превышать 9999999999,99!");
                return false;
            }
            if (textBox1.Text.Length != 0 && !NumericValueIsCorrect(textBox1.Text))
            {
                MessageBox.Show("Значение в поле Площадь торгового зала имеет неверный формат. Число не может превышать 9999999999,99!");
                return false;
            }
            return true;
        }        

        private bool NumericValueIsCorrect(string value)
        {
            return Convert.ToDecimal(value) <= (decimal)9999999999.99;
        }

        private void ini()
        {
            FillCbZloor();
            FillCbZdan();
            FillCmbObject();
        }

        private void FillCbZloor()
        {
            dtFloor = new DataTable();

            dtFloor = _proc.FillCbZdFl(1);

            dtFloor.DefaultView.RowFilter = "isActive = 1";

            if ((dtFloor != null) && (dtFloor.Rows.Count > 0))
            {
                cbFloor.DataSource = dtFloor;
                cbFloor.DisplayMember = "cName";
                cbFloor.ValueMember = "id";
            }

            try
            {
                if (mode == 1)
                {
                    cbFloor.SelectedIndex = -1;
                }
            }
            catch { }
        }

        private void FillCbZdan()
        {
            dtZdan = new DataTable();

            dtZdan = _proc.FillCbZdFl(0);

            dtZdan.DefaultView.RowFilter = "isActive = 1";

            if ((dtZdan != null) && (dtZdan.Rows.Count > 0))
            {
                cbZdan.DataSource = dtZdan;
                cbZdan.DisplayMember = "cName";
                cbZdan.ValueMember = "id";
            }

            try
            {
                if (mode == 1)
                {
                    cbZdan.SelectedIndex = -1;
                }                
            }
            catch { }
        }

      private void FillCmbObject()
      {
        dtObj = new DataTable();
        dtObj = _proc.GetObjects();
        dtObj.DefaultView.RowFilter = "isActive = 1";
        cmbObject.DataSource = dtObj;
        try
        {
          if (mode == 1)
          {
            cmbObject.SelectedIndex = -1;
          }
        }
        catch { }
      }

        private void check()
        {
            try
            {
                if (tbcName.Text.Trim().Length != 0 && cbZdan.Text.Length != 0 && cbFloor.Text.Length != 0 && cmbObject.Text.Length != 0)
                { 
                    btAddEq.Enabled = true; 
                }
                else 
                    btAddEq.Enabled = false;
            }
            catch { }
        }

        private void tbcName_TextChanged(object sender, EventArgs e)
        {
            check();
        }

        private void cbZdan_SelectedValueChanged(object sender, EventArgs e)
        {
            check();
        }

        private void cbFloor_SelectedValueChanged(object sender, EventArgs e)
        {
            check();
        }

        private void lockSimbols(KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[0-9]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private void tbKtl_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockSimbols(e);
        }

        private void tbkl_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockSimbols(e);
        }

        private void tbphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[0-9]|[-]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private void tbKtl_Leave(object sender, EventArgs e)
        {
            if (tbKtl.Text == "0")
                tbKtl.Clear();
        }

        private void tbkl_Leave(object sender, EventArgs e)
        {
            if (tbkl.Text == "0")
            {
                tbkl.Clear();
            }
        }

        private void tbphone_Leave(object sender, EventArgs e)
        {
            if (tbphone.Text == "0")
            {
                tbphone.Clear();
            }
        }

        private void tbcName_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[a-z]|[A-Z]|[а-я]|[А-Я]|[\b]|[0-9]|[\s]|[\w]|[-]|[/]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[0-9]|[,]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex pat = new Regex(@"[\b]|[0-9]|[,]");
            bool b = pat.IsMatch(e.KeyChar.ToString());
            if (b == false)
            {
                e.Handled = true;
            }
        }

        private void cmbObject_SelectedValueChanged(object sender, EventArgs e)
        {
          check();
        }

        private void cmbObject_SelectionChangeCommitted(object sender, EventArgs e)
        {
          check();
        }
    }
}
