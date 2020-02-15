using Massive;
using Massive.Events;
using Massive2.src.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.src.Controllers
{
  public class LobbyController
  {
    List<LobbyEntry> Entries;

    public LobbyController()
    {

    }

    public void Setup()
    {
      if (Entries == null)
      {
        Entries = new List<LobbyEntry>();
      }
    }

    public void GetLobbyList()
    {
      WebClient wc = new WebClient();
      try
      {
        string sResponse = wc.DownloadString(Globals.LobbyDataURL);
        Console.WriteLine(sResponse);
        ParseJSON(sResponse);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

      DataTable dt = DataTableTools.ConvertToDatatable(Entries);
      TableEvent te = new TableEvent(dt);
      MMessageBus.LobbyLoaded(this, te);
    }

    void ParseJSON(string sLobby)
    {
      LobbyList o = JsonConvert.DeserializeObject<LobbyList>(sLobby);
      if  ( o == null )
      {
        MMessageBus.Status(this, "Error retrieving lobby");
      }
      Entries = o.data;
    }
  }
}
