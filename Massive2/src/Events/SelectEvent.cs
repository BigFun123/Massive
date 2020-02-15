using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class SelectEvent : EventArgs
  {
    public MSceneObject Selected;
    public SelectEvent(MSceneObject _inSelected)
    {
      Selected = _inSelected;
    }
  }
}
