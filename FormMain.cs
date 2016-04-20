using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RCon
{
  public partial class FormMain : Form
  {
    public SamRevAPI Conn = null;
    public List<string> History = new List<string>();
    public int HistorySwitch = 0;
    public string HistoryRemember = "";

    public FormMain()
    {
      InitializeComponent();

      if (File.Exists("RconBookmarks.txt")) {
        string[] lines = File.ReadAllLines("RconBookmarks.txt");
        foreach (string line in lines) {
          string[] parse = line.Split('\t');

          string hostname = parse[0];
          int port = int.Parse(parse[1]);
          string password = parse[2];

          AddBookmark(hostname, port, password);
        }
      }
    }

    void AddBookmark(string hostname, int port, string password)
    {
      ToolStripItem tsi = bookmarksToolStripMenuItem.DropDownItems.Add(hostname + ":" + port);
      tsi.Click += new EventHandler((object sender, EventArgs e) => {
        Connect(hostname, port, password);
      });
    }

    void Disconnect()
    {
      if (Conn == null) {
        return;
      }
      Conn.UnregisterListener();
      Conn = null;
    }

    void Connect(string hostname, int port, string password)
    {
      if (Conn != null) {
        Disconnect();
      }

      Conn = new SamRevAPI(hostname, port, password);
      Conn.OnDataReceive += new SamRevAPI.OnDataReceiveDelegate(Conn_OnDataReceive);
      Conn.RegisterListener();

      labelStatus.Text = "Connected";
      connectToolStripMenuItem.Enabled = false;
      disconnectToolStripMenuItem.Enabled = true;
    }

    private void addCurrentToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (Conn == null) {
        return;
      }

      if (!File.Exists("RconBookmarks.txt")) {
        File.Create("RconBookmarks.txt").Close();
      }
      File.AppendAllLines("RconBookmarks.txt", new string[] {
        Conn.Hostname + "\t" + Conn.Port + "\t" + Conn.AdminPassword
      });

      AddBookmark(Conn.Hostname, Conn.Port, Conn.AdminPassword);
    }

    void Conn_OnDataReceive(string strData)
    {
      this.Invoke(new Action(delegate
      {
        textOutput.Text += strData.Replace("\n", "\r\n");
        textOutput.SelectionStart = textOutput.Text.Length;
        textOutput.ScrollToCaret();
      }));
    }

    void HistoryUp()
    {
      HistorySwitch++;
      if (HistorySwitch > History.Count) {
        HistorySwitch = History.Count;
      }
      HistoryUpdate();
    }

    void HistoryDown()
    {
      HistorySwitch--;
      if (HistorySwitch < 0) {
        HistorySwitch = 0;
      }
      HistoryUpdate();
    }

    void HistoryAdd(string strCommand)
    {
      if (History.Count == 0 || History.Last() != strCommand) {
        History.Add(strCommand);
      }
    }

    void HistoryUpdate()
    {
      if (HistorySwitch == 0) {
        if (HistoryRemember == "") {
          textInput.Text = HistoryRemember;
        } else {
          textInput.Text = "";
        }
      } else {
        textInput.Text = History[History.Count - HistorySwitch];
      }
      textInput.SelectionStart = textInput.Text.Length;
      textInput.ScrollToCaret();
    }

    private void textInput_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter) {
        e.Handled = true;

        if (Conn == null) {
          MessageBox.Show("Not connected to a server.", "Rcon", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        string strCommand = textInput.Text;
        textInput.Text = "";

        HistoryAdd(strCommand);
        Conn.Execute(strCommand, false);

        HistorySwitch = 0;
      } else if (e.KeyCode == Keys.Up) {
        e.Handled = true;
        HistoryUp();
        return;
      } else if(e.KeyCode == Keys.Down) {
        e.Handled = true;
        HistoryDown();
        return;
      }

      HistorySwitch = 0;
      HistoryRemember = "";
    }

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      Disconnect();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void connectToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FormServerDialog dialog = new FormServerDialog();
      if (dialog.ShowDialog() != DialogResult.OK) {
        return;
      }

      Connect(dialog.Hostname, dialog.Port, dialog.Password);
    }

    private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Disconnect();

      labelStatus.Text = "Not connected";
      connectToolStripMenuItem.Enabled = true;
      disconnectToolStripMenuItem.Enabled = false;
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      MessageBox.Show("Serious Sam Revolution Remote Admin tool\n\n" +
        "(c) Alligator Pit 2014 - 2016\n\n" +
        "Please report bugs at: http://ap.samrev.com/bugs/",
      "About Rcon", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
  }
}
