using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class ChangePropertyEvent
  {    
    public string InstanceID;
    public string PropertyTag;

    public ChangePropertyEvent(string inInstanceID, string inPropertyTag)
    {
      InstanceID = inInstanceID;
      PropertyTag = inPropertyTag;
    }
  }
}
