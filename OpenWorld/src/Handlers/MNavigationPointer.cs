using Massive;
using Massive.Events;
using Massive.Tools;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Handlers
{
  class MNavigationPointer : MObject
  {
    MSceneObject Widget;
    Vector3d Target;
    Vector3d ScreenPos;

    public MNavigationPointer() : base(EType.Other, "NavigationPointer")
    {
      MMessageBus.ZoneSelectHandler += MMessageBus_ZoneSelectHandler;
      MMessageBus.NavigationHandler += MMessageBus_NavigationHandler;
      MMessageBus.NavigationDisableHandler += MMessageBus_NavigationDisableHandler;
      MMessageBus.NavigationEnableHandler += MMessageBus_NavigationEnableHandler;
      MMessageBus.LateUpdateHandler += MMessageBus_LateUpdateHandler;


      ScreenPos = new Vector3d(-7.5, -3.5, 0);
    }

    private void MMessageBus_LateUpdateHandler(object sender, UpdateEvent e)
    {
      Update();
    }

    private void MMessageBus_NavigationEnableHandler(object sender, NavigationEvent e)
    {
      if (Widget != null)
      {
        Widget.Enabled = true;
      }
    }

    private void MMessageBus_NavigationDisableHandler(object sender, NavigationEvent e)
    {
      if (Widget != null)
      {
        Widget.Enabled = false;
      }
    }

    private void MMessageBus_NavigationHandler(object sender, NavigationEvent e)
    {
      Target = e.Target;
      Widget.Enabled = true;
    }

    private void MMessageBus_ZoneSelectHandler(object sender, ZoneEvent e)
    {
      if (e.Zone == null)
      {
        Widget.Enabled = false;
      }
      else
      {
        Target = MassiveTools.Vector3dFromVector3_Server(e.Zone.Position);
        Widget.Enabled = true;
      }
    }

    public override void Setup()
    {
      base.Setup();

      Widget = Helper.CreateModel(MScene.Overlay, "NavigationWidget", @"Models\arrow02.3ds", Vector3d.Zero);
      MMaterial mat = new MMaterial("WidgetMaterial");
      MScene.MaterialRoot.Add(mat);
      mat.AddShader((MShader)MScene.MaterialRoot.FindModuleByName(MShader.DEFAULT_SHADER));
      mat.SetDiffuseTexture(Globals.TexturePool.GetTexture("Textures\\arrow02.png"));
      Widget.AddMaterial(mat);
      Widget.transform.Scale = new Vector3d(1, 1, 1);
      Widget.Enabled = true;
    }

    public override void Update()
    {
      if (Widget.Enabled)
      {
        //ScreenPos = new Vector3d(Settings.Tweak1 * 0.01, Settings.Tweak2*0.01, Settings.Tweak3*0.01);
        
        Widget.transform.Position = ScreenPos;
        Vector3d AP = Globals.Avatar.GetPosition();

        if ( Target == AP)
        {
          Target = AP + new Vector3d(0, 0, 1);
        }
        Matrix4d Rot = Matrix4d.CreateFromQuaternion(Globals.Avatar.GetRotation())
           * Matrix4d.LookAt(Target, AP - Globals.Avatar.Up()*2, 
           Globals.Avatar.Up());
        Widget.transform.Rotation = Rot.Inverted().ExtractRotation();
      }
    }
  }
}
