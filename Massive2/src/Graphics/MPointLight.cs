using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MPointLight : MSceneObject
  {
    public Vector3 Ambient = new Vector3(0.4f, 0.4f, 0.4f);
    public Vector3 Diffuse = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 Specular = new Vector3(2.0f, 2.0f, 2.1f);

    public MPointLight(string sName) : base(EType.PointLight, sName)
    {
      
    }

    public override void Update()
    {
      MSceneObject p = (MSceneObject)Parent;
      if (p != null)
      {
        this.transform.Position = p.transform.Position;
        this.transform.Rotation = p.transform.Rotation;
      }

      base.Update();
    }

    public void Bind(MMaterial mat, int index)
    {
      mat.shader.SetVec3("pointLights[" + index + "].position", MTransform.GetVector3(transform.Position - Globals.GlobalOffset));
      mat.shader.SetVec3("pointLights[" + index + "].ambient", Ambient);
      mat.shader.SetVec3("pointLights[" + index + "].diffuse", Diffuse);
      mat.shader.SetVec3("pointLights[" + index + "].specular", Specular);
      mat.shader.SetFloat("pointLights[" + index + "].constant", 1.0f);
      mat.shader.SetFloat("pointLights[" + index + "].linear", 0.19f);
      mat.shader.SetFloat("pointLights[" + index + "].quadratic", 0.032f);
    }

    public void CopyTo(MPointLight p)
    {
      base.CopyTo(p);
      p.Ambient = Ambient;
      p.Diffuse = Diffuse;
      p.Specular = Specular;      
    }
  }
}
