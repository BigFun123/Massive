using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class ChangeModeEvent
  {
    public MAvatar.eMoveMode NewMode;

    public ChangeModeEvent(MAvatar.eMoveMode nMode)
    {
      NewMode = nMode;
    }

  }
}
