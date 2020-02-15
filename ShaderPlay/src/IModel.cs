using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;

namespace ShaderPlay
{
  public class IModel
  {
    public int VAO = -1;
    public bool Visible = false;

    public virtual void Setup() {
      GL.DeleteVertexArray(VAO);
    }
    public virtual void Render() { }
  }
}
