using Massive;
using Massive.Events;
using Massive.Graphics.Character;
using Massive2.Graphics.Character;
using OpenTK;
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
  public partial class DebugForm : DToolForm
  {
    BindingList<MSceneObject> AssetsList;
    int Counter;    
    Vector3d Max;
    Vector3d Min;
    Vector3d Normalizer;
    List<Vector3d> Buffer = new List<Vector3d>();
    Pen RedPen = new Pen(Color.Red);
    Pen GreenPen = new Pen(Color.Green);
    Pen BluePen = new Pen(Color.LightBlue);
    Graphics graphics;
    public DebugForm()
    {
      InitializeComponent();
      SetTitle("Debug");
      AssetsList = new BindingList<MSceneObject>();
      timer1.Start();
      Opacity = 0.8f;
      MMessageBus.LateUpdateHandler += MMessageBus_LateUpdateHandler;
    }

    private void MMessageBus_LateUpdateHandler(object sender, UpdateEvent e)
    {
      //Vector3d v = Globals.Avatar.GetPosition();
      //Vector3d v = MScene.Camera.transform.Position;
      Vector3d v = MScene.Camera.TargetOffset;
      Buffer.Add(v);
      if (Buffer.Count > GraphBox.Width)
      {
        Buffer.RemoveAt(0);
      }
      CalcNormalize();

    }

    public void Setup()
    {
      AssetsList.Clear();


      foreach (MObject mo in MScene.Background.Modules)
      {
        if (!mo.Renderable) continue;
        MSceneObject mso = (MSceneObject)mo;
        AssetsList.Add(mso);
      }

      /*
      foreach (Matrix4 mo in MAnimatedModel.debug_transforms)
      {                
        AssetsList.Add(mo);
      }
  */
      try
      {
        DebugTable.DataSource = AssetsList;
        DebugTable.Refresh();
      }
      catch (Exception e)
      {
        Console.WriteLine("DebugForm:"+e.Message);
      }
      //DebugTable.Columns["OwnerID"].Visible = false;

    }

    void CalcNormalize()
    {      
      Min = new Vector3d(double.MaxValue);
      Max = new Vector3d(double.MinValue);
      for(int i=0; i< Buffer.Count; i++)
      {
        Vector3d v = Buffer[i];
        if (v.X < Min.X) Min.X = v.X;
        if (v.Y < Min.Y) Min.Y = v.Y;
        if (v.Z < Min.Z) Min.Z = v.Z;
        if (v.X > Max.X) Max.X = v.X;
        if (v.Y > Max.Y) Max.Y = v.Y;
        if (v.Z > Max.Z) Max.Z = v.Z;        
      }
      
      Normalizer.X = (Max.X);
      Normalizer.Y = (Max.Y);
      Normalizer.Z = (Max.Z);     
    }

    void DrawGraph(Graphics g)
    { 
      g.Clear(Color.Black);      
      
      int px = 0;
      int py = 0;
      int pz = 0;
      for ( int i=0; i< Buffer.Count-1; i++)
      {
        double dx = ((Buffer[i].X-Min.X) / (Max.X-Min.X));
        int vx1 = (int)((dx * ( GraphBox.Height/2.0)) + GraphBox.Height/4.0);
        if (vx1 < -GraphBox.Height) continue;
        if (vx1 > GraphBox.Height) continue;
        g.DrawLine(RedPen, new Point(i, px ), new Point(i + 1, vx1));

        double dy = ((Buffer[i].Y - Min.Y) / (Max.Y - Min.Y));
        int vy1 = (int)((dy * (GraphBox.Height / 2.0)) + GraphBox.Height / 4.0);
        if (vy1 < -GraphBox.Height) continue;
        if (vy1 > GraphBox.Height) continue;
        g.DrawLine(GreenPen, new Point(i, py), new Point(i + 1, vy1));

        double dz = ((Buffer[i].Z - Min.Z) / (Max.Z - Min.Z));
        int vz1 = (int)((dz * (GraphBox.Height / 2.0)) + GraphBox.Height / 4.0);
        if (vz1 < -GraphBox.Height) continue;
        if (vz1 > GraphBox.Height) continue;
        g.DrawLine(BluePen, new Point(i, pz), new Point(i + 1, vz1));

        px = vx1;
        py = vy1;
        pz = vz1;
      } 
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      Counter++;
      if (Counter > 100)
      {
        Setup();
        Counter = 0;
      };
      if ( graphics == null)
      {
        graphics = GraphBox.CreateGraphics();
      }

      
      GraphBox.Refresh();      
      //GraphBox.Update();
    }

    private void GraphBox_Paint(object sender, PaintEventArgs e)
    {
      DrawGraph(e.Graphics);
    }
  }
}

