using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Massive.Events;
using OpenWorld.Controllers;
using Massive.Tools;
using Massive;

namespace OpenWorld.src.Controls
{
  public partial class BookmarksControl : UserControl
  {
    public BookmarksControl()
    {
      InitializeComponent();
      MMessageBus.BookmarkAddedEvent += MMessageBus_BookmarkAddedEvent;
    }

    private void MMessageBus_BookmarkAddedEvent(object sender, BookmarkEvent e)
    {
      Populate();
    }

    void Populate()
    {
      foreach (Button b in flowLayoutPanel1.Controls)
      {
        b.Click -= But_Click;
      }

      flowLayoutPanel1.Controls.Clear();

      foreach (MBookmark b in BookmarkController.GetBookmarks())
      {
        Button but = new Button();
        but.Text = b.Name;
        but.AutoSize = true;
        but.ContextMenuStrip = contextMenuStrip1;
        but.FlatAppearance.BorderSize = 0;
        but.FlatStyle = FlatStyle.Flat;
        but.ImageList = imageList1;
        but.ImageIndex = 0;
        but.TextImageRelation = TextImageRelation.ImageBeforeText;
        but.TextAlign = ContentAlignment.MiddleRight;
        but.Height = 24;
        but.Tag = b;
        but.Click += But_Click;
        but.Parent = flowLayoutPanel1;
        but.Margin = new Padding(0);
      }
    }

    private void But_Click(object sender, EventArgs e)
    {
      Button b = (Button)sender;
      MBookmark bm = (MBookmark)b.Tag;
      if (bm != null)
      {

        if (Globals.Network.Connected == true)
        {
          MMessageBus.TeleportRequest(this,
          MassiveTools.VectorFromArray(bm.Position),
          MassiveTools.QuaternionFromArray(bm.Rotation));
        }
        else
        {
          MScene.Camera.transform.Position = MassiveTools.VectorFromArray(bm.Position);
          Globals.UserAccount.CurrentPosition = bm.Position;
          Globals.Avatar.SetPosition(MScene.Camera.transform.Position);
          MMessageBus.AvatarMoved(this, Globals.UserAccount.UserID,
            MScene.Camera.transform.Position,
            MassiveTools.QuaternionFromArray(bm.Rotation));
        }


      }
    }

    public void Setup()
    {
      Populate();
    }

    private void renameToolStripMenuItem_Click(object sender, EventArgs e)
    {
     
    }

    private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Button b = (Button)contextMenuStrip1.SourceControl;
      if (b != null)
      {
        BookmarkController.Delete((MBookmark)b.Tag);
        Populate();
      }

    }

    private void RenameTextBox_TextChanged(object sender, EventArgs e)
    {
      Button b = (Button)contextMenuStrip1.SourceControl;
      if (b != null)
      {
        BookmarkController.Rename((MBookmark)b.Tag, RenameTextBox.Text);
        //Populate();
      }
    }

    private void RenameTextBox_Leave(object sender, EventArgs e)
    {
      Populate();
    }

    private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
      Populate();
    }

    private void contextMenuStrip1_Opened(object sender, EventArgs e)
    {
      Button b = (Button)contextMenuStrip1.SourceControl;
      if (b != null)
      {
        MBookmark bm = (MBookmark)b.Tag;
        if (bm != null)
        {
          RenameTextBox.Text = bm.Name;
        }
      }
    }

    private void RenameTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if ( e.KeyCode == Keys.Enter)
      {
        Populate();
        contextMenuStrip1.Close();
      }
    }

    private void setGPSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Button b = (Button)contextMenuStrip1.SourceControl;
      if (b != null)
      {
        MBookmark bm = (MBookmark)b.Tag;
        if (bm != null)
        {
          MMessageBus.Navigate(this, MassiveTools.VectorFromArray(bm.Position));
        }
      }
    }
  }
}
