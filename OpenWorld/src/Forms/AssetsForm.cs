using Massive;
using Massive.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWorld.Forms
{
  public partial class AssetsForm : DToolForm
  {
    List<MSceneObject> AssetsList;

    public AssetsForm()
    {
      InitializeComponent();
      SetTitle("Assets");
      AssetsList = new List<MSceneObject>();
      Populate();
      MMessageBus.ObjectCreatedHandler += MMessageBus_ObjectCreatedHandler;
      MMessageBus.ObjectDeletedEvent += MMessageBus_ObjectDeletedEvent;
    }

    private void MMessageBus_ObjectDeletedEvent(object sender, DeleteEvent e)
    {
      Populate();
    }

    private void MMessageBus_ObjectCreatedHandler(object sender, CreateEvent e)
    {
      Populate();
    }

    void Style()
    {
      foreach (DataGridViewColumn c in AssetsView.Columns)
      {
        c.HeaderCell.Style.BackColor = Color.Black;
        c.HeaderCell.Style.ForeColor = Color.LightGray;
        c.HeaderCell.Style.SelectionBackColor = Color.DarkGray;
      }
      foreach (DataGridViewRow r in AssetsView.Rows)
      {
        r.HeaderCell.Style.BackColor = Color.Black;
      }
    }

    private void AssetsView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
    {
      AssetsView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Black;
      AssetsView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
    }


    void AddModules(MObject mods)
    {
      foreach (MObject mo in mods.Modules.ToList())
      {
        //if (mo.Renderable == false) continue;
        if (mo is MSceneObject)
        {
          MSceneObject mso = (MSceneObject)mo;
          //if (mso.OwnerID == Globals.UserAccount.UserID)
          {
            AssetsList.Add(mso);
          }
        }
        AddModules(mo);
      }
    }

    void Populate()
    {
      AssetsList.Clear();
      AddModules(MScene.Priority1);
      AddModules(MScene.Priority2);
      AddModules(MScene.Background);
      AddModules(MScene.Background2);

      AssetsView.DataSource = null;
      AssetsView.DataSource = AssetsList;
      AssetsView.Columns["OwnerID"].Visible = false;
      AssetsView.Columns["OwnerID"].ReadOnly = true;
      AssetsView.Columns["InstanceID"].ReadOnly = true;

      Style();
    }

    private void AssetsView_SelectionChanged(object sender, EventArgs e)
    {
      //DataGridViewSelectedRowCollection col = AssetsView.SelectedRows;

      DataGridViewSelectedCellCollection col = AssetsView.SelectedCells;

      if (col.Count > 0)
      {
        MSceneObject mo = (MSceneObject)col[0].OwningRow.DataBoundItem;
        MMessageBus.Navigate(this, mo.transform.Position);
        MMessageBus.Select(this, new SelectEvent(mo));
      }

    }

    private void AssetsView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      DataGridViewRow vrow = AssetsView.Rows[e.RowIndex];
      MSceneObject row = (MSceneObject)vrow.DataBoundItem;
      
      MMessageBus.TeleportRequest(this, row.transform.Position + row.BoundingBox.Size(), row.transform.Rotation);

    }

    private void AssetsForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      AssetsView.DataSource = null;
      MMessageBus.ObjectCreatedHandler -= MMessageBus_ObjectCreatedHandler;
      MMessageBus.ObjectDeletedEvent -= MMessageBus_ObjectDeletedEvent;
    }

    private void AssetsView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      Console.WriteLine(e.ColumnIndex + "," + e.RowIndex);
    }

    private void AssetsView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
      Console.WriteLine("click");
      AssetsView.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }
  }
}
