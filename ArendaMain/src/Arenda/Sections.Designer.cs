namespace Arenda
{
    partial class Sections
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btExel = new System.Windows.Forms.Button();
            this.btDelSec = new System.Windows.Forms.Button();
            this.btEditSec = new System.Windows.Forms.Button();
            this.btAddSec = new System.Windows.Forms.Button();
            this.btDelEq = new System.Windows.Forms.Button();
            this.btEditEq = new System.Windows.Forms.Button();
            this.btAddEq = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btnAddDeviceToSection = new System.Windows.Forms.Button();
            this.btnEditDevice = new System.Windows.Forms.Button();
            this.btnDeleteDeviceFromSection = new System.Windows.Forms.Button();
            this.checEq = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checSec = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgEqVsSec = new System.Windows.Forms.DataGridView();
            this.Equipment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idEqVsSec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bds = new System.Windows.Forms.BindingSource(this.components);
            this.bds2 = new System.Windows.Forms.BindingSource(this.components);
            this.cbZdan = new System.Windows.Forms.ComboBox();
            this.cbZloor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbFEq = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tbSections1 = new System.Windows.Forms.DataGridView();
            this.sSec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cIdObj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cObj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sBuild = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sFloo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sisActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sTelephone_lines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sLamps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sPhone_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sTotal_Area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sArea_of_Trading_Hall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isAPPZ = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tbcRelations = new System.Windows.Forms.TabControl();
            this.tabEquipment = new System.Windows.Forms.TabPage();
            this.tabDevices = new System.Windows.Forms.TabPage();
            this.pbInactiveDevices = new System.Windows.Forms.PictureBox();
            this.dgvDevices = new System.Windows.Forms.DataGridView();
            this.id_device = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device_quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device_active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDeviceName = new System.Windows.Forms.TextBox();
            this.cbAllDevices = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.btReportArendSection = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgEqVsSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSections1)).BeginInit();
            this.tbcRelations.SuspendLayout();
            this.tabEquipment.SuspendLayout();
            this.tabDevices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInactiveDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevices)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(111, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(147, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Поиск по секции";
            // 
            // btExel
            // 
            this.btExel.Image = global::Arenda.Properties.Resources.pict_excel;
            this.btExel.Location = new System.Drawing.Point(927, 377);
            this.btExel.Name = "btExel";
            this.btExel.Size = new System.Drawing.Size(32, 32);
            this.btExel.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btExel, "Выгрузить");
            this.btExel.UseVisualStyleBackColor = true;
            this.btExel.Click += new System.EventHandler(this.btExel_Click);
            // 
            // btDelSec
            // 
            this.btDelSec.Image = global::Arenda.Properties.Resources.DeleteHS;
            this.btDelSec.Location = new System.Drawing.Point(596, 325);
            this.btDelSec.Name = "btDelSec";
            this.btDelSec.Size = new System.Drawing.Size(32, 32);
            this.btDelSec.TabIndex = 24;
            this.toolTip1.SetToolTip(this.btDelSec, "Удалить");
            this.btDelSec.UseVisualStyleBackColor = true;
            this.btDelSec.Click += new System.EventHandler(this.btDelSec_Click);
            // 
            // btEditSec
            // 
            this.btEditSec.Image = global::Arenda.Properties.Resources.EditTableHS;
            this.btEditSec.Location = new System.Drawing.Point(558, 325);
            this.btEditSec.Name = "btEditSec";
            this.btEditSec.Size = new System.Drawing.Size(32, 32);
            this.btEditSec.TabIndex = 23;
            this.toolTip1.SetToolTip(this.btEditSec, "Редактировать");
            this.btEditSec.UseVisualStyleBackColor = true;
            this.btEditSec.Click += new System.EventHandler(this.btEditSec_Click);
            // 
            // btAddSec
            // 
            this.btAddSec.Image = global::Arenda.Properties.Resources.DocumentHS;
            this.btAddSec.Location = new System.Drawing.Point(516, 325);
            this.btAddSec.Name = "btAddSec";
            this.btAddSec.Size = new System.Drawing.Size(32, 32);
            this.btAddSec.TabIndex = 22;
            this.toolTip1.SetToolTip(this.btAddSec, "Добавить");
            this.btAddSec.UseVisualStyleBackColor = true;
            this.btAddSec.Click += new System.EventHandler(this.btAddSec_Click);
            // 
            // btDelEq
            // 
            this.btDelEq.Image = global::Arenda.Properties.Resources.DeleteHS;
            this.btDelEq.Location = new System.Drawing.Point(326, 267);
            this.btDelEq.Name = "btDelEq";
            this.btDelEq.Size = new System.Drawing.Size(32, 32);
            this.btDelEq.TabIndex = 18;
            this.toolTip1.SetToolTip(this.btDelEq, "Удалить");
            this.btDelEq.UseVisualStyleBackColor = true;
            this.btDelEq.Click += new System.EventHandler(this.btDelEq_Click);
            // 
            // btEditEq
            // 
            this.btEditEq.Image = global::Arenda.Properties.Resources.EditTableHS;
            this.btEditEq.Location = new System.Drawing.Point(288, 267);
            this.btEditEq.Name = "btEditEq";
            this.btEditEq.Size = new System.Drawing.Size(32, 32);
            this.btEditEq.TabIndex = 17;
            this.toolTip1.SetToolTip(this.btEditEq, "Редактировать");
            this.btEditEq.UseVisualStyleBackColor = true;
            this.btEditEq.Click += new System.EventHandler(this.btEditEq_Click);
            // 
            // btAddEq
            // 
            this.btAddEq.Image = global::Arenda.Properties.Resources.DocumentHS;
            this.btAddEq.Location = new System.Drawing.Point(246, 267);
            this.btAddEq.Name = "btAddEq";
            this.btAddEq.Size = new System.Drawing.Size(32, 32);
            this.btAddEq.TabIndex = 16;
            this.toolTip1.SetToolTip(this.btAddEq, "Добавить");
            this.btAddEq.UseVisualStyleBackColor = true;
            this.btAddEq.Click += new System.EventHandler(this.btAddEq_Click);
            // 
            // btExit
            // 
            this.btExit.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btExit.Location = new System.Drawing.Point(965, 377);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btExit, "Выход");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btnAddDeviceToSection
            // 
            this.btnAddDeviceToSection.Image = global::Arenda.Properties.Resources.DocumentHS;
            this.btnAddDeviceToSection.Location = new System.Drawing.Point(247, 272);
            this.btnAddDeviceToSection.Name = "btnAddDeviceToSection";
            this.btnAddDeviceToSection.Size = new System.Drawing.Size(32, 32);
            this.btnAddDeviceToSection.TabIndex = 37;
            this.toolTip1.SetToolTip(this.btnAddDeviceToSection, "Добавить");
            this.btnAddDeviceToSection.UseVisualStyleBackColor = true;
            this.btnAddDeviceToSection.Click += new System.EventHandler(this.btnAddDeviceToSection_Click);
            // 
            // btnEditDevice
            // 
            this.btnEditDevice.Image = global::Arenda.Properties.Resources.EditTableHS;
            this.btnEditDevice.Location = new System.Drawing.Point(289, 272);
            this.btnEditDevice.Name = "btnEditDevice";
            this.btnEditDevice.Size = new System.Drawing.Size(32, 32);
            this.btnEditDevice.TabIndex = 38;
            this.toolTip1.SetToolTip(this.btnEditDevice, "Редактировать");
            this.btnEditDevice.UseVisualStyleBackColor = true;
            this.btnEditDevice.Click += new System.EventHandler(this.btnEditDevice_Click);
            // 
            // btnDeleteDeviceFromSection
            // 
            this.btnDeleteDeviceFromSection.Image = global::Arenda.Properties.Resources.DeleteHS;
            this.btnDeleteDeviceFromSection.Location = new System.Drawing.Point(327, 272);
            this.btnDeleteDeviceFromSection.Name = "btnDeleteDeviceFromSection";
            this.btnDeleteDeviceFromSection.Size = new System.Drawing.Size(32, 32);
            this.btnDeleteDeviceFromSection.TabIndex = 39;
            this.toolTip1.SetToolTip(this.btnDeleteDeviceFromSection, "Удалить");
            this.btnDeleteDeviceFromSection.UseVisualStyleBackColor = true;
            this.btnDeleteDeviceFromSection.Click += new System.EventHandler(this.btnDeleteDeviceFromSection_Click);
            // 
            // checEq
            // 
            this.checEq.AutoSize = true;
            this.checEq.Location = new System.Drawing.Point(13, 263);
            this.checEq.Name = "checEq";
            this.checEq.Size = new System.Drawing.Size(45, 17);
            this.checEq.TabIndex = 20;
            this.checEq.Text = "Все";
            this.checEq.UseVisualStyleBackColor = true;
            this.checEq.CheckedChanged += new System.EventHandler(this.checEq_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "неактивные";
            // 
            // checSec
            // 
            this.checSec.AutoSize = true;
            this.checSec.Location = new System.Drawing.Point(15, 328);
            this.checSec.Name = "checSec";
            this.checSec.Size = new System.Drawing.Size(45, 17);
            this.checSec.TabIndex = 26;
            this.checSec.Text = "Все";
            this.checSec.UseVisualStyleBackColor = true;
            this.checSec.CheckedChanged += new System.EventHandler(this.checSec_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "неактивные";
            // 
            // dgEqVsSec
            // 
            this.dgEqVsSec.AllowUserToAddRows = false;
            this.dgEqVsSec.AllowUserToDeleteRows = false;
            this.dgEqVsSec.AllowUserToResizeRows = false;
            this.dgEqVsSec.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgEqVsSec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEqVsSec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Equipment,
            this.idEqVsSec,
            this.isActive,
            this.Col});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgEqVsSec.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgEqVsSec.Location = new System.Drawing.Point(9, 29);
            this.dgEqVsSec.MultiSelect = false;
            this.dgEqVsSec.Name = "dgEqVsSec";
            this.dgEqVsSec.ReadOnly = true;
            this.dgEqVsSec.RowHeadersVisible = false;
            this.dgEqVsSec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEqVsSec.Size = new System.Drawing.Size(350, 214);
            this.dgEqVsSec.TabIndex = 29;
            this.dgEqVsSec.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEqVsSec_CellClick);
            this.dgEqVsSec.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEqVsSec_CellEnter);
            this.dgEqVsSec.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgEqVsSec_ColumnHeaderMouseClick);
            this.dgEqVsSec.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_RowPostPaint);
            this.dgEqVsSec.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgEqVsSec_RowPrePaint);
            this.dgEqVsSec.SelectionChanged += new System.EventHandler(this.dgEqVsSec_SelectionChanged);
            // 
            // Equipment
            // 
            this.Equipment.DataPropertyName = "cName";
            this.Equipment.HeaderText = "Оборудование";
            this.Equipment.Name = "Equipment";
            this.Equipment.ReadOnly = true;
            // 
            // idEqVsSec
            // 
            this.idEqVsSec.DataPropertyName = "id";
            this.idEqVsSec.HeaderText = "id";
            this.idEqVsSec.Name = "idEqVsSec";
            this.idEqVsSec.ReadOnly = true;
            this.idEqVsSec.Visible = false;
            // 
            // isActive
            // 
            this.isActive.DataPropertyName = "isActive";
            this.isActive.HeaderText = "isActive";
            this.isActive.Name = "isActive";
            this.isActive.ReadOnly = true;
            this.isActive.Visible = false;
            // 
            // Col
            // 
            this.Col.DataPropertyName = "Quantity";
            this.Col.HeaderText = "Количество";
            this.Col.Name = "Col";
            this.Col.ReadOnly = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Coral;
            this.pictureBox2.Location = new System.Drawing.Point(68, 330);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(12, 12);
            this.pictureBox2.TabIndex = 27;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Yellow;
            this.pictureBox1.Location = new System.Drawing.Point(66, 265);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(12, 12);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // cbZdan
            // 
            this.cbZdan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZdan.FormattingEnabled = true;
            this.cbZdan.Items.AddRange(new object[] {
            "Все здания"});
            this.cbZdan.Location = new System.Drawing.Point(251, 5);
            this.cbZdan.Name = "cbZdan";
            this.cbZdan.Size = new System.Drawing.Size(190, 21);
            this.cbZdan.TabIndex = 30;
            this.cbZdan.SelectedIndexChanged += new System.EventHandler(this.cbZdan_SelectedIndexChanged);
            this.cbZdan.TextChanged += new System.EventHandler(this.cbZdan_TextChanged);
            // 
            // cbZloor
            // 
            this.cbZloor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZloor.FormattingEnabled = true;
            this.cbZloor.Items.AddRange(new object[] {
            "Все этажи"});
            this.cbZloor.Location = new System.Drawing.Point(486, 5);
            this.cbZloor.Name = "cbZloor";
            this.cbZloor.Size = new System.Drawing.Size(151, 21);
            this.cbZloor.TabIndex = 31;
            this.cbZloor.SelectedIndexChanged += new System.EventHandler(this.cbZloor_SelectedIndexChanged);
            this.cbZloor.TextChanged += new System.EventHandler(this.cbZloor_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(201, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Здание";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(447, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Этаж";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Поиск по оборудованию";
            // 
            // tbFEq
            // 
            this.tbFEq.Location = new System.Drawing.Point(142, 3);
            this.tbFEq.Name = "tbFEq";
            this.tbFEq.Size = new System.Drawing.Size(100, 20);
            this.tbFEq.TabIndex = 36;
            this.tbFEq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbFEq.TextChanged += new System.EventHandler(this.tbFEq_TextChanged);
            this.tbFEq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFEq_KeyPress);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // tbSections1
            // 
            this.tbSections1.AllowUserToAddRows = false;
            this.tbSections1.AllowUserToDeleteRows = false;
            this.tbSections1.AllowUserToResizeRows = false;
            this.tbSections1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tbSections1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tbSections1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbSections1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sSec,
            this.cIdObj,
            this.cObj,
            this.sBuild,
            this.sFloo,
            this.sid,
            this.sisActive,
            this.sTelephone_lines,
            this.sLamps,
            this.sPhone_number,
            this.sTotal_Area,
            this.sArea_of_Trading_Hall,
            this.isAPPZ});
            this.tbSections1.Location = new System.Drawing.Point(12, 95);
            this.tbSections1.MultiSelect = false;
            this.tbSections1.Name = "tbSections1";
            this.tbSections1.ReadOnly = true;
            this.tbSections1.RowHeadersVisible = false;
            this.tbSections1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tbSections1.Size = new System.Drawing.Size(613, 214);
            this.tbSections1.TabIndex = 37;
            this.tbSections1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgSec_ColumnHeaderMouseClick);
            this.tbSections1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgSec_RowPrePaint);
            this.tbSections1.SelectionChanged += new System.EventHandler(this.dgSec_SelectionChanged);
            // 
            // sSec
            // 
            this.sSec.DataPropertyName = "Sec";
            this.sSec.HeaderText = "Секция";
            this.sSec.Name = "sSec";
            this.sSec.ReadOnly = true;
            // 
            // cIdObj
            // 
            this.cIdObj.DataPropertyName = "id_ObjectLease";
            this.cIdObj.HeaderText = "idObj";
            this.cIdObj.Name = "cIdObj";
            this.cIdObj.ReadOnly = true;
            this.cIdObj.Visible = false;
            // 
            // cObj
            // 
            this.cObj.DataPropertyName = "Obj";
            this.cObj.FillWeight = 70F;
            this.cObj.HeaderText = "Объект";
            this.cObj.Name = "cObj";
            this.cObj.ReadOnly = true;
            // 
            // sBuild
            // 
            this.sBuild.DataPropertyName = "Build";
            this.sBuild.HeaderText = "Здание";
            this.sBuild.Name = "sBuild";
            this.sBuild.ReadOnly = true;
            // 
            // sFloo
            // 
            this.sFloo.DataPropertyName = "Floo";
            this.sFloo.HeaderText = "Этаж";
            this.sFloo.Name = "sFloo";
            this.sFloo.ReadOnly = true;
            // 
            // sid
            // 
            this.sid.DataPropertyName = "id";
            this.sid.HeaderText = "id";
            this.sid.Name = "sid";
            this.sid.ReadOnly = true;
            this.sid.Visible = false;
            // 
            // sisActive
            // 
            this.sisActive.DataPropertyName = "isActive";
            this.sisActive.HeaderText = "isActive";
            this.sisActive.Name = "sisActive";
            this.sisActive.ReadOnly = true;
            this.sisActive.Visible = false;
            // 
            // sTelephone_lines
            // 
            this.sTelephone_lines.DataPropertyName = "Telephone_lines";
            this.sTelephone_lines.FillWeight = 60F;
            this.sTelephone_lines.HeaderText = "Кол. тел. линий";
            this.sTelephone_lines.Name = "sTelephone_lines";
            this.sTelephone_lines.ReadOnly = true;
            // 
            // sLamps
            // 
            this.sLamps.DataPropertyName = "Lamps";
            this.sLamps.FillWeight = 60F;
            this.sLamps.HeaderText = "Кол. свет-в";
            this.sLamps.Name = "sLamps";
            this.sLamps.ReadOnly = true;
            // 
            // sPhone_number
            // 
            this.sPhone_number.DataPropertyName = "Phone_number";
            this.sPhone_number.HeaderText = "Номер телефона";
            this.sPhone_number.Name = "sPhone_number";
            this.sPhone_number.ReadOnly = true;
            // 
            // sTotal_Area
            // 
            this.sTotal_Area.DataPropertyName = "Total_Area";
            this.sTotal_Area.FillWeight = 80F;
            this.sTotal_Area.HeaderText = "Общ. площадь";
            this.sTotal_Area.Name = "sTotal_Area";
            this.sTotal_Area.ReadOnly = true;
            // 
            // sArea_of_Trading_Hall
            // 
            this.sArea_of_Trading_Hall.DataPropertyName = "Area_of_Trading_Hall";
            this.sArea_of_Trading_Hall.FillWeight = 80F;
            this.sArea_of_Trading_Hall.HeaderText = "Пл. торг. зал";
            this.sArea_of_Trading_Hall.Name = "sArea_of_Trading_Hall";
            this.sArea_of_Trading_Hall.ReadOnly = true;
            // 
            // isAPPZ
            // 
            this.isAPPZ.DataPropertyName = "isAPPZ";
            this.isAPPZ.FalseValue = "0";
            this.isAPPZ.FillWeight = 60F;
            this.isAPPZ.HeaderText = "АППЗ";
            this.isAPPZ.Name = "isAPPZ";
            this.isAPPZ.ReadOnly = true;
            this.isAPPZ.TrueValue = "1";
            // 
            // tbcRelations
            // 
            this.tbcRelations.Controls.Add(this.tabEquipment);
            this.tbcRelations.Controls.Add(this.tabDevices);
            this.tbcRelations.Location = new System.Drawing.Point(634, 33);
            this.tbcRelations.Name = "tbcRelations";
            this.tbcRelations.SelectedIndex = 0;
            this.tbcRelations.Size = new System.Drawing.Size(375, 338);
            this.tbcRelations.TabIndex = 38;
            // 
            // tabEquipment
            // 
            this.tabEquipment.Controls.Add(this.dgEqVsSec);
            this.tabEquipment.Controls.Add(this.label7);
            this.tabEquipment.Controls.Add(this.tbFEq);
            this.tabEquipment.Controls.Add(this.checEq);
            this.tabEquipment.Controls.Add(this.label2);
            this.tabEquipment.Controls.Add(this.pictureBox1);
            this.tabEquipment.Controls.Add(this.btAddEq);
            this.tabEquipment.Controls.Add(this.btEditEq);
            this.tabEquipment.Controls.Add(this.btDelEq);
            this.tabEquipment.Location = new System.Drawing.Point(4, 22);
            this.tabEquipment.Name = "tabEquipment";
            this.tabEquipment.Padding = new System.Windows.Forms.Padding(3);
            this.tabEquipment.Size = new System.Drawing.Size(367, 312);
            this.tabEquipment.TabIndex = 0;
            this.tabEquipment.Text = "Оборудование";
            this.tabEquipment.UseVisualStyleBackColor = true;
            // 
            // tabDevices
            // 
            this.tabDevices.Controls.Add(this.pbInactiveDevices);
            this.tabDevices.Controls.Add(this.dgvDevices);
            this.tabDevices.Controls.Add(this.label6);
            this.tabDevices.Controls.Add(this.txtDeviceName);
            this.tabDevices.Controls.Add(this.cbAllDevices);
            this.tabDevices.Controls.Add(this.label8);
            this.tabDevices.Controls.Add(this.btnAddDeviceToSection);
            this.tabDevices.Controls.Add(this.btnEditDevice);
            this.tabDevices.Controls.Add(this.btnDeleteDeviceFromSection);
            this.tabDevices.Location = new System.Drawing.Point(4, 22);
            this.tabDevices.Name = "tabDevices";
            this.tabDevices.Padding = new System.Windows.Forms.Padding(3);
            this.tabDevices.Size = new System.Drawing.Size(367, 312);
            this.tabDevices.TabIndex = 1;
            this.tabDevices.Text = "Приборы";
            this.tabDevices.UseVisualStyleBackColor = true;
            // 
            // pbInactiveDevices
            // 
            this.pbInactiveDevices.BackColor = System.Drawing.Color.Yellow;
            this.pbInactiveDevices.Location = new System.Drawing.Point(65, 270);
            this.pbInactiveDevices.Name = "pbInactiveDevices";
            this.pbInactiveDevices.Size = new System.Drawing.Size(12, 12);
            this.pbInactiveDevices.TabIndex = 45;
            this.pbInactiveDevices.TabStop = false;
            // 
            // dgvDevices
            // 
            this.dgvDevices.AllowUserToAddRows = false;
            this.dgvDevices.AllowUserToDeleteRows = false;
            this.dgvDevices.AllowUserToResizeRows = false;
            this.dgvDevices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDevices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDevices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_device,
            this.device_name,
            this.device_quantity,
            this.device_unit,
            this.device_active});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDevices.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDevices.Location = new System.Drawing.Point(10, 34);
            this.dgvDevices.MultiSelect = false;
            this.dgvDevices.Name = "dgvDevices";
            this.dgvDevices.ReadOnly = true;
            this.dgvDevices.RowHeadersVisible = false;
            this.dgvDevices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDevices.Size = new System.Drawing.Size(350, 214);
            this.dgvDevices.TabIndex = 42;
            this.dgvDevices.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_RowPostPaint);
            this.dgvDevices.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvDevices_RowPrePaint);
            // 
            // id_device
            // 
            this.id_device.DataPropertyName = "id";
            this.id_device.HeaderText = "id_device";
            this.id_device.Name = "id_device";
            this.id_device.ReadOnly = true;
            this.id_device.Visible = false;
            // 
            // device_name
            // 
            this.device_name.DataPropertyName = "cname";
            this.device_name.HeaderText = "Приборы";
            this.device_name.Name = "device_name";
            this.device_name.ReadOnly = true;
            // 
            // device_quantity
            // 
            this.device_quantity.DataPropertyName = "quantity";
            this.device_quantity.HeaderText = "Количество";
            this.device_quantity.Name = "device_quantity";
            this.device_quantity.ReadOnly = true;
            // 
            // device_unit
            // 
            this.device_unit.DataPropertyName = "unit";
            this.device_unit.HeaderText = "Единицы измерения";
            this.device_unit.Name = "device_unit";
            this.device_unit.ReadOnly = true;
            // 
            // device_active
            // 
            this.device_active.DataPropertyName = "is_active";
            this.device_active.HeaderText = "device_active";
            this.device_active.Name = "device_active";
            this.device_active.ReadOnly = true;
            this.device_active.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "Поиск по приборам:";
            // 
            // txtDeviceName
            // 
            this.txtDeviceName.Location = new System.Drawing.Point(123, 9);
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.Size = new System.Drawing.Size(100, 20);
            this.txtDeviceName.TabIndex = 44;
            this.txtDeviceName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDeviceName.TextChanged += new System.EventHandler(this.txtDeviceName_TextChanged);
            // 
            // cbAllDevices
            // 
            this.cbAllDevices.AutoSize = true;
            this.cbAllDevices.Location = new System.Drawing.Point(14, 268);
            this.cbAllDevices.Name = "cbAllDevices";
            this.cbAllDevices.Size = new System.Drawing.Size(45, 17);
            this.cbAllDevices.TabIndex = 41;
            this.cbAllDevices.Text = "Все";
            this.cbAllDevices.UseVisualStyleBackColor = true;
            this.cbAllDevices.CheckedChanged += new System.EventHandler(this.cbAllDevices_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(80, 268);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 40;
            this.label8.Text = "неактивные";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(14, 9);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(48, 13);
            this.label44.TabIndex = 44;
            this.label44.Text = "Объект:";
            // 
            // cmbObject
            // 
            this.cmbObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbObject.DisplayMember = "cName";
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(68, 5);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(123, 21);
            this.cmbObject.TabIndex = 43;
            this.cmbObject.ValueMember = "id";
            this.cmbObject.SelectedIndexChanged += new System.EventHandler(this.cmbObject_SelectedIndexChanged);
            // 
            // btReportArendSection
            // 
            this.btReportArendSection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btReportArendSection.Image = global::Arenda.Properties.Resources.секции;
            this.btReportArendSection.Location = new System.Drawing.Point(889, 377);
            this.btReportArendSection.Name = "btReportArendSection";
            this.btReportArendSection.Size = new System.Drawing.Size(32, 32);
            this.btReportArendSection.TabIndex = 57;
            this.toolTip1.SetToolTip(this.btReportArendSection, "Отчёт о занятости секций");
            this.btReportArendSection.UseVisualStyleBackColor = true;
            this.btReportArendSection.Visible = false;
            this.btReportArendSection.Click += new System.EventHandler(this.btReportArendSection_Click);
            // 
            // Sections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 421);
            this.ControlBox = false;
            this.Controls.Add(this.btReportArendSection);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.tbcRelations);
            this.Controls.Add(this.tbSections1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbZloor);
            this.Controls.Add(this.cbZdan);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.checSec);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btDelSec);
            this.Controls.Add(this.btEditSec);
            this.Controls.Add(this.btAddSec);
            this.Controls.Add(this.btExel);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Sections";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Справочник секции";
            this.Load += new System.EventHandler(this.Sections_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgEqVsSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSections1)).EndInit();
            this.tbcRelations.ResumeLayout(false);
            this.tabEquipment.ResumeLayout(false);
            this.tabEquipment.PerformLayout();
            this.tabDevices.ResumeLayout(false);
            this.tabDevices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInactiveDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btExel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checEq;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btDelEq;
        private System.Windows.Forms.Button btEditEq;
        private System.Windows.Forms.Button btAddEq;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox checSec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btDelSec;
        private System.Windows.Forms.Button btEditSec;
        private System.Windows.Forms.Button btAddSec;
        private System.Windows.Forms.DataGridView dgEqVsSec;
        private System.Windows.Forms.BindingSource bds;
        private System.Windows.Forms.BindingSource bds2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Equipment;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEqVsSec;
        private System.Windows.Forms.DataGridViewTextBoxColumn isActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col;
        private System.Windows.Forms.ComboBox cbZdan;
        private System.Windows.Forms.ComboBox cbZloor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbFEq;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView tbSections1;
        private System.Windows.Forms.TabControl tbcRelations;
        private System.Windows.Forms.TabPage tabEquipment;
        private System.Windows.Forms.TabPage tabDevices;
        private System.Windows.Forms.PictureBox pbInactiveDevices;
        private System.Windows.Forms.DataGridView dgvDevices;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_device;
        private System.Windows.Forms.DataGridViewTextBoxColumn device_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn device_quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn device_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn device_active;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDeviceName;
        private System.Windows.Forms.CheckBox cbAllDevices;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAddDeviceToSection;
        private System.Windows.Forms.Button btnEditDevice;
        private System.Windows.Forms.Button btnDeleteDeviceFromSection;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn sSec;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIdObj;
        private System.Windows.Forms.DataGridViewTextBoxColumn cObj;
        private System.Windows.Forms.DataGridViewTextBoxColumn sBuild;
        private System.Windows.Forms.DataGridViewTextBoxColumn sFloo;
        private System.Windows.Forms.DataGridViewTextBoxColumn sid;
        private System.Windows.Forms.DataGridViewTextBoxColumn sisActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn sTelephone_lines;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLamps;
        private System.Windows.Forms.DataGridViewTextBoxColumn sPhone_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn sTotal_Area;
        private System.Windows.Forms.DataGridViewTextBoxColumn sArea_of_Trading_Hall;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isAPPZ;
        private System.Windows.Forms.Button btReportArendSection;
    }
}