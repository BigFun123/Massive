using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class DeleteEvent
  {
    public string InstanceID;
    public DeleteEvent(string inInstanceID)
    {
      InstanceID = inInstanceID;
    }
  }
}
