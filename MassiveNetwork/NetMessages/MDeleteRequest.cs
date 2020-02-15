using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class MDeleteMessage : IMSerializable
  {
    public string InstanceID;
    public string UserID; //user requesting the deletion
  }
}
