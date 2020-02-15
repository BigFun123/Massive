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
using Massive.Events;

namespace OpenWorld.src.Controls
{
  public partial class ConnectControl : UserControl
  {
    public enum eConnectState { Connected, Disconnected, Error };

    public ConnectControl()
    {
      InitializeComponent();

      Globals.Network.ConnectedToMASSIVEHandler += Network_ConnectedToMASSIVEHandler;
      Globals.Network.ConnectedToServerHandler += Network_ConnectedToServerHandler;
      Globals.Network.LoggedOutHandler += Network_LoggedOutHandler;
      Globals.Network.ErrorEventHandler += Network_ErrorEventHandler;
      Globals.Network.StatusEventHandler += Network_StatusEventHandler;
    }

    private void Network_StatusEventHandler(object sender, NetworkStatusEvent e)
    {
      if (Globals.Network.Connected == false)
      {
        SetButtonState(eConnectState.Disconnected);
      }
    }

    void SetButtonState(eConnectState state)
    {
      Invoke((MethodInvoker)delegate
      {
        if (state == eConnectState.Connected)
        {
          ConnectedCheck.Checked = true;
          ConnectedCheck.ImageIndex = 0;
        }
        if (state == eConnectState.Disconnected)
        {
          ConnectedCheck.Checked = false;
          ConnectedCheck.ImageIndex = 0;
        }
        if (state == eConnectState.Error)
        {
          ConnectedCheck.Checked = false;
          ConnectedCheck.ImageIndex = 1;
        }
      });     
    }

    private void Network_ConnectedToServerHandler(object sender, Massive.Events.StatusEvent e)
    {
      if (Globals.Network.Connected == false)
      {
        SetButtonState(eConnectState.Disconnected);
      }
    }

    private void Network_ConnectedToMASSIVEHandler(object sender, Massive.Events.StatusEvent e)
    {
      if (Globals.Network.Connected == true)
      {
        SetButtonState(eConnectState.Connected);
      }
    }

    private void Network_ErrorEventHandler(object sender, Massive.Events.ErrorEvent e)
    {
      //ConnectedCheck.BackColor = Color.Red;
      //ConnectedCheck.Checked = false;
      //ConnectedCheck.ImageIndex = 1;
      //Console.WriteLine(e.ErrorMessage);
    }

    private void Network_LoggedOutHandler(object sender, Massive.Events.LoggedOutEvent e)
    {
      SetButtonState(eConnectState.Disconnected);
    }

    private void ConnectedCheck_Click(object sender, EventArgs e)
    {
      if (Globals.Network.Connected == false)
      {
        // Globals.Network.Setup();
        MMessageBus.LobbyLoadRequest(this, new ChangeDetailsEvent(true, "Getting Lobby"));
      }
      else
      {
        Globals.Network.Disconnect();
      }
    }
  }
}
