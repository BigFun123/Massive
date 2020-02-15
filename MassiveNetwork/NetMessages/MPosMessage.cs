using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class MPosMessage : IMSerializable
  {
    public string UserID = "";
    public string InstanceID = "";
    public string TemplateID = ""; //id to spawn     
    public double[] Position = new double[3] { 0, 0, 0 };
    public double[] Rotation = new double[4] { 0, 0, 0, 1 };
    
  }
}
