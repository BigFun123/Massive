using IrrKlang;
using Massive;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MAudioListener : MObject
  {
    public ISoundEngine engine;
    public MTransform transform;
    public MAudioListener() : base(EType.AudioListener, "AudioListener")
    {
      transform = new MTransform();      
      engine = new ISoundEngine(SoundOutputDriver.AutoDetect);
    }

    public ISoundEngine GetEngine()
    {
      return engine;
    }

    public override void Update()
    {
      MCamera cam = (MCamera)Parent;
      transform = cam.transform;

      Vector3d WorldPos = transform.Position - Globals.GlobalOffset;
      Vector3D pos = new Vector3D((float)WorldPos.X, (float)WorldPos.Y, (float)WorldPos.Z);

      Vector3d fwd = transform.Forward();
      Vector3D audiofwd = new Vector3D((float)fwd.X, (float)fwd.Y, (float)fwd.Z);
      
      engine.SetListenerPosition(pos, audiofwd);
      engine.Update();
    }

  }
}
