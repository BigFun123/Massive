using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

/**
 * 
 * Splash Screen
 * 
 * */

namespace Massive
{
  public class MSplash : MControl
  {
    public string sPath;
    Timer t = new Timer();
    private double countdown = 3000;
    public double Countdown { get => countdown; set => countdown = value; }


    public MSplash(string sName = "Splash") : base(sName)
    {    
      t.Interval = 100;
      t.Elapsed += T_Elapsed;
      Enabled = false;
    }

    public void SetBackground(string sImagePath)
    {
      sPath = sImagePath;
    }

    public override void Setup()
    {
      AddMaterial(new MMaterial("ButtonMat"));
      material.AddShader(Helper.GetGUIShader());
      material.SetDiffuseTexture(Globals.TexturePool.GetTexture(sPath));
      base.Setup();
    }

    public override void OnPlay()
    {
      base.OnPlay();
      Enabled = true;
      t.Start();
    }

    private void T_Elapsed(object sender, ElapsedEventArgs e)
    {
      Countdown -= t.Interval;        
      if (Countdown < 1000)
      {               
        material.Opacity = (Countdown*Countdown) / 1000000;
      }
      
      if (Countdown <= 0)
      {
        t.Elapsed -= T_Elapsed;
        Hide();
      }
    }

    void Hide()
    {
      t.Stop();
      this.Enabled = false;
    }
  }
}
