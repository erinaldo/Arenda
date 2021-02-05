namespace dllPlanReport
{
    partial class frmMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.cmbBuilding = new System.Windows.Forms.ComboBox();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.cmbFloor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gbSection = new System.Windows.Forms.GroupBox();
            this.btnSection = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbTenant = new System.Windows.Forms.TextBox();
            this.btnTenant = new System.Windows.Forms.Button();
            this.btnUpdatePlan = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.gbSection.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(57, 23);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(121, 21);
            this.cmbObject.TabIndex = 0;
            this.cmbObject.SelectionChangeCommitted += new System.EventHandler(this.filter_section);
            // 
            // cmbBuilding
            // 
            this.cmbBuilding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuilding.FormattingEnabled = true;
            this.cmbBuilding.Location = new System.Drawing.Point(57, 54);
            this.cmbBuilding.Name = "cmbBuilding";
            this.cmbBuilding.Size = new System.Drawing.Size(202, 21);
            this.cmbBuilding.TabIndex = 1;
            this.cmbBuilding.SelectionChangeCommitted += new System.EventHandler(this.filter_section);
            // 
            // cmbSection
            // 
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(58, 120);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(201, 21);
            this.cmbSection.TabIndex = 4;
            // 
            // cmbFloor
            // 
            this.cmbFloor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFloor.FormattingEnabled = true;
            this.cmbFloor.Location = new System.Drawing.Point(57, 86);
            this.cmbFloor.Name = "cmbFloor";
            this.cmbFloor.Size = new System.Drawing.Size(202, 21);
            this.cmbFloor.TabIndex = 5;
            this.cmbFloor.SelectionChangeCommitted += new System.EventHandler(this.filter_section);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Объект";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Этаж";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Арендатор";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Здание";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Секция";
            // 
            // gbSection
            // 
            this.gbSection.Controls.Add(this.label1);
            this.gbSection.Controls.Add(this.label3);
            this.gbSection.Controls.Add(this.btnSection);
            this.gbSection.Controls.Add(this.label6);
            this.gbSection.Controls.Add(this.label7);
            this.gbSection.Controls.Add(this.cmbFloor);
            this.gbSection.Controls.Add(this.cmbObject);
            this.gbSection.Controls.Add(this.cmbSection);
            this.gbSection.Controls.Add(this.cmbBuilding);
            this.gbSection.Location = new System.Drawing.Point(13, 5);
            this.gbSection.Name = "gbSection";
            this.gbSection.Size = new System.Drawing.Size(268, 196);
            this.gbSection.TabIndex = 15;
            this.gbSection.TabStop = false;
            this.gbSection.Text = "По секции";
            // 
            // btnSection
            // 
            this.btnSection.Image = global::dllPlanReport.Properties.Resources.excel;
            this.btnSection.Location = new System.Drawing.Point(227, 158);
            this.btnSection.Name = "btnSection";
            this.btnSection.Size = new System.Drawing.Size(32, 32);
            this.btnSection.TabIndex = 13;
            this.btnSection.UseVisualStyleBackColor = true;
            this.btnSection.Click += new System.EventHandler(this.btnSection_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDel);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.tbTenant);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnTenant);
            this.groupBox1.Location = new System.Drawing.Point(287, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 90);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "По арендатору";
            // 
            // btnDel
            // 
            this.btnDel.Image = global::dllPlanReport.Properties.Resources.clear_filter1;
            this.btnDel.Location = new System.Drawing.Point(339, 20);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(28, 25);
            this.btnDel.TabIndex = 17;
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::dllPlanReport.Properties.Resources.pict_filter;
            this.btnAdd.Location = new System.Drawing.Point(308, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(28, 25);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbTenant
            // 
            this.tbTenant.Location = new System.Drawing.Point(74, 23);
            this.tbTenant.Name = "tbTenant";
            this.tbTenant.ReadOnly = true;
            this.tbTenant.Size = new System.Drawing.Size(233, 20);
            this.tbTenant.TabIndex = 15;
            // 
            // btnTenant
            // 
            this.btnTenant.Image = global::dllPlanReport.Properties.Resources.excel;
            this.btnTenant.Location = new System.Drawing.Point(329, 51);
            this.btnTenant.Name = "btnTenant";
            this.btnTenant.Size = new System.Drawing.Size(32, 32);
            this.btnTenant.TabIndex = 14;
            this.btnTenant.UseVisualStyleBackColor = true;
            this.btnTenant.Click += new System.EventHandler(this.btnTenant_Click);
            // 
            // btnUpdatePlan
            // 
            this.btnUpdatePlan.Image = global::dllPlanReport.Properties.Resources.update;
            this.btnUpdatePlan.Location = new System.Drawing.Point(574, 169);
            this.btnUpdatePlan.Name = "btnUpdatePlan";
            this.btnUpdatePlan.Size = new System.Drawing.Size(32, 32);
            this.btnUpdatePlan.TabIndex = 17;
            this.btnUpdatePlan.UseVisualStyleBackColor = true;
            this.btnUpdatePlan.Click += new System.EventHandler(this.btnUpdatePlan_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = global::dllPlanReport.Properties.Resources.exit;
            this.btnExit.Location = new System.Drawing.Point(626, 169);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(32, 32);
            this.btnExit.TabIndex = 14;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 214);
            this.Controls.Add(this.btnUpdatePlan);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbSection);
            this.Name = "frmMain";
            this.Text = "Отчеты по договорам";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbSection.ResumeLayout(false);
            this.gbSection.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.ComboBox cmbBuilding;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.ComboBox cmbFloor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSection;
        private System.Windows.Forms.Button btnTenant;
        private System.Windows.Forms.GroupBox gbSection;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox tbTenant;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnUpdatePlan;
    }
}

