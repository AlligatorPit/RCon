namespace RCon
{
  partial class FormServerDialog
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
      if (disposing && (components != null)) {
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
      this.buttonCancel = new System.Windows.Forms.Button();
      this.buttonOK = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.labelInfo = new System.Windows.Forms.Label();
      this.textHost = new System.Windows.Forms.TextBox();
      this.numPort = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.textPassword = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.Location = new System.Drawing.Point(327, 159);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 23);
      this.buttonCancel.TabIndex = 4;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // buttonOK
      // 
      this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOK.Location = new System.Drawing.Point(246, 159);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(75, 23);
      this.buttonOK.TabIndex = 3;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(78, 69);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(32, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Host:";
      // 
      // labelInfo
      // 
      this.labelInfo.Location = new System.Drawing.Point(12, 9);
      this.labelInfo.Name = "labelInfo";
      this.labelInfo.Size = new System.Drawing.Size(390, 44);
      this.labelInfo.TabIndex = 3;
      this.labelInfo.Text = "Please enter remote info. Server must have API enabled (net_bEnableAPI) and set a" +
    "n API port (net_iPortAPI) for this to work, as well as an administrator password" +
    " (net_strAdminPassword)";
      // 
      // textHost
      // 
      this.textHost.Location = new System.Drawing.Point(116, 66);
      this.textHost.Name = "textHost";
      this.textHost.Size = new System.Drawing.Size(286, 20);
      this.textHost.TabIndex = 0;
      // 
      // numPort
      // 
      this.numPort.Location = new System.Drawing.Point(116, 92);
      this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
      this.numPort.Name = "numPort";
      this.numPort.Size = new System.Drawing.Size(82, 20);
      this.numPort.TabIndex = 1;
      this.numPort.Value = new decimal(new int[] {
            27017,
            0,
            0,
            0});
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(62, 94);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(48, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "API port:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(23, 121);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(87, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Admin password:";
      // 
      // textPassword
      // 
      this.textPassword.Location = new System.Drawing.Point(116, 118);
      this.textPassword.Name = "textPassword";
      this.textPassword.Size = new System.Drawing.Size(286, 20);
      this.textPassword.TabIndex = 2;
      this.textPassword.UseSystemPasswordChar = true;
      // 
      // FormServerDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(414, 194);
      this.Controls.Add(this.numPort);
      this.Controls.Add(this.textPassword);
      this.Controls.Add(this.textHost);
      this.Controls.Add(this.labelInfo);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.buttonCancel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormServerDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Connect to server";
      ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label labelInfo;
    private System.Windows.Forms.TextBox textHost;
    private System.Windows.Forms.NumericUpDown numPort;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox textPassword;
  }
}