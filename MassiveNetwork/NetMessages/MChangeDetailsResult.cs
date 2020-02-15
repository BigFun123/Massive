using Massive.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveNetwork.NetMessages
{
  public class MChangeDetailsResult : IMSerializable
  {
    public bool Success;
    public string Message;
  }
}
