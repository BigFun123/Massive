using Massive;
using Massive.Events;
using Massive.Tools;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// An Astronomical body, Typically 1:1 scale planet
/// </summary>

//TILES

// tile number is the top left of the tile


namespace Massive.GIS
{
  //meters
  public class MAstroBody : MObject
  {
    public int ListIndex;
    public Vector3d Position;
    public Vector3d Velocity;
    public Vector3d Radius;
    public double Mass; //10^20
    public string TextureName;
    public bool HasAtmosphere = false;
    public double AtmosphereStart;
    public bool IsTemplate = false; //don't create mesh, textures
    public bool HasRings = false;
    public string Description;
    public double DistanceToAvatar = double.MaxValue;
    public double AvatarDistanceToSurface = double.MaxValue;
    public Vector3d DirectionToAvatar = Vector3d.Zero;
    public int ZoomLevel = 14;
    public Vector3d CurrentTileIndex;
    public MTerrainTile CurrentTile;

    //distance in m at which physics is generated for a tile. Landing on a tile with no physics means user falls through
    //if they fall through and physics is generated, they will be stuck underground.
    //TODO: either create smaller physics areas that gen faster, or lift user above ground if they're below.
    // This will mean we can't create terrain caves, but we can use other objects for caves
    public const double DistanceToPlanetThreshold = 2000; 

    private const double MinLatitude = -85.05112878;
    private const double MaxLatitude = 85.05112878;
    private const double MinLongitude = -180;
    private const double MaxLongitude = 180;

    public MTerrainHandler _terrainHandler;

    public double DistanceFromSun;

    public MAstroBody(string inName, string inDesc, double inMass,
      Vector3d inPos, Vector3d inRad, string sMat,
      bool inHasAtmosphere = false, double inAtmosStart = 0,
      bool inHasRings = false, bool inIsTemplate = true)
      : base(EType.AstroBody, inName)
    {
      Name = inName;
      Description = inDesc;
      Mass = inMass;
      Position = inPos;
      Radius = inRad;
      TextureName = sMat;
      HasAtmosphere = inHasAtmosphere;
      AtmosphereStart = inAtmosStart;
      HasRings = inHasRings;
      IsTemplate = inIsTemplate;

      /************************* HERE ******************************/
      MMessageBus.AvatarMovedEvent += MMessageBus_AvatarMovedEvent;
    }

    public override void Setup()
    {
      base.Setup();
      //_terrainHandler.Setup(this);
    }

    public void AddDynamicTerrain()
    {
      _terrainHandler = new MTerrainHandler(this);
      _terrainHandler.Setup(this);
      _terrainHandler.ZoomLevel = ZoomLevel;
      MScene.UtilityRoot.Add(_terrainHandler);
    }

    private void MMessageBus_AvatarMovedEvent(object sender, MoveEvent e)
    {
      if (_terrainHandler == null) return;

      CurrentTileIndex = GetTileFromPoint(e.Position);
      Vector3d LonLat = GetLonLatOnShere(e.Position);
      //Console.WriteLine(AvatarDistanceToSurface);      
      CurrentTile = _terrainHandler.GetTile((int)CurrentTileIndex.X, (int)CurrentTileIndex.Y, ZoomLevel);
      //If we're near enough to a planet to see the surface detail, show any nearby tiles
      if (AvatarDistanceToSurface < DistanceToPlanetThreshold)
      {        
        int NumTiles = Settings.MaxTerrains;
        _terrainHandler.GetPOI((int)CurrentTileIndex.X, (int)CurrentTileIndex.Y, LonLat);

        for (int y = -NumTiles; y <= NumTiles; y++)
        {
          for (int x = -NumTiles; x <= NumTiles; x++)
          {
            _terrainHandler.UpdateTileMesh((int)CurrentTileIndex.X + x, (int)CurrentTileIndex.Y + y, (int)CurrentTileIndex.Z, LonLat);            
          }
        }
      }      
    }

    /// <summary>
    // Given arbitrary point Point will return the nearest point on the surface
    /// </summary>
    /// <param name="Point"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public Vector3d GetNearestPointOnSurface(Vector3d Point, double offset = 1)
    {
      Vector3d dir = Point - Position;
      dir.Normalize();

      Vector3d Result = Point;
      if (Tag != null)
      {
        MModel model = (MModel)Tag;
        MPhysicsObject ph = (MPhysicsObject)model.FindModuleByType(EType.PhysicsObject);

        bool Success = false;

        MPhysics.Instance.RayCastRequest(Point, Position, this, (result) =>
        {
          Vector3d pt = result.Hitpoint;
          if (Success)
          {
            Result = pt + dir * offset;
          }
          else
          {
            Result = Position + dir * (Radius.X + offset);
          }
        });

      }
      return Result;
    }

    //point = arbitrary point in space
    public Vector3d GetNearestPointOnSphere(Vector3d Point, double offset = 0)
    {
      Vector3d dir = Point - Position;
      //Console.WriteLine(dir);
      dir.Normalize();
      return dir * (Radius.X + offset) + Position;
    }

    public Vector3d LatitudeLongitudeToTileId(float latitude, float longitude, int zoom)
    {
      var x = (int)Math.Floor((longitude + 180.0) / 360.0 * Math.Pow(2.0, zoom));
      var y = (int)Math.Floor((1.0 - Math.Log(Math.Tan(latitude * Math.PI / 180.0)
          + 1.0 / Math.Cos(latitude * Math.PI / 180.0)) / Math.PI) / 2.0 * Math.Pow(2.0, zoom));

      return new Vector3d(x, y, zoom);
    }

    //point = arbitrary point in space
    public Vector3d GetTileFromPoint(Vector3d Point)
    {
      Vector3d lonlat = GetLonLatOnShere(Point);
      int TilesPerZoom = (int)(256 * Math.Pow(2, ZoomLevel));

      double sinLatitude = Math.Sin(lonlat.Y * Math.PI / 180.0);
      double pixelX = ((lonlat.X + 180) / 360) * 256 * Math.Pow(2, ZoomLevel);
      double pixelY = (0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI)) * 256 * Math.Pow(2, ZoomLevel);

      int tileX = (int)Math.Floor(pixelX / 256);
      int tileY = (int)Math.Floor(pixelY / 256);

      Vector3d Result = new Vector3d(tileX, tileY, ZoomLevel);
      //Vector3d Result = new Vector3d(Math.Round(lonlat.X * 4096),Math.Round(lonlat.Y * 4096), ZoomLevel);
      return Result;
    }

    public double GetTileScaleInMeters(float latitude, int zoom)
    {
      return 40075000 * Math.Cos(MathHelper.DegreesToRadians(latitude)) / Math.Pow(2f, zoom + 8);
    }

    //point = arbitrary point in space
    public Vector3d GetLonLatOnShere(Vector3d point)
    {
      Vector3d uv = GetUVPointOnSphere(point);

      Vector3d LonLat = new Vector3d(uv.X * 360 - 180, (uv.Y * 180 - 90), 0);
      LonLat.Y = -LonLat.Y;
      //LonLat.Y = -LonLat.Y;
      if (LonLat.X > 180)
      {
        LonLat.X -= 360;
      }
      else if (LonLat.X < -180)
      {
        LonLat.X += 360;
      }
      //assert - 90 <= lat <= 90
      //assert - 180 <= lon <= 180
      //return lat, lon
      return LonLat;
    }

    //point = arbitrary point in space
    public Vector3d GetUVPointOnSphere(Vector3d Point)
    {
      Vector3d p = GetNearestPointOnSphere(Point) - Position;
      double x = (p.X) / (-1 * Radius.X);
      double y = (p.Y) / Radius.X;
      double z = (p.Z) / Radius.X;

      var u = (Math.Atan2(z, x) / (2 * Math.PI) + 0.5);
      //var v = ((Math.Acos(y) / (Math.PI))) ;
      var v = 0.5 - Math.Asin(y) / Math.PI;
      return new Vector3d(u, v, 0);
    }

    /// <summary>
    /// Converts a point from latitude/longitude WGS-84 coordinates (in degrees)
    /// into pixel XY coordinates at a specified level of detail.
    /// </summary>
    /// <param name="latitude">Latitude of the point, in degrees.</param>
    /// <param name="longitude">Longitude of the point, in degrees.</param>
    /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
    /// to 23 (highest detail).</param>
    /// <param name="pixelX">Output parameter receiving the X coordinate in pixels.</param>
    /// <param name="pixelY">Output parameter receiving the Y coordinate in pixels.</param>
    public void LatLongToPixelXY(double latitude, double longitude, int levelOfDetail, out int pixelX, out int pixelY)
    {
      latitude = Clip(latitude, MinLatitude, MaxLatitude);
      longitude = Clip(longitude, MinLongitude, MaxLongitude);

      double x = (longitude + 180) / 360;
      double sinLatitude = Math.Sin(latitude * Math.PI / 180);
      double y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

      uint mapSize = MapSize(levelOfDetail);
      pixelX = (int)Clip(x * mapSize + 0.5, 0, mapSize - 1);
      pixelY = (int)Clip(y * mapSize + 0.5, 0, mapSize - 1);
    }


    /// <summary>
    /// Converts a pixel from pixel XY coordinates at a specified level of detail
    /// into latitude/longitude WGS-84 coordinates (in degrees).
    /// </summary>
    /// <param name="pixelX">X coordinate of the point, in pixels.</param>
    /// <param name="pixelY">Y coordinates of the point, in pixels.</param>
    /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
    /// to 23 (highest detail).</param>
    /// <param name="latitude">Output parameter receiving the latitude in degrees.</param>
    /// <param name="longitude">Output parameter receiving the longitude in degrees.</param>
    public Vector3d PixelXYToLonLat(int pixelX, int pixelY, int Zoom)
    {
      double mapSize = MapSize(Zoom);
      double x = (Clip(pixelX, 0, mapSize - 1) / mapSize) - 0.5;
      double y = 0.5 - (Clip(pixelY, 0, mapSize - 1) / mapSize);

      Vector3d result = new Vector3d();
      result.X = 360 * x;
      result.Y = 90 - 360 * Math.Atan(Math.Exp(-y * 2 * Math.PI)) / Math.PI;
      result.Z = Zoom;
      return result;
    }

    /// <summary>
    /// Converts tile XY coordinates into pixel XY coordinates of the upper-left pixel
    /// of the specified tile.
    /// </summary>
    /// <param name="tileX">Tile X coordinate.</param>
    /// <param name="tileY">Tile Y coordinate.</param>
    /// <param name="pixelX">Output parameter receiving the pixel X coordinate.</param>
    /// <param name="pixelY">Output parameter receiving the pixel Y coordinate.</param>
    public void TileXYToPixelXY(int tileX, int tileY, out int pixelX, out int pixelY)
    {
      pixelX = tileX * 256;
      pixelY = tileY * 256;
    }


    /// <summary>
    /// Clips a number to the specified minimum and maximum values.
    /// </summary>
    /// <param name="n">The number to clip.</param>
    /// <param name="minValue">Minimum allowable value.</param>
    /// <param name="maxValue">Maximum allowable value.</param>
    /// <returns>The clipped value.</returns>
    private double Clip(double n, double minValue, double maxValue)
    {
      return Math.Min(Math.Max(n, minValue), maxValue);
    }



    /// <summary>
    /// Determines the map width and height (in pixels) at a specified level
    /// of detail.
    /// </summary>
    /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
    /// to 23 (highest detail).</param>
    /// <returns>The map width and height in pixels.</returns>
    public uint MapSize(int levelOfDetail)
    {
      return (uint)256 << levelOfDetail;
    }



    /// <summary>
    /// Determines the ground resolution (in meters per pixel) at a specified
    /// latitude and level of detail.
    /// </summary>
    /// <param name="latitude">Latitude (in degrees) at which to measure the
    /// ground resolution.</param>
    /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
    /// to 23 (highest detail).</param>
    /// <returns>The ground resolution, in meters per pixel.</returns>
    public double GroundResolution(double latitude, int levelOfDetail)
    {
      latitude = Clip(latitude, MinLatitude, MaxLatitude);
      return Math.Cos(latitude * Math.PI / 180) * 2 * Math.PI * Radius.X / MapSize(levelOfDetail);
    }



    /// <summary>
    /// Determines the map scale at a specified latitude, level of detail,
    /// and screen resolution.
    /// </summary>
    /// <param name="latitude">Latitude (in degrees) at which to measure the
    /// map scale.</param>
    /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
    /// to 23 (highest detail).</param>
    /// <param name="screenDpi">Resolution of the screen, in dots per inch.</param>
    /// <returns>The map scale, expressed as the denominator N of the ratio 1 : N.</returns>
    public double MapScale(double latitude, int levelOfDetail, int screenDpi)
    {
      return GroundResolution(latitude, levelOfDetail) * screenDpi / 0.0254;
    }


    /// <summary>
    /// Converts pixel XY coordinates into tile XY coordinates of the tile containing
    /// the specified pixel.
    /// </summary>
    /// <param name="pixelX">Pixel X coordinate.</param>
    /// <param name="pixelY">Pixel Y coordinate.</param>
    /// <param name="tileX">Output parameter receiving the tile X coordinate.</param>
    /// <param name="tileY">Output parameter receiving the tile Y coordinate.</param>
    public void PixelXYToTileXY(int pixelX, int pixelY, out int tileX, out int tileY)
    {
      tileX = pixelX / 256;
      tileY = pixelY / 256;
    }

    //
    public MTerrainBoundary GetTileBoundaryLonLat(int TileX, int TileY, int Zoom)
    {
      MTerrainBoundary tb = new MTerrainBoundary();

      tb.LonLatTL = PixelXYToLonLat(TileX * 256, TileY * 256, Zoom);
      tb.LonLatTR = PixelXYToLonLat((TileX + 1) * 256, TileY * 256, Zoom);
      tb.LonLatBL = PixelXYToLonLat((TileX) * 256, (TileY + 1) * 256, Zoom);
      tb.LonLatBR = PixelXYToLonLat((TileX + 1) * 256, (TileY + 1) * 256, Zoom);

      tb.TL = MGISTools.LonLatMercatorToPosition(tb.LonLatTL.X, tb.LonLatTL.Y, Radius.X) + Position;
      tb.TR = MGISTools.LonLatMercatorToPosition(tb.LonLatTR.X, tb.LonLatTR.Y, Radius.X) + Position;
      tb.BL = MGISTools.LonLatMercatorToPosition(tb.LonLatBL.X, tb.LonLatBL.Y, Radius.X) + Position;
      tb.BR = MGISTools.LonLatMercatorToPosition(tb.LonLatBR.X, tb.LonLatBR.Y, Radius.X) + Position;

      return tb;
    }

    //not working correctly?
    public Vector3d LonLatToUniPosition(double deglon, double deglat, double altitude)
    {
      /*
      Vector3d result = new Vector3d();
      double a = Radius.X;
      double e = 8.1819190842622e-2;

      double lat = MathHelper.DegreesToRadians(deglat);
      double lon = MathHelper.DegreesToRadians(deglon);
      double alt = altitude;
      double asq = Math.Pow(a, 2);
      double esq = Math.Pow(e, 2);

      double N = a / Math.Sqrt(1 - esq * Math.Pow(Math.Sin(lat), 2));

      result.X = (N + alt) * Math.Cos(lat) * Math.Cos(lon);
      result.Y = (N + alt) * Math.Cos(lat) * Math.Sin(lon);
      result.Z = ((1 - esq) * N + alt) * Math.Sin(lat);

      return result + Position;
      */

      double lat = deglat * MathHelper.DegreesToRadians(deglat);
      double lon = deglon * MathHelper.DegreesToRadians(deglon);
      double f = (1.0 / 298.257223563); //WGS84    
      double cosLat = Math.Cos(lat);
      double sinLat = Math.Sin(lat);
      double FF = Math.Pow((1.0 - f), 2);
      double C = 1 / Math.Sqrt(Math.Pow(cosLat, 2) + FF * Math.Pow(sinLat, 2));
      double S = C * FF;

      double x = (Radius.X * C + altitude) * cosLat * Math.Cos(lon);
      double y = (Radius.X * C + altitude) * cosLat * Math.Sin(lon);
      double z = (Radius.X * S + altitude) * sinLat;

      //      double ls = Math.Atan(Math.Pow((1 - f), 2) * Math.Tan(lat));
      //double x = Radius.X * Math.Cos(ls) * Math.Cos(lon) + altitude * Math.Cos(lat) * Math.Cos(lon);
      //double y = Radius.X * Math.Cos(ls) * Math.Sin(lon) + altitude * Math.Cos(lat) * Math.Sin(lon);
      //double z = Radius.X * Math.Sin(ls) + altitude * Math.Sin(lat);

      return new Vector3d(x, y, z) + Position;

    }

    public Vector3d LonLatToTilePos(double lon, double lat, int zoom)
    {
      Vector3d p = new Vector3d();
      p.X = (float)((lon + 180.0) / 360.0 * (1 << zoom));
      p.Y = (float)((1.0 - Math.Log(Math.Tan(lat * Math.PI / 180.0) +
        1.0 / Math.Cos(lat * Math.PI / 180.0)) / Math.PI) / 2.0 * (1 << zoom));
      p.Z = zoom;

      return p;
    }

    //return lon lat
    public Vector3d TileToLonLat(double tile_x, double tile_y, int zoom)
    {
      Vector3d p = new Vector3d();
      double n = Math.PI - ((2.0 * Math.PI * tile_y) / Math.Pow(2.0, zoom));

      p.X = (float)((tile_x / Math.Pow(2.0, zoom) * 360.0) - 180.0);
      p.Y = (float)(180.0 / Math.PI * Math.Atan(Math.Sinh(n)));

      return p;
    }

    double tile2lon(int x, int z)
    {
      return x / Math.Pow(2.0, z) * 360.0 - 180;
    }

    double tile2lat(int y, int z)
    {
      double n = Math.PI - (2.0 * Math.PI * y) / Math.Pow(2.0, z);
      return MathHelper.RadiansToDegrees(Math.Atan(Math.Sinh(n)));
    }

    /// <summary>
    /// USed for positioning in Astronimical coords e.g. NASA relative position (Right Ascension & Declination)
    /// </summary>
    /// <param name="RA_hours"></param>
    /// <param name="RA_minutes"></param>
    /// <param name="RA_seconds"></param>
    /// <param name="Dec_degrees"></param>
    /// <param name="Dec_minutes"></param>
    /// <param name="Dec_seconds"></param>
    /// <param name="DistanceLY"></param>
    /// <returns></returns>
    public Vector3d GetPositionFromRADECL(double RA_hours, double RA_minutes, double RA_seconds,
      double Dec_degrees, double Dec_minutes, double Dec_seconds, double DistanceLY)
    {
      double A = (RA_hours * 15) + (RA_minutes * 0.25) + (RA_seconds * 0.004166);
      double B = (Math.Abs(Dec_degrees) + (Dec_minutes / 60) + (Dec_seconds / 3600)) * Math.Sign(Dec_degrees);
      double C = DistanceLY;

      double X = (C * Math.Cos(B)) * Math.Cos(A);
      double Y = (C * Math.Cos(B)) * Math.Sin(A);
      double Z = C * Math.Sin(B);
      return new Vector3d(X, Y, Z);
    }
  }
}
