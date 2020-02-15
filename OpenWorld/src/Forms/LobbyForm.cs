using Massive;
using Massive.Events;
using OpenWorld.src.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWorld.Forms
{
  public partial class LobbyForm : DToolForm
  {
    LobbyController _lobbyController;
    public LobbyForm()
    {
      InitializeComponent();
      _lobbyController = new LobbyController();
      Globals.Network.ConnectedToServerHandler += Network_ConnectedToServerHandler;
      MMessageBus.LobbyLoadedHandler += MMessageBus_LobbyLoadedHandler;
      MMessageBus.LobbyLoadRequestHandler += MMessageBus_LobbyLoadRequestHandler;
    }

    private void MMessageBus_LobbyLoadRequestHandler(object sender, ChangeDetailsEvent e)
    {
      Globals.GUIThreadOwner.BeginInvoke((MethodInvoker)delegate
      {       
        this.Show();
      });
    }

    void SetupDatagrid(DataTable dt)
    {
      dataGridView1.DataSource = dt;
      if (dataGridView1.Columns.Count == 0)
      {
        MMessageBus.Error(this, "LobbyForm:Problem loading lobby");
        return;
      }
      dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
      dataGridView1.Columns[1].Visible = false;

      DataGridViewRow dg = dataGridView1.Rows[0];
      if (dg == null) return;
      ServerIPBox.Text = dg.Cells[1].Value.ToString();
    }

    private void MMessageBus_LobbyLoadedHandler(object sender, TableEvent e)
    {
      SetupDatagrid(e.Table);
    }

    private void Network_ConnectedToServerHandler(object sender, Massive.Events.StatusEvent e)
    {
      Globals.GUIThreadOwner.BeginInvoke((MethodInvoker)delegate
      {
        this.Hide();
      });
    }

    private void LobbyForm_Load(object sender, EventArgs e)
    {
      _lobbyController.Setup();
      _lobbyController.GetLobbyList();
    }

    private void JoinButton_Click(object sender, EventArgs e)
    {
      //string sName = dg.Cells[0].Value.ToString();
      //string sIP = dg.Cells[1].Value.ToString();
      Join();      
    }

    void Join()
    {
      Globals.Network.ServerIP = ServerIPBox.Text;
      Globals.Network.ServerDomain = DomainBox.Text;
      Globals.Network.Setup();
    }

    private void LobbyForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      Hide();
    }

    private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
    {
     
    }

    private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (dataGridView1.SelectedRows.Count == 0) return;
     DataGridViewRow dg = dataGridView1.SelectedRows[0];
      if (dg == null) return;
      //todo: get this from lobby model
      NameLabel.Text = dg.Cells[0].Value.ToString();
      ServerIPBox.Text = dg.Cells[1].Value.ToString();
      DomainBox.Text = dg.Cells[5].Value.ToString();
    }

    private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      /// removed because the double-click is unreliable and causes false connections
      //DataGridViewRow dg = dataGridView1.SelectedRows[0];
      //if (dg == null) return;
      //ServerIPBox.Text = dg.Cells[1].Value.ToString();
      //Join();
    }

  
  }
}
