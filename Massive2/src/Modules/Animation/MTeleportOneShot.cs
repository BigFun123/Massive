using Massive.Events;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MTeleportOneShot : MObject
  {
    public event EventHandler<MoveEvent> TeleportCompleteHandler;
    public Vector3d StartPosition;
    public Vector3d TargetPosition;

    public Quaterniond StartRotation;
    public Quaterniond TargetRotation;

    public double Speed = 1;
    double Start = 0;
    //private MSceneObject parent;
    public MTeleportOneShot(MSceneObject inparent, Vector3d inTargetPos, Quaterniond inTargetRot) : base(EType.Teleport, "TeleportAnim")
    {
      Parent = inparent;
      StartPosition = inparent.transform.Position;
      StartRotation = inparent.transform.Rotation;

      TargetPosition = inTargetPos;
      TargetRotation = inTargetRot;
      MPhysicsObject po = (MPhysicsObject)Parent.FindModuleByType(EType.PhysicsObject);
      if (po != null)
      {
        po.SetActive(false);
      }
      Start = 0;
    }

    public override void Update()
    {
      MSceneObject msp = (MSceneObject)Parent;
      Vector3d CurrentPos = msp.transform.Position;
      Quaterniond CurrentRot = msp.transform.Rotation;
      //CurrentPos = Vector3d.Lerp(CurrentPos, TargetPosition, Time.DeltaTime * Speed);
      Start += Time.DeltaTime * Speed;
      if (Start > 1) Start = 1;
      CurrentPos = Extensions.SmoothStep(StartPosition, TargetPosition, Start);

      CurrentRot = Quaterniond.Slerp(StartRotation, TargetRotation, Start);
      msp.SetRotation(CurrentRot);
      msp.SetPosition(CurrentPos);

      //msp.LookAt(TargetPosition);
      double distance = Vector3d.Distance(CurrentPos, TargetPosition);
      if ( distance < 1)
      {
        TeleportComplete(); 
      }
      
    }

    public void TeleportComplete()
    {      
      MPhysicsObject po = (MPhysicsObject)Parent.FindModuleByType(EType.PhysicsObject);      
      MSceneObject msp = (MSceneObject)Parent;
      msp.SetPosition(TargetPosition);
      msp.SetRotation(TargetRotation);
      if (po != null)
      {
        po.SetActive(true);
        po.StopAllMotion();        
      }

      if ( TeleportCompleteHandler != null)
      {        
        TeleportCompleteHandler(this, new MoveEvent(Parent.InstanceID, TargetPosition, TargetRotation));
      }

      Parent.Remove(this);
      Dispose();
    }
  }
}
