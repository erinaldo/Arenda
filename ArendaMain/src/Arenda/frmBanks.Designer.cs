namespace Arenda
{
  partial class frmBanks
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlInActive = new System.Windows.Forms.Panel();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.dgBanks = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorrespondentAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BIC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Used = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbCA = new System.Windows.Forms.TextBox();
            this.tbBIC = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btExel = new System.Windows.Forms.Button();
            this.btChoose = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btDel = new System.Windows.Forms.Button();
            this.btEdit = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgBanks)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 347);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "- недействующие";
            // 
            // pnlInActive
            // 
            this.pnlInActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlInActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlInActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInActive.Location = new System.Drawing.Point(29, 346);
            this.pnlInActive.Name = "pnlInActive";
            this.pnlInActive.Size = new System.Drawing.Size(15, 15);
            this.pnlInActive.TabIndex = 12;
            // 
            // cbIsActive
            // 
            this.cbIsActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Location = new System.Drawing.Point(12, 347);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(15, 14);
            this.cbIsActive.TabIndex = 11;
            this.cbIsActive.UseVisualStyleBackColor = true;
            this.cbIsActive.CheckedChanged += new System.EventHandler(this.cbIsActive_CheckedChanged);
            // 
            // dgBanks
            // 
            this.dgBanks.AllowUserToAddRows = false;
            this.dgBanks.AllowUserToDeleteRows = false;
            this.dgBanks.AllowUserToResizeRows = false;
            this.dgBanks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgBanks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgBanks.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBanks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgBanks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBanks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.cName,
            this.CorrespondentAccount,
            this.BIC,
            this.isActive,
            this.Used});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgBanks.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgBanks.Location = new System.Drawing.Point(12, 38);
            this.dgBanks.Name = "dgBanks";
            this.dgBanks.ReadOnly = true;
            this.dgBanks.RowHeadersVisible = false;
            this.dgBanks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBanks.Size = new System.Drawing.Size(451, 293);
            this.dgBanks.TabIndex = 18;
            this.dgBanks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBanks_CellClick);
            this.dgBanks.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgBanks_ColumnWidthChanged);
            this.dgBanks.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgBanks_RowPostPaint);
            this.dgBanks.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgBanks_RowPrePaint);
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
            this.cName.FillWeight = 55F;
            this.cName.HeaderText = "Наименование";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // CorrespondentAccount
            // 
            this.CorrespondentAccount.DataPropertyName = "CorrespondentAccount";
            this.CorrespondentAccount.FillWeight = 30F;
            this.CorrespondentAccount.HeaderText = "Корр. счёт";
            this.CorrespondentAccount.Name = "CorrespondentAccount";
            this.CorrespondentAccount.ReadOnly = true;
            // 
            // BIC
            // 
            this.BIC.DataPropertyName = "BIC";
            this.BIC.FillWeight = 15F;
            this.BIC.HeaderText = "БИК";
            this.BIC.Name = "BIC";
            this.BIC.ReadOnly = true;
            // 
            // isActive
            // 
            this.isActive.DataPropertyName = "isActive";
            this.isActive.HeaderText = "isActive";
            this.isActive.Name = "isActive";
            this.isActive.ReadOnly = true;
            this.isActive.Visible = false;
            // 
            // Used
            // 
            this.Used.DataPropertyName = "Used";
            this.Used.HeaderText = "Used";
            this.Used.Name = "Used";
            this.Used.ReadOnly = true;
            this.Used.Visible = false;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(12, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(170, 20);
            this.tbName.TabIndex = 19;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
            // 
            // tbCA
            // 
            this.tbCA.Location = new System.Drawing.Point(188, 12);
            this.tbCA.Name = "tbCA";
            this.tbCA.Size = new System.Drawing.Size(102, 20);
            this.tbCA.TabIndex = 20;
            this.tbCA.TextChanged += new System.EventHandler(this.tbCA_TextChanged);
            this.tbCA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCA_KeyPress);
            // 
            // tbBIC
            // 
            this.tbBIC.Location = new System.Drawing.Point(296, 12);
            this.tbBIC.Name = "tbBIC";
            this.tbBIC.Size = new System.Drawing.Size(102, 20);
            this.tbBIC.TabIndex = 21;
            this.tbBIC.TextChanged += new System.EventHandler(this.tbBIC_TextChanged);
            this.tbBIC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBIC_KeyPress);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btExel
            // 
            this.btExel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExel.Image = global::Arenda.Properties.Resources.pict_excel;
            this.btExel.Location = new System.Drawing.Point(278, 337);
            this.btExel.Name = "btExel";
            this.btExel.Size = new System.Drawing.Size(32, 32);
            this.btExel.TabIndex = 36;
            this.btExel.UseVisualStyleBackColor = true;
            this.btExel.Click += new System.EventHandler(this.btExel_Click);
            // 
            // btChoose
            // 
            this.btChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btChoose.Image = global::Arenda.Properties.Resources.pict_ok;
            this.btChoose.Location = new System.Drawing.Point(240, 337);
            this.btChoose.Name = "btChoose";
            this.btChoose.Size = new System.Drawing.Size(32, 32);
            this.btChoose.TabIndex = 35;
            this.btChoose.UseVisualStyleBackColor = true;
            this.btChoose.Click += new System.EventHandler(this.btChoose_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Image = global::Arenda.Properties.Resources.pict_close;
            this.btExit.Location = new System.Drawing.Point(431, 337);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 17;
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btDel
            // 
            this.btDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDel.Image = global::Arenda.Properties.Resources.pict_delete;
            this.btDel.Location = new System.Drawing.Point(392, 337);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(32, 32);
            this.btDel.TabIndex = 16;
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // btEdit
            // 
            this.btEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEdit.Image = global::Arenda.Properties.Resources.pict_edit;
            this.btEdit.Location = new System.Drawing.Point(354, 337);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(32, 32);
            this.btEdit.TabIndex = 15;
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Image = global::Arenda.Properties.Resources.DocumentHS;
            this.btAdd.Location = new System.Drawing.Point(316, 337);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 14;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // frmBanks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 411);
            this.ControlBox = false;
            this.Controls.Add(this.btExel);
            this.Controls.Add(this.btChoose);
            this.Controls.Add(this.tbBIC);
            this.Controls.Add(this.tbCA);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.dgBanks);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlInActive);
            this.Controls.Add(this.cbIsActive);
            this.MinimumSize = new System.Drawing.Size(491, 419);
            this.Name = "frmBanks";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Справочник банков";
            this.Load += new System.EventHandler(this.frmBanks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgBanks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btExit;
    private System.Windows.Forms.Button btDel;
    private System.Windows.Forms.Button btEdit;
    private System.Windows.Forms.Button btAdd;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel pnlInActive;
    private System.Windows.Forms.CheckBox cbIsActive;
    private System.Windows.Forms.DataGridView dgBanks;
    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.TextBox tbCA;
    private System.Windows.Forms.TextBox tbBIC;
    private System.Windows.Forms.Button btExel;
    private System.Windows.Forms.Button btChoose;
    private System.ComponentModel.BackgroundWorker backgroundWorker1;
    private System.Windows.Forms.DataGridViewTextBoxColumn id;
    private System.Windows.Forms.DataGridViewTextBoxColumn cName;
    private System.Windows.Forms.DataGridViewTextBoxColumn CorrespondentAccount;
    private System.Windows.Forms.DataGridViewTextBoxColumn BIC;
    private System.Windows.Forms.DataGridViewTextBoxColumn isActive;
    private System.Windows.Forms.DataGridViewTextBoxColumn Used;
  }
}