using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RCon
{
  public partial class FormServerDialog : Form
  {
    public string Hostname { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }

    public FormServerDialog()
    {
      InitializeComponent();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Escape) {
        buttonCancel.PerformClick();
      } else if (keyData == Keys.Enter) {
        buttonOK.PerformClick();
      }

      return base.ProcessCmdKey(ref msg, keyData);
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      Hostname = textHost.Text;
      Port = (int)numPort.Value;
      Password = textPassword.Text;

      DialogResult = DialogResult.OK;
      Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }
  }
}
