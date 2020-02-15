using Massive;
using Massive.Events;
using Massive.GIS;
using OpenTK;

namespace OpenWorld.Handlers
{
  public class MLightHandler
  {
    MLight light;
    MSceneObject Sun;
    
    public MLightHandler()
    {
      MMessageBus.TeleportCompleteHandler += MMessageBus_TeleportCompleteHandler;
      MMessageBus.AvatarSetupHandler += MMessageBus_AvatarSetupHandler;
      MMessageBus.AvatarMovedEvent += MMessageBus_AvatarMovedEvent;
    }

    void MoveToSunVector()
    {
      Vector3d pos = Globals.Avatar.GetPosition();

      MLight light = (MLight)MScene.UtilityRoot.FindModuleByType(MObject.EType.DirectionalLight);
      if (light == null) return;

      if (Sun == null)
      {
        Sun = (MSceneObject)MScene.AstroRoot.FindModuleByName("Sol");
        if (Sun == null) return;
      }

      Vector3d dir = Sun.transform.Position - pos;
      dir.Normalize();

      Vector3d TargetPos = pos + dir * 20;

      light.transform.Position = TargetPos;

      light.LookAt(pos);
    }

    private void MMessageBus_AvatarMovedEvent(object sender, MoveEvent e)
    {
      


      /*
      MMoveSync ms = null;
      MObject ms0 = light.FindModuleByType(MObject.EType.MoveSync);
      if (ms == null)
      {
        // ms = new MMoveSync(light, e.Position
        // + Globals.LocalUpVector * 5
        //+ Globals.LocalUpRotation() * new Vector3d(10, 10, 0), Quaterniond.Identity);
        ms = new MMoveSync(light, TargetPos, Quaterniond.Identity);
        ms.Speed = 1;
        light.Add(ms);
      }
      else
      {
        ms = (MMoveSync)ms0;
        ms.SetTarget(TargetPos, Quaterniond.Identity);
      }
      */
      
     
     
      //light.DebugDepth = true;
    }

    private void MMessageBus_AvatarSetupHandler(object sender, InfoEvent e)
    {
      MoveLightNearAvatar(Globals.Avatar.GetPosition());
    }

    private void MMessageBus_TeleportCompleteHandler(object sender, MoveEvent e)
    {
      MoveLightNearAvatar(e.Position);
    }

    void MoveLightNearAvatar(Vector3d Position)
    {
      if ( light == null)
      {
        light = (MLight)MScene.UtilityRoot.FindModuleByType(MObject.EType.DirectionalLight);
      }
      
      if (light != null)
      {
        MPlanetHandler._Instance.Update();
        light.transform.Position = Position
          + Globals.LocalUpVector * 22
          + Globals.Avatar.Forward() * 21
          + Globals.Avatar.Right() * 21;
        light.LookAt(Globals.Avatar.GetPosition());
      }

      MSceneObject mo = (MSceneObject)MScene.ModelRoot.FindModuleByName("LightSphere");
      if (mo != null)
      {
        mo.transform.Position = light.transform.Position + Globals.LocalUpVector;
        mo.transform.Rotation = Extensions.LookAt(light.transform.Position, light.TargetVector, Vector3d.UnitY);
      }
    }

    public void Update()
    {
      MoveToSunVector();
    }
  }
}
