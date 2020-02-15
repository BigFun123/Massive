using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Massive.Events;

namespace Massive
{
  public class MButton : MControl
  {
    public event EventHandler<GUIEvent> ClickEvent;
    MText TextItem;
    MPanel Panel;
    public string Text;
    public string IconPath = "";

    public MButton(string sName="Button", string sIcon= "UI\\Art\\button-bg-default.png") : base(sName)
    {
      CreateGeometry = false;
      IconPath = sIcon;
    }

    public override void Setup()
    {
     // MMaterial mt = new MMaterial("nullmat");
     // AddMaterial(mt);
      Panel = MGUI.AddPanel(this, 0, 0, Width, Height, "Panel");
      MMaterial m = new MMaterial("ButtonMat");
      Panel.AddMaterial(m);
      
      m.AddShader(Helper.GetGUIShader());
      m.SetDiffuseTexture(Globals.TexturePool.GetTexture(IconPath));

      TextItem = MGUI.AddText(this, 0, 0, Width, Height, "ButtonText", "Hello");
      TextItem.Text = Text;
     
      base.Setup();
      this.material = null;
    }

    public override void DoClick()
    {
      if ( ClickEvent != null)
      {
        ClickEvent(this, new GUIEvent(this));
      }
     // base.OnClick();
    }
  }
}
