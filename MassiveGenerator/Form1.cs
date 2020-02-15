using Mapbox.Vector.Tile;
using MassiveGenerator.src;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MassiveGenerator
{
  public partial class Form1 : Form
  {
    HeightMapGenerator mapgen = new HeightMapGenerator();
    BackgroundWorker _worker;
    BackgroundWorker _contworker;
    public Form1()
    {
      InitializeComponent();
      Directory.CreateDirectory("data");
      Directory.CreateDirectory(@"data\rgb");
      Directory.CreateDirectory(@"data\height");
      Directory.CreateDirectory(@"data\mesh");
      mapgen.Complete += Mapgen_Complete;
    }

    private void Mapgen_Complete(object sender, CompleteEvent e)
    {
      RGBBox.ImageLocation = e.rgb;
      DestBox.ImageLocation = e.simple;
      StatusLabel.Text = "Complete";
    }

    private void DownloadButton_Click(object sender, EventArgs e)
    {
      StatusLabel.Text = "Downloading...";
      int zoom = int.Parse(Zoom.Text);
      int x = int.Parse(xpos.Text);
      int y = int.Parse(ypos.Text);
      mapgen.Download(zoom, x, y);
      mapTileInspector1.Process(x, y, zoom);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      //mapgen.CreateMesh(new vector3(0, 0, 0));
    }

    private void Doanload3x3_Click(object sender, EventArgs e)
    {
      int zoom = int.Parse(Zoom.Text);
      int xp = int.Parse(xpos.Text);
      int yp = int.Parse(ypos.Text);
      _worker = new BackgroundWorker();
      _worker.DoWork += Bw_DoWork;
      _worker.WorkerSupportsCancellation = true;
      _worker.WorkerReportsProgress = true;

      WorkerInfo wi = new WorkerInfo();
      wi.centertile = new vector3(xp, yp, zoom);
      wi.NumberOfTiles = 1;
      _worker.RunWorkerAsync(wi);
      _worker.ProgressChanged += Bw_ProgressChanged;
    }

    private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      StatusLabel.Text = e.ProgressPercentage.ToString();
    }

    private void Bw_DoWork(object sender, DoWorkEventArgs e)
    {
      WorkerInfo wi = (WorkerInfo)e.Argument;
      vector3 v = wi.centertile;
      int xp = (int)v.x;
      int yp = (int)v.y;
      int zoom = (int)v.z;

      int NumSquare = wi.NumberOfTiles;

      int progress = 0;
      for (int x = -NumSquare; x < NumSquare + 1; x++)
      {
        for (int y = -NumSquare; y < NumSquare + 1; y++)
        {
          progress++;
          _worker.ReportProgress(progress);
          mapgen.Download(zoom, x + xp, y + yp);
          mapTileInspector1.Process(x + xp, y + yp, zoom);
          mapTileInspector1.Decode(x + xp, y + yp, zoom);
        }
      }
    }

    private void Download6x6_Click(object sender, EventArgs e)
    {
      int zoom = int.Parse(Zoom.Text);
      int xp = int.Parse(xpos.Text);
      int yp = int.Parse(ypos.Text);
      _worker = new BackgroundWorker();
      _worker.DoWork += Bw_DoWork;
      _worker.WorkerSupportsCancellation = true;
      _worker.WorkerReportsProgress = true;

      WorkerInfo wi = new WorkerInfo();
      wi.centertile = new vector3(xp, yp, zoom);
      wi.NumberOfTiles = 5;
      _worker.RunWorkerAsync(wi);
      _worker.ProgressChanged += Bw_ProgressChanged;
    }

    private void Download12x12_Click(object sender, EventArgs e)
    {
      int zoom = int.Parse(Zoom.Text);
      int xp = int.Parse(xpos.Text);
      int yp = int.Parse(ypos.Text);
      _worker = new BackgroundWorker();
      _worker.DoWork += Bw_DoWork;
      _worker.WorkerSupportsCancellation = true;
      _worker.WorkerReportsProgress = true;

      WorkerInfo wi = new WorkerInfo();
      wi.centertile = new vector3(xp, yp, zoom);
      wi.NumberOfTiles = 10;
      _worker.RunWorkerAsync(wi);
      _worker.ProgressChanged += Bw_ProgressChanged;

    }

    private void button3_Click(object sender, EventArgs e)
    {
      int zoom = int.Parse(Zoom.Text);
      int x = int.Parse(xpos.Text);
      int y = int.Parse(ypos.Text);

      mapTileInspector1.Process(x, y, zoom);
    }

    private void Continuity_Click(object sender, EventArgs e)
    {
      StatusLabel.Text = "Stitching...";

      int zoom = int.Parse(Zoom.Text);
      int x = int.Parse(xpos.Text);
      int y = int.Parse(ypos.Text);
      _contworker = new BackgroundWorker();
      _contworker.DoWork += Bw_DoWorkCont;
      _contworker.RunWorkerCompleted += _worker_RunWorkerCompleted;
      _contworker.WorkerSupportsCancellation = true;
      _contworker.WorkerReportsProgress = true;

      WorkerInfo wi = new WorkerInfo();
      wi.centertile = new vector3(x, y, zoom);
      wi.NumberOfTiles = 10;
      _contworker.RunWorkerAsync(wi);
      _contworker.ProgressChanged += Bw_ProgressChanged;
    }

    private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      StatusLabel.Text = "Complete";
    }

    private void Bw_DoWorkCont(object sender, DoWorkEventArgs e)
    {
      WorkerInfo wi = (WorkerInfo)e.Argument;
      vector3 v = wi.centertile;
      int xp = (int)v.x;
      int yp = (int)v.y;
      int zoom = (int)v.z;

      int NumSquare = wi.NumberOfTiles;

      int progress = 0;
      for (int x = -NumSquare; x < NumSquare + 1; x++)
      {
        for (int y = -NumSquare; y < NumSquare + 1; y++)
        {
          progress++;
          _contworker.ReportProgress(progress);
          ContinuityMap c = new ContinuityMap("rgb", 512);
          c.Combine(x + xp, y + yp, zoom, ".jpg");
          c = new ContinuityMap("height", 256);
          c.Combine(x + xp, y + yp, zoom, ".png");
        }
      }
    }

    private void BiomeButton_Click(object sender, EventArgs e)
    {


      int zoom = int.Parse(Zoom.Text);
      int x = int.Parse(xpos.Text);
      int y = int.Parse(ypos.Text);

      Directory.CreateDirectory("data\\14\\biome");
      string path = FilePath.GetPath(new vector3(x, y, zoom), "biome") + ".png";
      //mapTileInspector1.MakeBiome(x, y, zoom);
      mapTileInspector1.Decode(x, y, zoom);

      RGBBox.ImageLocation = path;
    }


  }
}
