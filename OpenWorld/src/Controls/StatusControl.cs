using Massive;
using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Massive.Events;
using System.Diagnostics;

namespace OpenWorld.src.Controls
{
  public partial class StatusControl : UserControl
  {
    public Color ErrorColor = Color.LightCoral;
    public Color DefaultColor = Color.White;

    Stopwatch _stopwatch = new Stopwatch();
    long PreviousMS = 0;
    int Counter = 0;

    public StatusControl()
    {
      InitializeComponent();
      Globals.Network.PositionChangeHandler += Network_PositionChangeHandler;
      Globals.Network.TeleportHandler += Network_TeleportHandler;
      Globals.Network.StatusEventHandler += Network_StatusEventHandler;
      //Globals.Network.ErrorEventHandler += Network_ErrorEventHandler;
      MMessageBus.ErrorHandler += MMessageBus_ErrorHandler;
      MMessageBus.InfoEventHandler += MMessageBus_InfoEventHandler;
      MMessageBus.AvatarMovedEvent += MMessageBus_AvatarMovedEvent;
      MMessageBus.UserRegistered += MMessageBus_UserRegistered;
      MMessageBus.LateUpdateHandler += MMessageBus_LateUpdateHandler;
      _stopwatch.Start();
    }

    private void MMessageBus_LateUpdateHandler(object sender, UpdateEvent e)
    {
      long t = _stopwatch.ElapsedMilliseconds - PreviousMS;
      PreviousMS = _stopwatch.ElapsedMilliseconds;

      Counter++;
      if ( Counter > 100)
      {
        Counter = 0;
        FPSCounterLable.Text = t.ToString();
      }
      
    }

    private void MMessageBus_UserRegistered(object sender, ChangeDetailsEvent e)
    {
      StatusBox.BackColor = DefaultColor;
      StatusBox.Text = e.Message + "\r\n";
    }

    private void MMessageBus_AvatarMovedEvent(object sender, MoveEvent e)
    {
      SetPosition(e.Position);
    }

    private void Network_StatusEventHandler(object sender, NetworkStatusEvent e)
    {
      BeginInvoke(new MethodInvoker(delegate ()
      {
        if (Globals.Network.Connected == false)
        {
          StatusBox.BackColor = ErrorColor;
          StatusBox.Text = "SERVER DISCONNECTED\r\n";
        }
        else
        {
          StatusBox.BackColor = DefaultColor;
          StatusBox.Text = "Connected\r\n";
        }
      }));
    }

    private void MMessageBus_ErrorHandler(object sender, ErrorEvent e)
    {
      StatusBox.BackColor = ErrorColor;
      StatusBox.Text += e.ErrorMessage + "\r\n";
    }

    private void MMessageBus_InfoEventHandler(object sender, InfoEvent e)
    {
      StatusBox.BackColor = DefaultColor;
      StatusBox.Text = e.Message;
    }

    private void Network_PositionChangeHandler(object sender, Massive.Events.MoveEvent e)
    {
      SetPosition(e.Position);
    }

    private void Network_TeleportHandler(object sender, Massive.Events.MoveEvent e)
    {
      SetPosition(e.Position);
    }

    void SetPosition(Vector3d pos)
    {
      BeginInvoke(new MethodInvoker(delegate ()
      {
        PositionBox.Text = pos.ToString();
      }));      
    }   
  }
}
