using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MObjectAnimation : MObject
  {
    //public MTransform Speed = new MTransform();
    public double Speed = 1;
    MPhysicsObject _physics;
    public Quaterniond AngleOffset;

    public MObjectAnimation(string inname = "Animation") : base(EType.Animation, inname)
    {
      AngleOffset = Quaterniond.Identity;
    }

    public override void Setup()
    {
      MSceneObject so = (MSceneObject)Parent;
      _physics = (MPhysicsObject)so.FindModuleByType(EType.PhysicsObject);
    }

    public override void Update()
    {
      if (Enabled == false) return;
      MSceneObject so = (MSceneObject)Parent;
      if (so != null)
      { 
        Quaterniond q = so.transform.Rotation * AngleOffset;
        so.transform.Rotation = Quaterniond.Slerp(so.transform.Rotation, q, Time.DeltaTime * Speed) ;
      }
      if (_physics != null)
      {
        _physics.SetRotation(so.transform.Rotation);
      }
    }
  }
}
