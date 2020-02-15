using Massive;
using Massive.Events;
using Massive.Tools;
using Massive.GIS;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * Manages a group of terrain tiles on an AstroBody
 * Also triggers forest tree planting on active tile
 * 
 * */

namespace Massive
{
  public class MTerrainHandler : MObject
  {
    public int ZoomLevel;
    MSceneObject Gnomon;
    Vector3d LastPoint = Vector3d.Zero;
    MAstroBody CurrentBody;   
    MShader TerrainShader;
    int TileCount = 0;

    Dictionary<string, MTerrainTile> Tiles;
    //int NumTiles = 64;

    MObject Container;
    
    bool IsSetup = false;

    public MTerrainHandler(MAstroBody body) :
      base(EType.Other, "TerrainHandler")
    {
      CurrentBody = body;
      Tiles = new Dictionary<string, MTerrainTile>();
      Container = new MObject(EType.Other, "container");
    }

    public void Setup(MAstroBody b)
    {
      CurrentBody = b;     

     // TerrainShader = Helper.CreateShader(MShader.TERRAIN_SHADER);

      TerrainShader = new MShader(MShader.TERRAIN_SHADER);
      TerrainShader.Load("default_v.glsl",
        "Terrain\\terrain_f.glsl",
        "",
        ""
        );
      TerrainShader.Bind();
      TerrainShader.SetInt("material.diffuse", MShader.LOCATION_DIFFUSE);
      TerrainShader.SetInt("material.specular", MShader.LOCATION_SPECULAR);
      TerrainShader.SetInt("material.multitex", MShader.LOCATION_MULTITEX);
      TerrainShader.SetInt("material.normalmap", MShader.LOCATION_NORMALMAP);
      TerrainShader.SetInt("material.shadowMap", MShader.LOCATION_SHADOWMAP);

      base.Setup();
    }

    string TerrainNumber(int tx, int ty)
    {
      return "" + tx + "," + ty;
    }

    string GetInfo(int tx, int ty)
    {
      if (Tiles == null) return "";
      if (CurrentBody == null) return "";

      string TileNum = TerrainNumber(tx, ty);

      if (Tiles.ContainsKey(TileNum))
      {
        return Tiles[TileNum].GetInfo();
      }
      return "";
    }

    public MTerrainTile GetTile(int TileX, int TileY, int Zoom)
    {
      string TileNum = TerrainNumber(TileX, TileY);
      if (!Tiles.ContainsKey(TileNum))
      {
        InitializeTile(TileNum, TileX, TileY, Zoom);
      }
      return Tiles[TileNum];
    }

    public void UpdateTileMesh(int tx, int ty, int zoom, Vector3d AvatarPosLonLat)
    {
      if (Tiles == null) return;
      if (CurrentBody == null) return;

      string TileNum = TerrainNumber(tx, ty);
      if (!Tiles.ContainsKey(TileNum))
      {
        InitializeTile(TileNum, tx, ty, ZoomLevel);
      }
    }

    public void GetPOI(int tx, int ty, Vector3d AvatarPosLonLat)
    {
      string TileNum = TerrainNumber(tx, ty);
      if (!Tiles.ContainsKey(TileNum)) return;
      MTerrainTile tile = Tiles[TileNum];
      Tiles[TileNum].UpdateAvatarLonLat(AvatarPosLonLat);
    }

      public string GetTileInfo(Vector3d TileXYZ)
    {
      string TileNum = TerrainNumber((int)TileXYZ.X, (int)TileXYZ.Y);
      if (Tiles.ContainsKey(TileNum))
      {
        return Tiles[TileNum].GetInfo();
      }
      return "";
    }

    void InitializeTile(string TileNum, int tx, int ty, int zoom)
    {
      TileCount++;
      Console.WriteLine("Tile:" + TileCount + " Initialize " + tx + "," + ty);

      MTerrainTile tile = new MTerrainTile(tx, ty, ZoomLevel, CurrentBody.Radius.X);
      tile.SetShader(TerrainShader);
      Tiles.Add(TileNum, tile);
      MScene.Background.Add(Tiles[TileNum]);
      MTerrainBoundary tb = CurrentBody.GetTileBoundaryLonLat(tx, ty, ZoomLevel);      
      double metersperpixel = CurrentBody.GroundResolution(tb.LonLatTL.Y, ZoomLevel);

      //Tiles[TileNum].transform.Scale = new Vector3d(256*metersperpixel, 1, 256 *metersperpixel);
      //Vector3d ll = CurrentBody.TileToLonLat(tx, ty, zoom);
      Vector3d ll = tb.LonLatTL;
      //Vector3d pos = CurrentBody.LonLatToUniPosition(ll.X, ll.Y, 0);
      Vector3d pos = MGISTools.LonLatMercatorToPosition(ll.X, ll.Y, CurrentBody.Radius.X) + CurrentBody.Position;
      Tiles[TileNum].transform.Position = pos;
      Tiles[TileNum].SetBoundary(tb);
      Tiles[TileNum].Setup(CurrentBody);
    }

   

    Vector3d TileSizeAtPoint(Vector3d p)
    {
      return new Vector3d(10000, 1, 10000);
    }

    Quaterniond Perp(Vector3d dir)
    {
      double angle = Math.Atan2(dir.X, dir.Z); // Note: I expected atan2(z,x) but OP reported success with atan2(x,z) instead! Switch around if you see 90° off.      
      Quaterniond q = new Quaterniond(0, 1 * Math.Sin(angle / 2), 0, Math.Cos(angle / 2));
      q *= Quaterniond.FromEulerAngles(0, 0, 90 * Math.PI / 180.0);
      return q;
    }

    Vector3d GetQuantizedPoint(Vector3d p, Vector3d origin, double xrot, double yrot)
    {
      Vector3d delta = p - origin;
      Matrix4d rotx = Matrix4d.CreateFromQuaternion(
        Quaterniond.FromEulerAngles(xrot * Math.PI / 180.0, yrot * Math.PI / 180.0, 0)
        );
      Matrix4d pos = Matrix4d.CreateTranslation(delta);

      Matrix4d result = pos * rotx;
      Vector3d vresult = result.ExtractTranslation() + origin;
      return vresult;
    }

    public void CullTiles()
    {
      foreach ( KeyValuePair<string, MTerrainTile> kv in Tiles.ToArray())
      {
        if ( kv.Value.DistanceFromAvatar > kv.Value.DistanceThreshold)
        {
          MScene.Background.Remove(kv.Value);
          Tiles.Remove(kv.Key);
          TileCount--;
          kv.Value.Dispose();
        }
      }
    }

    void ShowCoords()
    {
      //Console.WriteLine("Dist:" + Vector3d.Distance(Globals.Avatar.GetPosition(), Tiles[2257, 2458].transform.Position));
      //Vector3d ap = Globals.Avatar.GetPosition();
      //Vector3d tp = CurrentBody.GetTileFromPoint(ap);   
      // Console.WriteLine("tx:" + tp.X + " ty:" + tp.Y + " Zoom:" + tp.Z);
      //Console.WriteLine("lat:" + tp.X + " lon:" + tp.Y + " Zoom:" + tp.Z);

    }

    void CalcPlanes()
    {
      MAstroBody planet = MPlanetHandler.CurrentNear;
      if (planet != null)
      {
        if (CurrentBody == planet) return;
        CurrentBody = planet;
        Vector3d pt = planet.GetNearestPointOnSurface(Globals.Avatar.GetPosition() + Globals.Avatar.Forward() * 10);
        //Forest.SetStart(GetQuantizedPoint(pt, planet.Position, 0, 0), planet);

        // Gnomon.SetPosition(pt);        
       // Console.WriteLine(Vector3d.Distance(pt, Globals.Avatar.GetPosition()));
      }
    }
  }
}
