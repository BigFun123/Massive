using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class MModelURL : IMSerializable
  {
    public string ModelURL;
    public string TextureURL;

    public MModelURL(string inURL, string inTextureURL)
    {
      ModelURL = inURL;
      TextureURL = inTextureURL;
    }
  }
}
