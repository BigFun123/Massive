using Massive.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class ZoneEvent
  {
    public MServerZone Zone;

    public ZoneEvent(MServerZone inZone)
    {
      Zone = inZone;
    }
  }
}
