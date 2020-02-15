using Assimp;

namespace Massive.Graphics.Character
{
  public class MAnimationController
  {
    public const string IDLE = "idle";
    public const string WALK = "walk";
    public const string RUN = "run";
    public string sActiveAnimation = "idle";
    
    public float FrameCounter = 0;
    public double TotalTime = 5;    
    MAnimatedModel _model;    
    float BaseSpeed = 50.2f;
    float Speed = 1;
    Animation CurrentAnimation = null;

    public void Setup(MAnimatedModel model)
    {
      _model = model;
    }

    public void CopyTo(MAnimationController m)
    {
      m.TotalTime = TotalTime;
      m.FrameCounter = FrameCounter;
      m.sActiveAnimation = sActiveAnimation;
      m.TotalTime = TotalTime;
    }

    public void Update()
    {
      FrameCounter += (BaseSpeed + (float)Speed) * (float)Time.DeltaTime ;
      if (FrameCounter > TotalTime) FrameCounter = 0;
    }

    public Animation GetCurrentAnimation()
    {
      //return FindAnimation(_model.scene, sActiveAnimation);
      return CurrentAnimation;
    }

    public Animation FindAnimation(Scene scene, string sName)
    {
      foreach (Animation ani in scene.Animations)
      {
        if (ani.Name.ToLower().Contains(sName.ToLower()))
        {
          return ani;
        }
      }
      return null;
    }

    public void PlayAnimation(string sName, float inspeed)
    {      
      Speed = inspeed;
      CurrentAnimation = FindAnimation(_model.scene, sName);
      if (CurrentAnimation != null)
      {
        sActiveAnimation = sName;
        TotalTime = CurrentAnimation.DurationInTicks;
        if (FrameCounter >= TotalTime)
        {
          FrameCounter = 0;
        }
      }
    }
  }
}
