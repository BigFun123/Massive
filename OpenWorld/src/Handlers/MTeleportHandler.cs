using Massive;
using Massive.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Handles all Avatar teleport requests
/// </summary>

namespace OpenWorld.Handlers
{
  public class MTeleportHandler
  {

    public MTeleportHandler()
    {
      Globals.Network.TeleportHandler += Network_TeleportHandler;

      MMessageBus.TeleportRequestHandler += MMessageBus_TeleportRequestHandler;
      MMessageBus.TeleportCompleteHandler += MMessageBus_TeleportCompleteHandler;
    }

    private void MMessageBus_TeleportCompleteHandler(object sender, MoveEvent e)
    {
      //throw new NotImplementedException();
      if (Globals.Avatar.Target != null)
      {
        MGravityCalculator.CalculateGravityAtAvatar();
        Globals.Avatar.SetRotation(Globals.LocalUpRotation());
      }
    }

    private void Network_TeleportHandler(object sender, MoveEvent e)
    {
      MSceneObject mo = (MSceneObject)MScene.ModelRoot.FindModuleByInstanceID(e.InstanceID);
      if (mo != null)
      {
        mo.SetPosition(e.Position);
        mo.SetRotation(e.Rotation);
      }
      //throw new NotImplementedException();
      MMessageBus.TeleportComplete(this, e.InstanceID, e.Position, e.Rotation);
    }

    private void MMessageBus_TeleportRequestHandler(object sender, TeleportRequestHandler e)
    {
      if ( Globals.Network.Connected == true) { 
      Globals.Network.TeleportRequest(Globals.UserAccount.UserID, e.Position, e.Rotation);
      }
      else
      {
        Globals.Avatar.SetPosition(e.Position);
        Globals.Avatar.SetRotation(e.Rotation);
      }
    }
  }
}
