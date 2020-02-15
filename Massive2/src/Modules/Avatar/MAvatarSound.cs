using Massive.Events;
using Massive.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Modules.Avatar
{
  public class MAvatarSound : MObject
  {
    MSound WalkSound;
    MSound IdleSound;
    MAvatar _avatar;
    MAudioListener _listener;

    public MAvatarSound() 
      : base(EType.Sound, "AvatarSound")
    {
      MMessageBus.AvatarChangedHandler += MMessageBus_AvatarChangedHandler;
    }

    private void MMessageBus_AvatarChangedHandler(object sender, ChangeAvatarEvent e)
    {
      string sPath = Path.Combine(MFileSystem.AssetsPath, "Audio", "walking-in-snow-1.wav");
      WalkSound.Load(sPath, _avatar.Target);
      WalkSound.Loop = true;
    }

    public override void Setup()
    {
      base.Setup();
      _avatar = (MAvatar)Parent;
      _listener = (MAudioListener)MScene.Camera.FindModuleByType(EType.AudioListener);
      WalkSound = new MSound();
      Add(WalkSound);     
    }

    public override void Update()
    {
      base.Update();

      switch( _avatar.MoveState )
      {
        case MAvatar.eMoveState.Idle:
          WalkSound.Stop(_listener);
          break;
        case MAvatar.eMoveState.Run:
        case MAvatar.eMoveState.Walk:
          WalkSound.SetRate(0.5f+_avatar.CurrentSpeed * 1.4f);
          WalkSound.Play(_listener);          
          break;
      }
    }
  }
}
