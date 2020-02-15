using Massive;
using Massive.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Handlers
{
  public class MDeleteHandler
  {
    public MDeleteHandler()
    {
      MMessageBus.ObjectDeletedEvent += MMessageBus_ObjectDeletedEvent;

      MMessageBus.DeleteObjectRequestHandler += MMessageBus_DeleteObjectRequestHandler;
    }

    private void MMessageBus_DeleteObjectRequestHandler(object sender, DeleteRequestEvent e)
    {
      MSceneObject o = e.TargetObject;
      if (o == null) return;

      MPhysicsObject po = (MPhysicsObject)o.FindModuleByType(MObject.EType.PhysicsObject);
      MScene.Physics.Remove(po);

      MScene.ModelRoot.Remove(o);
      MScene.Priority1.Remove(o);
      MScene.Priority2.Remove(o);
      MScene.SelectedObject = null;
    }

    private void MMessageBus_ObjectDeletedEvent(object sender, DeleteEvent e)
    {
      MSceneObject o = (MSceneObject)MScene.ModelRoot.FindModuleByInstanceID(e.InstanceID);
      if (o == null) return;

      MPhysicsObject po = (MPhysicsObject)o.FindModuleByType(MObject.EType.PhysicsObject);
      MScene.Physics.Remove(po);

      MScene.ModelRoot.Remove(o);
      MScene.Priority1.Remove(o);
      MScene.Priority2.Remove(o);
      MScene.SelectedObject = null;

      //o.Dispose(); //don't dispose, it disposes templates, let GC pick up unallocated objects
    }
  }
}
