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

namespace OpenWorld
{
  public partial class View3D : UserControl
  {
    public View3D()
    {
      InitializeComponent();
     
    }

    public void Setup()
    {
     // AddDemoData();
    }

    void AddDemoData()
    {
      MCube c = Helper.CreateCube(MScene.ModelRoot, "Cube");
      MObjectAnimation ma = new MObjectAnimation("Animation");
      ma.Speed = 1;
      ma.AngleOffset = new OpenTK.Quaterniond(0.1, 0.1, 0.1);
      c.Add(ma);
      c.Setup();
    }

    private void Application_Idle(object sender, EventArgs e)
    {
      Globals._scene.ClearBackBuffer();
      Globals._scene.Render();
      glControl1.SwapBuffers();
    }
  }
}
