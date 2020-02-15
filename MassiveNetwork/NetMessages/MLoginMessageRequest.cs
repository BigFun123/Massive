using Massive.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveNetwork.NetMessages
{
  public class MLoginMessageRequest : IMSerializable
  {    
    public string UserName;
    public string Email;
    public string Password;
    public string Zone;
  }
}
