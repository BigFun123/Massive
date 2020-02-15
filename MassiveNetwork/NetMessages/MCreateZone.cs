using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class MCreateZone : IMSerializable
  {
    public string Name;
    public string Description;
    public string OwnerID;
    public string TextureID;
  }
}
