using Massive;
using Massive.Events;
using Massive.GIS;
using Massive.Platform;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Manages climate, sun, time of day, rain, wind, etc
/// </summary>

namespace Massive.GIS
{
  public class MClimate : MObject
  {
    public static double timeOffset = 17;
    public static double time = 12;
    public static MSound AmbientSound;

    public MClimate() 
      : base (EType.Other, "MClimate")
    {      
      MMessageBus.AvatarChangedHandler += MMessageBus_AvatarChangedHandler;
    }

    public override void Update()
    {
      time += Time.DeltaTime / 60.0 ;
      SetTimeOfDay(time);
      base.Update();
    }

    private void MMessageBus_AvatarChangedHandler(object sender, ChangeAvatarEvent e)
    {
      AmbientSound = (MSound)Globals.Avatar.Target.FindModuleByName("ClimateSound");
      if ( AmbientSound == null)
      {
        AmbientSound = new MSound("ClimateSound");
        Globals.Avatar.Target.Add(AmbientSound);
      }

      AmbientSound.Load(Path.Combine(MFileSystem.AssetsPath, "Audio", "ambiencem.wav"), Globals.Avatar.Target);
      AmbientSound.Loop = true;
      AmbientSound.Volume = 0.15f;
      AmbientSound.Play((MAudioListener)MScene.Camera.FindModuleByType(EType.AudioListener));
      
    }

    public static void SetTimeOfDay(double _time)
    {
      time = _time;
      MAstroBody Sol = MPlanetHandler.Get("Sol");
      Quaterniond q = Quaterniond.FromEulerAngles(0, (15 * time) * Math.PI / 180.0, 0);
      MSceneObject mo = (MSceneObject)MScene.AstroRoot.FindModuleByName("Sol");      
      mo.SetPosition(q * new Vector3d(Sol.DistanceToAvatar, 0, 0));
      MMessageBus.Navigate(null, mo.transform.Position);

      double ottime = (time+timeOffset) % 24;

      float f24 = (float)Math.Sin((ottime * 15.0) * Math.PI / 180.0);
      //f24 = Math.Min((float)Math.Pow(f24, 0.2), 1);
      f24 = Math.Max(f24, 0.02f);
      MScene.light.Ambient = new Vector3(f24, f24, f24);
    }
  }
}
