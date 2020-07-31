namespace Arenda
{
    partial class AdditionalDoc
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
            this.btExit = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTypeDoc = new System.Windows.Forms.ComboBox();
            this.dateadddoc = new System.Windows.Forms.DateTimePicker();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.dateren = new System.Windows.Forms.DateTimePicker();
            this.tbAreaNew = new System.Windows.Forms.TextBox();
            this.lblAreaNew = new System.Windows.Forms.Label();
            this.dtpDeparture = new System.Windows.Forms.DateTimePicker();
            this.lblDeparture = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbComment = new System.Windows.Forms.TextBox();
            this.dtpOutDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btExit.Location = new System.Drawing.Point(380, 126);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 39;
            this.toolTip1.SetToolTip(this.btExit, "Выйти");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Image = global::Arenda.Properties.Resources.saveHS;
            this.btAdd.Location = new System.Drawing.Point(342, 126);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 38;
            this.toolTip1.SetToolTip(this.btAdd, "Сохранить");
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Тип доп. документа:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Дата доп. документа:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "№:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 26);
            this.label4.TabIndex = 43;
            this.label4.Text = "Дата продления \r\nдоговора";
            // 
            // cbTypeDoc
            // 
            this.cbTypeDoc.DisplayMember = "Rus_Name";
            this.cbTypeDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeDoc.FormattingEnabled = true;
            this.cbTypeDoc.Location = new System.Drawing.Point(128, 9);
            this.cbTypeDoc.Name = "cbTypeDoc";
            this.cbTypeDoc.Size = new System.Drawing.Size(284, 21);
            this.cbTypeDoc.TabIndex = 44;
            this.cbTypeDoc.ValueMember = "id";
            this.cbTypeDoc.SelectedValueChanged += new System.EventHandler(this.cbTypeDoc_SelectedValueChanged);
            // 
            // dateadddoc
            // 
            this.dateadddoc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateadddoc.Location = new System.Drawing.Point(190, 40);
            this.dateadddoc.Name = "dateadddoc";
            this.dateadddoc.Size = new System.Drawing.Size(100, 20);
            this.dateadddoc.TabIndex = 45;
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(128, 69);
            this.tbNumber.MaxLength = 9;
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(100, 20);
            this.tbNumber.TabIndex = 46;
            this.tbNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNumber_KeyPress);
            // 
            // dateren
            // 
            this.dateren.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateren.Location = new System.Drawing.Point(128, 95);
            this.dateren.Name = "dateren";
            this.dateren.Size = new System.Drawing.Size(100, 20);
            this.dateren.TabIndex = 47;
            // 
            // tbAreaNew
            // 
            this.tbAreaNew.Location = new System.Drawing.Point(128, 131);
            this.tbAreaNew.Name = "tbAreaNew";
            this.tbAreaNew.Size = new System.Drawing.Size(100, 20);
            this.tbAreaNew.TabIndex = 48;
            this.tbAreaNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbAreaNew.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAreaNew_KeyPress);
            this.tbAreaNew.Leave += new System.EventHandler(this.tbAreaNew_Leave);
            // 
            // lblAreaNew
            // 
            this.lblAreaNew.AutoSize = true;
            this.lblAreaNew.Location = new System.Drawing.Point(12, 134);
            this.lblAreaNew.Name = "lblAreaNew";
            this.lblAreaNew.Size = new System.Drawing.Size(81, 13);
            this.lblAreaNew.TabIndex = 49;
            this.lblAreaNew.Text = "Общ. площадь";
            // 
            // dtpDeparture
            // 
            this.dtpDeparture.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeparture.Location = new System.Drawing.Point(312, 95);
            this.dtpDeparture.Name = "dtpDeparture";
            this.dtpDeparture.Size = new System.Drawing.Size(100, 20);
            this.dtpDeparture.TabIndex = 50;
            this.dtpDeparture.Visible = false;
            // 
            // lblDeparture
            // 
            this.lblDeparture.AutoSize = true;
            this.lblDeparture.Location = new System.Drawing.Point(232, 99);
            this.lblDeparture.Name = "lblDeparture";
            this.lblDeparture.Size = new System.Drawing.Size(74, 13);
            this.lblDeparture.TabIndex = 51;
            this.lblDeparture.Text = "Дата выезда";
            this.lblDeparture.Visible = false;
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(15, 115);
            this.tbComment.MaxLength = 9;
            this.tbComment.Multiline = true;
            this.tbComment.Name = "tbComment";
            this.tbComment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbComment.Size = new System.Drawing.Size(316, 43);
            this.tbComment.TabIndex = 52;
            this.tbComment.Visible = false;
            // 
            // dtpOutDate
            // 
            this.dtpOutDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOutDate.Location = new System.Drawing.Point(190, 68);
            this.dtpOutDate.Name = "dtpOutDate";
            this.dtpOutDate.Size = new System.Drawing.Size(100, 20);
            this.dtpOutDate.TabIndex = 53;
            this.dtpOutDate.Visible = false;
            // 
            // AdditionalDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 167);
            this.ControlBox = false;
            this.Controls.Add(this.dtpOutDate);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.lblDeparture);
            this.Controls.Add(this.lblAreaNew);
            this.Controls.Add(this.dateren);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.dateadddoc);
            this.Controls.Add(this.cbTypeDoc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.dtpDeparture);
            this.Controls.Add(this.tbAreaNew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdditionalDoc";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дополнительный документ";
            this.Load += new System.EventHandler(this.AdditionalDoc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTypeDoc;
        private System.Windows.Forms.DateTimePicker dateadddoc;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.DateTimePicker dateren;
        private System.Windows.Forms.TextBox tbAreaNew;
        private System.Windows.Forms.Label lblAreaNew;
        private System.Windows.Forms.DateTimePicker dtpDeparture;
        private System.Windows.Forms.Label lblDeparture;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.DateTimePicker dtpOutDate;
    }
}