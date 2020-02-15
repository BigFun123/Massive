using Massive.Events;
using Massive.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Services
{
  public class MZoneService
  {
    public static List<MServerZone> Zones = new List<MServerZone>();

    static MZoneService()
    {    
      MMessageBus.ZoneAddHandler += MMessageBus_ZoneAddHandler;
    }

    private static void MMessageBus_ZoneAddHandler(object sender, ZoneEvent e)
    {
      Zones.Add(e.Zone);
    }

    public static MServerZone Find(string s)
    {
      foreach( MServerZone zone in Zones)
      {
        if ( zone.Name.Equals(s, StringComparison.CurrentCultureIgnoreCase))
        {
          return zone;
        }
      }
      return null;
    }
    
  }
}
