using Massive;
using Massive.GIS;
using Massive.Tools;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Controllers
{
  public class NavigationBarDecoder
  {
    //takes a string as input and tries to figure out the physical location of that thing.
    // e.g. cape town waterfront takes you there.
    // -18.33,33.00 takes you to that lon/lat
    // 10,20,20 for xyz coords
    public Vector3d Decode(string sText)
    {
      Vector3d v = Globals.Avatar.GetPosition(); //contingency if we don't find the position

      bool success = false;
      Vector3d vTemp = Vector3d.Zero;

      success = DecodeXYZ(sText, out vTemp)
        || DecodeLonLat(sText, out vTemp)
        || DecodePOI(sText, out vTemp)
        || DecodeZone(sText, out vTemp);

      if (success == true)
      {
        return vTemp;
      }
      else
      {
        return v;
      }
    }

    bool DecodePOI(string sText, out Vector3d v)
    {
      bool found = false;
      Vector3d vt = Vector3d.Zero;
      v = vt;

      foreach ( MAstroBody b in MPlanetHandler.Bodies)
      {
        if (b.Name.ToLower().Contains(sText.ToLower())) {          
          v = b.Position + b.Radius*1.1;
          found = true;
          break;
        }
      }
      return found;
    }

    bool DecodeZone(string sText, out Vector3d v)
    {
      Vector3d vt = Vector3d.Zero;
      v = vt;
      return false;
    }

    bool DecodeXYZ(string sText, out Vector3d v)
    {
      Vector3d vt = Vector3d.Zero;
      string[] parts = sText.Split(',');
      if (parts.Length == 3)
      {
        if (GetNumbers(parts, out vt) == true)
        {
          v = vt;
          return true;
        }
      }
      v = vt;
      return false;
    }

    // coordinates are entered lat lon, so will be flipped
    bool DecodeLonLat(string sText, out Vector3d v)
    {
      Vector3d vt = Vector3d.Zero;      

      string[] parts = sText.Split(',');
      if (parts.Length == 2)
      {
        MAstroBody ab = MPlanetHandler.CurrentNear;
        double radius = 0;
        if (ab != null)
        {
          radius = ab.Radius.X;
        }        
        if (GetNumbers(parts, out vt) == true)
        { 
          //y = lon, x = lat
          Vector3d vr = MGISTools.LonLatMercatorToPosition(vt.Y, vt.X, radius + 1000) + ab.Position;
          v = vr;
          return true;
        }
      }

      v = vt;
      return false;
    }

    bool GetNumbers(string[] parts, out Vector3d v)
    {
      bool success = true;
      double d1 = 0;
      double d2 = 0;
      double d3 = 0;

      if (double.TryParse(parts[0], out d1) == false) success = false;

      if (parts.Length > 1)
      {
        if (double.TryParse(parts[1], out d2) == false) success = false;
      }

      if (parts.Length > 2)
      {      
        if (double.TryParse(parts[2], out d3) == false) success = false;
      }
      v = new Vector3d(d1, d2, d3);
      return success;

    }
  }
}
