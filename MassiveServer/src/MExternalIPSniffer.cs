using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MassiveServer.src
{
  public class MExternalIPSniffer
  {
    public static string WebServerFolderToWrite = @"c:\dev\bigfun\";
    public static string externalip = "0.0.0.0";

    public static void SniffIP()
    {
      externalip = new WebClient().DownloadString("http://icanhazip.com");
      externalip = externalip.Trim();
      Console.WriteLine("External IP is:" + externalip);

      string sPath = Path.Combine(WebServerFolderToWrite, "ip.txt");

      if (Directory.Exists(WebServerFolderToWrite))
      {        
        File.WriteAllText(sPath, externalip);
        Console.WriteLine("Written to Web Server Folder::" + sPath);
      }      
      else
      {
        Console.WriteLine("WARNING:Web Server Folder does not exist :" + sPath);
      }
    }
  }
}
