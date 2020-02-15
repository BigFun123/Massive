using Massive;
using Massive.Events;
using OpenWorld.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Widgets
{
  public class MStatusWidget
  {
    public static void Mc_DoubleClick(MObject mo)
    {
      if (string.IsNullOrEmpty((string)mo.Tag)) return;
      MMessageBus.Status(null, "Opening:" + mo.Tag);
      UserStatusForm form = new UserStatusForm();
      form.Setup(mo, true);
      form.Show(Globals.GUIThreadOwner);
    }

    public static void Mc_RightClick(MObject mo)
    {
      if (mo.OwnerID.Equals(Globals.UserAccount.UserID))
      {
        UserStatusForm form = new UserStatusForm();
        form.Setup(mo);
        form.Show(Globals.GUIThreadOwner);
      }
    }
  }
}
