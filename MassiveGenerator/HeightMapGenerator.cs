using Massive;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MassiveGenerator
{
  public class CompleteEvent : EventArgs
  {
    public vector3 pos;
    public string rgb;
    public string simple;
    public string text;
    public CompleteEvent(vector3 inv, string inrgb, string insimple, string intext)
    {
      pos = inv;
      rgb = inrgb;
      simple = insimple;
      text = intext;
    }
  }

  public class vector3
  {
    public double x, y, z;
    public vector3(double ix, double iy, double iz)
    {
      x = ix;
      y = iy;
      z = iz;
    }
  }

  public class HeightMapGenerator
  {

    public event EventHandler<CompleteEvent> Complete;
    private const int TileSize = 256;
    private const int EarthRadius = 6378137;
    private const double InitialResolution = 2 * Math.PI * EarthRadius / TileSize;
    private const double OriginShift = 2 * Math.PI * EarthRadius / 2;
    List<WebClient> Clients = new List<WebClient>();

    bool CreateMeshes = false;

    string MapBoxKey = "pk.eyJ1IjoiYmlnZnVubWIiLCJhIjoiY2ppc3YxcHUxMThrejNwcDZvbW9zOHN2ZiJ9.5cMMctVxvEOHQxoZsZIorQ";

    public void Download(int z, int x, int y)
    {
      Directory.CreateDirectory("data\\" + z.ToString());
      Directory.CreateDirectory("data\\" + z.ToString() + "\\height");
      Directory.CreateDirectory("data\\" + z.ToString() + "\\rgb");
      Directory.CreateDirectory("data\\" + z.ToString() + "\\mesh");
      Directory.CreateDirectory("data\\" + z.ToString() + "\\data");
      Directory.CreateDirectory("data\\" + z.ToString() + "\\biome");

      string url = "https://api.mapbox.com/v4/mapbox.terrain-rgb/{z}/{x}/{y}.pngraw?access_token={key}";
      url = url.Replace("{z}", z.ToString());
      url = url.Replace("{x}", x.ToString());
      url = url.Replace("{y}", y.ToString());
      url = url.Replace("{key}", MapBoxKey);
      string url2 = "https://api.mapbox.com/v4/mapbox.satellite/{z}/{x}/{y}@2x.jpg?access_token={key}";
      url2 = url2.Replace("{z}", z.ToString());
      url2 = url2.Replace("{x}", x.ToString());
      url2 = url2.Replace("{y}", y.ToString());
      url2 = url2.Replace("{key}", MapBoxKey);

      WebClient c = new WebClient();
      Clients.Add(c);
      vector3 v = new vector3(x, y, z);
      c.DownloadFileCompleted += C_DownloadFileCompleted;

      string sPath = GetPath(v, "height") + ".png";
      if (!File.Exists(sPath))
      {
        c.DownloadFile(url, sPath);
      }

      sPath = GetPath(v, "rgb") + ".jpg";
      if (!File.Exists(sPath))
      {
        c.DownloadFile(url2, sPath);
      }

      Process(v);
    }

    string GetPath(vector3 v, string add)
    {
      string path = string.Format(@"data\{0}\{3}\{1}_{2}", v.z, v.x, v.y, add);
      return path;
    }

    private void C_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
      WebClient c = (WebClient)sender;
      Process((vector3)e.UserState);
    }

    void Process(vector3 v)
    {
      string height = GetPath(v, "height") + ".png";
      string rgb = GetPath(v, "rgb") + ".jpg";
      string textname = GetPath(v, "heighttxt") + ".txt";

      // ExtractHeights(v);

      if (CreateMeshes == true)
      {
        CreateMesh(v);
      }

      if (Complete != null)
      {
        Complete(this, new CompleteEvent(v, height, rgb, textname));
      }

      Thread.Sleep(110);
    }

    public void ExtractHeights(vector3 v)
    {
      string heightname = GetPath(v, "height") + ".png";
      string textname = GetPath(v, "heighttxt") + ".txt";
      Bitmap b = (Bitmap)Image.FromFile(heightname);

      Bitmap dest = new Bitmap(b);
      Color c;
      double height = 0;
      double[,] n = new double[b.Width, b.Height];
      StringBuilder sb = new StringBuilder();
      for (int y = 0; y < b.Height; y++)
      {
        for (int x = 0; x < b.Width; x++)
        {
          c = b.GetPixel(x, y);
          height = -10000 + ((c.R * 256 * 256 + c.G * 256 + c.B) * 0.1);
          int nh = (int)((height / 8000.0) * 255);
          if (nh > 255) nh = 255;
          dest.SetPixel(x, y, Color.FromArgb(nh, 0, 0));
          n[x, y] = height;
          sb.Append(height);
          if (x < b.Width - 1)
          {
            sb.Append(",");
          }
          else
          {
            sb.Append("\n");
          }
        }
      }
      File.WriteAllText(textname, sb.ToString());
    }

    public void CreateMesh(vector3 v)
    {
      string rgbname = GetPath(v, "height") + ".png";
      Bitmap bmp = (Bitmap)Image.FromFile(rgbname);

      StringBuilder b = new StringBuilder();
      b.Append("o massive_" + v.x + "_" + v.y + "_" + v.z + "\n");

      RectD bounds = TileBounds(new Vector2d(v.x, v.y), (int)v.z);

      const int w = 256;
      const int h = 256;

      const int NumVertsX = 256;
      const int NumVertsY = 256;

      StringBuilder tex = new StringBuilder();
      StringBuilder f = new StringBuilder();

      //double xscale = w * bounds.Size.x;
      //double zscale = h * bounds.Size.y;
      double xscale = bounds.Size.x;
      double zscale = bounds.Size.y;

      Color c;
      for (int z = 0; z < NumVertsY; z++)
      {
        for (int x = 0; x < NumVertsX; x++)
        {

          c = bmp.GetPixel(x, z);
          double height = -10000 + ((c.R * 256 * 256 + c.G * 256 + c.B) * 0.1);
          //height = height / 8000.0;
          b.Append(string.Format("v {0:0.00000000} {1:0.00000000} {2:0.00000000}", (x / (double)NumVertsX) * xscale, height, (z / (double)NumVertsY) * zscale) + "\n");
          tex.Append("vt " + ((float)x / (float)(NumVertsX - 1)) + " " + (NumVertsY - ((float)z / (float)(NumVertsY - 1))) + "\n");

        }
      }

      b.Append("vn 0 1 0\n");

      const int totalVerts = w * NumVertsY;
      const int totalTriangles = 2 * (NumVertsX - 1) * (NumVertsY - 1);
      const int triangleIndexStride = 3 * sizeof(int);

      int counter = 0;

      for (int x = 0; x < (NumVertsX - 1) * (NumVertsY - 1); x++)
      {
        // for (int z = 0; z < NumVertsY ; z++)
        //{
        counter++;
        f.Append("f ");
        f.Append(counter + 1 + "/");
        f.Append(counter + 1 + "/");
        f.Append(1 + " ");

        f.Append(counter + "/");
        f.Append(counter + "/");
        f.Append(1 + " ");

        f.Append((counter + NumVertsX) + "/");
        f.Append(counter + NumVertsX + "/");
        f.Append(1 + " ");

        f.Append((counter + NumVertsX + 1) + "/");
        f.Append((counter + NumVertsX + 1) + "/");
        f.Append(1 + "\n");
        if (counter % NumVertsX == NumVertsX - 1)
        {
          counter++;
        }

        //b.Append("F ")
        //b.Append(row1Index);
        //b.Append(row2Index + 1);
        //b.Append(row2Index);
        //}
      }

      b.Append(tex.ToString());
      b.Append(f.ToString());

      File.WriteAllText(GetPath(v, "mesh") + ".obj", b.ToString());
    }

    public static RectD TileIdToBoundsLatLon(int x, int y, int zoom)
    {
      var sw = new Vector2d(TileYToNWLatitude(y, zoom), TileXToNWLongitude(x + 1, zoom));
      var ne = new Vector2d(TileYToNWLatitude(y + 1, zoom), TileXToNWLongitude(x, zoom));
      return new RectD(sw, ne);
    }

    public static RectD TileBounds(Vector2d tileCoordinate, int zoom)
    {
      var min = PixelsToMeters(new Vector2d(tileCoordinate.x * TileSize, tileCoordinate.y * TileSize), zoom);
      var max = PixelsToMeters(new Vector2d((tileCoordinate.x + 1) * TileSize, (tileCoordinate.y + 1) * TileSize), zoom);
      return new RectD(min, max - min);
    }

    public static double TileYToNWLatitude(int y, int zoom)
    {
      var n = Math.Pow(2.0, zoom);
      var lat_rad = Math.Atan(Math.Sinh(Math.PI * (1 - 2 * y / n)));
      var lat_deg = lat_rad * 180.0 / Math.PI;
      return lat_deg;
    }

    public static double TileXToNWLongitude(int x, int zoom)
    {
      var n = Math.Pow(2.0, zoom);
      var lon_deg = x / n * 360.0 - 180.0;
      return lon_deg;
    }

    private static Vector2d PixelsToMeters(Vector2d p, int zoom)
    {

      var res = Resolution(zoom);
      var met = new Vector2d(0, 0);
      met.x = (float)(p.x * res - OriginShift);
      met.y = (float)-(p.y * res - OriginShift);
      return met;
    }

    private static double Resolution(int zoom)
    {
      return InitialResolution / Math.Pow(2, zoom);
    }

    public static Vector2d LatitudeLongitudeToTileId(float latitude, float longitude, int zoom)
    {
      var x = (int)Math.Floor((longitude + 180.0) / 360.0 * Math.Pow(2.0, zoom));
      var y = (int)Math.Floor((1.0 - Math.Log(Math.Tan(latitude * Math.PI / 180.0)
          + 1.0 / Math.Cos(latitude * Math.PI / 180.0)) / Math.PI) / 2.0 * Math.Pow(2.0, zoom));

      return new Vector2d(x, y);
    }

    public static double GetTileScaleInMeters(float latitude, int zoom)
    {
      return 40075000 * Mathd.Cos(Mathd.Deg2Rad * latitude) / Mathd.Pow(2f, zoom + 8);
    }

  }
}
