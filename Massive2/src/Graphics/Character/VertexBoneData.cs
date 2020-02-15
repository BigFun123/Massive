using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Massive.Graphics.Character
{
  [StructLayout(LayoutKind.Sequential)]
  public struct VertexBoneData
  {
    //public const int Size = (4 + 4) * 4;
    //const int NUM_BONES_PER_VEREX = 4; 
    // uint[] = has unknown size for 'extra internal stuff'
    public uint id0;   // we have 4 bone ids for EACH vertex & 4 weights for EACH vertex    
    public uint id1;
    public uint id2;
    public uint id3;
    public Vector4 weights;

    public void InitVertexBoneData()
    {
      id0 = 0;
      id1 = 0;
      id2 = 0;
      id3 = 0;
      weights = new Vector4(0f, 0f, 0f, 0f);
    }

    public void addBoneData(uint bone_id, float weight)
    {
      const int NUM_BONES_PER_VEREX = 4;
      for (int i = 0; i < NUM_BONES_PER_VEREX; i++)
      {
        if (weights[i] == 0.0)
        {
          if (i == 0) id0 = bone_id;
          if (i == 1) id1 = bone_id;
          if (i == 2) id2 = bone_id;
          if (i == 3) id3 = bone_id;
          weights[i] = weight;
          return;
        }
      }
    }
  };
}
