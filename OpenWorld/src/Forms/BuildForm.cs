using Massive;
using Massive.Events;
using Massive.Network;
using Massive.Tools;
using OpenTK;
using OpenWorld.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OpenWorld.Forms
{
  public partial class BuildForm : DToolForm
  {
    MBuildingBlock LastBuild = null;
    int BuildCounter = 100;
    
    MSceneObject SelectedItem;
    public BuildForm()
    {
      InitializeComponent();
      SetTitle("Building and Texturing");
      MMessageBus.SelectEventHandler += MMessageBus_SelectEventHandler;
      MMessageBus.ObjectDeletedEvent += MMessageBus_ObjectDeletedEvent;
      MMessageBus.ObjectCreatedHandler += MMessageBus_ObjectCreatedHandler;
      ZoneCheckTimer.Start();
      Style(this);
    }

    void ClearEvents()
    {
      MMessageBus.SelectEventHandler -= MMessageBus_SelectEventHandler;
      MMessageBus.ObjectDeletedEvent -= MMessageBus_ObjectDeletedEvent;
      MMessageBus.ObjectCreatedHandler -= MMessageBus_ObjectCreatedHandler;
    }

    private void MMessageBus_ObjectCreatedHandler(object sender, CreateEvent e)
    {
      MScene.SelectedObject = e.CreatedObject;
    }

    private void MMessageBus_ObjectDeletedEvent(object sender, DeleteEvent e)
    {
      if (SelectedItem == null) return;
      if (e.InstanceID == SelectedItem.InstanceID)
      {
        SelectedItem = null;
        SelectedLabel.Text = "...";
      }
    }

    private void MMessageBus_SelectEventHandler(object sender, SelectEvent e)
    {
      if (e == null) return;
      SelectedItem = e.Selected;
      if (SelectedItem != null)
      {
        SelectedLabel.Text = SelectedItem.Name;
      }
    }

    void Add(MBuildingBlock bb)
    {
      LastBuild = bb;
      if (MStateMachine.ZoneLocked == true)
      {
        MMessageBus.Error(this, "Can't build here, zone is locked / other building nearby");
        return;
      }

      Vector3d pos = Globals.Avatar.GetPosition();

      if (bb.TemplateID.Equals(MBuildParts.FOUNDATION01))
      {
        pos += Globals.Avatar.Forward() * 13 - Globals.Avatar.Up() * 1.0;
      }
      else
      {
        pos += Globals.Avatar.Forward() * 4 + Globals.Avatar.Up() * 3.0;
      }

      Quaterniond rot = Globals.LocalUpRotation();

      if (SelectedItem != null)
      {
        rot = SelectedItem.transform.Rotation;
        //prevent smaller items getting lost in the foundation
        if (SelectedItem.TemplateID != MBuildParts.FOUNDATION01)
        {
          //pos = SelectedItem.transform.Position;
        }
      }

      if (Globals.Network.Connected == true)
      {
        Globals.Network.SpawnRequest(bb.TemplateID, "TEXTURE01", bb.TemplateID, bb.TemplateID, pos, rot);
      }
      else
      {
        MServerObject mso = new MServerObject();
        mso.InstanceID = Helper.GUID();
        mso.OwnerID = Globals.UserAccount.UserID;
        mso.TextureID = bb.TextureID;
        mso.TemplateID = bb.TemplateID;
        mso.Position = MassiveTools.ArrayFromVector(pos);
        mso.Rotation = MassiveTools.ArrayFromQuaterniond(rot);        
        MMessageBus.CreateObjectRequest(this, mso);
      }
    }

    private void Duplicate_Click(object sender, EventArgs e)
    {
      if (SelectedItem == null) return;
      Vector3d pos = SelectedItem.transform.Position;
      Quaterniond rot = SelectedItem.transform.Rotation;
      string sTag = "";
      Globals.Network.SpawnRequest(SelectedItem.TemplateID, SelectedItem.material.MaterialID, SelectedItem.Name, sTag, pos, rot);
    }


    void Populate()
    {
      PartsView.Items.Clear();
      Dictionary<string, MBuildingBlock> Blocks = MBuildParts.GetBlocks();

      foreach (KeyValuePair<string, MBuildingBlock> kv in Blocks)
      {
        if (string.IsNullOrEmpty(kv.Value.Model)) continue;
        string sIconPath = GetIconPathForModel(kv.Value.Model);
        ListViewItem lvi = new ListViewItem(kv.Value.Name);
        lvi.Tag = kv.Value;
        if (!File.Exists(sIconPath))
        {
          Console.WriteLine("BuildForm: File not found: " + sIconPath);
        }
        else
        {
          Bitmap icon = new Bitmap(sIconPath);
          imageList1.Images.Add(kv.Value.Name, icon);
        }

        lvi.ImageKey = kv.Value.Name;
        PartsView.Items.Add(lvi);
      }
    }

    string GetIconPathForModel(string sModelPath)
    {
      string sName = Path.Combine(Path.GetDirectoryName(sModelPath), Path.GetFileNameWithoutExtension(sModelPath));
      return Path.GetFullPath(Path.Combine("Assets", sName + ".png"));
    }

    private void BuildForm_Load(object sender, EventArgs e)
    {
      Populate();
      textureControl1.Setup();
    }

    private void PartsView_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (PartsView.SelectedItems.Count > 0)
      {
        ListViewItem lvi = PartsView.SelectedItems[0];
        MBuildingBlock mb = (MBuildingBlock)lvi.Tag;
        if (mb != null)
        {
          Add(mb);
        }
      }
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
      if (SelectedItem == null) return;
      Globals.Network.DeleteRequest(SelectedItem.InstanceID);
    }

    private void BuildForm_Shown(object sender, EventArgs e)
    {
      Rectangle r = Main.ClientRect;
      this.Location = new Point(r.Location.X + r.Width - this.Width, r.Y);
    }

    private void ZoneCheckTimer_Tick(object sender, EventArgs e)
    {
      if (Globals.Avatar == null) return;
      if (Globals.Avatar.Target == null) return;

      if ( LastBuild != null)
      {
        BuildCounter--;
        if (BuildCounter > 0)
        {
         // Add(LastBuild);
        }
      }

      MBuildCheckService.ZoneLocked(Globals.Avatar.GetPosition());
      if (MStateMachine.ZoneLocked)
      {
        CanIBuildHere.BackColor = Color.Red;
        CanIBuildHere.Text = "Zone Locked";
      }
      else
      {
        CanIBuildHere.BackColor = Color.Green;
        CanIBuildHere.Text = "Zone Unlocked";
      }
    }

    private void BuildForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      ClearEvents();
    }
  }
}
