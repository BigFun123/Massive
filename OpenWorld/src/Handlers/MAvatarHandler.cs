using Massive;
using Massive.Events;
using Massive.Network;
using Massive.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Handlers
{
  public class MAvatarHandler
  {
    public MAvatarHandler()
    {
      MMessageBus.ChangeAvatarRequestHandler += MMessageBus_ChangeAvatarRequestHandler;
      MMessageBus.AvatarMovedEvent += MMessageBus_AvatarMovedEvent;
      MMessageBus.MoveAvatarRequestEventHandler += MMessageBus_MoveAvatarRequestEventHandler;
    }


    //relay move avatar request to server
    private void MMessageBus_MoveAvatarRequestEventHandler(object sender, MoveEvent e)
    {
      Globals.Network.PositionRequest(Globals.UserAccount.UserID, e.Position, e.Rotation);
    }

    private void MMessageBus_AvatarMovedEvent(object sender, MoveEvent e)
    {
      Globals.UserAccount.CurrentPosition = MassiveTools.ArrayFromVector(Globals.Avatar.GetPosition());
    }

    private void MMessageBus_ChangeAvatarRequestHandler(object sender, ChangeAvatarEvent e)
    {
      Globals.UserAccount.AvatarID = e.TemplateID;      
    }

    
  }
}
