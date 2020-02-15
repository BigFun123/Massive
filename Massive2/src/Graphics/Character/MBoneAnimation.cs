using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive2.src.Graphics.Character
{
  public class MBoneAnimation
  {
    //at time, the Keys are sent
    //bone order is implicit
    Dictionary<float, List<Matrix4>> Keys = new Dictionary<float, List<Matrix4>>();

    public void AddKey(float time, Matrix4 key)
    {
      if (!Keys.ContainsKey(time))
      {
        Keys.Add(time, new List<Matrix4>());  
      }
      
      Keys[time].Add(key);
    }

  }
}
