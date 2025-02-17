﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Massive.Tools;
using Massive.GIS;
using System.Threading;
using Massive.Events;
using Massive.Platform;

/// <summary>
/// A terrain tile is built from a 257x257 bitmap (encoded heightmap)
/// and texture map
/// The bitmap contains an extra row and column of pixels from the neighboring tile map,
/// and is generated by the TerrainGenerator
/// It follows Bing Standard Tile Addressing
/// </summary>

namespace Massive
{
  public class MTerrainTile : MMesh
  {
    public const double PHYSICS_ACTIVE_DISTANCE = 12000.0;

    Stopwatch stopwatch;
    public MTerrainBoundary Boundary;
    public int z_res = 257;
    public int x_res = 257;
    public bool IsSetup = false;
    public string ClosestCity = "";
    public MPOI ClosestPOI;
    public MPOI ClosestSuburb;

    public int TileX, TileY, ZoomLevel;

    List<MPOI> PointsOfInterest;
    List<MPOI> Suburbs;
    MTexture Heightmap;
    double[,] heights;
    
    

    public MTexture Biome;
    public MBiome _biome;
    //float[] Heights;
    BackgroundWorker _backgroundWorker = null;
    MAstroBody CurrentBody;
    MShader TerrainShader;
    MPhysicsObject _physics;
    public bool DoSetupPhysics = false;
    public bool PhysicsIsComplete = false; //true when all physics has been created

    MForest Forest;
    //public MGrassPlanter GrassPlanter;

    public MTerrainTile(int TX, int TY, int Zoom, double Radius)
      : base("TerrainSlice", EType.Terrain)
    {
      stopwatch = new Stopwatch();
      TileX = TX;
      TileY = TY;
      ZoomLevel = Zoom;
      //Heights = new float[] { x_res * z_res };
      PointsOfInterest = new List<MPOI>();
      Suburbs = new List<MPOI>();
      DistanceThreshold = 7000;

      _biome = new MBiome(x_res, z_res);

      Forest = new MForest();
      Forest.transform.Scale = new Vector3d(1, 1, 1);
      Forest.DistanceThreshold = 3000;
      Forest.CastsShadow = true;

      //GrassPlanter = new MGrassPlanter();

      CastsShadow = true;
      heights = new double[x_res, z_res];
      
     
      // Add(Forest);
      MScene.Background2.Add(Forest);
    }

    public override void Update()
    {
      base.Update();

      /*
      if (DoSetupPhysics == true)
      {
        DoSetupPhysics = false;
        SetupPhysics();
      }
      */
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      //Forest.transform.Position = this.transform.Position;
      if (Settings.DrawTerrains == false) return;
      base.Render(viewproj, parentmodel);
    }

    public void SetShader(MShader shader)
    {
      TerrainShader = shader;
    }

    public override void Dispose()
    {
      if (_backgroundWorker != null)
      {
        _backgroundWorker.CancelAsync();
      }
      if (Heightmap != null)
      {
        Heightmap.Dispose();
        Heightmap = null;
      }
      if (material != null)
      {
        material.Dispose();
        material = null;
      }

      if (Biome != null)
      {
        Biome.Dispose();
        Biome = null;
        _biome.Dispose();
      }

      if (MPhysics.Instance != null)
      {
        MPhysics.Instance.Remove(_physics);
      }

      MScene.Background2.Remove(Forest);
      Forest.Dispose();

      //TODO: Fix reentrant loop
      //Vertices = null;
      //Indices = null;


      base.Dispose();
    }

    public void Setup(MAstroBody body)
    {
      string sText = string.Format("Earth\\{0}\\biome\\{1}_{2}.png", ZoomLevel, TileX, TileY);
      sText = Path.Combine(Settings.TileDataPath, sText);
      if (!File.Exists(sText))
      {
        return;
      }
      CurrentBody = body;
      if (_backgroundWorker != null)
      {
        _backgroundWorker.CancelAsync();
      };

      Console.WriteLine("Memory:" + GC.GetTotalMemory(false));
      MMessageBus.LoadingStatus(this, "Loading:" + TileX + "," + TileY);
      _backgroundWorker = new BackgroundWorker();
      _backgroundWorker.DoWork += Bw_DoWork;
      _backgroundWorker.RunWorkerCompleted += Bw_RunWorkerCompleted;
      _backgroundWorker.WorkerSupportsCancellation = true;
      _backgroundWorker.RunWorkerAsync(body);
    }

    private void Bw_DoWork(object sender, DoWorkEventArgs e)
    {
      Globals.Tasks++;
      SetupMesh();
      if (_backgroundWorker.CancellationPending) return;
      CreateMaterial();
      LoadBiome();
      if (_backgroundWorker.CancellationPending) return;

     // LoadTexture();
      //if (_backgroundWorker.CancellationPending) return;

      LoadHeightMap();

      LoadMetaData((MAstroBody)e.Argument);

      ApplyHeightMap();

      DistanceFromAvatar = Vector3d.Distance(Globals.Avatar.GetPosition(), this.transform.Position);

      //stopwatch.Start();
      //MPhysicsObject po = new MPhysicsObject(this, "Terrain_collider", 0, MPhysicsObject.EShape.ConcaveMesh, false, this.transform.Scale);
      //stopwatch.Stop();
      //Console.WriteLine("Phyics Upload " + TileX + "," + TileY + " in " + stopwatch.ElapsedMilliseconds + "ms");
      if (Settings.DrawTrees == true)
      {
        Forest.PlantTrees(CurrentBody, this);
      }
    }



    private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (Vertices == null) return;
      //Globals.GUIThreadOwner.Invoke((MethodInvoker)delegate
      //  {
      int MSize = TexturedVertex.Size;
      GL.GenVertexArrays(1, out VAO);
      GL.GenBuffers(1, out VBO);
      GL.GenBuffers(1, out EBO);

      GL.BindVertexArray(VAO);
      GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
      GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * MSize, Vertices, BufferUsageHint.DynamicDraw);

      GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
      GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(int), Indices, BufferUsageHint.StaticDraw);

      // vertex positions
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, MSize, 0);
      // vertex normals
      GL.EnableVertexAttribArray(1);
      GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, MSize, sizeof(float) * 3);
      // vertex texture coords
      GL.EnableVertexAttribArray(2);
      GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, MSize, sizeof(float) * 6);

      //  });

      _backgroundWorker.Dispose();
      _backgroundWorker = null;
      VerticesLength = Vertices.Length;
      IndicesLength = Indices.Length;
      //Indices = null;
      // Vertices = null;
      IsSetup = true;

      if (Settings.TerrainPhysics == true)
      {
        MMessageBus.LoadingStatus(this, "Preparing:" + TileX + "," + TileY);
        SetupPhysics(); //must run on main thread
      }
    }

    void ApplyHeightMap()
    {
      //Random r = new Random(123);
      int i = 0;
      double d = z_res / (z_res + 1);
      for (int y = 0; y < z_res; y++)
      {
        //for (int x = -x_res / 2; x < x_res / 2; x++)
        for (int x = 0; x < x_res; x++)
        {
          int pos = y * x_res + x;
          float height = HeightColorToHeight(x, y);
          Vector3d normal = MassiveTools.Vector3dFromVector3(Vertices[i]._position) + transform.Position - CurrentBody.Position;
          normal.Normalize();
          Vertices[i]._position += MassiveTools.Vector3FromVector3d(normal) * height;
          Vertices[i]._normal = MassiveTools.Vector3FromVector3d(normal);
          heights[x, y] = height;
          ///Vertices[i]._position.Y = HeightColorToHeight(x, y);
          i++;
        }
      }
    }

    public void SetupPhysics()
    {
      if (IsSetup == false) return;
      if (_physics == null)
      {
        stopwatch.Start();
        Console.WriteLine("MTerrainTile.SetupPhysics " + TileX + "," + TileY);
        //string sText = string.Format("PHYSICS: Cache\\earth\\{0}\\biome\\{1}_{2}.png", ZoomLevel, TileX, TileY);                
        _physics = new MPhysicsObject(this, "Terrain_collider", 0, MPhysicsObject.EShape.ConcaveProxy, false, this.transform.Scale);
        stopwatch.Stop();
        Console.WriteLine("MTerrainTile.SetupPhysics " + TileX + "," + TileY + " completed in  " + stopwatch.ElapsedTicks);
        DoSetupPhysics = false;
      }
    }

    public override void Notify(string sMessage)
    {
      base.Notify(sMessage);
      if ( sMessage == "PhysicsProxyComplete")
      {
        PhysicsIsComplete = true;
      }
    }

    public void UpdateAvatarLonLat(Vector3d LonLat)
    {
      // Console.WriteLine(TileX + " : " + DistanceFromAvatar);

      if ((IsSetup == true) && (DistanceFromAvatar < PHYSICS_ACTIVE_DISTANCE) && (_physics == null))
      {
        DoSetupPhysics = true;
      }

      if (Settings.PointsOfInterest == true)
      {
        double Distance = 9999999999999999;
        foreach (MPOI p in PointsOfInterest)
        {
          //double d = Vector3d.Distance(LonLat, p.LonLat);
          double d = MGISTools.GetDistance(LonLat.Y, LonLat.X, p.LonLat.Y, p.LonLat.X);
          if (d < Distance)
          {
            ClosestPOI = p;
            Distance = d;
          }
        }

        Distance = 9999999999999999;
        foreach (MPOI p in Suburbs)
        {
          //double d = Vector3d.Distance(LonLat, p.LonLat);
          double d = MGISTools.GetDistance(LonLat.Y, LonLat.X, p.LonLat.Y, p.LonLat.X);
          if (d < Distance)
          {
            ClosestSuburb = p;
            Distance = d;
          }
        }

      }

    }

    public string GetInfo()
    {
      string sReturn = ClosestCity;

      if (ClosestSuburb != null)
      {
        sReturn += ":" + ClosestSuburb.Name;
      }

      if (ClosestPOI != null)
      {
        sReturn += ":" + ClosestPOI.Name;
      }

      return sReturn;
    }

    public void SetBoundary(MTerrainBoundary b)
    {
      Boundary = b;
    }

    //Heightmaps are RGB packed, generated by Generator app
    float HeightColorToHeight(int x, int y)
    {
      int pos = (y * x_res + x) * 4;
      //byte r = Heightmap.rgbValues[pos + 2];
      //byte g = Heightmap.rgbValues[pos + 1];
      //byte b = Heightmap.rgbValues[pos];
      if (Heightmap == null) return 0;
      float[] col = Heightmap.GetPixel(x, y);
      byte r = (byte)(col[0] * 255);
      byte g = (byte)(col[1] * 255);
      byte b = (byte)(col[2] * 255);
      float height = -10000 + ((r * 256 * 256 + g * 256 + b) * 0.1f);

      return height;
    }

    public void CreateMaterial()
    {
      material = new MMaterial("TerrainMaterial");
      MScene.MaterialRoot.Add(material);
      material.AddShader(TerrainShader);
      SetMaterial(material);
    }

    public void LoadBiome()
    {
      string sText = string.Format("earth\\{0}\\biome\\{1}_{2}.png", ZoomLevel, TileX, TileY);
      sText = Path.Combine(Settings.TileDataPath, sText);
      if (File.Exists(sText))
      {        
        Biome = new MTexture(TileX + "," + TileY + " RGB");
        Biome.Readable = true;
        Biome.LoadTextureData(sText);
        _biome.InitFromBitmap(Biome);
        Biome.DoAssign = true;
        Biome._TextureWrapMode = TextureWrapMode.ClampToEdge;
        material.SetDiffuseTexture(Biome);
      }
    }

    public void LoadTexture()
    {
      //string sText = string.Format("earth\\{0}\\continuity\\{1}_{2}.jpg", ZoomLevel, TileX, TileY);
      string sText = string.Format("earth\\{0}\\biome\\{1}_{2}.png", ZoomLevel, TileX, TileY);
      sText = Path.Combine(Settings.TileDataPath, sText);
      if (File.Exists(sText))
      {
        //MTexture rgb = Globals.TexturePool.GetTexture(sText);
        MTexture rgb = new MTexture(TileX + "," + TileY + " RGB");
        rgb.Readable = false;
        rgb.LoadTextureData(sText);
        rgb.DoAssign = true;
        rgb._TextureWrapMode = TextureWrapMode.ClampToEdge;
        if (material != null)
        {
          material.SetDiffuseTexture(rgb);
        }
      }

      //string sText2 = string.Format("earth\\{0}\\biome\\{1}_{2}.png", ZoomLevel, TileX, TileY);
      //string sText2 = string.Format("earth\\{0}\\biome\\{1}_{2}.png", ZoomLevel, TileX, TileY);
      //string sText = string.Format("earth\\{0}\\biome\\{1}_{2}.png", ZoomLevel, TileX, TileY);
      string sText2 = Path.Combine(MFileSystem.AssetsPath, "Textures\\terrain\\dirt.jpg");
      if (File.Exists(sText2))
      {
        //MTexture rgb = Globals.TexturePool.GetTexture(sText);
        MTexture rgb = new MTexture(TileX + "," + TileY + " DIRT");
        rgb.Readable = false;
        rgb.LoadTextureData(sText2);
        rgb.DoAssign = true;
        rgb._TextureUnit = TextureUnit.Texture1;
        rgb._TextureWrapMode = TextureWrapMode.Repeat;
        if (material != null)
        {
          material.SetMultiTexture(rgb);
        }
      }
    }

    public void LoadHeightMap()
    {
      string sHeight = string.Format("earth\\{0}\\continuity\\{1}_{2}.png", ZoomLevel, TileX, TileY);
      sHeight = Path.Combine(Settings.TileDataPath, sHeight);
      if (File.Exists(sHeight))
      {
        Heightmap = new MTexture("Heightmap");
        Heightmap.LoadTextureData(sHeight);
        if (Heightmap != null)
        {
          Heightmap.DoAssign = true;
        }
      }
    }

    void AddPOI(string sName, string sclass, double lon, double lat, MAstroBody _body)
    {
      MPOI poi = new MPOI();
      poi.sClass = sclass;
      poi.Position = MGISTools.LonLatMercatorToPosition(lon, lat, _body.Radius.X)
                 + _body.Position;
      poi.LonLat = new Vector3d(lon, lat, 0);
      poi.Name = sName;
      if (sclass.Equals("suburb"))
      {
        Suburbs.Add(poi);
      }
      if (sclass.Equals("minor"))
      {
        PointsOfInterest.Add(poi);
      }
      if (sclass.Equals("tertiary"))
      {
        PointsOfInterest.Add(poi);
      }
      if (sclass.Equals("major"))
      {
        PointsOfInterest.Add(poi);
      }
      if (sclass.Equals("primary"))
      {
        PointsOfInterest.Add(poi);
      }
      if (sclass.Equals("highway"))
      {
        PointsOfInterest.Add(poi);
      }
      if (sclass.Equals("stream"))
      {
        PointsOfInterest.Add(poi);
      }

    }

    public void LoadMetaData(MAstroBody _body)
    {
      string path = string.Format(@"earth\{0}\data\{1}_{2}", ZoomLevel, TileX, TileY) + ".json";
      path = Path.Combine(Settings.TileDataPath, path);
      if (File.Exists(path))
      {
        string sJSON = File.ReadAllText(path);
        try
        {
          //todo query against results directly, using line-proximity
          FeatureCollection results = JsonConvert.DeserializeObject<FeatureCollection>(sJSON);

          //FeatureCollection results = (FeatureCollection) 
          if (results != null)
          {
            var features = results.Features.Select(x => x)
              .Where(x => x.Properties.Keys.Contains("class") &&
              (x.Properties.Values.Contains("suburb"))
              || (x.Properties.Values.Contains("city"))
              || (x.Properties.Values.Contains("primary"))
              || (x.Properties.Values.Contains("minor"))
              );

            foreach (Feature f in features)
            {
              if (!f.Properties.ContainsKey("name"))
              {
                continue;
              }

              if (f.Properties["class"].Equals("city"))
              {
                ClosestCity = f.Properties["name"].ToString();
                continue;
              }

              if (f.Geometry.Type == GeoJSON.Net.GeoJSONObjectType.Point)
              {
                Point pt = (Point)f.Geometry;
                AddPOI(f.Properties["name"].ToString(), f.Properties["class"].ToString(),
                  pt.Coordinates.Longitude,
                  pt.Coordinates.Latitude, _body);
              }

              if (f.Geometry.Type == GeoJSON.Net.GeoJSONObjectType.LineString)
              {
                LineString ls = (LineString)f.Geometry;
                foreach (Position p in ls.Coordinates)
                {
                  AddPOI(f.Properties["name"].ToString(), f.Properties["class"].ToString(),
                  p.Longitude,
                  p.Latitude, _body);
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
      }
    }

    public double ElevationAtPoint(double x, double y)
    {
      if (x >= x_res-1) return 0;
      if (y >= z_res-1) return 0;
      if (x == 0.0) return 0;
      int x1 = (int)Math.Floor(x);
      int x2 = (int)Math.Ceiling(x);
      int y1 = (int)Math.Floor(y);
      int y2 = (int)Math.Ceiling(y);      

      double q11 = ElevationAtVertex(x1, y1);
      double q12 = ElevationAtVertex(x1, y2);
      double q21 = ElevationAtVertex(x2, y1);
      double q22 = ElevationAtVertex(x2, y2);

      double r1 = ((x2 - x) / (x2 - x1)) * q11 + ((x - x1) / (x2 - x1)) * q21;
      double r2 = ((x2 - x) / (x2 - x1)) * q12 + ((x - x1) / (x2 - x1)) * q22;

      double result = ((y2 - y) / (y2 - y1)) * r1 + ((y - y1) / (y2 - y1)) * r2;
      if (double.IsNaN(result)) return 0;
      return result;
    }

    public double ElevationAtVertex(int x, int z)
    {
      if (x < 0) return 0;
      if (x > x_res) return 0;
      if (z < 0) return 0;
      if (z > z_res) return 0;
      return heights[x, z];
    }

    public Vector3d GetInterpolatedPointOnSurfaceFromGrid(Vector3d grid)
    {
      double y = ElevationAtPoint(grid.X, grid.Z);
      Vector3d res = new Vector3d(grid.X, y, grid.Z);
      return res;

      /*
      Vector3d Floor = new Vector3d(Math.Round(grid.X), Math.Round(grid.Y), Math.Round(grid.Z));
      Vector3d Right = new Vector3d(Math.Round(grid.X+1), Math.Round(grid.Y), Math.Round(grid.Z));
      Vector3d BRight = new Vector3d(Math.Round(grid.X + 1), Math.Round(grid.Y), Math.Round(grid.Z+1));

      Vector3d pt1 = GetPointOnSurfaceFromGrid(Floor);
      Vector3d pt2 = GetPointOnSurfaceFromGrid(Right);
      Vector3d pt3 = GetPointOnSurfaceFromGrid(BRight);

      double fraction = (double )grid.X;
      int iPart = (int)fraction;
      double dPart = fraction % 1.0;
      Vector3d res = Vector3d.Lerp(pt1, pt2, dPart);

      fraction = (double)grid.Z;
      double dPartZ = fraction % 1.0;
      res = Vector3d.Lerp(res, pt3, dPart);
*/

      //return res;
    }

    public Vector3d GetInterpolatedPointOnSurfaceFromGrid2(Vector3d grid)
    {

      Vector3d Left = new Vector3d(Math.Floor(grid.X), Math.Round(grid.Y), Math.Floor(grid.Z));
      Vector3d Right = new Vector3d(Math.Ceiling(grid.X), Math.Round(grid.Y), Math.Floor(grid.Z));
      Vector3d BLeft = new Vector3d(Math.Floor(grid.X), Math.Round(grid.Y), Math.Ceiling(grid.Z));
      Vector3d BRight = new Vector3d(Math.Ceiling(grid.X), Math.Round(grid.Y), Math.Ceiling(grid.Z));

      Vector3d pt1 = GetPointOnSurfaceFromGrid(Left);
      Vector3d pt2 = GetPointOnSurfaceFromGrid(Right);

      Vector3d pt3 = GetPointOnSurfaceFromGrid(BLeft);
      Vector3d pt4 = GetPointOnSurfaceFromGrid(BRight);

      double fraction = (double)grid.X;
      int iPart = (int)fraction;
      double dPart = fraction % 1.0;
      Vector3d x1 = Vector3d.Lerp(pt1, pt2, dPart);
      Vector3d x2 = Vector3d.Lerp(pt3, pt4, dPart);

      fraction = (double)grid.Z;
      double dPartZ = fraction % 1.0;
      Vector3d res = Vector3d.Lerp(x1, x2, dPartZ);
      return res;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pt">an x z index to a vertex (0-255)</param>
    /// <returns></returns>
    public Vector3d GetPointOnSurfaceFromGrid(Vector3d grid)
    {
      int index = (int)(grid.Z * z_res + grid.X);
      if (index >= Vertices.Length) index = 0;
      if (index < 0) index = 0;

      Vector3d pi = MassiveTools.Vector3dFromVector3(Vertices[index]._position);
      //Vector3d zpl = Vector3d.Lerp(Boundary.TL, Boundary.BL, pt.Z);
      //Vector3d zpr = Vector3d.Lerp(Boundary.TR, Boundary.BR, pt.Z);
      //Vector3d p = Vector3d.Lerp(zpr, zpl, pt.X ) - transform.Position;
      return pi;
    }

    public Vector3d RayCastOnSurface(Vector3d from)
    {
      Vector3d Result = new Vector3d();
      MRaycastTask rt = MScene.Physics.RayCast(from, CurrentBody.Position);
      Result = rt.Hitpoint;

      return Result;
    }

   

    public void SetupMesh()
    {
      Vertices = new TexturedVertex[x_res * z_res];
      Indices = new int[6 * x_res * z_res];
      int i = 0;

      for (float z = 0; z < z_res; z++)
      {
        Vector3d zpl = Vector3d.Lerp(Boundary.TL, Boundary.BL, z / (float)(z_res - 1));
        Vector3d zpr = Vector3d.Lerp(Boundary.TR, Boundary.BR, z / (float)(z_res - 1));
        for (double x = x_res; x > 0; x--)
        {
          Vector3d xp = Vector3d.Lerp(zpr, zpl, x / (float)(x_res - 1)) - transform.Position;
          BoundingBox.Expand(xp);
          // Console.WriteLine("x:" + x + ",z:" + z + " = " + xp);
          Vertices[i]._position.X = (float)(xp.X);
          Vertices[i]._position.Y = (float)(xp.Y);
          Vertices[i]._position.Z = (float)(xp.Z);
          Vertices[i]._normal.X = 0;
          Vertices[i]._normal.Y = 1;
          Vertices[i]._normal.Z = 0;
          Vertices[i]._textureCoordinate = new Vector2((float)(x_res - x) / (float)(x_res - 1), (float)z / (float)(z_res - 1));
          i++;
        }
      }

      i = 0;
      for (int y = 0; y < z_res - 1; y++)
      {
        for (int x = 0; x < x_res - 1; x++)
        {
          Indices[i++] = (y + 0) * x_res + x;
          Indices[i++] = (y + 1) * x_res + x;
          Indices[i++] = (y + 0) * x_res + x + 1;

          Indices[i++] = (y + 0) * x_res + x + 1;
          Indices[i++] = (y + 1) * x_res + x;
          Indices[i++] = (y + 1) * x_res + x + 1;
        }
      }
      //base.Setup();
    }
  }
}
