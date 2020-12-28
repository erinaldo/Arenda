namespace Arenda.AddNewDocToFolder
{
    partial class frmAddDoc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddDoc));
            this.tbNameImg = new System.Windows.Forms.TextBox();
            this.dgvScan = new System.Windows.Forms.DataGridView();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btSave = new System.Windows.Forms.Button();
            this.btSaveDoc = new System.Windows.Forms.Button();
            this.btEditName = new System.Windows.Forms.Button();
            this.btAddFile = new System.Windows.Forms.Button();
            this.btView = new System.Windows.Forms.Button();
            this.btZoomIn = new System.Windows.Forms.Button();
            this.btZoomOut = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btClose = new System.Windows.Forms.Button();
            this.btDel = new System.Windows.Forms.Button();
            this.imagePanel1 = new Arenda.ImagePanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScan)).BeginInit();
            this.SuspendLayout();
            // 
            // tbNameImg
            // 
            this.tbNameImg.Location = new System.Drawing.Point(12, 12);
            this.tbNameImg.Name = "tbNameImg";
            this.tbNameImg.Size = new System.Drawing.Size(299, 20);
            this.tbNameImg.TabIndex = 25;
            this.tbNameImg.TextChanged += new System.EventHandler(this.tbNameImg_TextChanged);
            this.tbNameImg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNameImg_KeyPress);
            // 
            // dgvScan
            // 
            this.dgvScan.AllowUserToAddRows = false;
            this.dgvScan.AllowUserToDeleteRows = false;
            this.dgvScan.AllowUserToResizeRows = false;
            this.dgvScan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvScan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvScan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cName});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvScan.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvScan.Location = new System.Drawing.Point(12, 38);
            this.dgvScan.MultiSelect = false;
            this.dgvScan.Name = "dgvScan";
            this.dgvScan.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvScan.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvScan.RowHeadersVisible = false;
            this.dgvScan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvScan.Size = new System.Drawing.Size(299, 304);
            this.dgvScan.TabIndex = 26;
            this.dgvScan.CurrentCellChanged += new System.EventHandler(this.dgvScan_CurrentCellChanged);
            // 
            // cName
            // 
            this.cName.DataPropertyName = "cName";
            this.cName.HeaderText = "Имя файла";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // btSave
            // 
            this.btSave.Image = global::Arenda.Properties.Resources.saveHS;
            this.btSave.Location = new System.Drawing.Point(687, 348);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 35;
            this.toolTip1.SetToolTip(this.btSave, "Сохранить");
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Visible = false;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btSaveDoc
            // 
            this.btSaveDoc.Image = global::Arenda.Properties.Resources.document_library;
            this.btSaveDoc.Location = new System.Drawing.Point(279, 348);
            this.btSaveDoc.Name = "btSaveDoc";
            this.btSaveDoc.Size = new System.Drawing.Size(32, 32);
            this.btSaveDoc.TabIndex = 28;
            this.toolTip1.SetToolTip(this.btSaveDoc, "Выгрузить");
            this.btSaveDoc.UseVisualStyleBackColor = true;
            this.btSaveDoc.Click += new System.EventHandler(this.btSaveDoc_Click);
            // 
            // btEditName
            // 
            this.btEditName.Image = global::Arenda.Properties.Resources.pict_edit;
            this.btEditName.Location = new System.Drawing.Point(326, 348);
            this.btEditName.Name = "btEditName";
            this.btEditName.Size = new System.Drawing.Size(32, 32);
            this.btEditName.TabIndex = 29;
            this.toolTip1.SetToolTip(this.btEditName, "Переименовать");
            this.btEditName.UseVisualStyleBackColor = true;
            this.btEditName.Click += new System.EventHandler(this.btEditName_Click);
            // 
            // btAddFile
            // 
            this.btAddFile.Image = global::Arenda.Properties.Resources.upload;
            this.btAddFile.Location = new System.Drawing.Point(364, 348);
            this.btAddFile.Name = "btAddFile";
            this.btAddFile.Size = new System.Drawing.Size(32, 32);
            this.btAddFile.TabIndex = 30;
            this.toolTip1.SetToolTip(this.btAddFile, "Добавить документ");
            this.btAddFile.UseVisualStyleBackColor = true;
            this.btAddFile.Click += new System.EventHandler(this.btAddFile_Click);
            // 
            // btView
            // 
            this.btView.Image = global::Arenda.Properties.Resources.glaz;
            this.btView.Location = new System.Drawing.Point(649, 348);
            this.btView.Name = "btView";
            this.btView.Size = new System.Drawing.Size(32, 32);
            this.btView.TabIndex = 34;
            this.toolTip1.SetToolTip(this.btView, "Просмотр");
            this.btView.UseVisualStyleBackColor = true;
            this.btView.Click += new System.EventHandler(this.btView_Click);
            // 
            // btZoomIn
            // 
            this.btZoomIn.Image = global::Arenda.Properties.Resources.zoom_in;
            this.btZoomIn.Location = new System.Drawing.Point(451, 348);
            this.btZoomIn.Name = "btZoomIn";
            this.btZoomIn.Size = new System.Drawing.Size(32, 32);
            this.btZoomIn.TabIndex = 32;
            this.toolTip1.SetToolTip(this.btZoomIn, "Приблизить");
            this.btZoomIn.UseVisualStyleBackColor = true;
            this.btZoomIn.Click += new System.EventHandler(this.btZoomIn_Click);
            // 
            // btZoomOut
            // 
            this.btZoomOut.Image = global::Arenda.Properties.Resources.zoom_out;
            this.btZoomOut.Location = new System.Drawing.Point(413, 348);
            this.btZoomOut.Name = "btZoomOut";
            this.btZoomOut.Size = new System.Drawing.Size(32, 32);
            this.btZoomOut.TabIndex = 31;
            this.toolTip1.SetToolTip(this.btZoomOut, "Отдалить");
            this.btZoomOut.UseVisualStyleBackColor = true;
            this.btZoomOut.Click += new System.EventHandler(this.btZoomOut_Click);
            // 
            // btClose
            // 
            this.btClose.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btClose.Location = new System.Drawing.Point(725, 348);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 36;
            this.toolTip1.SetToolTip(this.btClose, "Выйти");
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btDel
            // 
            this.btDel.Image = ((System.Drawing.Image)(resources.GetObject("btDel.Image")));
            this.btDel.Location = new System.Drawing.Point(12, 348);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(32, 32);
            this.btDel.TabIndex = 38;
            this.toolTip1.SetToolTip(this.btDel, "Удалить");
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click_1);
            // 
            // imagePanel1
            // 
            this.imagePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagePanel1.CanvasSize = new System.Drawing.Size(600, 400);
            this.imagePanel1.Image = null;
            this.imagePanel1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.imagePanel1.Location = new System.Drawing.Point(326, 12);
            this.imagePanel1.Name = "imagePanel1";
            this.imagePanel1.Size = new System.Drawing.Size(431, 330);
            this.imagePanel1.TabIndex = 37;
            this.imagePanel1.Zoom = 1F;
            // 
            // frmAddDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 389);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.imagePanel1);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btSaveDoc);
            this.Controls.Add(this.btEditName);
            this.Controls.Add(this.btAddFile);
            this.Controls.Add(this.btView);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btZoomIn);
            this.Controls.Add(this.btZoomOut);
            this.Controls.Add(this.tbNameImg);
            this.Controls.Add(this.dgvScan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddDoc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление документов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDocument_FormClosing);
            this.Load += new System.EventHandler(this.frmDocument_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btSaveDoc;
        private System.Windows.Forms.Button btEditName;
        private System.Windows.Forms.Button btAddFile;
        private System.Windows.Forms.Button btView;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btZoomIn;
        private System.Windows.Forms.Button btZoomOut;
        private System.Windows.Forms.TextBox tbNameImg;
        private System.Windows.Forms.DataGridView dgvScan;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private ImagePanel imagePanel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btDel;
    }
}