using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections.TCP;
using NetworkCommsDotNet.Connections.UDP;
using NetworkCommsDotNet.DPSBase;
using NetworkCommsDotNet.Tools;
using NetworkCommsDotNet.Connections;



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using Massive.Network;
using System.Diagnostics;
using System.IO;
using MassiveServer.src.postgis;
using MassiveServer.src.forms;
using System.Threading;

namespace Massive.Server
{
  public partial class MainWindow : Form
  {
    bool ShowLog = true;
    MServer _Server;
    public string LocalName { get; set; }
    //public BindingList<MClient> MassiveConnections = new BindingList<MClient>(); //BindingList for live view list
    public BindingList<MUniverse> MassiveZones = new BindingList<MUniverse>(); //BindingList for live view list
    public List<ConsoleColor> ColorCodes = new List<ConsoleColor>();
    MConsole _console;

    public MainWindow()
    {
      InitializeComponent();
     
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
      _console = new MConsole();
      _console.CreateConsole();

      ColorCodes.Add(ConsoleColor.DarkYellow);
      ColorCodes.Add(ConsoleColor.Green);
      ColorCodes.Add(ConsoleColor.Cyan);
      ColorCodes.Add(ConsoleColor.Gray);
      ColorCodes.Add(ConsoleColor.Red);
      OutputLogLegend();
      Log("AutoStart in 4 Seconds...", MServer.UTILITY);
      _Server = new MServer();

      IPAddressBox.Text = _Server.ServerIPAddress;
      PortBox.Text = _Server.ServerPort.ToString();
      
      _Server.Version = MVersion.VERSION.ToString();

      _Server.ZoneChanged += _Server_ZoneChanged;
      _Server.UniverseChanged += _Server_UniverseChanged;
      
      _Server.ServerInfo += _Server_ServerInfo;
      _Server.MetricInfo += _Server_MetricInfo;
      _Server.ClientConnected += _Server_ClientConnected;
      _Server.ClientLoggedIn += _Server_ClientLoggedIn;
      _Server.ClientDisconnected += _Server_ClientDisconnected;      

      Log("Created Server Version:" + _Server.Version, MServer.UTILITY);
      Text += " v" + _Server.Version;
      LocalName = HostInfo.HostName;     
      //ConnectionsList.DataSource = MassiveConnections;
      //ZoneList.DataSource = MassiveZones;      
      ZoneList.DisplayMember = "Name";


#if DEBUG
      timer1.Interval = 1000;
      Text += " DEBUG";
#endif
      timer1.Start();
      BackupTimer.Start();
     // NetworkTimer.Start();

#if DEBUG

#else
      
#endif
      MaxConnectionsLabel.Text = "Max Connections" + MServer.MAXCONNECTIONS;
      Log("Backup schedule is: Backup every " + (BackupTimer.Interval / 3600000) + " hour", 3);
      GetPublicIP();
    }

    void GetPublicIP()
    {
      WebClient wc = new WebClient();
      try { 
        string s = wc.DownloadString("http://icanhazip.com");
        PublicIPBox.Text = s;
      } catch( Exception e)
      {
        PublicIPBox.Text = "Could not get Public IP";
      }
    }

    private void _Server_UniverseChanged(object sender, ServerEvent e)
    {
      Log(e.Message, e.ColorCode);
      RefreshUI();
      
    }

    private void _Server_ZoneChanged(object sender, ZoneEvent e)
    {
      RefreshUI();
    }

    void AutoStart()
    {

      timer1.Stop();
      //ConsoleBox.Text = "";
      StartServer();
      CMDTimer.Start();
    }

    public void OutputLogLegend()
    {
      if (ShowLog == false) return;
      
      MConsole.LogSingle("OUTGOING", (ConsoleColor)ColorCodes[MServer.OUTGOING]);
      MConsole.LogSingle("OUTGOINGBROADCAST", (ConsoleColor)ColorCodes[MServer.OUTGOINGBROADCAST]);
      MConsole.LogSingle("INCOMING", (ConsoleColor)ColorCodes[MServer.INCOMING]);
      MConsole.LogSingle("UTILITY", (ConsoleColor)ColorCodes[MServer.UTILITY]);
      MConsole.LogSingle("ERROR", (ConsoleColor)ColorCodes[MServer.ERROR]);
      Log("", MServer.UTILITY);
    }

    public void Log(string s, int ColorCode)
    {
      if (ShowLog == false) return;
      MConsole.Log(s, (ConsoleColor)ColorCodes[ColorCode]);
    }

    public void StartServer()
    {
      _Server.ServerIPAddress = IPAddressBox.Text;
      Log("======= Network Startup =======", MServer.UTILITY);
      _Server.Start();
      RefreshUI();
    }

    private void _Server_MetricInfo(object sender, ServerEvent e)
    {
      Invoke((MethodInvoker)delegate
      {
        StatusText.Text = e.Message;
      });
    }

    private void _Server_ClientLoggedIn(object sender, ServerEvent e)
    {
      RefreshUI();
    }

    void RefreshUI()
    {
        ConnectionsList.Invoke((MethodInvoker)delegate
        {
          ConnectionsList.Items.Clear();
          foreach (MClient c in _Server.MassiveConnections)
          {
            ListViewItem lvi = new ListViewItem(c.Address.ToString());            
            lvi.SubItems.Add(c.Account.UserID);
            lvi.SubItems.Add(c.Account.Email);
            lvi.SubItems.Add(c.Account.UserName);
            lvi.SubItems.Add(c.Account.TotalObjects.ToString());
            lvi.SubItems.Add(c.Account.MaxObjects.ToString());
            lvi.SubItems.Add(c.State);
            lvi.Tag = c;

            TimeSpan ts = DateTime.Now - c.Account.LastActivity;
            if (ts.Seconds > 120)
            {
              lvi.BackColor = Color.LightBlue;
            }

            ConnectionsList.Items.Add(lvi);
          }
        });
     
      UniverseList.Invoke((MethodInvoker)delegate
      {
        UniverseList.DataSource = null;
        UniverseList.DataSource = _Server.MassiveUniverses;
        ZoneList.DataSource = null;
        ZoneList.DataSource = _Server._ZoneHandler.Zones;
        ZoneList.DisplayMember = "Name";
        ObjectCountLabel.Text = _Server.GetObjectCount().ToString();
      });
      
    }

    private void _Server_ClientConnected(object sender, ServerEvent e)
    {
      Invoke((MethodInvoker)delegate
      {
        RefreshUI();
      });
    }

    private void _Server_ClientDisconnected(object sender, ServerEvent e)
    {
      Invoke((MethodInvoker)delegate
      {
        RefreshUI();
      });
    }

    private void _Server_ServerInfo(object sender, ServerEvent e)
    {
      Invoke((MethodInvoker)delegate
      {
        Log(e.Message, e.ColorCode);
      });
    }

    public void StopServer()
    {
      _Server.ServerInfo -= _Server_ServerInfo;
      NetworkComms.Shutdown();
      Log("======= Network Shutdown ======= ", MServer.UTILITY);
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
      StartServer();
    }

    private void StopButton_Click(object sender, EventArgs e)
    {
      timer1.Stop();
      StopServer();
    }

    private void NetworkTimer_Tick(object sender, EventArgs e)
    {
      if (AliveMeter.Value >= AliveMeter.Maximum-1) AliveMeter.Value = 0;
      AliveMeter.Value++;      
    }

    /**
     * Send a test message to self over the adapter, and to all connected clients
     * */
    private void label3_Click(object sender, EventArgs e)
    {

    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      timer1.Stop();
      AutoStart();
    }
   

    private void FlushButton_Click(object sender, EventArgs e)
    {
      _Server.FlushInactiveObjects();

      MNetMessage m = new MNetMessage();
      m.Command = MNetMessage.CHAT;

      MChatMessage cm = new MChatMessage();
      cm.OwnerID = "SERVER";
      cm.Message = "Server flushed all avatars. Please restart your viewer";
      m.Payload = cm.Serialize();

      _Server.SendToAllClients(null, m.Serialize());
    }

    private void UniverseList_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      MUniverse z = (MUniverse)UniverseList.SelectedItem;
      Process.Start("notepad.exe", "data\\" + z.UniverseFile);
    }

    private void ConnectionsList_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      //MClient c = (MClient)ConnectionsList.SelectedItems[0].Tag;
      //c.Save();
      //Process.Start("notepad.exe", "data\\" + c.Account.UserID + ".json");
    }

    private void ShowLogButton_Click(object sender, EventArgs e)
    {
      OutputLogLegend();
    }

    private void ClearLog_Click(object sender, EventArgs e)
    {
      _console.Clear();
    }

    private void BackupTimer_Tick(object sender, EventArgs e)
    {
      string BackupDirectory = "data\\backups";
      if (!Directory.Exists(BackupDirectory)){
        Directory.CreateDirectory(BackupDirectory);

      }
      Log("|||||||||| Backing up Universe ||||||||||||", 3);
      foreach( MUniverse m in _Server.MassiveUniverses)
      {
        m.MakeBackup(BackupDirectory);
      }
      Log("|||||||||| Backing up Zones ||||||||||||", 3);
      _Server._ZoneHandler.MakeBackup(BackupDirectory);
      
    }

    private void DisableLogTimer_Tick(object sender, EventArgs e)
    {      
      Log(">>>>>>>>>>>>>>>Log output disabled for performance reasons.",3);
      ShowLog = false;      
    }

    private void TestButton_Click_1(object sender, EventArgs e)
    {
      try
      {
        MNetMessage mn = new MNetMessage();
        mn.Command = MNetMessage.CHAT;
        MChatMessage mc = new MChatMessage();
        mc.Message = ChatBox.Text;
        mc.OwnerID = "SERVER";
        mc.TargetID = "*";
        mn.Payload = mc.Serialize();

        _Server.ChatMessage(null, mn);
      }
      catch (Exception ex)
      {
        Log(ex.Message, MServer.ERROR);
        if (ex.InnerException != null)
        {
          Log(ex.InnerException.Message, MServer.ERROR);
        }
      }
    }

    private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
      Log("Saving Universe", 1);
      _Server.Close();            
    }

    private void MaxConnectionsLabel_MouseClick(object sender, MouseEventArgs e)
    {
      //show max connections dialog
    }

    private void DoTweakTest_Click(object sender, EventArgs e)
    {
      
    }

    private void DatabaseButton_Click(object sender, EventArgs e)
    {
      //DBForm db = new DBForm();
      //db.Show(this);
      SQLForm sf = new SQLForm(_Server._DataBase);
      sf.Show(this);
    }

    private void ConnectionsList_AfterLabelEdit(object sender, LabelEditEventArgs e)
    {
      //ListViewItem lvi = ConnectionsList.Items[e.Item];
      //MClient c = (MClient)lvi.Tag;
    }

    private void FlushButton_Click_1(object sender, EventArgs e)
    {
      _Server.FlushInactiveObjects();
      Log("Flushed ALL non-static Objects", MServer.UTILITY);
    }

    private void RegisterLobbyButton_Click(object sender, EventArgs e)
    {
      RegisterForm rf = new RegisterForm();      
      rf.Show(this);
    }

    void Restart()
    {
      try
      {
        MNetMessage mn = new MNetMessage();
        mn.Command = MNetMessage.CHAT;
        MChatMessage mc = new MChatMessage();
        mc.Message = "Server is shutting down. Please log back on in a few minutes";
        mc.OwnerID = "SERVER";
        mc.TargetID = "*";
        mn.Payload = mc.Serialize();

        _Server.ChatMessage(null, mn);
      }
      catch (Exception ex)
      {
        Log(ex.Message, MServer.ERROR);
        if (ex.InnerException != null)
        {
          Log(ex.InnerException.Message, MServer.ERROR);
        }
      }

      Thread.Sleep(1000);

      Application.Restart();
      Environment.Exit(0);            
    }

    void Stop()
    {
      try
      {
        MNetMessage mn = new MNetMessage();
        mn.Command = MNetMessage.CHAT;
        MChatMessage mc = new MChatMessage();
        mc.Message = "Server is shutting down. Please log back on in a few minutes";
        mc.OwnerID = "SERVER";
        mc.TargetID = "*";
        mn.Payload = mc.Serialize();

        _Server.ChatMessage(null, mn);
      }
      catch (Exception ex)
      {
        Log(ex.Message, MServer.ERROR);
        if (ex.InnerException != null)
        {
          Log(ex.InnerException.Message, MServer.ERROR);
        }
      }
      Thread.Sleep(1000);
      Close();
    }

      //commands can be placed in a command file to, e.g. restart the app
      void ParseCmd()
    {
      string sCmdFile = ".\\cmd\\servercmd.txt";
      if ( File.Exists(sCmdFile))
      {
        string cmd = File.ReadAllText(sCmdFile);
        File.Move(sCmdFile, sCmdFile + "_" + DateTime.Now.ToString("yyyymmddhhmmss"));
        if (string.IsNullOrEmpty(cmd)) return;
        cmd = cmd.Trim();
        if ( cmd == "RESTART")
        {
          Restart();
        }
        if (cmd == "STOP")
        {
          Stop();
        }

      }
    }

    private void CMDTimer_Tick(object sender, EventArgs e)
    {
      ParseCmd();
    }

    private void ShowDebug_CheckedChanged(object sender, EventArgs e)
    {
      _Server.DisplayEvents = ShowDebug.Checked;
    }
  }
}
