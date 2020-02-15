using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  //public delegate void WorkCompletedCallBack(MRaycastTask Result);

  public class MRaycastTask
  {
    public Vector3d From;
    public Vector3d To;
    public double Depth;
    public Action<MRaycastTask> Completion;
    public bool Result = false;
    public Vector3d Hitpoint;
    public Vector3d Hitnormal;
    public object UserObject;
    public object Info;

    public void Notify()
    {
      if ( Completion != null )
      {
        Completion(this);
      }
    }
  }
}
