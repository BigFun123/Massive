using Massive;
using Massive.Tools;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive2.Modules.Widgets
{
  public class MTeleporter
  {
    static string[] DecodeTag(string s)
    {
      string[] tags = s.Split('|');
      if ( tags.Length!=3)
      {
        tags = new string[3] { "TELEPORT01", "10,10,10", "The Sun" };
        Console.WriteLine("MTeleporter.DecodeTag Tag was corrupt");
      }
      return tags;
    }

    public static Vector3d GetDestination(MSceneObject mo)
    {
      Vector3d dest = Vector3d.Zero;
      string[] tags = DecodeTag((string)mo.Tag);
      string sDest = tags[1];
      MassiveTools.VectorFromString(sDest);
      return dest;
    }

    public static string GetDescription(MSceneObject mo)
    {
      string[] tags = DecodeTag((string)mo.Tag);
      return tags[2];
    }
  }
}
