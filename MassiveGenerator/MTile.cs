using Mapbox.Vector.Tile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MassiveGenerator
{
  public class MTile
  {

    public void Decode(int x, int y, int zoom, TreeView tv)
    {
      Bitmap bmp = new Bitmap(4096, 4096);
      Graphics g = Graphics.FromImage(bmp);
      g.Clear(Color.Black);


      WebClient client = new WebClient();
      client.Headers[HttpRequestHeader.AcceptEncoding] = "json";
      string path = "http://{ip}:{port}/data/v3/{zoom}/{x}/{y}.geojson";
      path = "http://{ip}:{port}/data/v3/{zoom}/{x}/{y}.pbf";
      path = path.Replace("{zoom}", zoom.ToString());
      path = path.Replace("{x}", x.ToString());
      path = path.Replace("{y}", y.ToString());
      path = path.Replace("{port}", FilePath.Port);
      path = path.Replace("{ip}", FilePath.IP);

      client.DownloadData(path);
      var responseStream = new GZipStream(client.OpenRead(path), CompressionMode.Decompress);

      List<VectorTileLayer> results = VectorTileParser.Parse(responseStream);

      tv.Invoke((MethodInvoker)delegate
      {
        tv.Nodes.Clear();
        tv.BeginUpdate();

        //Pen gray = new Pen(Color.Gray);
        //Pen water = new Pen(Color.Blue);
        //Pen grass = new Pen(Color.Green);
        //Pen CurrentPen = gray;

        Color WaterColor = Color.Blue;
        Color ForestColor = Color.DarkGreen;
        Color GrassColor = Color.LightGreen;

        Brush water = new SolidBrush(WaterColor);
        Brush industrial = new SolidBrush(Color.Brown);
        Brush residential = new SolidBrush(Color.Purple);
        Brush commercial = new SolidBrush(Color.Gray);
        Brush building = new SolidBrush(Color.DarkGray);
        Brush retail = new SolidBrush(Color.Orange);
        Brush grass = new SolidBrush(GrassColor);
        Brush forest = new SolidBrush(ForestColor);
        Brush railway = new SolidBrush(Color.LightGray);
        Brush road = new SolidBrush(Color.White);
        Pen transportpen = new Pen(Color.White);
        //Brush gray = new SolidBrush(Color.Gray);
        Brush none = new SolidBrush(Color.Transparent);
        Brush CurrentBrush = none;

        bool Transport = false;

        foreach (VectorTileLayer layer in results)
        {
          CurrentBrush = none;
          if (layer.Name == "water") { CurrentBrush = water; }
          if (layer.Name == "park") { CurrentBrush = forest; }
          if (layer.Name == "building") { CurrentBrush = building; }
          if (layer.Name == "transportation")
          {
            CurrentBrush = road;
            Transport = true;
          }
          else
          {
            Transport = false;
          }

          TreeNode layernode = new TreeNode(layer.Name);
          uint extent = layer.Extent;
          tv.Nodes.Add(layernode);
          foreach (VectorTileFeature feature in layer.VectorTileFeatures)
          {
            if ((layer.Name == "landcover")
             && (feature.Attributes[0].Key == "class")
             && (feature.Attributes[0].Value.Equals("grass")))
            {
              CurrentBrush = grass;
            }


            if ((layer.Name == "landuse")
             && (feature.Attributes[0].Key == "class"))
            {

              if (feature.Attributes[0].Value.Equals("residential"))
              {
                CurrentBrush = residential;
              }

              if (feature.Attributes[0].Value.Equals("commercial"))
              {
                CurrentBrush = commercial;
              }

              if (feature.Attributes[0].Value.Equals("retail"))
              {
                CurrentBrush = retail;
              }

              if (feature.Attributes[0].Value.Equals("industrial"))
              {
                CurrentBrush = industrial;
              }

              if (feature.Attributes[0].Value.Equals("railway"))
              {
                CurrentBrush = railway;
              }
            }

            if ((layer.Name == "park")
              && (feature.Attributes[0].Key == "class")
              && (feature.Attributes[0].Value.Equals("nature_reserve")))
            {
              CurrentBrush = forest;
            }

            TreeNode featurenode = new TreeNode(feature.Id);
            layernode.Nodes.Add(featurenode);

            featurenode.Nodes.Add(feature.GeometryType.ToString());

            foreach (List<Coordinate> coordlist in feature.Geometry)
            {
              TreeNode coordlistnode = new TreeNode("Coordinates");
              featurenode.Nodes.Add(coordlistnode);

              List<Point> points = new List<Point>();

              foreach (Coordinate c in coordlist)
              {
                var v = c.ToPosition(x, y, zoom, extent);
                string coord = string.Format("{0},{1} ({2},{3})", v.Latitude, v.Longitude, c.X, c.Y);
                TreeNode coordnode = new TreeNode(coord);
                coordlistnode.Nodes.Add(coordnode);
                points.Add(new Point((int)c.X, (int)c.Y));
              }

              if (points.Count > 1)
              {
                if (Transport == true)
                {
                  g.DrawLines(transportpen, points.ToArray());
                }
                else
                {
                  g.FillPolygon(CurrentBrush, points.ToArray());
                }

              }

              //TreeNode coord = new TreeNode(item.)
            }

            foreach (KeyValuePair<string, object> att in feature.Attributes)
            {
              TreeNode attNode = new TreeNode(att.Key);
              if (att.Key.Equals("class"))
              {
                featurenode.Text += " " + att.Value;
              }

              if (att.Key.Equals("name"))
              {
                featurenode.Text += ": " + att.Value;
              }

              featurenode.Nodes.Add(att.Key + ":" + att.Value);
              //Console.WriteLine("   -" + att.Key + ":" + att.Value);
            }
          }
          // layernode.ExpandAll();
        }

        tv.EndUpdate();

        g.Dispose();
        string spath = FilePath.GetPath(new vector3(x, y, zoom), "biome") + ".png";

        string sKey = "Key\n";
        sKey += ColToS("water", WaterColor);
        sKey += ColToS("grass", GrassColor);
        sKey += ColToS("forest", ForestColor);
        sKey += ColToS("industrial", Color.Brown);
        sKey += ColToS("residential", Color.Purple);
        sKey += ColToS("commercial", Color.Gray);
        sKey += ColToS("retail", Color.Orange);
        sKey += ColToS("railway", Color.LightGray);
        sKey += ColToS("road", Color.White);
        sKey += ColToS("building", Color.DarkGray);

        File.WriteAllText("data\\" + zoom + "\\biome\\key.txt", sKey);

        Bitmap lo = new Bitmap(256, 256);
        using (var gr = Graphics.FromImage(lo))
        {
          gr.DrawImage(bmp, 0, 0, lo.Width, lo.Height);
        }
        lo.Save(spath, ImageFormat.Png);
        bmp.Dispose();

      });

    }

    string ColToS(string title, Color c)
    {
      return string.Format("{0} {1},{2},{3} {4},{5},{6}\n", title, c.R, c.G, c.B, (float)c.R / 255.0, (float)c.G / 255.0, (float)c.B / 255.0);
    }

    public void DecodeGEOJSON(int x, int y, int zoom)
    {
      WebClient client = new WebClient();
      client.Headers[HttpRequestHeader.AcceptEncoding] = "json";
      string path = "http://{ip}:{port}/data/v3/{zoom}/{x}/{y}.geojson";
      path = path.Replace("{zoom}", zoom.ToString());
      path = path.Replace("{x}", x.ToString());
      path = path.Replace("{y}", y.ToString());
      path = path.Replace("{port}", FilePath.Port);
      path = path.Replace("{ip}", FilePath.IP);

      try
      {
        client.DownloadData(path);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return;
      }
      var responseStream = new GZipStream(client.OpenRead(path), CompressionMode.Decompress);
      //string = client.DownloadString(path);
      //var responseStream = new GZipStream(client.OpenRead(path), CompressionMode.Decompress);
      var reader = new StreamReader(responseStream);
      var textResponse = reader.ReadToEnd();
      string sPath = FilePath.GetPath(new vector3(x, y, zoom), "data") + ".json";
      File.WriteAllText(sPath, textResponse);
      //Console.WriteLine(textResponse);
    }
  }
}
