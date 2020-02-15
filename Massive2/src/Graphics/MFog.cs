using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MFog : MSceneObject
  {
    private int fogEnabled = 1;
    public int FogEnabled { get => fogEnabled; set => fogEnabled = value; }
    public float FogAmount { get => fogAmount; set => fogAmount = value; }
    private float fogAmount = 0.36f;
    public float FogMultiplier = 1;

    public MFog() : base(EType.Fog, "Fog")
    {

    }

    public void Bind(MShader shader)
    {
      shader.SetInt("FogEnabled", fogEnabled);
      shader.SetFloat("FogAmount", fogAmount);
      shader.SetFloat("FogMultiplier", FogMultiplier);
    }
  }
}
