using Massive;
using Massive.Events;
using System;
using System.IO;

namespace OpenWorld.Forms
{
  public partial class ProductInfoForm : DToolForm
  {
    string InstanceID;
    MSceneObject Target;
    public string Description;
    public string Price;

    public ProductInfoForm()
    {
      InitializeComponent();     
      MMessageBus.PropertyChangeEvent += MMessageBus_PropertyChangeEvent;
    }

    private void MMessageBus_PropertyChangeEvent(object sender, ChangePropertyEvent e)
    {
      if ( e.InstanceID == InstanceID)
      {
        StatusText.Text = "SUCCESS";
      }
      else
      {
        StatusText.Text = "Can't update. Retry?";
      }
    }

    public void Setup(MObject o, bool ReadOnly = false)
    {
      MSceneObject mo = (MSceneObject)o;
      if (mo == null) return;
      Target = mo;
      InstanceID = Target.InstanceID;
      UpdateImage();
      UpdateData();

      if ( ReadOnly == true )
      {
        UpdateButton.Visible = false;
        InfoBox.ReadOnly = true;
      }

    }

    void UpdateData()
    {
      SetTitle("Info");
      if ( Target.Tag != null)
      {
        string sTag = (string)Target.Tag;
        if (string.IsNullOrEmpty(sTag)) return;
        string[] Items = sTag.Split('|');
        
        if (Items.Length > 1)
        {
          Description = Items[1];
          InfoBox.Text = Description;
        }

        if (Items.Length > 2)
        {
          Price= Items[2];
          MetaData.Text = Price;
        }
      }
    }

    void UpdateImage()
    {
      MTexture mt = (MTexture)Target.FindModuleByType(MObject.EType.Texture);
      if (mt == null) return;

      if ( File.Exists(mt.CacheFilename ))
      {
        pictureBox1.ImageLocation = mt.CacheFilename;
      }
      else
      if (File.Exists(mt.Filename))
      {
        pictureBox1.ImageLocation = mt.Filename;
      }
      else
      {
        //URL?
        //pictureBox1.ImageLocation = mt.Filename;
      }      
    }

    private void UpdateButton_Click(object sender, EventArgs e)
    {
      Description = InfoBox.Text;
      Price = MetaData.Text;

      Globals.Network.ChangePropertyRequest(InstanceID, MBuildParts.BOOK01+ "|" + Description + "|" + Price);
    }
  }
}
