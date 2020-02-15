using Massive;
using OpenTK;
using OpenTK.Graphics;
using OpenWorld.Controllers;
using OpenWorld.Forms;
using OpenWorld.Handlers;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace OpenWorld
{
  public partial class Main : Form
  {
    COpenWorld openWorld;
    private GLControl glControl1;
    //public static Point ClientLocation;
    //public static Size RenderClientSize;
    LobbyForm lobbyForm;
    MKeyboardHandler keyboardHandler;
    MMouseHandler mouseHandler;
    InfoOverlayForm _infoForm;
    WindowManager _windowManager;    

    public static Rectangle ClientRect;

    public Main()
    {
      InitializeComponent();
      SetupGLControl();
      Globals.GUIThreadOwner = this;      
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      openWorld = new COpenWorld();
      keyboardHandler = new MKeyboardHandler(this, glControl1);
      lobbyForm = new LobbyForm();
      openWorld.Setup();
      mouseHandler = new MMouseHandler(glControl1);
      timer1.Start();
      Application.Idle += Application_Idle;
      bookmarksControl1.Setup();
      SetupInfo();
      SetupWindow();
    }

    void SetupWindow()
    {
      _windowManager = WindowManager.Load(this);
      Left = _windowManager.Left;
      Top = _windowManager.Top;
      Width = _windowManager.Width;
      Height = _windowManager.Height;
    }

    void SetupInfo()
    {
      _infoForm = new InfoOverlayForm();
      _infoForm.Show(this);
      UpdateWindowVariables();
    }

    private void Application_Idle(object sender, EventArgs e)
    {
      keyboardHandler.Update();
      openWorld.Update();
      if (Settings.DebugRender == false)
      {
        openWorld.Render();
      }
      glControl1.SwapBuffers();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      
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
      this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
      this.tableLayoutPanel1.Controls.Add(this.glControl1, 0, 2);
    }

    private void glControl1_Resize(object sender, EventArgs e)
    {
      Point p = PointToScreen(glControl1.Location);
      ClientRect.X = p.X;
      ClientRect.Y = p.Y;
      ClientRect.Width = glControl1.Width;
      ClientRect.Height = glControl1.Height;

      int w = 800;
      int h = 600;

      if (glControl1 != null)
      {
        w = glControl1.Width;
        h = glControl1.Height;
        glControl1.MakeCurrent();
      }
      //GL.Viewport(0, 32, w, h);
      if ((w > 16) && (h > 16))
      {
        MScreen.Resize(w, h);
      }
      //if (_infoForm != null)
      //{
      //_infoForm.UpdateData();
      //}
      UpdateWindowVariables();
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
      MKeyboardHandler.KeyState[(int)e.KeyCode] = true;
    }

    private void Form1_KeyUp(object sender, KeyEventArgs e)
    {
      /*if (_keyboardHandler == null)
      {
        Console.WriteLine("KeyboardHandler is null in  Main_KeyUp ");
        return;
      }
      _keyboardHandler.HandleKey(e);
      */
      MKeyboardHandler.KeyState[(int)e.KeyCode] = false;
    }


    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      _windowManager.Save();
      timer1.Stop();
      Application.Idle -= Application_Idle;
      openWorld.Dispose();

    }

    void UpdateWindowVariables()
    {
      //ClientLocation = glControl1.PointToScreen(new Point(0,0));
      ClientRect.Location = glControl1.PointToScreen(glControl1.ClientRectangle.Location);
      if (_infoForm != null)
      {
        _infoForm.UpdateData();
      }
    }

    private void Main_Move(object sender, EventArgs e)
    {
      UpdateWindowVariables();
    }

    private void Main_Shown(object sender, EventArgs e)
    {
      UpdateWindowVariables();
    }
  }
}
