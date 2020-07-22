namespace Arenda
{
    partial class test
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(test));
            this.BIK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btChoose = new System.Windows.Forms.Button();
            this.btExel = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btEdit = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorrespondentAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbBanks = new System.Windows.Forms.CheckBox();
            this.isActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgtest = new System.Windows.Forms.DataGridView();
            this.sBanks = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.bds1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgtest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // BIK
            // 
            this.BIK.DataPropertyName = "BIC";
            this.BIK.HeaderText = "БИК";
            this.BIK.MaxInputLength = 9;
            this.BIK.Name = "BIK";
            this.BIK.ReadOnly = true;
            // 
            // btChoose
            // 
            this.btChoose.Location = new System.Drawing.Point(332, 336);
            this.btChoose.Name = "btChoose";
            this.btChoose.Size = new System.Drawing.Size(32, 32);
            this.btChoose.TabIndex = 42;
            this.btChoose.Text = "V";
            this.toolTip1.SetToolTip(this.btChoose, "Выбрать банк");
            this.btChoose.UseVisualStyleBackColor = true;
            // 
            // btExel
            // 
            this.btExel.Image = global::Arenda.Properties.Resources.pict_excel;
            this.btExel.Location = new System.Drawing.Point(370, 336);
            this.btExel.Name = "btExel";
            this.btExel.Size = new System.Drawing.Size(32, 32);
            this.btExel.TabIndex = 44;
            this.toolTip1.SetToolTip(this.btExel, "Выгрузить в Excel");
            this.btExel.UseVisualStyleBackColor = true;
            // 
            // btExit
            // 
            this.btExit.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btExit.Location = new System.Drawing.Point(429, 336);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 43;
            this.toolTip1.SetToolTip(this.btExit, "Выйти");
            this.btExit.UseVisualStyleBackColor = true;
            // 
            // btEdit
            // 
            this.btEdit.Image = global::Arenda.Properties.Resources.DeleteHS;
            this.btEdit.Location = new System.Drawing.Point(262, 336);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(32, 32);
            this.btEdit.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btEdit, "Изменить признак");
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btAdd
            // 
            this.btAdd.Image = global::Arenda.Properties.Resources.DocumentHS;
            this.btAdd.Location = new System.Drawing.Point(224, 336);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 40;
            this.toolTip1.SetToolTip(this.btAdd, "Добавить банк");
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
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
            // cbBanks
            // 
            this.cbBanks.AutoSize = true;
            this.cbBanks.Location = new System.Drawing.Point(12, 336);
            this.cbBanks.Name = "cbBanks";
            this.cbBanks.Size = new System.Drawing.Size(98, 17);
            this.cbBanks.TabIndex = 45;
            this.cbBanks.Text = "Действующие";
            this.cbBanks.UseVisualStyleBackColor = true;
            // 
            // isActive
            // 
            this.isActive.DataPropertyName = "isActive";
            this.isActive.HeaderText = "isActive";
            this.isActive.Name = "isActive";
            this.isActive.ReadOnly = true;
            this.isActive.Visible = false;
            // 
            // dgtest
            // 
            this.dgtest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgtest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgtest.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.cName,
            this.CorrespondentAccount,
            this.BIK,
            this.isActive});
            this.dgtest.Location = new System.Drawing.Point(12, 33);
            this.dgtest.MultiSelect = false;
            this.dgtest.Name = "dgtest";
            this.dgtest.RowHeadersVisible = false;
            this.dgtest.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgtest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgtest.Size = new System.Drawing.Size(127, 284);
            this.dgtest.TabIndex = 39;
            // 
            // sBanks
            // 
            this.sBanks.Location = new System.Drawing.Point(12, 7);
            this.sBanks.Name = "sBanks";
            this.sBanks.Size = new System.Drawing.Size(445, 20);
            this.sBanks.TabIndex = 38;
            this.sBanks.TextChanged += new System.EventHandler(this.sBanks_TextChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.pictureBox2.Location = new System.Drawing.Point(117, 336);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(12, 12);
            this.pictureBox2.TabIndex = 46;
            this.pictureBox2.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(159, 33);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(436, 284);
            this.dataGridView1.TabIndex = 47;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 453);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cbBanks);
            this.Controls.Add(this.btExel);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btChoose);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.dgtest);
            this.Controls.Add(this.sBanks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "test";
            this.ShowInTaskbar = false;
            this.Text = "test";
            this.Load += new System.EventHandler(this.test_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgtest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn BIK;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btExel;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btChoose;
        private System.Windows.Forms.Button btEdit;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.BindingSource bds1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorrespondentAccount;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox cbBanks;
        private System.Windows.Forms.DataGridViewTextBoxColumn isActive;
        private System.Windows.Forms.DataGridView dgtest;
        private System.Windows.Forms.TextBox sBanks;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}