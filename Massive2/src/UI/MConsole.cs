using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Massive.Events;
/**
 * Sends text to the editor console. Does nothing in game
 * 
 * */

namespace Massive
{
  public static class MConsole
  {
    public static event EventHandler<InfoEvent> ConsoleEvent;

    public static void Print(string s)
    {
      if ( ConsoleEvent != null )
      {
        ConsoleEvent(null, new InfoEvent(s));
      }
    }

  }
}
