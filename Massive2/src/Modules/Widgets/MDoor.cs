using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MDoor : MObject
  {
    public const string LOCKED = "LOCKED";
    public const string ONLYME = "ONLYME";
    public const string PUBLIC = "PUBLIC";
    public const string FRIENDS = "FRIENDS";
    Quaterniond OriginalOrientation;
    MSceneObject msoParent;
    MPhysicsObject msoParentPhysics;
    bool DoorIsOpen = false;
    public double DistanceToOpen = 3;
    public bool DoorIsLocked = false;

    public MDoor(MSceneObject inParent) : base(EType.Door, "Door")
    {
      OriginalOrientation = inParent.transform.Rotation;
      msoParent = inParent;
      msoParentPhysics = (MPhysicsObject)msoParent.FindModuleByType(EType.PhysicsObject);
    }

    bool CanIOpen()
    {
      string stag = (string)msoParent.Tag;
      if (string.IsNullOrEmpty(stag)) return true;
      string[] items = stag.Split('|');
      string permissions = items[1];

      string UserID = Globals.UserAccount.UserID;
      string OwnerID = msoParent.OwnerID;

      if (permissions.Equals(LOCKED)) return false;

      if (UserID.Equals(OwnerID)) return true;

      if (permissions.Equals(ONLYME))
      {
        if (UserID.Equals(OwnerID)) return true;
      }
      if (permissions.Equals(PUBLIC)) return true;
      if (permissions.Equals(FRIENDS))
      {        
        return Globals.UserAccount.FriendOf(OwnerID);        
      }

      return false;
    }

    void OpenDoor()
    {      
      if (DoorIsOpen == true) return;
      if (!CanIOpen()) return;
      DoorIsOpen = true;
      Quaterniond TargetRot = OriginalOrientation * new Quaterniond(0, 90 * Math.PI / 180.0, 0);
      MMoveSync ms = new MMoveSync(msoParent, msoParent.transform.Position, TargetRot);
        ms.Speed = 0.5;
      msoParent.Add(ms);
      //Globals.Network.PositionRequest(msoParent.InstanceID, msoParent.transform.Position, TargetRot);
      //msoParentPhysics.SetRotation(OriginalOrientation * new Quaterniond(0, 90 * Math.PI / 180.0, 0));
    }

    void CloseDoor()
    {
      if (DoorIsOpen == false) return;
      DoorIsOpen = false;
      MMoveSync ms = (MMoveSync)msoParent.FindModuleByType(EType.MoveSync);
      if (ms != null)
      {
        ms.Stop();
        msoParent.Remove(ms);
      }
      
        ms = new MMoveSync(msoParent, msoParent.transform.Position, OriginalOrientation);
        ms.Speed = 0.5;
      msoParent.Add(ms);
      //Globals.Network.PositionRequest(msoParent.InstanceID, msoParent.transform.Position, OriginalOrientation);

    }

    public override void ParentChanged()
    {
      OriginalOrientation = msoParentPhysics.GetRotation();
     // DoorIsOpen = true;
      CloseDoor();
    }

    public override void Update()
    {
      MSceneObject parent = (MSceneObject)Parent;      

      if (DoorIsLocked == false)
      {
        if (parent.DistanceFromAvatar < DistanceToOpen)
        {
          OpenDoor();
        }
        else
        {
          CloseDoor();
        }
      }
    }
  }
}
