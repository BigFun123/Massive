using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MFlyBirdController : IControllerContext
  {
    public double _Throttle = 0;
    public double Rudder = 0;
    public double Aileron = 0;
    public double Elevator = 0;
    public double Rudder_Aileron_Mix = 0.1;


    public MFlyBirdController(MAvatar avatar)
      : base(avatar)
    {
    }

    public override void Brake(double b)
    {
      Vector3d vo = Vector3d.Zero;
      vo = _Avatar.GetRotation()
            * new Vector3d(0, 0, -b); //boost   
      _Avatar._physics._rigidBody.ApplyCentralImpulse(vo * 0.1);
    }

    public override void Jump(double b)
    {
      Vector3d vo = _Avatar.GetRotation()
         * new Vector3d(0, b, 0); //boost      
      _Avatar._physics._rigidBody.ApplyCentralForce(vo);
    }

    public override void MouseWheel(double b)
    {
      Settings.OffsetThirdPerson.Z += b;
    }

    public override void Throttle(double d)
    {
      _Throttle += d;
      _Throttle = Math.Max(0, _Throttle);
    }

    public override void Pitch(double b)
    {
      Elevator += b;
    }

    public override void Yaw(double b)
    {
      Rudder += b;
    }

    public override void Bank(double b)
    {
      Aileron = b;
    }

    void CalcAeroDynamics()
    {
      Vector3d vo = Vector3d.Zero;
      double lift = 0;
      if (_Avatar.DistanceToSurface < 10)
      {
        lift = _Throttle * 0.1;          
      }

      vo = _Avatar.GetRotation()
        * new Vector3d(0, 0, _Throttle)
        + Globals.LocalUpRotation() * new Vector3d(0, lift, 0);
      //boost
      //if we're near the ground and the throttle is up, add a little lift.
      

      _Avatar._physics._rigidBody.ApplyCentralForce(vo);
      _Avatar.InputPitchVDirect(Elevator);
      _Avatar.InputRollHDirect(Aileron);
      _Avatar.InputYawHDirect(Rudder - (Rudder_Aileron_Mix * Aileron));

      
    }

    public override void Update()
    {
      CalcAeroDynamics();
      Aileron *= 0.90;
      Elevator *= 0.90;
      Rudder *= 0.90;
    }


  }
}
