using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Server
{
  public class Logger
  {
    public static object LogLocker = new object();

    public static void WriteLog(string s)
    {
      lock (LogLocker)
      {
        File.AppendAllText("log.txt", s + "\n");
      }
    }
  }
}
