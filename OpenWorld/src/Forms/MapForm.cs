using Massive;
using Massive.Events;
using Massive.GIS;
using Massive.Platform;
using Massive.Tools;
using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWorld.Forms
{
  public partial class MapForm : Form
  {
    MAstroBody CurrentBody;

    Point ClickPoint = new Point();
    Vector3d WorldLocationPoint = new Vector3d();

    Image bmp;

    public MapForm()
    {
      InitializeComponent();
      MMessageBus.AvatarMovedEvent += MMessageBus_AvatarMovedEvent;      
    }

    private void MMessageBus_AvatarMovedEvent(object sender, MoveEvent e)
    {
      //UpdateCurrentLocation();
    }

    public void Setup()
    {
      AstroBodies.DataSource = MPlanetHandler.Bodies;
      AstroBodies.DisplayMember = "Name";      
      CurrentBody = MPlanetHandler.CurrentNear;
      if ( CurrentBody == null)
      {
        CurrentBody = MPlanetHandler.Bodies.Find(x => "Earth" == x.Name);
      }
      AstroBodies.SelectedIndex = CurrentBody.ListIndex;
    }

    private void MapBox_MouseMove(object sender, MouseEventArgs e)
    {
      //Vector3d Pos = MGISTools.PositionFromMap(CurrentBody,
//        new Vector2d(e.X, e.Y),
        //new Vector2d(MapBox.Size.Width - 1, MapBox.Size.Height - 1));      
      //UpdateCurrentLocation();
    }


    Vector3d MapTo3D(int x, int y)
    {
      //Vector3d destpos = CurrentBody.Position + CurrentBody.Radius;
      Vector3d v = MGISTools.PositionFromMap(CurrentBody, new Vector2d(x, y), new Vector2d(MapBox.Size.Width - 1, MapBox.Size.Height - 1));

      double dist = (double)DistanceVal.Value;
      if (dist == 0) dist = 0.001;

      double d = dist / 100.0 + 2; //+2m or we are placed inside the body on the edge.);
      Vector3d P = MGISTools.PositionOnBodyFromMapPoint(v.X, v.Y, CurrentBody.Radius.X, d);
      return CurrentBody.Position + P;
    }

    private void MapBox_MouseClick(object sender, MouseEventArgs e)
    {      
      WorldLocationPoint = MapTo3D(e.X, e.Y);
      ClickPoint = e.Location;
      MMessageBus.Navigate(this, WorldLocationPoint);

      Console.WriteLine("Validate");
      if (bmp == null) return;
      Bitmap bmp2 = new Bitmap(bmp);
      Pen RedPen = new Pen(Color.Red);
      RedPen.Width = 2;
      Graphics g = Graphics.FromImage(bmp2);
      float posx = (float)ClickPoint.X / (float)MapBox.Width * (float)bmp.Width;
      float posy = (float)ClickPoint.Y / (float)MapBox.Height* (float)bmp.Height;
      Rectangle rec = new Rectangle((int)posx-7, (int)posy-7, 15, 15);
      g.DrawRectangle(RedPen, rec);
      RedPen.Dispose();
      g.Dispose();
      MapBox.BackgroundImage = bmp2;
      Invalidate(new Rectangle(ClickPoint.X-2, ClickPoint.Y-2, 5,5));

      Vector3d Pos3d = MapTo3D(e.X, MapBox.Height-e.Y);
      Vector3d uv = CurrentBody.GetUVPointOnSphere(Pos3d);
      Vector3d lonlat = CurrentBody.GetLonLatOnShere(Pos3d);
      Vector3d tile = CurrentBody.GetTileFromPoint(Pos3d);
      WorldLocationPoint = CurrentBody.LonLatToUniPosition(lonlat.X, lonlat.Y, 0);
      MTerrainBoundary tb = CurrentBody.GetTileBoundaryLonLat((int)tile.X, (int)tile.Y, (int)tile.Z);
      WorldLocationPoint = Pos3d;
      WorldCoords.Text =
       " pos:" + Pos3d.ToString() + "\r\n"
  //     + " uv:" + uv.ToString() + "\r\n"
       + " lonlat:" + lonlat.ToString() + "\r\n"
       + " Tile: " + tile.ToString() + "\r\n"
      // + " Bounds: " + tb.ToString() + "\r\n"
//       + " Round Trip: " + WorldLocationPoint.ToString()
       ;
      
    }

    private void AstroBodies_SelectedIndexChanged(object sender, EventArgs e)
    {
      CurrentBody = (MAstroBody)AstroBodies.SelectedItem;
      //string sPath = 
      string sPath = CurrentBody.TextureName;
      if (MassiveTools.IsURL(sPath))
      {
        string cache = MassiveTools.GetCachePath(sPath);
        if (!File.Exists(cache)) return;
        bmp = Image.FromFile(cache);
        MapBox.BackgroundImage = bmp;
      }
      else
      {
        bmp = Image.FromFile(Path.Combine(MFileSystem.AssetsPath, CurrentBody.TextureName));
        MapBox.BackgroundImage = bmp;
      }

    }

    Vector3d GetLonLatInMeters(double height = 0)
    {
      if (height == 0)
      {
        height = CurrentBody.Radius.X * 1.001;
      }
      string s = LonLatBox.Text.Trim();
      double lat = 0;
      double lon = 0;

      if (s.Contains(','))
      {
        string[] coords = LonLatBox.Text.Split(',');
        lon = double.Parse(coords[0]);
        lat = double.Parse(coords[1]);
        
      }
      else
      if (s.Contains(' '))
      {
        string[] coords = LonLatBox.Text.Split(' ');
        lon = double.Parse(coords[0]);
        lat = double.Parse(coords[1]); 
      }

      Vector3d d = MGISTools.LonLatMercatorToPosition(lon, lat, height) + CurrentBody.Position;
      return d;
    }

    private void GoLatLon_Click(object sender, EventArgs e)
    {
      Vector3d d = GetLonLatInMeters();
      MMessageBus.TeleportRequest(this, d, Quaterniond.Identity);
    }

    private void GotoMapPointButton_Click(object sender, EventArgs e)
    {
      if (Globals.Network.Connected == true)
      {
        MMessageBus.TeleportRequest(this, WorldLocationPoint, Quaterniond.Identity);
      }
      else
      {
        Globals.Avatar.SetPosition(WorldLocationPoint);
        MMessageBus.AvatarMoved(this, Globals.UserAccount.UserID, WorldLocationPoint, Quaterniond.Identity);
      }
    }


    private void Navigate_Click(object sender, EventArgs e)
    {
      double height = 0;
      if (MPlanetHandler.CurrentNear != null) {
        height = MPlanetHandler.CurrentNear.DistanceToAvatar;
      }
      
      Vector3d LatLon = GetLonLatInMeters(height);
      MMessageBus.Navigate(this, LatLon);
    }

    private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      MMessageBus.AvatarMovedEvent -= MMessageBus_AvatarMovedEvent;
    }

    private void MapBox_Validated(object sender, EventArgs e)
    {
     
    }

    private void MapBox_Paint(object sender, PaintEventArgs e)
    {
     
    }

  }
}
