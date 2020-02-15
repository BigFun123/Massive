using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MAnimation : MObject
  {
    public MTransform Speed = new MTransform();

    public MAnimation(string inname = "Animation") : base(EType.Animation, inname)
    {
      Speed.Scale = Vector3d.Zero;
    }

    public override void Update()
    {
      if (Enabled == false) return;
      MSceneObject so = (MSceneObject)Parent;
      if (so != null)
      {        
        so.transform.Position += Speed.Position * Time.DeltaTime;
        Quaterniond q = so.transform.Rotation * Speed.Rotation ;
        so.transform.Rotation = q;// Quaterniond.Slerp(so.transform.Rotation, q, 0.5) ;
        // Console.WriteLine(so.transform.Rotation.X + "," + so.transform.Rotation.Y + "," + so.transform.Rotation.Z + "," + so.transform.Rotation.W);
        so.transform.Scale += Speed.Scale * Time.DeltaTime;
      }
    }
  }
}
