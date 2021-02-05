namespace ArendaViewSection
{
    partial class frmView
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chbFree = new System.Windows.Forms.CheckBox();
            this.chbBusy = new System.Windows.Forms.CheckBox();
            this.chbClearing = new System.Windows.Forms.CheckBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBuilding = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFloor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBeginArenda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cEndArenda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDocNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbBuilding = new System.Windows.Forms.ComboBox();
            this.cmbFloor = new System.Windows.Forms.ComboBox();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.tbSection = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Здание:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(389, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Этаж:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Объект:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Поиск по секции:";
            // 
            // chbFree
            // 
            this.chbFree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbFree.AutoSize = true;
            this.chbFree.Checked = true;
            this.chbFree.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbFree.Location = new System.Drawing.Point(39, 382);
            this.chbFree.Name = "chbFree";
            this.chbFree.Size = new System.Drawing.Size(83, 17);
            this.chbFree.TabIndex = 4;
            this.chbFree.Text = "Свободные";
            this.chbFree.UseVisualStyleBackColor = true;
            this.chbFree.CheckedChanged += new System.EventHandler(this.setFilters);
            // 
            // chbBusy
            // 
            this.chbBusy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbBusy.AutoSize = true;
            this.chbBusy.Checked = true;
            this.chbBusy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbBusy.Location = new System.Drawing.Point(38, 405);
            this.chbBusy.Name = "chbBusy";
            this.chbBusy.Size = new System.Drawing.Size(70, 17);
            this.chbBusy.TabIndex = 5;
            this.chbBusy.Text = "Занятые";
            this.chbBusy.UseVisualStyleBackColor = true;
            this.chbBusy.CheckedChanged += new System.EventHandler(this.setFilters);
            // 
            // chbClearing
            // 
            this.chbClearing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbClearing.AutoSize = true;
            this.chbClearing.Checked = true;
            this.chbClearing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbClearing.Location = new System.Drawing.Point(39, 428);
            this.chbClearing.Name = "chbClearing";
            this.chbClearing.Size = new System.Drawing.Size(125, 17);
            this.chbClearing.TabIndex = 6;
            this.chbClearing.Text = "Освобождающиеся";
            this.chbClearing.UseVisualStyleBackColor = true;
            this.chbClearing.CheckedChanged += new System.EventHandler(this.setFilters);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cObject,
            this.cBuilding,
            this.cFloor,
            this.cSection,
            this.cArea,
            this.cBeginArenda,
            this.cEndArenda,
            this.cDocNum,
            this.cPerson});
            this.dgvData.Location = new System.Drawing.Point(12, 70);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(874, 306);
            this.dgvData.TabIndex = 9;
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            // 
            // cObject
            // 
            this.cObject.DataPropertyName = "nameObject";
            this.cObject.HeaderText = "Объект";
            this.cObject.Name = "cObject";
            this.cObject.ReadOnly = true;
            // 
            // cBuilding
            // 
            this.cBuilding.DataPropertyName = "nameBuilding";
            this.cBuilding.HeaderText = "Здание";
            this.cBuilding.Name = "cBuilding";
            this.cBuilding.ReadOnly = true;
            // 
            // cFloor
            // 
            this.cFloor.DataPropertyName = "nameFloor";
            this.cFloor.HeaderText = "Этаж";
            this.cFloor.Name = "cFloor";
            this.cFloor.ReadOnly = true;
            // 
            // cSection
            // 
            this.cSection.DataPropertyName = "nameSection";
            this.cSection.HeaderText = "Секция";
            this.cSection.Name = "cSection";
            this.cSection.ReadOnly = true;
            // 
            // cArea
            // 
            this.cArea.DataPropertyName = "agrTotalArea";
            this.cArea.HeaderText = "Площадь секции";
            this.cArea.Name = "cArea";
            this.cArea.ReadOnly = true;
            // 
            // cBeginArenda
            // 
            this.cBeginArenda.DataPropertyName = "StartDate";
            this.cBeginArenda.HeaderText = "Начало аренды";
            this.cBeginArenda.Name = "cBeginArenda";
            this.cBeginArenda.ReadOnly = true;
            // 
            // cEndArenda
            // 
            this.cEndArenda.DataPropertyName = "EndDate";
            this.cEndArenda.HeaderText = "Конец аренды";
            this.cEndArenda.Name = "cEndArenda";
            this.cEndArenda.ReadOnly = true;
            // 
            // cDocNum
            // 
            this.cDocNum.DataPropertyName = "numDoc";
            this.cDocNum.HeaderText = "Номер договора";
            this.cDocNum.Name = "cDocNum";
            this.cDocNum.ReadOnly = true;
            // 
            // cPerson
            // 
            this.cPerson.DataPropertyName = "nameArenda";
            this.cPerson.HeaderText = "Арендатор";
            this.cPerson.Name = "cPerson";
            this.cPerson.ReadOnly = true;
            // 
            // cmbBuilding
            // 
            this.cmbBuilding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuilding.FormattingEnabled = true;
            this.cmbBuilding.Location = new System.Drawing.Point(248, 10);
            this.cmbBuilding.Name = "cmbBuilding";
            this.cmbBuilding.Size = new System.Drawing.Size(135, 21);
            this.cmbBuilding.TabIndex = 10;
            this.cmbBuilding.SelectionChangeCommitted += new System.EventHandler(this.setFilters);
            // 
            // cmbFloor
            // 
            this.cmbFloor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFloor.FormattingEnabled = true;
            this.cmbFloor.Location = new System.Drawing.Point(422, 10);
            this.cmbFloor.Name = "cmbFloor";
            this.cmbFloor.Size = new System.Drawing.Size(135, 21);
            this.cmbFloor.TabIndex = 11;
            this.cmbFloor.SelectionChangeCommitted += new System.EventHandler(this.setFilters);
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(64, 10);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(135, 21);
            this.cmbObject.TabIndex = 12;
            this.cmbObject.SelectionChangeCommitted += new System.EventHandler(this.setFilters);
            // 
            // tbSection
            // 
            this.tbSection.Location = new System.Drawing.Point(115, 37);
            this.tbSection.Name = "tbSection";
            this.tbSection.Size = new System.Drawing.Size(116, 20);
            this.tbSection.TabIndex = 13;
            this.tbSection.TextChanged += new System.EventHandler(this.setFilters);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panel1.Location = new System.Drawing.Point(16, 405);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(16, 16);
            this.panel1.TabIndex = 14;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.panel2.Location = new System.Drawing.Point(16, 427);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(16, 16);
            this.panel2.TabIndex = 15;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Image = global::ArendaViewSection.Properties.Resources.refresh_178141;
            this.btnUpdate.Location = new System.Drawing.Point(852, 22);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(34, 33);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Image = global::ArendaViewSection.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(854, 447);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(32, 32);
            this.btnExit.TabIndex = 8;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Image = global::ArendaViewSection.Properties.Resources.Print;
            this.btnPrint.Location = new System.Drawing.Point(12, 447);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(32, 32);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panel3.Location = new System.Drawing.Point(183, 406);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(16, 16);
            this.panel3.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(206, 408);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Проверьте документы";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 431);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Есть будущие договора";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panel4.Location = new System.Drawing.Point(183, 429);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(16, 16);
            this.panel4.TabIndex = 18;
            // 
            // frmView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 492);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbSection);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.cmbFloor);
            this.Controls.Add(this.cmbBuilding);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.chbClearing);
            this.Controls.Add(this.chbBusy);
            this.Controls.Add(this.chbFree);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmView";
            this.Text = "Просмотр занятости секций";
            this.Load += new System.EventHandler(this.frmView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chbFree;
        private System.Windows.Forms.CheckBox chbBusy;
        private System.Windows.Forms.CheckBox chbClearing;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ComboBox cmbBuilding;
        private System.Windows.Forms.ComboBox cmbFloor;
        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.TextBox tbSection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn cObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBuilding;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFloor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSection;
        private System.Windows.Forms.DataGridViewTextBoxColumn cArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBeginArenda;
        private System.Windows.Forms.DataGridViewTextBoxColumn cEndArenda;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDocNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPerson;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
    }
}

