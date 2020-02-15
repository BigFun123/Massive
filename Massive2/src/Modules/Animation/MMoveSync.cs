using BulletSharp;
using Massive;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MMoveSync : MObject
  {
    Vector3d StartPosition;
    Vector3d TargetPosition;
    Quaterniond TargetRotation;
    public double Speed = 5;
    double Start = 0;
    double Value = 0;
    MPhysicsObject po;
    bool Idle = true;
    Vector3d PreviousPosition;
    MSceneObject msoParent;


    public MMoveSync(MSceneObject inParent, Vector3d inTargetPos, Quaterniond inTargetRot)
      : base(EType.MoveSync, "MoveSync")
    {
      msoParent = inParent;
      StartPosition = msoParent.transform.Position;
      TargetPosition = inTargetPos;
      TargetRotation = inTargetRot;
      Idle = false;
      Start = 0;
    }

    public void Stop()
    {
      if (msoParent == null) return;
      msoParent.SetPosition(TargetPosition);
      msoParent.SetRotation(TargetRotation, false);

      msoParent = null;
    }


    public void SetTarget(Vector3d tpos, Quaterniond tRot)
    {
      TargetPosition = tpos;
      TargetRotation = tRot;
      Idle = false;
      if ( po != null )
      {
        po.SetActive(true);
      }
    }

    public override void Update()
    {
      if (Idle == true) return;
      if (msoParent == null) return;

      if (po == null)
      {
        po = (MPhysicsObject)Parent.FindModuleByType(EType.PhysicsObject);
      }

      MSceneObject msp = (MSceneObject)Parent;
      Vector3d CurrentPos = msp.transform.Position;
      Quaterniond CurrentRot = msp.transform.Rotation;

      //CurrentPos = Vector3d.Lerp(CurrentPos, TargetPosition, Time.DeltaTime * Speed);
      Value += Time.DeltaTime * Speed;
      if (Value > 1) Value = 1;
      double dist = (Vector3d.Distance(TargetPosition, CurrentPos));
      if (dist > 1000)
      {
        Start = 0.999;
        if (po != null)
        {
          po.SetActive(false);
        }
      }
      CurrentPos = Vector3d.Lerp(StartPosition, TargetPosition, Value);
      CurrentRot = Quaterniond.Slerp(CurrentRot, TargetRotation, Value);
      
      //if ( dist< 0.1)
      if ( Value >=1 )
      {
        CurrentPos = TargetPosition;
        CurrentRot = TargetRotation;
        Idle = true;
        //msp.SetPosition(TargetPosition);
        //msp.SetRotation(TargetRotation, false);
        //Console.WriteLine("idle");
        if (po != null)
        {
          po.SetActive(true);
        }
        Complete();
      }
      else
      {
        if (po != null)
        {
          po.SetActive(true);
        }
      }      
      
      msp.SetPosition(CurrentPos);
      msp.SetRotation(CurrentRot, false);
      
    }

    public void Complete()
    {
      MSceneObject msp = (MSceneObject)Parent;
      //MPhysicsObject po = (MPhysicsObject)msp.FindModuleByType(EType.PhysicsObject);
      msp.SetPosition(TargetPosition);
      msp.SetRotation(TargetRotation, false);

      Parent.Remove(this);
      Parent = null;
    }
  }
}
