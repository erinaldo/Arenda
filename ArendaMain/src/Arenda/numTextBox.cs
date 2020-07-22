using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Arenda
{
    //класс преобразования числа исходя из региональных настроек компьютера пользователя
    static class numTextBox
    {
        /*
         1) с сервера получать как CheckAndChange ниже, таки образом обрабатывается наличие некорректной записи на сервере
         и отсутствие криворучек среди тестировщиков
         
         2) на событие KeyPress
         if (!numTextBox.CorrectSymbol(<имя текстбокса>, e.KeyChar, <true если целое число, false - если decimal>, <true если допускаются отрицательные числа>))
            {
                e.Handled = true;
            }
         например
         if (!numTextBox.CorrectSymbol(txtNds, e.KeyChar, true, false))
            {
                e.Handled = true;
            }
        
         3) на потерю фокуса, событие Leave()
        <имя текстбокса>.Text = numTextBox.CheckAndChange(<имя текстбокса>, <знаков после запятой>, <минимум>, <максимум>, <true если допускаются отрицательные числа>, <то что будет писаться в текстбокс при некорректном значении или если не попали в диапазон>);
        например
        txtPeni.Text = numTextBox.CheckAndChange(txtPeni, 2, 0, 100, false,"");        
        или
        txtPeni.Text = numTextBox.CheckAndChange(txtPeni, 2, 0, 100, false,"0.00");        
        
        4) при сохранении
        Convert.ToDecimal(numTextBox.ConvertToCompPunctuation(<имя текст бокса>.Text))
        например
        Convert.ToDecimal(numTextBox.ConvertToCompPunctuation(tbphone.Text))
        */


        public static string ConvertToSqlPunctuation(string text)
        {
            return text.Replace(NumericSeparator(), '.');
        }

        public static string ConvertToCompPunctuation(string text)
        {
            string t = text;
            t = t.Replace('.', NumericSeparator());
            t = t.Replace(" ", "");

            return t;
        }

        /// <summary>
        /// Процедура получения текущего разделителя целой и дробной части числа на локальном компьютере
        /// </summary>
        /// <returns> разделитель </returns>
        public static char NumericSeparator()
        {
            //обновление информации по региональным настройкам windows
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            return char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }

        /// <summary>
        /// процедура обработки вводимых символов 
        /// </summary>
        /// <param name="txt">ТекстБокс - контрол формы</param>
        /// <param name="e">обработчик события </param>
        /// <param name="integerr">true - целое число, false - допускается ввод разделителя целой и дробной части</param>
        /// <param name="negative">признак допустимости отрицательных значений: true - число может быть отрицательным, false - не может</param>
        public static void KeyPress(TextBox txt, KeyPressEventArgs e, bool integerr, bool negative)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (txt.SelectedText != "") { txt.Text.Replace(txt.SelectedText, ""); }

                char s = CorrectSymbols(txt, e.KeyChar, integerr, negative);
                
                if (s != ' ')
                    e.KeyChar = s;
                else
                    e.Handled = true;
            }   
        }

        /// <summary>
        /// процедура обработки вводимых символов 
        /// </summary>
        /// <param name="txt">ТекстБокс - контрол формы</param>
        /// <param name="ch">Вводимый символ</param>
        /// <param name="integerr">true - целое число, false - допускается ввод разделителя целой и дробной части</param>
        /// <param name="negative">признак допустимости отрицательных значений: true - число может быть отрицательным, false - не может</param>
        /// <returns></returns>
        public static char CorrectSymbols(TextBox txt, char ch, bool integerr, bool negative)
        {
            if (integerr)
            {
                if (char.IsDigit(ch))
                {
                    return ch;
                }
            }
            else
            {
                //число
                if (char.IsDigit(ch))
                {
                    return ch;
                }

                if (negative)
                {
                    if (
                        //знак минуса
                        (ch == '-')
                        &&
                        //первый
                        (txt.Text.Length == 0)
                        )
                    {
                        return ch;
                    }
                }

                if (
                    //символ разделитель
                    ((ch == '.') || (ch == ',') || (ch == '/') || (ch == 'ю') || (ch == 'б'))
                    &&
                    //не первый
                    (txt.Text.Length != 0)
                    &&
                    //других разделителей не было
                    (!AnotherSeparatorWasDetected(txt))
                    )
                {
                    return '.';
                }
            }

            return ' ';
        }

        /// <summary>
        /// проверка, что в поле еще не было разделителей
        /// </summary>
        /// <param name="txt">текст бокс</param>
        /// <returns>true - разделитель уже был введен, false - разделитель еще не вводился </returns>
        public static bool AnotherSeparatorWasDetected(TextBox txt)
        {
            bool res = false;

            char[] dd = txt.Text.ToCharArray();

            for (int i = 0; dd.Count() > i; i++)
            {
                if (dd[i] == '.')
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        /// <summary>
        /// Процедура проверки корректности введенного значения в текстбоксе
        /// </summary>
        /// <param name="txt">текст бокс</param>
        /// <param name="signsAfterZpt">количество знаков после запятой</param>
        /// <param name="minValue">минимальное допустимое значение</param>
        /// <param name="maxValue">максимальное допустимое значение</param>
        /// <param name="negative">признак допустимости отрицательных значений: true - число может быть отрицательным, false - не может</param>
        /// <param name="defaultVal">значение по умолчанию которым заполняется текст бокс если введено некорректное значение</param>
        /// <param name="SpecFormat">специальный формат текста</param>        
        /// <returns>Скорректированное значение ("пусто" в случае если число не входило в заданный диапазон, либо если в текстбоксе был введен текст)</returns>
        public static string CheckAndChange(string txt, int signsAfterZpt, decimal minValue, decimal maxValue, bool negative, string defaultVal, string SpecFormat)
        {
            string res = "";
            int IntChislo = 0;
            decimal DecChislo = 0;
            bool correct = true; //признак корректного значения в текстбоксе
            string format = "0:0";
            string originText = ConvertToCompPunctuation(txt);

            
            if (signsAfterZpt < 0)
            {
                res = defaultVal;
                return res;
            }

            if (signsAfterZpt == 0)
            {
                if (negative)
                {
                    if (!int.TryParse(originText, out IntChislo))
                        correct = false;
                }
                else
                {
                    if (!int.TryParse(originText, out IntChislo) || (IntChislo < 0))
                        correct = false;
                }                
            }

            if (signsAfterZpt > 0)
            {
                if (negative)
                {
                    if (!decimal.TryParse(originText, out DecChislo))
                        correct = false;
                }
                else
                {
                    if (!decimal.TryParse(originText, out DecChislo) || (DecChislo < 0))
                        correct = false;
                }                
            }

            if (signsAfterZpt == 0)
            {
                if ((!correct) || (IntChislo < minValue) || (IntChislo > maxValue))
                {
                    res = defaultVal;
                }
                else
                {
                    format = "{" + format + "}";

                    if (SpecFormat == "")
                    {
                        res = String.Format(format, IntChislo);
                    }
                    else
                    {
                        res = String.Format(SpecFormat, IntChislo);
                    }
                }
            }

            if (signsAfterZpt > 0)
            {
                if ((!correct) || (DecChislo < minValue) || (DecChislo > maxValue))
                {
                    res = defaultVal;
                }
                else
                {
                    format += ".";
                    for (int i = 0; signsAfterZpt > i; i++)
                    {
                        format += "0";
                    }

                    format = "{" + format + "}";

                    if (SpecFormat == "")
                    {
                        res = String.Format(format, DecChislo);
                    }
                    else
                    {
                        res = String.Format(SpecFormat, DecChislo).Trim();
                    }                    
                }
            }            

            return ConvertToSqlPunctuation(res);
        }

    }
}
