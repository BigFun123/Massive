using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Massive
{
  public class RenderSettings : MObject
  {
    private PolygonMode renderPolygonMode;
    public PolygonMode RenderPolygonMode { get => renderPolygonMode; set => renderPolygonMode = value; }

    CullFaceMode renderCullMode;
    public CullFaceMode RenderCullMode { get => renderCullMode; set => renderCullMode = value; }

    MaterialFace renderMaterialFace;
    public MaterialFace RenderMaterialFace { get => renderMaterialFace; set => renderMaterialFace = value; }

    private bool renderDepthBuffer = true;
    public bool RenderDepthBuffer { get => renderDepthBuffer; set => renderDepthBuffer = value; }


    public RenderSettings() :
      base(EType.RenderSettings, "RenderSettings")
    {
      renderMaterialFace = MaterialFace.Front;
      renderPolygonMode = PolygonMode.Fill;
      renderCullMode = CullFaceMode.Back;
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      if (RenderDepthBuffer == true)
      {
        GL.Enable(EnableCap.DepthTest);
        GL.DepthMask(true); //make depth buffer readonly
      }
      else
      {
        GL.Disable(EnableCap.DepthTest);
        GL.DepthMask(false); //make depth buffer readonly
      }


      //GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
      GL.Enable(EnableCap.CullFace);
      GL.CullFace(RenderCullMode);      
      base.Render(viewproj, parentmodel);
    }
  }
}
