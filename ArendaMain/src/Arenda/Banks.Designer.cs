namespace Arenda
{
    partial class Banks
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
            this.sBanks = new System.Windows.Forms.TextBox();
            this.dgBanks = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorrespondentAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BIK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btChoose = new System.Windows.Forms.Button();
            this.cbBanks = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btExel = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btEdit = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.bds = new System.Windows.Forms.BindingSource(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgBanks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).BeginInit();
            this.SuspendLayout();
            // 
            // sBanks
            // 
            this.sBanks.Location = new System.Drawing.Point(12, 12);
            this.sBanks.Name = "sBanks";
            this.sBanks.Size = new System.Drawing.Size(445, 20);
            this.sBanks.TabIndex = 0;
            this.sBanks.TextChanged += new System.EventHandler(this.sBanks_TextChanged);
            this.sBanks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sBanks_KeyPress);
            // 
            // dgBanks
            // 
            this.dgBanks.AllowUserToAddRows = false;
            this.dgBanks.AllowUserToResizeRows = false;
            this.dgBanks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgBanks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBanks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.cName,
            this.CorrespondentAccount,
            this.BIK,
            this.isActive});
            this.dgBanks.Location = new System.Drawing.Point(12, 38);
            this.dgBanks.MultiSelect = false;
            this.dgBanks.Name = "dgBanks";
            this.dgBanks.ReadOnly = true;
            this.dgBanks.RowHeadersVisible = false;
            this.dgBanks.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgBanks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBanks.Size = new System.Drawing.Size(445, 284);
            this.dgBanks.TabIndex = 47;
            this.dgBanks.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBanks_CellDoubleClick);
            this.dgBanks.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBanks_CellEndEdit);
            this.dgBanks.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBanks_CellEnter);
            this.dgBanks.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgBanks_ColumnHeaderMouseClick);
            this.dgBanks.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgBanks_ColumnHeaderMouseDoubleClick);
            this.dgBanks.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgBanks_EditingControlShowing);
            this.dgBanks.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgBanks_RowHeaderMouseClick);
            this.dgBanks.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgBanks_RowHeaderMouseDoubleClick);
            this.dgBanks.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBanks_RowLeave);
            this.dgBanks.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgBanks_RowPrePaint);
            this.dgBanks.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgBanks_RowsAdded);
            this.dgBanks.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgBanks_RowStateChanged);
            this.dgBanks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgBanks_KeyDown);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // cName
            // 
            this.cName.DataPropertyName = "cName";
            this.cName.HeaderText = "Наименование";
            this.cName.MaxInputLength = 100;
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // CorrespondentAccount
            // 
            this.CorrespondentAccount.DataPropertyName = "CorrespondentAccount";
            this.CorrespondentAccount.HeaderText = "Корр. счет";
            this.CorrespondentAccount.MaxInputLength = 20;
            this.CorrespondentAccount.Name = "CorrespondentAccount";
            this.CorrespondentAccount.ReadOnly = true;
            // 
            // BIK
            // 
            this.BIK.DataPropertyName = "BIC";
            this.BIK.HeaderText = "БИК";
            this.BIK.MaxInputLength = 9;
            this.BIK.Name = "BIK";
            this.BIK.ReadOnly = true;
            // 
            // isActive
            // 
            this.isActive.DataPropertyName = "isActive";
            this.isActive.HeaderText = "isActive";
            this.isActive.Name = "isActive";
            this.isActive.ReadOnly = true;
            this.isActive.Visible = false;
            // 
            // btChoose
            // 
            this.btChoose.Image = global::Arenda.Properties.Resources.pict_ok;
            this.btChoose.Location = new System.Drawing.Point(332, 341);
            this.btChoose.Name = "btChoose";
            this.btChoose.Size = new System.Drawing.Size(32, 32);
            this.btChoose.TabIndex = 32;
            this.toolTip1.SetToolTip(this.btChoose, "Выбрать банк");
            this.btChoose.UseVisualStyleBackColor = true;
            this.btChoose.Click += new System.EventHandler(this.btChoose_Click);
            // 
            // cbBanks
            // 
            this.cbBanks.AutoSize = true;
            this.cbBanks.Location = new System.Drawing.Point(12, 341);
            this.cbBanks.Name = "cbBanks";
            this.cbBanks.Size = new System.Drawing.Size(98, 17);
            this.cbBanks.TabIndex = 36;
            this.cbBanks.Text = "Действующие";
            this.cbBanks.UseVisualStyleBackColor = true;
            this.cbBanks.CheckedChanged += new System.EventHandler(this.cbBanks_CheckedChanged);
            // 
            // btExel
            // 
            this.btExel.Image = global::Arenda.Properties.Resources.pict_excel;
            this.btExel.Location = new System.Drawing.Point(370, 341);
            this.btExel.Name = "btExel";
            this.btExel.Size = new System.Drawing.Size(32, 32);
            this.btExel.TabIndex = 34;
            this.toolTip1.SetToolTip(this.btExel, "Выгрузить в Excel");
            this.btExel.UseVisualStyleBackColor = true;
            this.btExel.Click += new System.EventHandler(this.btExel_Click);
            // 
            // btExit
            // 
            this.btExit.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btExit.Location = new System.Drawing.Point(429, 341);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 33;
            this.toolTip1.SetToolTip(this.btExit, "Выйти");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btEdit
            // 
            this.btEdit.Image = global::Arenda.Properties.Resources.DeleteHS;
            this.btEdit.Location = new System.Drawing.Point(262, 341);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(32, 32);
            this.btEdit.TabIndex = 31;
            this.toolTip1.SetToolTip(this.btEdit, "Изменить признак");
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btAdd
            // 
            this.btAdd.Image = global::Arenda.Properties.Resources.DocumentHS;
            this.btAdd.Location = new System.Drawing.Point(224, 341);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 30;
            this.toolTip1.SetToolTip(this.btAdd, "Добавить банк");
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.pictureBox2.Location = new System.Drawing.Point(117, 341);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(12, 12);
            this.pictureBox2.TabIndex = 37;
            this.pictureBox2.TabStop = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // Banks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 385);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cbBanks);
            this.Controls.Add(this.btExel);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btChoose);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.dgBanks);
            this.Controls.Add(this.sBanks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Banks";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Справочник банков";
            this.Load += new System.EventHandler(this.Banks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgBanks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sBanks;
        private System.Windows.Forms.DataGridView dgBanks;
        private System.Windows.Forms.Button btExel;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btChoose;
        private System.Windows.Forms.Button btEdit;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox cbBanks;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.BindingSource bds;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorrespondentAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BIK;
        private System.Windows.Forms.DataGridViewTextBoxColumn isActive;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}