using Massive;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Optimize
{
  public class MFrustrumCuller : MObject
  {
    public Vector3d AvatarPos;
    Frustum _frustrum = new Frustum();

    int CullCounter = 0;
    int CullTrigger = 20;

    public MFrustrumCuller()
      : base(EType.FrustrumCuller, "MFrustrumCuller")
    {

    }

    public override void Update()
    {
      base.Update();
      if (Settings.FrustrumCullingEnabled == false) return;

      AvatarPos = Globals.Avatar.GetPosition();
      CullCounter++;
      if (CullCounter > CullTrigger) {
        CullCounter = 0;
        CalcFrustrum(MScene.Background);
      }      
    }

    void CalcFrustrum(MObject moParent)
    {
      Array ar = moParent.Modules.ToArray();

      for (int i = 0; i < ar.Length; i++)
      {
        MObject m = (MObject)ar.GetValue(i);
        if (m == null) continue;

        if (m.Renderable)
        {
          MSceneObject mo = (MSceneObject)m;
          //meshes use their parent's transforms
          if (mo.Type == EType.Other) continue;

          if ((mo.Type != EType.Mesh) && (mo.Type != EType.BoneMesh))
          {
            _frustrum.CalculateFrustum(MScene.Camera.getProjectionMatrix(), MScene.Camera.getViewMatrix());

            //if ( _frustrum.VolumeVsFrustum(mo.transform.Position - Globals.GlobalOffset, mo.BoundingBox))
            if (!_frustrum.SphereVsFrustum(mo.transform.Position - Globals.GlobalOffset, mo.BoundingBox.Size().Length))
            //if (!_frustrum.PointVsFrustum(mo.transform.Position - Globals.GlobalOffset))
            {
              mo.Culled = true;
             // mo.Enabled = false;
            }
            else
            {
              mo.Culled = false;
             // mo.Enabled = true;
            }
          

            //mo.DistanceFromAvatar = Vector3d.Distance(mo.transform.Position, AvatarPos);
          }
        }
        CalcFrustrum(m);

      }
    }
  }
}
