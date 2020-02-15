using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class BooleanEvent
  {
    public bool State;

    public BooleanEvent(bool bState)
    {
      State = bState;
    }

  }
}
