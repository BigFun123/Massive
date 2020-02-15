using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Massive;

namespace OpenWorld.src.Controls
{
  public partial class ConnectControl : UserControl
  {
    public ConnectControl()
    {
      InitializeComponent();

      Globals.Network.ConnectedToLobbyHandler += Network_ConnectedToLobbyHandler;
      Globals.Network.ConnectedToMASSIVEHandler += Network_ConnectedToMASSIVEHandler;
      Globals.Network.ConnectedToServerHandler += Network_ConnectedToServerHandler;
      Globals.Network.LoggedOutHandler += Network_LoggedOutHandler;
      Globals.Network.ErrorEventHandler += Network_ErrorEventHandler;
    }

    private void Network_ConnectedToServerHandler(object sender, Massive.Events.StatusEvent e)
    {
      if ( Globals.Network.Connected == false)
      {
        ConnectedCheck.BackColor = Color.Gray;
      }
    }

    private void Network_ConnectedToMASSIVEHandler(object sender, Massive.Events.StatusEvent e)
    {
      ConnectedCheck.BackColor = Color.Green;
    }

    private void Network_ErrorEventHandler(object sender, Massive.Events.ErrorEvent e)
    {
      ConnectedCheck.BackColor = Color.Red;
      ConnectedCheck.Checked = false;
      ConnectedCheck.ImageIndex = 1;
    }

    private void Network_LoggedOutHandler(object sender, Massive.Events.LoggedOutEvent e)
    {
      ConnectedCheck.BackColor = Color.White;
      ConnectedCheck.Checked = false;
      ConnectedCheck.ImageIndex = 1;
    }

    private void Network_ConnectedToLobbyHandler(object sender, Massive.Events.StatusEvent e)
    {
      ConnectedCheck.BackColor = Color.White;
      ConnectedCheck.Checked = true;
      ConnectedCheck.ImageIndex = 0;
    }

    private void ConnectedCheck_CheckedChanged(object sender, EventArgs e)
    {
      if ( ConnectedCheck.Checked == true)
      {
        Globals.Network.Setup();
      }
      else
      {
        Globals.Network.Disconnect();
      }
    }
  }
}
