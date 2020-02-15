using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autoversion
{
  class Program
  {
    static void Main(string[] args)
    {
      int ver = 0;
      if ( File.Exists(@"..\..\version.txt"))
      {
        string s = File.ReadAllText(@"..\..\version.txt");
        int.TryParse(s, out ver);
      }
      ver++;


      string sVersionClass =
@"public class MVersion 
{
   public static int VERSION={version};
}";

      sVersionClass = sVersionClass.Replace("{version}", ver.ToString());
      
      File.WriteAllText(@"..\..\version.txt", ver.ToString());
      File.WriteAllText(@"..\..\src\Version.cs", sVersionClass.ToString());
    }
  }
}
