using Massive.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class StateEvent : EventArgs
  {
    public MStateMachine.eStates state;
    public StateEvent(MStateMachine.eStates inState)
    {
      state = inState;
    }
  }
}
