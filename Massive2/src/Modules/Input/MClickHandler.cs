using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Add this to a sceneobject
 * and subscribe to Callback to get click notification
 * 
 * */

namespace Massive
{
  public delegate void ClickFunction(MObject mo);

  public class MClickHandler : MObject
  {
    public ClickFunction Clicked;
    public ClickFunction RightClicked;
    public ClickFunction DoubleClicked;

    public MClickHandler() : base(EType.ClickHandler, "ClickHandler")
    {
    }

    public override void OnClick(bool DoubleClick, bool RightClick)
    {
      base.OnClick();

      if (DoubleClick==true)
      {
        if (DoubleClicked != null) DoubleClicked(this.Parent);
      }
      else
      if (RightClick==true)
      {
        if (RightClicked != null) RightClicked(this.Parent);
      }
      else
      {
        if (Clicked != null) Clicked(this.Parent);
      }

    }
  }
}
