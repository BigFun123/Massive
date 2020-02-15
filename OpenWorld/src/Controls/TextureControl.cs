using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Massive;
using Massive.Events;
using Massive.Platform;
using System.IO;
using OpenWorld.Forms;

namespace OpenWorld.src.Controls
{
  public partial class TextureControl : UserControl
  {
    MSceneObject SelectedItem;
    string sClipboard;

    public TextureControl()
    {
      InitializeComponent();
      MMessageBus.SelectEventHandler += MMessageBus_SelectEventHandler;
      MMessageBus.ObjectDeletedEvent += MMessageBus_ObjectDeletedEvent;
    }

    public void Setup()
    {
      CreateButtons();
    }

    void CreateButtons()
    {
      Dictionary<string, MBuildingBlock> blocks = MBuildParts.GetBlocks();
      foreach (KeyValuePair<string, MBuildingBlock> k in blocks)
      {
        MBuildingBlock bb = k.Value;
        if (bb.Type != MBuildingBlock.MATERIAL_TYPE) continue;
        string sPath = Path.Combine(MFileSystem.AssetsPath, bb.Path);
        Bitmap bmp = new Bitmap(Path.GetFullPath(sPath));
        ListViewItem lvi = new ListViewItem(bb.Name);
        imageList1.Images.Add(bb.TextureID, bmp);
        lvi.ImageKey = bb.TextureID;
        lvi.Tag = bb.TextureID;
        TextureView.Items.Add(lvi);
      }
    }

    private void MMessageBus_ObjectDeletedEvent(object sender, DeleteEvent e)
    {
      if (SelectedItem == null) return;
      if (e.InstanceID == SelectedItem.InstanceID)
      {
        SelectedItem = null;
      }
    }

    private void MMessageBus_SelectEventHandler(object sender, SelectEvent e)
    {
      if (e == null) return;
      SelectedItem = e.Selected;
    }

    void SetTexture(string TextureID)
    {
      if (SelectedItem == null) return;
      MMessageBus.ChangeTextureRequest(this, SelectedItem.InstanceID, TextureID);
    }

    private void MATERIAL_Click(object sender, EventArgs e)
    {
      Button b = (Button)sender;
      SetTexture((string)b.Tag);
    }

    private void TextureView_MouseClick(object sender, MouseEventArgs e)
    {
      if (TextureView.SelectedItems.Count > 0)
      {
        ListViewItem lvi = TextureView.SelectedItems[0];
        if (lvi != null)
        {
          SetTexture((string)lvi.Tag);
        }
      }
    }

    private void UploadTexture_Click(object sender, EventArgs e)
    {
      if (SelectedItem == null) return;
      ImageUploader iu = new ImageUploader();
      //iu.Show(TopLevelControl);
      iu.sTargetID = SelectedItem.InstanceID;
      if (iu.ShowDialog() == DialogResult.OK)
      {
        //sLocus now contains the URL of the uploaded image
        MMessageBus.ChangeTextureRequest(this, SelectedItem.InstanceID, iu.sPublicFile);
      }

    }

    private void CopyButton_Click(object sender, EventArgs e)
    {
      if (SelectedItem == null) return;
      sClipboard = SelectedItem.material.MaterialID;
    }

    private void PasteButton_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(sClipboard)) return;
      MMessageBus.ChangeTextureRequest(this, SelectedItem.InstanceID, sClipboard);
    }
  }
}
