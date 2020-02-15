using OpenTK;
using OpenTK.Graphics;
using System;
using System.Windows.Forms;

namespace ShaderPlay
{
  public partial class Form1 : Form
  {
    private GLControl glControl1;
    Renderer renderer;
    ShaderEditor _editor;
    MShader shader;
    bool IsSetup = false;
    float TotalTime = 0;    

    public Form1()
    {
      InitializeComponent();
      SetupGLControl();
      renderer = new Renderer();
      _editor = new ShaderEditor();
      timer1.Start();
    }

    void SetupGLControl()
    {
      this.glControl1 = new OpenTK.GLControl(new GraphicsMode(new ColorFormat(32), 32, 8, 8), 4, 3, OpenTK.Graphics.GraphicsContextFlags.Default);
      this.glControl1.BackColor = System.Drawing.Color.Black;
      this.tableLayoutPanel1.SetColumnSpan(this.glControl1, 1);
      this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.glControl1.Location = new System.Drawing.Point(180, 64);
      this.glControl1.Margin = new System.Windows.Forms.Padding(0);
      this.glControl1.Name = "glControl1";
      this.glControl1.Size = new System.Drawing.Size(920, 542);
      this.glControl1.TabIndex = 0;
      this.glControl1.TabStop = false;
      this.glControl1.VSync = false;
      //this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
      //this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);      
      this.tableLayoutPanel1.Controls.Add(this.glControl1, 0, 1);
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      TotalTime += timer1.Interval;
      renderer.renderScene(TotalTime / 1000f);
      glControl1.SwapBuffers();
      IsSetup = true;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      shader = new MShader("shader");
      renderer.Setup(shader);
      _editor.Setup(FragmentShaderEditor, VertexEditor, shader);
      _editor.SetFragShaderEditorStyle(new FastColoredTextBoxNS.TextChangedEventArgs(new FastColoredTextBoxNS.Range(FragmentShaderEditor)));
      _editor.SetVertexEditorStyle(new FastColoredTextBoxNS.TextChangedEventArgs(new FastColoredTextBoxNS.Range(VertexEditor)));
    }

    private void FragmentShaderEditor_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
    {
      
      if (IsSetup == false) return;
      _editor.SetFragShaderEditorStyle(e);
      if (shader != null)
      {
       string Result= shader.CompileFragment(FragmentShaderEditor.Text);
        ResultBox.Text = Result;
       
      }
    }

    private void VertexEditor_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
    {
     
      if (IsSetup == false) return;
      _editor.SetVertexEditorStyle(e);
      if (shader != null)
      {
        string Result = shader.CompileVertexShader(VertexEditor.Text);
        ResultBox.Text = Result;
       
      }
    }

    private void ShowNormalsCheck_CheckedChanged(object sender, EventArgs e)
    {
      renderer.ShowNormals = ShowNormalsCheck.Checked;
    }

    private void RotationCheck_CheckedChanged(object sender, EventArgs e)
    {
      renderer.DoRotation = RotationCheck.Checked;
    }

    private void TriangleButton_CheckedChanged(object sender, EventArgs e)
    {
      renderer.SetModel(Renderer.eModels.Triangle);
    }

    private void CubeButton_CheckedChanged(object sender, EventArgs e)
    {
      renderer.SetModel(Renderer.eModels.Cube);
    }

    private void SphereButton_CheckedChanged(object sender, EventArgs e)
    {
      renderer.SetModel(Renderer.eModels.Sphere);
    }

    private void PlaneButton_CheckedChanged(object sender, EventArgs e)
    {
      renderer.SetModel(Renderer.eModels.Plane);
    }
  }
}
