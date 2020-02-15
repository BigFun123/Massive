using Massive;
using OpenWorld.controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWorld
{
  public partial class MainForm : Form
  {
    COpenWorld ow = new COpenWorld();
    public MainForm()
    {
      InitializeComponent();
      Globals.GUIThreadOwner = this;
      timer1.Tick += Timer1_Tick;
    }

    private void Timer1_Tick(object sender, EventArgs e)
    {
      UpdateWorld();
    }

    public void UpdateWorld()
    {
      ow.Update();
    }

    private void MainForm_Shown(object sender, EventArgs e)
    {
      ow.Setup();
      view3D1.Setup();
      timer1.Interval = 30;
      timer1.Start();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {

    }
  }
}
