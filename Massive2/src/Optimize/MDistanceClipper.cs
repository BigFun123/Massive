using Massive;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Hides objects that are out of range. 
/// each MSceneObject has a DistanceToAvatar attribute that is constantly being updated by MDistanceClipper
/// </summary>

namespace Massive.Optimize
{
  public class MDistanceClipper : MObject
  {
    public Vector3d AvatarPos;

    int SkipCounter = 0;
    int SkipMax = 600;

    public MDistanceClipper() : base(EType.DistanceClipper, "DistanceClipper")
    {

    }

    public override void Update()
    {
      base.Update();
      if (Settings.DistanceClippingEnabled == false) return;
      AvatarPos = Globals.Avatar.GetPosition();
      SkipCounter++;
      if (SkipCounter < SkipMax)
      {
        return;
      }
      SkipCounter = 0;

      CalcObject(MScene.ModelRoot);
      CalcObject(MScene.Background);
      CalcObject(MScene.Background2);
    }

    //This is a potentially slow operation and can be called every nth frame.
    void CalcObject(MObject moParent)
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
          if ((mo.Type != EType.Mesh) && (mo.Type != EType.BoneMesh))
          {
            mo.DistanceFromAvatar = Vector3d.Distance(mo.transform.Position, AvatarPos);
          }

          if (mo.DistanceFromAvatar > mo.DistanceThreshold)
          {
            mo.Index = -2;
            mo.Clipped = true;
            //return;
          }
          else
          {
            mo.Clipped = false;
          }

        }
        CalcObject(m);

      }
    }

  }
}
