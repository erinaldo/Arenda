using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Arenda
{
    public partial class frmSealSections : Form
    {
        readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        public int id_agreements { set; private get; }
        private DataTable dtData;
        public string placeName { set; private get; }

        public frmSealSections()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btExit, "Выход");
            tp.SetToolTip(btSeal, "Опечатать секцию");
            tp.SetToolTip(btOpen, "Открыть секцию");
        }

        private void btSeal_Click(object sender, EventArgs e)
        {
            frmSelectDate frmSelectDate = new frmSelectDate() { Text ="Опечатать секцию"};
            if (DialogResult.OK == frmSelectDate.ShowDialog())
            {
                _proc.setSealSections(id_agreements, frmSelectDate.date, 1);

                Logging.StartFirstLevel(765);

                Logging.Comment("Смена статуса секции на «Опечатана»");
                Logging.Comment($"ID договора:{id_agreements}");
                Logging.Comment($"Место:{placeName}");
                Logging.Comment($"Дата опечатывания:{frmSelectDate.date.ToShortDateString()}");
                Logging.StopFirstLevel();

                getData();
            }
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable()
                        .Where(r => r.Field<object>("DateOpen") == null);

            frmSelectDate frmSelectDate = new frmSelectDate() { Text = "Открыть секцию", dateMin = (DateTime)rowCollect.First()["DateSeal"] };
            if (DialogResult.OK == frmSelectDate.ShowDialog())
            {
                _proc.setSealSections(id_agreements, frmSelectDate.date, 2);

                Logging.StartFirstLevel(765);

                Logging.Comment("Смена статуса секции на «Открыта»");
                Logging.Comment($"ID договора:{id_agreements}");
                Logging.Comment($"Место:{placeName}");
                Logging.Comment($"Дата открытия:{frmSelectDate.date.ToShortDateString()}");
                Logging.StopFirstLevel();

                getData();
            }
        }

        private void frmSealSections_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void getData()
        {
            dtData = _proc.getSealSections(id_agreements);
            dgvData.DataSource = dtData;

            if (dtData != null && dtData.Rows.Count > 0)
            {
                EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable()
                        .Where(r => r.Field<object>("DateOpen") == null);

                btOpen.Enabled = rowCollect.Count() != 0;
                btSeal.Enabled = rowCollect.Count() == 0;
            }
            else
            {
                btOpen.Enabled = false;
            }

        }
    }
}
