using Massive;
using Massive.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Handlers
{
  public class MMoveHandler
  {
    public MMoveHandler()
    {
      Globals.Network.PositionChangeHandler += Network_PositionChangeHandler;
      MMessageBus.MoveRequestEventHandler += MMessageBus_MoveRequestEventHandler;
    }

    //sends local move requests to the server for handling
    private void MMessageBus_MoveRequestEventHandler(object sender, MoveEvent e)
    {
      if ( Globals.Network.Connected == true) { 
      Globals.Network.PositionRequest(e.InstanceID, e.Position, e.Rotation);
      }
      else
      {
        MSceneObject mo = (MSceneObject)MScene.ModelRoot.FindModuleByInstanceID(e.InstanceID);
        if ( mo != null)
        {
          mo.SetPosition(e.Position);
          mo.SetRotation(e.Rotation);
        }
      }
    }

    //this waits for the server to respond before actually moving the object.
    //another strategy might be to move the object, then correct
    private void Network_PositionChangeHandler(object sender, Massive.Events.MoveEvent e)
    {
      //throw new NotImplementedException();
      if (!e.InstanceID.Equals(Globals.UserAccount.UserID))
      {
        MSceneObject mo = (MSceneObject)MScene.ModelRoot.FindModuleByInstanceID(e.InstanceID);
        if (mo != null)
        {
          //movesync smoothly moves an object into position
          MMoveSync ms = (MMoveSync)mo.FindModuleByType(MObject.EType.MoveSync);
          if (ms == null)
          {
            ms = new MMoveSync(mo, e.Position, e.Rotation);
            mo.Add(ms);
          }
          else
          {
            ms.SetTarget(e.Position, e.Rotation);
          }
        }
      }
      else
      {
        MMessageBus.AvatarMoved(this, e.InstanceID, e.Position, e.Rotation);
      }
      //Console.WriteLine(e.Position);
    }
  }
}
