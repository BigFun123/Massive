using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWorld.Controllers
{
  public class WindowManager
  {
    const string sFileName = "window.json";
    public static Form Window;
    private int left = 0;
    private int top = 0;
    private int width = 1024;
    private int height = 800;
    private int state = 1;

    public int Left { get => left; set => left = value; }
    public int Top { get => top; set => top = value; }
    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    public int State { get => state; set => state = value; }

    public WindowManager(Form f)
    {
      Window = f;
    }

    static public WindowManager Load(Form f)
    {
      Window = f;
      WindowManager wm = null;
      if (File.Exists(sFileName))
      {
        string sData = File.ReadAllText(sFileName);
        wm = JsonConvert.DeserializeObject<WindowManager>(sData);
        
      }
      
      if ( wm == null)
      {
        wm = new WindowManager(Window);
      }
      else
      {
        WindowManager.Window = f;
      }
      return wm;
    }

    public void Save()
    {
      Left = Window.Left;
      Top = Window.Top;
      Width = Window.Width;
      Height = Window.Height;
      string sData = JsonConvert.SerializeObject(this);
      File.WriteAllText(sFileName, sData);
    }
  }
}
