using IrrKlang;
using OpenTK;
using System;

namespace Massive
{
  public class MSound : MObject
  {
    public string Filename;
    public bool Loop = false;
    public bool PlayOnAwake = false;
    public float Volume = 1;
    const int CutoffDistance = 60;
    float Rate = 1;
    ISound _sound;
    MSceneObject Target;

    public MSound(string sName ="Sound") : base(EType.Sound, sName)
    {      
    }

    public override void CopyTo(MObject dobj)
    {
      MSound d = (MSound)dobj;
      d.Filename = Filename;
      d.Loop = Loop;
      d.Volume = Volume;
      d.PlayOnAwake = PlayOnAwake;
      d.Rate = Rate;
      base.CopyTo(d);
    }

    public void Load(string sFile, MSceneObject inTarget)
    {
      Target = inTarget;
      //Filename = Path.Combine(Globals.ResourcePath, sFile);
      Filename = sFile;
      if (PlayOnAwake == true)
      {
        Play(MScene.audioListener);
      }
    }

    public void Stop(MAudioListener listener)
    {
      if (listener == null) return;
      if (_sound == null) return;
      //_sound.Stop();
      _sound.Paused = true;
    }

    public void SetRate(float f)
    {
     // Console.WriteLine(f);
      Rate = f;
      if (_sound == null) return;
      bool bBpause = _sound.Paused;      
      _sound.PlaybackSpeed = Rate;
      //Console.WriteLine(f);
    }

    public void Play(MAudioListener listener)
    {
      if (listener == null) return;

      Vector3d pos = Target.transform.Position - Globals.GlobalOffset;

      if (_sound == null)
      {
        // _sound = listener.engine.Play2D(Filename, Loop);

        if ( Target != null) { 
        

        _sound = listener.engine.Play3D(Filename,
          (float)pos.X,
          (float)pos.Y,
          (float)pos.Z,
          Loop, false, StreamMode.AutoDetect);

        }
      }
      else
      {
        _sound.Position = new Vector3D(
          (float)pos.X,
          (float)pos.Y,
          (float)pos.Z);
      }
   
      _sound.Paused = false;
      
      /*
      _sound = listener.engine.Play3D(Filename,
        (float)this.transform.Position.X,
        (float)this.transform.Position.Y,
        (float)this.transform.Position.Z,
        Loop, false, StreamMode.AutoDetect);
        */
    }

    public override void Update()
    {
      base.Update();

      if (Parent.Renderable == false) return;      

      MSceneObject msp = (MSceneObject)Parent;
      Vector3d Delta = msp.transform.Position - (Globals.Avatar.GetPosition()+ new Vector3d(0.000001,0,0));
      double d = Vector3d.Dot(Delta.Normalized(), Globals.Avatar.Right());

      if (_sound != null)
      {
        //_sound.Position = v3d;
        if ( msp.DistanceFromAvatar > CutoffDistance)
        {
          _sound.Volume = 0;
          if (_sound.Paused == false)
          {
            _sound.Paused = true;
          }
        }
        else
        {
          if ( _sound.Paused == true)
          {
            _sound.Paused = false;
          }
        }
        _sound.Pan = (float)d;
        _sound.Volume = (1.0f / (float)(1+msp.DistanceFromAvatar+0.00001)) * Volume;
      }
    }

    public override void Dispose()
    {
      base.Dispose();
    }
  }
}
