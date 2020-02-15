using Massive;
using Massive.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Handlers
{
  public class MPropertyChangeHandler
  {
    public MPropertyChangeHandler()
    {
      MMessageBus.PropertyChangeEvent += MMessageBus_PropertyChangeEvent;
    }

    private void MMessageBus_PropertyChangeEvent(object sender, ChangePropertyEvent e)
    {
      MSceneObject mo = (MSceneObject)MScene.ModelRoot.FindModuleByInstanceID(e.InstanceID);
      if ( mo != null)
      {
        mo.Tag = e.PropertyTag;
      }
      
    }
  }
}
