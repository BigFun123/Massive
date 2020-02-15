using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class GraphChangedEvent : EventArgs
  {
    public MObject ChangedObject;
    public enum ChangeType { Created, Added, Removed, Disposed };
    public ChangeType Reason;
    public GraphChangedEvent(MObject inObject, ChangeType inReason)
    {
      ChangedObject = inObject;
      Reason = inReason;
    }
  }
}
