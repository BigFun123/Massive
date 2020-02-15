using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.src.Controllers
{
  public class LobbyList
  {
    public List<LobbyEntry> data;
  }

  public class LobbyEntry
  {
    private string name = "";
    private string iP = "127.0.0.1";
    private string domain = "bigfun.co.za";
    private string worldType = "Real World";
    private DateTime lastUsed = DateTime.Now;
    private int users = 0;

    public string Name { get => name; set => name = value; }
    public string IP { get => iP; set => iP = value; }
    public DateTime LastUsed { get => lastUsed; set => lastUsed = value; }
    public string WorldType { get => worldType; set => worldType = value; }
    public int Users { get => users; set => users = value; }
    public string Domain { get => domain; set => domain = value; }
  }
}
