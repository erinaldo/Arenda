namespace Arenda
{
  partial class frmNameFile
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNameFile));
      this.tbName = new System.Windows.Forms.TextBox();
      this.btSelect = new System.Windows.Forms.Button();
      this.btClose = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // tbName
      // 
      this.tbName.Location = new System.Drawing.Point(12, 12);
      this.tbName.MaxLength = 150;
      this.tbName.Name = "tbName";
      this.tbName.Size = new System.Drawing.Size(242, 20);
      this.tbName.TabIndex = 15;
      // 
      // btSelect
      // 
      this.btSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btSelect.Image = ((System.Drawing.Image)(resources.GetObject("btSelect.Image")));
      this.btSelect.Location = new System.Drawing.Point(191, 44);
      this.btSelect.Name = "btSelect";
      this.btSelect.Size = new System.Drawing.Size(32, 32);
      this.btSelect.TabIndex = 13;
      this.btSelect.UseVisualStyleBackColor = true;
      this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
      // 
      // btClose
      // 
      this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btClose.Image = ((System.Drawing.Image)(resources.GetObject("btClose.Image")));
      this.btClose.Location = new System.Drawing.Point(229, 44);
      this.btClose.Name = "btClose";
      this.btClose.Size = new System.Drawing.Size(32, 32);
      this.btClose.TabIndex = 14;
      this.btClose.UseVisualStyleBackColor = true;
      this.btClose.Click += new System.EventHandler(this.btClose_Click);
      // 
      // frmNameFile
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(265, 80);
      this.ControlBox = false;
      this.Controls.Add(this.tbName);
      this.Controls.Add(this.btSelect);
      this.Controls.Add(this.btClose);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmNameFile";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Ввод имени файла";
      this.Load += new System.EventHandler(this.frmNameFile_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Button btSelect;
    private System.Windows.Forms.Button btClose;
  }
}