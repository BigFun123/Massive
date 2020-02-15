using Massive;
using Massive.Events;
using Massive.Tools;
using OpenTK;
using OpenWorld;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWorld.Forms
{
  public partial class InfoOverlayForm : Form
  {

    Vector3d NavigationTarget;
    Vector3d UserPosition;
    string sStatus = "Loading...";


    public InfoOverlayForm()
    {
      InitializeComponent();
      MMessageBus.AvatarMovedEvent += MMessageBus_AvatarMovedEvent;
      MMessageBus.NavigationHandler += MMessageBus_NavigationHandler;
      MMessageBus.ZoneSelectHandler += MMessageBus_ZoneSelectHandler;
      MMessageBus.LoadingStatusHandler += MMessageBus_LoadingStatusHandler;
      MMessageBus.LoggedIn += MMessageBus_LoggedIn;      
      //string sDist = string.Format("{0,12:#.00}", 12341234123.6623452345);
      //UserInfo.Text = sDist;

    }

    private void MMessageBus_LoggedIn(object sender, ChangeDetailsEvent e)
    {
      NavigationTarget = MassiveTools.VectorFromArray(Globals.UserAccount.HomePosition);
    }

    private void MMessageBus_LoadingStatusHandler(object sender, InfoEvent e)
    {
      //UserInfo.Text = e.Message;
      sStatus = e.Message;
      UpdateData();
      Thread.Sleep(10);
      timer1.Start();
    }

    private void MMessageBus_ZoneSelectHandler(object sender, ZoneEvent e)
    {
      NavigationTarget = MassiveTools.Vector3dFromVector3_Server(e.Zone.Position);
    }

    private void MMessageBus_NavigationHandler(object sender, NavigationEvent e)
    {
      NavigationTarget = e.Target;
    }

    private void MMessageBus_AvatarMovedEvent(object sender, MoveEvent e)
    {
      UserPosition = e.Position;
      UpdateData();
    }

    public void UpdateData()
    {
      Location = Main.ClientRect.Location;
      Location.Offset(10, Height);      
      
      if (MPlanetHandler.CurrentNear == null) return;

      //UserInfo.Text = "";      
      UserInfo.Text = MPlanetHandler.CurrentNear.Name + ".";     
      

      Vector3d pos = MPlanetHandler.CurrentNear.GetLonLatOnShere(UserPosition);
      UserInfo.Text += string.Format("LonLat: {0:0.0000},{1:0.0000} Alt:{2:0.0}", pos.X, pos.Y, MPlanetHandler.CurrentNear.AvatarDistanceToSurface);

      Vector3d TilePos = MPlanetHandler.CurrentNear.GetTileFromPoint(UserPosition);
      UserInfo.Text += " " + TilePos.ToString();

      GetMetaData(TilePos);

      string sDist = string.Format("{0,12:#.00}km", Vector3d.Distance(Globals.Avatar.GetPosition(), NavigationTarget) / 1000.0);
      UserInfo.Text += " |> " +sDist;
      //Location = Main.ClientLocation;
      //Location.Offset(10, Main.RenderClientSize.Height - Height);
      if (string.IsNullOrEmpty(sStatus))
      {
        UserInfo.BackColor = Color.Black;
      }
      else
      {
        UserInfo.BackColor = Color.DarkRed;
      }
      UserInfo.Text += "\r\n"+sStatus;
    }

    public void GetMetaData(Vector3d Tile)
    {
      if (MPlanetHandler.CurrentNear == null) return;
      if (MPlanetHandler.CurrentNear._terrainHandler == null) return;
      UserInfo.Text += "\r\n" + MPlanetHandler.CurrentNear._terrainHandler.GetTileInfo(Tile);
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      sStatus = "";
      timer1.Stop();
    }
  }
}
