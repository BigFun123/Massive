using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class FocusEvent : EventArgs
  {
    public MSceneObject FocusObject;

    public FocusEvent(MSceneObject inFocusObject)
    {
      FocusObject = inFocusObject;
    }
  }
}
