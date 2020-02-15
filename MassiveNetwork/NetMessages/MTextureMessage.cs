using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class MTextureMessage : IMSerializable
  {
    public string OwnerID;
    public string InstanceID;
    public string TextureID;
  }
}
