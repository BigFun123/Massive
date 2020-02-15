using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class UpdateEvent
  {
    public double DeltaTime;
    public UpdateEvent(double inDeltaTime)
    {
      DeltaTime = inDeltaTime;
    }
  }
}
