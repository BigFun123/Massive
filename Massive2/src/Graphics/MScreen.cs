using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***
 * 
 * Represents the main output view
 * Either the size of the container, or the fullscreen size, depending how we are running
 * */

namespace Massive
{
  public static class MScreen
  {
    public static int Width = 800;
    public static int Height = 600;

    public static void Resize(int w, int h)
    {
      Width = w;
      Height = h;
      if (MScene.Renderer != null)
      {
        MScene.Renderer.Resize(w, h);
      }
    }

  }
}
