using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class MChangeProperty : IMSerializable
  {    
    public string InstanceID;
    public string PropertyTag; // TYPE|PARM1|PARM2    
  }
}
