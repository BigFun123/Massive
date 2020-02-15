using Massive;
using Massive.GIS;
using Massive.Tools;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MGravityCalculator
  {
    //F = G*(m1*m2/d^2)
    public static Vector3d GetGravityVector(Vector3d AP)
    {
      double G = 6.67191e-11;
      if (Globals.Avatar.Target == null)
      {
        //return Vector3d.UnitY * -9.8;
        // AP = Globals.UserAccount.CurrentPosition;
      }
      Vector3d g = Vector3d.Zero;
      //Vector3d g = GetGravityAtXYZ(Globals.Avatar.GetPosition());

      //bool EnableFog = false;
      double FogHeight = 0;
      MAstroBody Closest = null;
      double closestcandidate = double.MaxValue;
      //Vector3d AP = Globals.Avatar.GetPosition();
      foreach (MAstroBody b in MPlanetHandler.Bodies)
      {
        //+1000 because our mesh is not perfectly spherical, so choose the lowest point
        b.DistanceToAvatar = Vector3d.Distance(AP, b.Position);
        b.AvatarDistanceToSurface = b.DistanceToAvatar - b.Radius.X;
        if (b.DistanceToAvatar < closestcandidate)
        {
          Closest = b;
          closestcandidate = b.DistanceToAvatar;
        }

        if (b.Name == "Earth")
        {
          FogHeight = Math.Pow(Math.Abs((b.AtmosphereStart) / (b.DistanceToAvatar)), 64);
          //FogHeight *= 0.01;          
          //Console.WriteLine(b.DistanceToAvatar + ",FH " + FogHeight + ",AS " + b.AtmosphereStart);
          FogHeight = Extensions.Clamp(FogHeight, 0, 1.0);
          ///MPlanetHandler.CurrentNear = b;
        }

        Vector3d delta = (b.Position - AP).Normalized();
        double bbmass = b.Mass;
        double force = G * (bbmass / (b.DistanceToAvatar * b.DistanceToAvatar));
        g += delta * force;

      }

      //Console.WriteLine("G:" + g);

      //g = -(AP - Closest.Position).Normalized() * 9.8;
      MScene.Fog.FogMultiplier = (float)FogHeight;
      //Console.WriteLine(MScene.Fog.FogMultiplier);

      //we always need to calculate some useful up vector
      if (g.Length < 0.0001) g = new Vector3d(0, -0.8, 0);

      MPlanetHandler.CurrentNear = Closest;
      return g;
    }

    public static void CalculateGravityAtAvatar()
    {
      //Console.WriteLine("Update");

      Vector3d g = GetGravityVector(Globals.Avatar.GetPosition());      
      if ( Globals.Avatar.Target == null)
      {
        g = GetGravityVector(MassiveTools.VectorFromArray(Globals.UserAccount.CurrentPosition));
      }

      MPhysics.Instance.SetGravity(g);
      Globals.LocalUpVector = -g.Normalized();
      if (double.IsNaN(Globals.LocalUpVector.X))
      {
        if (Globals.Avatar.Target != null)
        {
          Globals.LocalUpVector = Globals.Avatar.Target.transform.Up();
        }
      }
      if (double.IsNaN(g.X))
      {
        g = new Vector3d(0, -0.8, 0);
      }
      Globals.LocalGravity = g;
    }
  }
}
