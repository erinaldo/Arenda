namespace Arenda
{
    partial class frmAddPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddPayment));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btSelectJFines = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblSum = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtSum = new System.Windows.Forms.TextBox();
            this.lblRub = new System.Windows.Forms.Label();
            this.prbExcel = new System.Windows.Forms.ProgressBar();
            this.bgwToExcel = new System.ComponentModel.BackgroundWorker();
            this.cmbPayType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPlaneDate = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCard = new System.Windows.Forms.RadioButton();
            this.rbRealMoney = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbPayArend = new System.Windows.Forms.RadioButton();
            this.rbSendMoney = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbSummaPay = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbTypePay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDateCreate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMonth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSelectJFines
            // 
            this.btSelectJFines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelectJFines.Image = global::Arenda.Properties.Resources._1493746719_shopping_cart;
            this.btSelectJFines.Location = new System.Drawing.Point(258, 95);
            this.btSelectJFines.Name = "btSelectJFines";
            this.btSelectJFines.Size = new System.Drawing.Size(30, 30);
            this.btSelectJFines.TabIndex = 57;
            this.toolTip1.SetToolTip(this.btSelectJFines, "Выбор счёта");
            this.btSelectJFines.UseVisualStyleBackColor = true;
            this.btSelectJFines.Click += new System.EventHandler(this.btSelectJFines_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(246, 458);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(30, 30);
            this.btnSave.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btnSave, "Сохранить");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.Location = new System.Drawing.Point(282, 458);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(30, 30);
            this.btnQuit.TabIndex = 40;
            this.toolTip1.SetToolTip(this.btnQuit, "Выход");
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 28);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(39, 13);
            this.lblDate.TabIndex = 42;
            this.lblDate.Text = "Дата: ";
            // 
            // lblSum
            // 
            this.lblSum.AutoSize = true;
            this.lblSum.Location = new System.Drawing.Point(12, 53);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(84, 13);
            this.lblSum.TabIndex = 43;
            this.lblSum.Text = "Сумма оплаты:";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(114, 24);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(198, 20);
            this.dtpDate.TabIndex = 44;
            this.dtpDate.CloseUp += new System.EventHandler(this.dtpDate_CloseUp);
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            this.dtpDate.Leave += new System.EventHandler(this.dtpDate_Leave);
            // 
            // txtSum
            // 
            this.txtSum.Location = new System.Drawing.Point(114, 50);
            this.txtSum.Name = "txtSum";
            this.txtSum.Size = new System.Drawing.Size(166, 20);
            this.txtSum.TabIndex = 45;
            this.txtSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSum_KeyPress);
            this.txtSum.Leave += new System.EventHandler(this.txtSum_Leave);
            // 
            // lblRub
            // 
            this.lblRub.AutoSize = true;
            this.lblRub.Location = new System.Drawing.Point(289, 53);
            this.lblRub.Name = "lblRub";
            this.lblRub.Size = new System.Drawing.Size(27, 13);
            this.lblRub.TabIndex = 46;
            this.lblRub.Text = "руб.";
            // 
            // prbExcel
            // 
            this.prbExcel.Location = new System.Drawing.Point(12, 5);
            this.prbExcel.Name = "prbExcel";
            this.prbExcel.Size = new System.Drawing.Size(300, 13);
            this.prbExcel.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prbExcel.TabIndex = 51;
            this.prbExcel.Visible = false;
            // 
            // bgwToExcel
            // 
            this.bgwToExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwToExcel_DoWork);
            this.bgwToExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwToExcel_RunWorkerCompleted);
            // 
            // cmbPayType
            // 
            this.cmbPayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayType.FormattingEnabled = true;
            this.cmbPayType.Location = new System.Drawing.Point(114, 76);
            this.cmbPayType.Name = "cmbPayType";
            this.cmbPayType.Size = new System.Drawing.Size(198, 21);
            this.cmbPayType.TabIndex = 52;
            this.cmbPayType.SelectionChangeCommitted += new System.EventHandler(this.cmbPayType_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Тип оплаты:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "План месяц:";
            // 
            // cmbPlaneDate
            // 
            this.cmbPlaneDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlaneDate.FormattingEnabled = true;
            this.cmbPlaneDate.Location = new System.Drawing.Point(114, 103);
            this.cmbPlaneDate.Name = "cmbPlaneDate";
            this.cmbPlaneDate.Size = new System.Drawing.Size(198, 21);
            this.cmbPlaneDate.TabIndex = 52;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCard);
            this.groupBox1.Controls.Add(this.rbRealMoney);
            this.groupBox1.Location = new System.Drawing.Point(15, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 41);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип оплаты";
            // 
            // rbCard
            // 
            this.rbCard.AutoSize = true;
            this.rbCard.Location = new System.Drawing.Point(79, 17);
            this.rbCard.Name = "rbCard";
            this.rbCard.Size = new System.Drawing.Size(60, 17);
            this.rbCard.TabIndex = 54;
            this.rbCard.Text = "Б/Нал.";
            this.rbCard.UseVisualStyleBackColor = true;
            // 
            // rbRealMoney
            // 
            this.rbRealMoney.AutoSize = true;
            this.rbRealMoney.Checked = true;
            this.rbRealMoney.Location = new System.Drawing.Point(15, 17);
            this.rbRealMoney.Name = "rbRealMoney";
            this.rbRealMoney.Size = new System.Drawing.Size(48, 17);
            this.rbRealMoney.TabIndex = 54;
            this.rbRealMoney.TabStop = true;
            this.rbRealMoney.Text = "Нал.";
            this.rbRealMoney.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbPayArend);
            this.groupBox2.Controls.Add(this.rbSendMoney);
            this.groupBox2.Location = new System.Drawing.Point(15, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(297, 41);
            this.groupBox2.TabIndex = 55;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Вид оплаты";
            // 
            // rbPayArend
            // 
            this.rbPayArend.AutoSize = true;
            this.rbPayArend.Checked = true;
            this.rbPayArend.Location = new System.Drawing.Point(120, 17);
            this.rbPayArend.Name = "rbPayArend";
            this.rbPayArend.Size = new System.Drawing.Size(124, 17);
            this.rbPayArend.TabIndex = 54;
            this.rbPayArend.TabStop = true;
            this.rbPayArend.Text = "Оплата арендатора";
            this.rbPayArend.UseVisualStyleBackColor = true;
            // 
            // rbSendMoney
            // 
            this.rbSendMoney.AutoSize = true;
            this.rbSendMoney.Location = new System.Drawing.Point(15, 17);
            this.rbSendMoney.Name = "rbSendMoney";
            this.rbSendMoney.Size = new System.Drawing.Size(99, 17);
            this.rbSendMoney.TabIndex = 54;
            this.rbSendMoney.Text = "Возврат денег";
            this.rbSendMoney.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.btSelectJFines);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tbSummaPay);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tbTypePay);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.tbDateCreate);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.tbMonth);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(15, 319);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(297, 133);
            this.groupBox3.TabIndex = 56;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Счёт";
            this.groupBox3.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(230, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "руб.";
            // 
            // tbSummaPay
            // 
            this.tbSummaPay.Location = new System.Drawing.Point(120, 97);
            this.tbSummaPay.Name = "tbSummaPay";
            this.tbSummaPay.ReadOnly = true;
            this.tbSummaPay.Size = new System.Drawing.Size(104, 20);
            this.tbSummaPay.TabIndex = 58;
            this.tbSummaPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "Сумма";
            // 
            // tbTypePay
            // 
            this.tbTypePay.Location = new System.Drawing.Point(120, 71);
            this.tbTypePay.Name = "tbTypePay";
            this.tbTypePay.ReadOnly = true;
            this.tbTypePay.Size = new System.Drawing.Size(166, 20);
            this.tbTypePay.TabIndex = 58;
            this.tbTypePay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 57;
            this.label5.Text = "Тип платежа";
            // 
            // tbDateCreate
            // 
            this.tbDateCreate.Location = new System.Drawing.Point(120, 45);
            this.tbDateCreate.Name = "tbDateCreate";
            this.tbDateCreate.ReadOnly = true;
            this.tbDateCreate.Size = new System.Drawing.Size(166, 20);
            this.tbDateCreate.TabIndex = 58;
            this.tbDateCreate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "Дата создания";
            // 
            // tbMonth
            // 
            this.tbMonth.Location = new System.Drawing.Point(120, 19);
            this.tbMonth.Name = "tbMonth";
            this.tbMonth.ReadOnly = true;
            this.tbMonth.Size = new System.Drawing.Size(166, 20);
            this.tbMonth.TabIndex = 58;
            this.tbMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "Месяц";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 221);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 57;
            this.label8.Text = "Примечание";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(15, 237);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(297, 76);
            this.tbDescription.TabIndex = 58;
            // 
            // frmAddPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 496);
            this.ControlBox = false;
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbPlaneDate);
            this.Controls.Add(this.cmbPayType);
            this.Controls.Add(this.prbExcel);
            this.Controls.Add(this.lblRub);
            this.Controls.Add(this.txtSum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSum);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnQuit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddPayment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddPayment";
            this.Load += new System.EventHandler(this.frmAddPayment_Load);
            this.Click += new System.EventHandler(this.frmAddPayment_Click);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblSum;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtSum;
        private System.Windows.Forms.Label lblRub;
        private System.Windows.Forms.ProgressBar prbExcel;
        private System.ComponentModel.BackgroundWorker bgwToExcel;
        private System.Windows.Forms.ComboBox cmbPayType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPlaneDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCard;
        private System.Windows.Forms.RadioButton rbRealMoney;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbPayArend;
        private System.Windows.Forms.RadioButton rbSendMoney;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbSummaPay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbTypePay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDateCreate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btSelectJFines;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbDescription;
    }
}