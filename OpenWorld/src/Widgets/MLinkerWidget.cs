using Massive;
using Massive.Events;
using OpenWorld.Forms;
using System;
using System.Diagnostics;

namespace OpenWorld.Widgets
{
  public class MLinkerWidget
  {
    public static void Mc_DoubleClick(MObject mo)
    {
      if (string.IsNullOrEmpty((string)mo.Tag)) return;
      MMessageBus.Status(null, "Opening:" + mo.Tag);
      try
      {
        Process.Start((string)mo.Tag);
      }
      catch (Exception ee)
      {
        Console.WriteLine(ee.Message);
        MMessageBus.Error(mo, ee.Message);
      }
    }

    public static void Mc_RightClick(MObject mo)
    {
      if (mo.OwnerID.Equals(Globals.UserAccount.UserID))
      {
        LinkerConfigForm form = new LinkerConfigForm();
        form.Setup(mo);
        form.Show(Globals.GUIThreadOwner);
      }
    }
  }
}
