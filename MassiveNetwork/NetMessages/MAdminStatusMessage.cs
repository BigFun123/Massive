using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class MAdminStatusMessage : IMSerializable
  {
    public int TotalConnections = 0;
    public int TotalZones = 0;
    public int TotalObjects = 0;
  }
}
