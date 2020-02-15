using Massive;
using Massive.Events;
using OpenTK;
using OpenWorld.Controllers;
using OpenWorld.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpernWorld.Widgets
{
  public class MTeleporterWidget 
  {
    public static void Mc_DoubleClick(MObject mo)
    {
      string sTag = (string)mo.Tag;
      string[] parms = sTag.Split('|');
      if (parms.Length > 0)
      {
        NavigationBarDecoder dec = new NavigationBarDecoder();
        Vector3d v = dec.Decode(parms[1]);
        MMessageBus.Status(mo, "Teleporting to:" + parms[1] + " - " + parms[2]);
        MMessageBus.TeleportRequest(mo, v, Quaterniond.Identity);
      }
    }

    public static void Mc_RightClick(MObject mo)
    {
      if (mo.OwnerID.Equals(Globals.UserAccount.UserID))
      {
        TeleporterConfigForm form = new TeleporterConfigForm();
        form.Setup(mo);
        form.Show(Globals.GUIThreadOwner);
      }
    }
  }
}
