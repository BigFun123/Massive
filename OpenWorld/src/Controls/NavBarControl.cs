using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Massive.Network;
using OpenTK;
using Massive;
using Massive.Services;
using OpenWorld.Controllers;
using Massive.Events;
using Massive.Tools;

namespace OpenWorld.Controls
{
  public partial class NavBarControl : UserControl
  {
    NavigationBarDecoder Decoder;

    public NavBarControl()
    {
      InitializeComponent();
      Decoder = new NavigationBarDecoder();
    }

    private void HomeButton_Click(object sender, EventArgs e)
    {
      MServerZone zone = MZoneService.Find("Earth");
      //Vector3d pos = MassiveTools.Vector3dFromVector3_Server(zone.Position);

      //cape town
      Vector3d pos = MassiveTools.VectorFromArray(Globals.UserAccount.HomePosition);

      if (Globals.Network.Connected == true)
      {
        MMessageBus.TeleportRequest(Globals.UserAccount.UserID, pos, Quaterniond.Identity);
      }
      else
      {
        MScene.Camera.transform.Position = pos;
        Globals.UserAccount.CurrentPosition = MassiveTools.ArrayFromVector(pos);
        Globals.Avatar.SetPosition(pos);
        MMessageBus.AvatarMoved(this, Globals.UserAccount.UserID, pos, Quaterniond.Identity);
      }

    }

    private void SiteBox_TextChanged(object sender, EventArgs e)
    {

    }

    private void SiteBox_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        Go();
      }
    }

    void Go()
    {
      string sText = SiteBox.Text;
      if (string.IsNullOrEmpty(sText)) return;
      sText = sText.Trim();
      SiteBox.Text = sText;
      Vector3d dest = Decoder.Decode(sText);
      if (Globals.Network.Connected == true)
      {
        MMessageBus.TeleportRequest(this, dest, Globals.LocalUpRotation());
      }
      else
      {
        MScene.Camera.transform.Position = dest;
        Globals.UserAccount.CurrentPosition = MassiveTools.ArrayFromVector(dest);
        Globals.Avatar.SetPosition(dest);
        MMessageBus.AvatarMoved(this, Globals.UserAccount.UserID, dest, Quaterniond.Identity);
      }
      MMessageBus.Navigate(this, dest);
    }

    private void BookmarkButton_Click(object sender, EventArgs e)
    {
      MMessageBus.AddBookmarkRequest(this, 
        new BookmarkEvent("Bookmark", 
        Globals.Avatar.GetPosition(), 
        Globals.Avatar.GetRotation()));
    }
  }
}
