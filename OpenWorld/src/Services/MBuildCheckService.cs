using Massive;
using Massive.Events;
using Massive.Network;
using Massive.Tools;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Services
{
  /// <summary>
  /// Checks if a user can build in this area
  /// pre-emptive check, another check will be done on the server
  /// </summary>
  public class MBuildCheckService : MObject
  {
    public static List<MServerZone> Zones ;
    MServerZone SelectedZone;
    public static object ZoneLocker;

    public MBuildCheckService() : base(EType.Other, "BuildCheckService")
    {
      ZoneLocker = new object();
      //Globals.Network.ZoneCreateHandler += Network_ZoneHandler;
      Zones = new List<MServerZone>();
    }
    
    private void Network_ZoneHandler(object sender, ZoneEvent e)
    {
     // Vector3d vs = new Vector3d(e.Zone.Position.X, e.Zone.Position.Y, e.Zone.Position.Z);
      //MMessageBus.AddBookmark(this, new Massive2.Tools.MBookmark(vs, Quaterniond.Identity, e.Zone.Name));
    }

    public static Vector3d Find(string s)
    {
      Vector3d pos = Vector3d.Zero;
      lock (ZoneLocker)
      {
        MServerZone zone = Zones.Find(x => x.Name.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) > -1);
        if (zone != null) pos = MassiveTools.Vector3dFromVector3_Server(zone.Position);
      }
      
      return pos;
    }

    public static bool ZoneLocked(Vector3d pos, MObject mParent)
    {
      if (mParent.OwnerID == null) return true;
      if (!mParent.OwnerID.Equals(Globals.UserAccount.UserID)        
        && !mParent.OwnerID.Equals(MObject.OWNER_NONE)
        && !mParent.OwnerID.Equals(MObject.OWNER_ADMIN)
        && !mParent.OwnerID.Equals(MObject.OWNER_MAYOR)
        && !mParent.OwnerID.Equals(MObject.OWNER_SYSTEM))
      {
        if (mParent.Renderable)
        {
          MSceneObject mso = (MSceneObject)mParent;
          if (mso.IsAvatar == true) return false;
          double dist = Vector3d.Distance(pos, (mso.transform.Position));
          if (dist < Globals.BuildThreshold)
          {
            return true;
          }
        }        
      }

      foreach (MObject m in mParent.Modules.ToList())
      {        
        bool b = (ZoneLocked(pos, m));
        if (b == true) return true;
      }

      return false;
    }

    //Checks if anyone else has built nearby
    //not a solid test, probably need to double-check on the server.
    public static bool ZoneLocked(Vector3d pos)
    {
      bool result = false;
      //create a copy to avoid locking conflict, as this is a slowish operation
      if (MScene.ModelRoot == null) return true;
      result = ZoneLocked(pos, MScene.ModelRoot);
      MStateMachine.ZoneLocked = result;
      return result;
    }
  }
}
