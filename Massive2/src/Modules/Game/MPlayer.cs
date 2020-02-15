using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MAvatar : MObject
  {

    public MSceneObject Target;

    public MPhysicsObject _physics;
    double mass = 20;

    private double thrustX = 0;
    private double thrustY = 0;
    private double thrustZ = 2222;

    public double ThrustX { get => thrustX; set => thrustX = value; }
    public double ThrustY { get => thrustY; set => thrustY = value; }
    public double ThrustZ { get => thrustZ; set => thrustZ = value; }

    public MAvatar(string name)
      : base(EType.Player, name)
    {

    }

    public void SetSceneObject(MSceneObject so)
    {
      Target = so;
      if (Target != null)
      {
        _physics = (MPhysicsObject)Target.FindModuleByType(EType.Physics);        
      }
      //Target.animation.TranslationSpeed = 1;
    }

    public Vector3d GetPosition()
    {
      if (Target == null) return Vector3d.Zero;
      return Target.transform.Position;
    }

    public Quaterniond GetRotation()
    {
      if (Target == null) return Quaterniond.Identity;
      return Target.transform.Rotation;
    }

    public void SetPosition(Vector3d pos)
    {
      if (_physics == null) return;
      Matrix4d newpos = Matrix4d.CreateTranslation(pos);
      _physics._rigidBody.ProceedToTransform(newpos);
    }

    public void SetRotation(Quaterniond rot)
    {
      if (_physics == null) return;      
      _physics.SetRotation(rot);
    }

    public void InputH(double h)
    {
      if (_physics == null) return;
      _physics.SetActive(true);      
      Vector3d vo = _physics._rigidBody.WorldTransform.ExtractRotation() * new Vector3d(h * mass, 0, 0);
      //ph._rigidBody.ApplyCentralForce(vo);
      _physics._rigidBody.ApplyCentralImpulse(vo);
      //_physics._rigidBody.ApplyTorque(new Vector3d(vo.X, vo.Y, vo.Z));
    }

    public void InputV(double v)
    {
      if (_physics == null) return;      
      _physics.SetActive(true);      
      // ph.Thrust.Z = v * 2;
      Vector3d vo = _physics._rigidBody.WorldTransform.ExtractRotation() * new Vector3d(0, 0, v * mass);
      //ph._rigidBody.ApplyCentralForce(vo);
      _physics._rigidBody.ApplyCentralImpulse(vo);
      //_physics._rigidBody.ApplyTorque(vo);
    }

    public void InputB1(double b)
    {
      if (_physics == null) return;
      _physics.SetActive(true);
      //MPhysicsObject ph = (MPhysicsObject)Target.FindModuleByType(EType.Physics);
      //ph.Thrust = new MVector3(0, 100, b*1009);
      ///ph.Thrust = new Vector3d(thrustX, thrustY, thrustZ);
      ///_physics.Active = true;      
      // ph.Thrust.Z = v * 2;
      //Vector3d vo = _physics._rigidBody.WorldTransform.ExtractRotation() * new Vector3d(0, b * 1, 0);
       Vector3d vo = Globals.LocalUpRotation() * new Vector3d(0, b* mass, 0);
      _physics._rigidBody.ApplyCentralImpulse(vo);
      //_physics._rigidBody.ApplyTorque(vo);
    }

    public Vector3d Up()
    {
      if (_physics == null) return new Vector3d(0, 1, 0);
      Vector3d vo = _physics._rigidBody.WorldTransform.ExtractRotation() * new Vector3d(0, 1, 0);
      return vo;
    }

    public Vector3d Forward()
    {
      if (_physics == null) return new Vector3d(0, 0, 1);
      Vector3d vo = _physics._rigidBody.WorldTransform.ExtractRotation() * new Vector3d(0, 0, 1);
      return vo;
    }

    //assuming  walking player
    public void RotateBy(double deltax, double deltay)
    {
      if (Target != null)
      {
        
        Quaterniond ir = _physics._rigidBody.CenterOfMassTransform.ExtractRotation();
        //Quaterniond d = Quaterniond.FromAxisAngle(Globals.GlobalUpVector, deltax);
        //Quaterniond d = Quaterniond.FromAxisAngle(Vector3d.UnitY, deltax);

        Quaterniond d = new Quaterniond(0, deltax, 0);
        Quaterniond dy = new Quaterniond(0, 0, deltay);
        _physics.SetRotation(d * ir * dy);
        //ph.SetRotation(d * ir);
      }
    }

    public void Hide()
    {
      if (Target != null)
      {
        Target.Visible = false;
      }
    }

    public void Show()
    {
      if (Target != null)
      {
        Target.Visible = true;
      }
    }

    public override void Update()
    {
      base.Update();
      if (Target == null) return;

      if (_physics != null)
      {
        
       // _physics.SetRotation(Quaterniond.Slerp(Target.transform.Rotation, Quaterniond.FromEulerAngles(Globals.GlobalUpVector), Time.DeltaTime));
       // _physics._rigidBody.ApplyCentralForce(new Vector3d(ThrustX, ThrustY, ThrustZ));
      }
    }

  }
}
