using OpenTK;
using OpenTK.Audio.OpenAL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MAudioListener2 : MObject
  {
    IntPtr device;
    ContextHandle context;    
    public MTransform transform;

    static MAudioListener2 _instance;

    public static MAudioListener2 GetInstance()
    {      
      return _instance;
    }

    public MAudioListener2(string inname) : base(EType.AudioListener, inname)
    {
      _instance = this;

      transform = new MTransform();

      device = Alc.OpenDevice(null);
      unsafe
      {
        context = Alc.CreateContext(device, (int*)null);
      }

      Alc.MakeContextCurrent(context);

      
      //Console.ReadKey();
    }

    public override void Setup()
    {
      base.Setup();
      var version = AL.Get(ALGetString.Version);
      var vendor = AL.Get(ALGetString.Vendor);
      var renderer = AL.Get(ALGetString.Renderer);
      //Console.WriteLine(version);
      //Console.WriteLine(vendor);
      //Console.WriteLine(renderer);

      AL.Listener(ALListenerf.EfxMetersPerUnit, 1);
      AL.Listener(ALListenerf.Gain, 1.9f);      
    }

    public override void Update()
    {
      Vector3 pos = MTransform.GetVector3(transform.Position - Globals.GlobalOffset);
      // Vector3 pos = new Vector3(0, 0, 1);
      AL.Listener(ALListener3f.Position, ref pos);

      Vector3 Vel = Vector3.Zero;
      AL.Listener(ALListener3f.Velocity, ref Vel);
      Vector3d up = new Vector3d(0, 1, 0);
      Vector3 vup = MTransform.GetVector3(up);      
      AL.Listener(ALListenerfv.Orientation, ref pos, ref vup);
    }

    public override void Dispose()
    {
      base.Dispose();

      ///Dispose
			if (context != ContextHandle.Zero)
      {
        Alc.MakeContextCurrent(ContextHandle.Zero);
        Alc.DestroyContext(context);
      }
      context = ContextHandle.Zero;

      if (device != IntPtr.Zero)
      {
        Alc.CloseDevice(device);
      }
      device = IntPtr.Zero;      
    }

    //public Audio
  }
}
