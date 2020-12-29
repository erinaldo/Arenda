namespace DllLink1CForAgreements
{
    partial class frmSelectAgreementsTo1C
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTypeDoc = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cLandLord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameTenant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAgreements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypeContract = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNameBuild = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFloor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbPlace = new System.Windows.Forms.TextBox();
            this.tbAgreements = new System.Windows.Forms.TextBox();
            this.tbTenant = new System.Windows.Forms.TextBox();
            this.cmbLandLord = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(102, 12);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(230, 21);
            this.cmbObject.TabIndex = 9;
            this.cmbObject.SelectionChangeCommitted += new System.EventHandler(this.cmbObject_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Объект аренды";
            // 
            // cmbTypeDoc
            // 
            this.cmbTypeDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeDoc.FormattingEnabled = true;
            this.cmbTypeDoc.Location = new System.Drawing.Point(433, 12);
            this.cmbTypeDoc.Name = "cmbTypeDoc";
            this.cmbTypeDoc.Size = new System.Drawing.Size(230, 21);
            this.cmbTypeDoc.TabIndex = 30;
            this.cmbTypeDoc.SelectionChangeCommitted += new System.EventHandler(this.cmbLandLord_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(341, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Тип договора";
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cObject,
            this.cLandLord,
            this.nameTenant,
            this.cAgreements,
            this.cTypeContract,
            this.cPlace,
            this.cNameBuild,
            this.cFloor,
            this.cSection});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Location = new System.Drawing.Point(12, 98);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(912, 423);
            this.dgvData.TabIndex = 32;
            this.dgvData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellDoubleClick);
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            // 
            // cObject
            // 
            this.cObject.DataPropertyName = "nameObjectLease";
            this.cObject.HeaderText = "Объект";
            this.cObject.Name = "cObject";
            this.cObject.ReadOnly = true;
            // 
            // cLandLord
            // 
            this.cLandLord.DataPropertyName = "nameLandLord";
            this.cLandLord.HeaderText = "Арендодатель";
            this.cLandLord.Name = "cLandLord";
            this.cLandLord.ReadOnly = true;
            // 
            // nameTenant
            // 
            this.nameTenant.DataPropertyName = "nameTenant";
            this.nameTenant.HeaderText = "Арендатор";
            this.nameTenant.Name = "nameTenant";
            this.nameTenant.ReadOnly = true;
            // 
            // cAgreements
            // 
            this.cAgreements.DataPropertyName = "Agreement";
            this.cAgreements.HeaderText = "№ договора";
            this.cAgreements.Name = "cAgreements";
            this.cAgreements.ReadOnly = true;
            // 
            // cTypeContract
            // 
            this.cTypeContract.DataPropertyName = "TypeContract";
            this.cTypeContract.HeaderText = "Тип договора";
            this.cTypeContract.Name = "cTypeContract";
            this.cTypeContract.ReadOnly = true;
            // 
            // cPlace
            // 
            this.cPlace.DataPropertyName = "namePlace";
            this.cPlace.HeaderText = "Местоположение";
            this.cPlace.Name = "cPlace";
            this.cPlace.ReadOnly = true;
            // 
            // cNameBuild
            // 
            this.cNameBuild.DataPropertyName = "buildName";
            this.cNameBuild.HeaderText = "Здание";
            this.cNameBuild.Name = "cNameBuild";
            this.cNameBuild.ReadOnly = true;
            this.cNameBuild.Visible = false;
            // 
            // cFloor
            // 
            this.cFloor.DataPropertyName = "floorName";
            this.cFloor.HeaderText = "Этаж";
            this.cFloor.Name = "cFloor";
            this.cFloor.ReadOnly = true;
            this.cFloor.Visible = false;
            // 
            // cSection
            // 
            this.cSection.DataPropertyName = "sectionName";
            this.cSection.HeaderText = "Секция";
            this.cSection.Name = "cSection";
            this.cSection.ReadOnly = true;
            this.cSection.Visible = false;
            // 
            // tbPlace
            // 
            this.tbPlace.Location = new System.Drawing.Point(222, 72);
            this.tbPlace.MaxLength = 250;
            this.tbPlace.Name = "tbPlace";
            this.tbPlace.Size = new System.Drawing.Size(100, 20);
            this.tbPlace.TabIndex = 35;
            this.tbPlace.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbAgreements
            // 
            this.tbAgreements.Location = new System.Drawing.Point(116, 72);
            this.tbAgreements.MaxLength = 250;
            this.tbAgreements.Name = "tbAgreements";
            this.tbAgreements.Size = new System.Drawing.Size(100, 20);
            this.tbAgreements.TabIndex = 36;
            this.tbAgreements.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbTenant
            // 
            this.tbTenant.Location = new System.Drawing.Point(10, 72);
            this.tbTenant.MaxLength = 250;
            this.tbTenant.Name = "tbTenant";
            this.tbTenant.Size = new System.Drawing.Size(100, 20);
            this.tbTenant.TabIndex = 37;
            this.tbTenant.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // cmbLandLord
            // 
            this.cmbLandLord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLandLord.FormattingEnabled = true;
            this.cmbLandLord.Location = new System.Drawing.Point(102, 39);
            this.cmbLandLord.Name = "cmbLandLord";
            this.cmbLandLord.Size = new System.Drawing.Size(230, 21);
            this.cmbLandLord.TabIndex = 42;
            this.cmbLandLord.SelectionChangeCommitted += new System.EventHandler(this.cmbLandLord_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Арендодатель";
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::DllLink1CForAgreements.Properties.Resources.save_edit;
            this.btSave.Location = new System.Drawing.Point(854, 532);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 33;
            this.toolTip1.SetToolTip(this.btSave, "Сохранить");
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Image = global::DllLink1CForAgreements.Properties.Resources.exit_8633;
            this.btExit.Location = new System.Drawing.Point(892, 532);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 34;
            this.toolTip1.SetToolTip(this.btExit, "Выход");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Image = global::DllLink1CForAgreements.Properties.Resources.reload_8055;
            this.btUpdate.Location = new System.Drawing.Point(876, 12);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(48, 48);
            this.btUpdate.TabIndex = 31;
            this.toolTip1.SetToolTip(this.btUpdate, "Обновить");
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // frmSelectAgreementsTo1C
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 576);
            this.ControlBox = false;
            this.Controls.Add(this.cmbLandLord);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPlace);
            this.Controls.Add(this.tbAgreements);
            this.Controls.Add(this.tbTenant);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.cmbTypeDoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectAgreementsTo1C";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор договора";
            this.Load += new System.EventHandler(this.frmSelectAgreementsTo1C_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTypeDoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.TextBox tbPlace;
        private System.Windows.Forms.TextBox tbAgreements;
        private System.Windows.Forms.TextBox tbTenant;
        private System.Windows.Forms.ComboBox cmbLandLord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLandLord;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameTenant;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAgreements;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypeContract;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNameBuild;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFloor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSection;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

