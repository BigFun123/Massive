using Massive;
using Massive.Network;
using Massive.Platform;
using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Tools
{
  public static class MassiveTools
  {
    public static bool IsURL(string sPath)
    {
      if ((sPath.ToLower().StartsWith("www.")) || (sPath.ToLower().StartsWith("http")))
      {
        return true;
      }
      return false;
    }

    public static string GetCachePath(string URL)
    {
      string extension = ".jpg";
      if (URL.EndsWith(".png")) extension = ".png";
      return Path.Combine(MFileSystem.CachePath, Helper.HashString(Globals.UserAccount.UserID + "_" + URL)) + extension;
    }

    public static Vector3d Vector3dFromVector3(Vector3 v)
    {
      Vector3d vr = new Vector3d(v.X, v.Y, v.Z);
      return vr;
    }

    public static Vector3 Vector3FromVector3d(Vector3d v)
    {
      Vector3 vr = new Vector3((float)v.X, (float)v.Y, (float)v.Z);
      return vr;
    }

    public static Vector3d VectorFromArray(double[] a)
    {
      Vector3d d = new Vector3d(a[0], a[1], a[2]);
      return d;
    }

    public static Quaterniond QuaternionFromArray(double[] a)
    {
      Quaterniond d = new Quaterniond(a[0], a[1], a[2], a[3]);
      return d;
    }

    public static double[] ArrayFromVector(Vector3d v)
    {
      double[] d = new double[3];
      d[0] = v[0];
      d[1] = v[1];
      d[2] = v[2];
      return d;
    }

    public static string VectorToString(Vector3d v)
    {
      return v.X + "," + v.Y + "," + v.Z;
    }

      public static Vector3d VectorFromString(string s)
    {
      Vector3d v = new Vector3d();
      s = s.Replace("(", "");
      s = s.Replace(")", "");
      string[] coords = s.Split(',');
      if (coords.Count() > 2)
      {
        decimal temp = 0;
        if (decimal.TryParse(coords[0], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out temp))
        {
          v.X = (double)temp;
        }
        if (decimal.TryParse(coords[1], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out temp))
        {
          v.Y = (double)temp;
        }
        if (decimal.TryParse(coords[2], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out temp))
        {
          v.Z = (double)temp;
        }
      }
      return v;
    }

    public static Quaterniond QuaternionFromString(string s)
    {
      Quaterniond v = new Quaterniond();
      s = s.Replace("V: ", "");
      s = s.Replace("W: ", "");
      s = s.Replace("(", "");
      s = s.Replace(")", "");
      string[] coords = s.Split(',');
      if (coords.Count() > 2)
      {
        double temp = 0;
        if (double.TryParse(coords[0], out temp))
        {
          v.X = temp;
        }
        if (double.TryParse(coords[1], out temp))
        {
          v.Y = temp;
        }
        if (double.TryParse(coords[2], out temp))
        {
          v.Z = temp;
        }
        if (double.TryParse(coords[3], out temp))
        {
          v.W = temp;
        }
      }
      return v;
    }

    public static Vector3d Vector3dFromVector3_Server(Vector3d_Server v)
    {
      return new Vector3d(v.X, v.Y, v.Z);
    }

    public static Vector3d_Server ToVector3_Server(Vector3d v)
    {
      return new Vector3d_Server(v.X, v.Y, v.Z);
    }

    public static double[] ArrayFromQuaterniond(Quaterniond q)
    {
      return new double[] { q.X, q.Y, q.Z, q.W };
    }

    public static void GLkError()
    {
      ErrorCode err = GL.GetError();
      if (err != ErrorCode.NoError)
      {
        Console.WriteLine("MTexture Error:" + err);
      }
    }

    public static Dictionary<string, MServerObject> JsonToDictionary(string s)
    {
      try
      {
        return JsonConvert.DeserializeObject<Dictionary<string, MServerObject>>(s);
      }
      catch (Exception e)
      {
        return null;
      }
    }

    public static Vector3d RandomPointInSphere(Vector3d Origin, double Radius, Random r)
    {      
      double MAX_RADIUS = Radius;
      double RandomRadius = r.NextDouble() * MAX_RADIUS;
      double Theta = r.NextDouble() * 260;
      double Phi = r.NextDouble() * 180 - 180;
      double X = RandomRadius * Math.Cos(MathHelper.DegreesToRadians(Theta)) * Math.Cos(MathHelper.DegreesToRadians(Phi));
      double Y = RandomRadius * Math.Sin(MathHelper.DegreesToRadians(Phi));
      double Z = RandomRadius * Math.Sin(MathHelper.DegreesToRadians(Theta)) * Math.Cos(MathHelper.DegreesToRadians(Phi));
      return new Vector3d(X, Y, Z);
    }
  }
}
