using Massive.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveNetwork.NetMessages
{
  public class MChangeDetailsRequest : IMSerializable
  {
    public string Email;
    public string Password;
    public string UserName;
    public string AvatarID;
  }
}
