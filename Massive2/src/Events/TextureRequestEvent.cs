using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class TextureRequestEvent
  {
    public string InstanceID = "";
    public string TextureID = "";

    public TextureRequestEvent(string inInstanceID, string inTexID)
    {
      InstanceID = inInstanceID;
      TextureID = inTexID;
    }
  }
}
