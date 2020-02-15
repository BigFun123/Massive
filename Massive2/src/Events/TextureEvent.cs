using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class TextureEvent
  {
    public string InstanceID = "";
    public string TextureID = "";

    public TextureEvent(string inInstanceID, string inTexID)
    {
      TextureID = inTexID;
      InstanceID = inInstanceID;
    }
  }
}
