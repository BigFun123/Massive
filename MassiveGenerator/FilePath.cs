using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveGenerator
{
  public class FilePath
  {
    public static string IP = "192.168.99.100";
    public static string Port = "32794";

    public static string GetPath(vector3 v, string subfolder, string append="")
    {
      string path = string.Format(@"data\{0}\{3}\{1}_{2}{4}", v.z, v.x, v.y, subfolder, append);
      return path;
    }
  }
}
