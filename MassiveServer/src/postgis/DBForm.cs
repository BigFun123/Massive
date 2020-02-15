using Massive.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MassiveServer.src.postgis
{
  public partial class DBForm : Form
  {
    MPostGISAdapter postgis;

    public DBForm()
    {
      InitializeComponent();
      postgis = new MPostGISAdapter();
      //ResultsView.DataSource = GetResults();
    }

    DataTable GetResults()
    {
      DataTable res = new DataTable();    
      
      return res;
    }

    void Execute()
    {
      string sResult;
      ResultsView.DataSource = postgis.Query(CodeBox.Text, out sResult);
      ResultBox.Text = sResult;
    }

    private void ExecuteButton_Click(object sender, EventArgs e)
    {
      Execute();
    }

    private void DBForm_Shown(object sender, EventArgs e)
    {
      postgis.Connect();
    }

    private void DBForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      postgis.Close();
    }

    private void CodeBox_KeyDown(object sender, KeyEventArgs e)
    {
      if ( e.KeyCode == Keys.F5)
      {
        Execute();
      }
    }

    private void DBForm_Load(object sender, EventArgs e)
    {
      // TODO: This line of code loads data into the 'postgresDataSet.objects' table. You can move, or remove it, as needed.
      //this.objectsTableAdapter.Fill(this.postgresDataSet.objects);

    }
  }
}
