using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class KeyboardEvent
  {
    public int KeyCode;
    public bool Down;
    public KeyboardEvent(int inKeyCode, bool inDown)
    {
      KeyCode = inKeyCode;
      Down = inDown;
    }
  }
}
