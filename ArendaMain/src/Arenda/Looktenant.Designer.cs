namespace Arenda
{
    partial class Looktenant
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
            this.dglookten = new System.Windows.Forms.DataGridView();
            this.btExit = new System.Windows.Forms.Button();
            this.btChoose = new System.Windows.Forms.Button();
            this.bds = new System.Windows.Forms.BindingSource(this.components);
            this.tbINN = new System.Windows.Forms.TextBox();
            this.tbTenant = new System.Windows.Forms.TextBox();
            this.tbFIO = new System.Windows.Forms.TextBox();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aren = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.midname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address_trade_premises = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_ObjectLease = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_TenantParent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_TenantChild = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CadastralNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dglookten)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).BeginInit();
            this.SuspendLayout();
            // 
            // dglookten
            // 
            this.dglookten.AllowUserToAddRows = false;
            this.dglookten.AllowUserToDeleteRows = false;
            this.dglookten.AllowUserToOrderColumns = true;
            this.dglookten.AllowUserToResizeRows = false;
            this.dglookten.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dglookten.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dglookten.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.inn,
            this.aren,
            this.fam,
            this.name,
            this.midname,
            this.Address_trade_premises,
            this.id_ObjectLease,
            this.id_TenantParent,
            this.id_TenantChild,
            this.CadastralNumber});
            this.dglookten.Location = new System.Drawing.Point(12, 38);
            this.dglookten.MultiSelect = false;
            this.dglookten.Name = "dglookten";
            this.dglookten.ReadOnly = true;
            this.dglookten.RowHeadersVisible = false;
            this.dglookten.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dglookten.Size = new System.Drawing.Size(515, 231);
            this.dglookten.TabIndex = 0;
            this.dglookten.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dglookten_CellDoubleClick);
            this.dglookten.Paint += new System.Windows.Forms.PaintEventHandler(this.dglookten_Paint);
            this.dglookten.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dglookten_KeyDown);
            // 
            // btExit
            // 
            this.btExit.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btExit.Location = new System.Drawing.Point(495, 275);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 35;
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btChoose
            // 
            this.btChoose.Image = global::Arenda.Properties.Resources.pict_ok;
            this.btChoose.Location = new System.Drawing.Point(447, 275);
            this.btChoose.Name = "btChoose";
            this.btChoose.Size = new System.Drawing.Size(32, 32);
            this.btChoose.TabIndex = 34;
            this.btChoose.UseVisualStyleBackColor = true;
            this.btChoose.Click += new System.EventHandler(this.btChoose_Click);
            // 
            // tbINN
            // 
            this.tbINN.Location = new System.Drawing.Point(12, 12);
            this.tbINN.Name = "tbINN";
            this.tbINN.Size = new System.Drawing.Size(131, 20);
            this.tbINN.TabIndex = 36;
            this.tbINN.TextChanged += new System.EventHandler(this.tbINN_TextChanged);
            this.tbINN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            // 
            // tbTenant
            // 
            this.tbTenant.Location = new System.Drawing.Point(174, 12);
            this.tbTenant.Name = "tbTenant";
            this.tbTenant.Size = new System.Drawing.Size(131, 20);
            this.tbTenant.TabIndex = 37;
            this.tbTenant.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            this.tbTenant.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress2);
            // 
            // tbFIO
            // 
            this.tbFIO.Location = new System.Drawing.Point(348, 12);
            this.tbFIO.Name = "tbFIO";
            this.tbFIO.Size = new System.Drawing.Size(131, 20);
            this.tbFIO.TabIndex = 38;
            this.tbFIO.TextChanged += new System.EventHandler(this.tbFIO_TextChanged);
            this.tbFIO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress2);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // inn
            // 
            this.inn.DataPropertyName = "INN";
            this.inn.HeaderText = "ИНН";
            this.inn.Name = "inn";
            this.inn.ReadOnly = true;
            // 
            // aren
            // 
            this.aren.DataPropertyName = "aren";
            this.aren.HeaderText = "Арендодатель";
            this.aren.Name = "aren";
            this.aren.ReadOnly = true;
            // 
            // fam
            // 
            this.fam.DataPropertyName = "Contact_Lastname";
            this.fam.HeaderText = "ФИО";
            this.fam.Name = "fam";
            this.fam.ReadOnly = true;
            // 
            // name
            // 
            this.name.DataPropertyName = "Contact_Firstname";
            this.name.HeaderText = "Имя";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Visible = false;
            // 
            // midname
            // 
            this.midname.DataPropertyName = "Contact_Middlename";
            this.midname.HeaderText = "Отчество";
            this.midname.Name = "midname";
            this.midname.ReadOnly = true;
            this.midname.Visible = false;
            // 
            // Address_trade_premises
            // 
            this.Address_trade_premises.DataPropertyName = "Address_trade_premises";
            this.Address_trade_premises.HeaderText = "Адрес сдав.-го помещ.";
            this.Address_trade_premises.Name = "Address_trade_premises";
            this.Address_trade_premises.ReadOnly = true;
            // 
            // id_ObjectLease
            // 
            this.id_ObjectLease.DataPropertyName = "id_ObjectLease";
            this.id_ObjectLease.HeaderText = "id_ObjectLease";
            this.id_ObjectLease.Name = "id_ObjectLease";
            this.id_ObjectLease.ReadOnly = true;
            this.id_ObjectLease.Visible = false;
            // 
            // id_TenantParent
            // 
            this.id_TenantParent.DataPropertyName = "id_TenantParent";
            this.id_TenantParent.HeaderText = "id_TenantParent";
            this.id_TenantParent.Name = "id_TenantParent";
            this.id_TenantParent.ReadOnly = true;
            this.id_TenantParent.Visible = false;
            // 
            // id_TenantChild
            // 
            this.id_TenantChild.DataPropertyName = "id_TenantChild";
            this.id_TenantChild.HeaderText = "id_TenantChild";
            this.id_TenantChild.Name = "id_TenantChild";
            this.id_TenantChild.ReadOnly = true;
            this.id_TenantChild.Visible = false;
            // 
            // CadastralNumber
            // 
            this.CadastralNumber.DataPropertyName = "CadastralNumber";
            this.CadastralNumber.HeaderText = "CadastralNumber";
            this.CadastralNumber.Name = "CadastralNumber";
            this.CadastralNumber.ReadOnly = true;
            this.CadastralNumber.Visible = false;
            // 
            // Looktenant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 314);
            this.ControlBox = false;
            this.Controls.Add(this.tbFIO);
            this.Controls.Add(this.tbTenant);
            this.Controls.Add(this.tbINN);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btChoose);
            this.Controls.Add(this.dglookten);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Looktenant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список арендаторов";
            this.Load += new System.EventHandler(this.Looktenant_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dglookten)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dglookten;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btChoose;
        private System.Windows.Forms.BindingSource bds;
        private System.Windows.Forms.TextBox tbINN;
        private System.Windows.Forms.TextBox tbTenant;
        private System.Windows.Forms.TextBox tbFIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn inn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aren;
        private System.Windows.Forms.DataGridViewTextBoxColumn fam;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn midname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address_trade_premises;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_ObjectLease;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_TenantParent;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_TenantChild;
        private System.Windows.Forms.DataGridViewTextBoxColumn CadastralNumber;
    }
}