using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class ISerializable
  {
    public string Serialize()
    {
      return JsonConvert.SerializeObject(this);
    }

    public static T Deserialize<T>(string sJson)
    {
      return JsonConvert.DeserializeObject<T>(sJson);
    }

    public void CopyTo<T>(T dest)
    {

    }
  }
}
