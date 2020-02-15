using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MBiome
  {
    public const int UNKNOWN = 0;
    public const int GRASS = 1;
    public const int TREES = 2;
    public const int TREES2 = 3;
    public const int ROAD = 4;
    public const int ROCK = 5;
    public const int WATER = 6;
    public const int RAILWAY = 7;
    int[,] Lookup;
    int[] pGrass = new int[] { 0, 100, 0 };
    int[] pWater = new int[] { 0, 0, 255 };
    int[] pTrees = new int[] { 144, 238, 144 };
    int[] pRailway = new int[] { 169, 169, 169 };
    int[] pTrees2 = new int[] { 0,0,0 };
    int x_res = 255;
    int z_res = 255;

    public MBiome(int sx, int sz)
    {
      x_res = sx;
      z_res = sz;
      Lookup = new int[sx, sz];
      for (int x = 0; x < x_res; x++)
      {
        for (int z = 0; z < z_res; z++)
        {
          Lookup[x, z] = UNKNOWN;
        }
      }
    }

    public int GetBiomeAt(int x, int z)
    {
      if (x <0) return UNKNOWN;
      if (z < 0) return UNKNOWN;
      if (x >= x_res-1) return UNKNOWN;
      if (z >= z_res-1) return UNKNOWN;
      return Lookup[x, z];
    }

    void Set(int x, int z, int[] p, int[] test, int Flag)
    {
      if (( p[0] == test[0])
        && (p[1] == test[1])
          && (p[2] == test[2])){
        Lookup[x, z] = Flag;
      }
    }

    int[] ColorFromBiome(int n)
    {
      switch(n)
      {
        case GRASS:
          return pGrass;
        case WATER:
          return pWater;
        case TREES:
          return pTrees;
        case TREES2:
          return pTrees2;
        case RAILWAY:
          return pRailway;
      }
      return new int[] { 0,0,0 };
    }

    public void InitFromBitmap(MTexture tex)
    {
      StringBuilder sb = new StringBuilder();
      for ( int x=0; x<tex.Width; x++)
      {
        for (int z=0; z<tex.Height; z++)
        {
          int[] p = tex.GetPixelInt(x, z);
          Set(x, z, p, pWater, WATER);
          Set(x, z, p, pGrass, GRASS);
          Set(x, z, p, pTrees, TREES);
          Set(x, z, p, pTrees2, TREES2);
          Set(x, z, p, pTrees2, RAILWAY);
          tex.SetPixelInt(x, z, ColorFromBiome(Lookup[x,z]));
          sb.Append(((int)p[0]).ToString("000") + "," 
            + ((int)p[1]).ToString("000") + "," 
            + ((int)p[2]).ToString("000") + " | ");
          //sb.Append(Lookup[x, z] + " ") ;
        }
        sb.Append("\n");
      }

      Console.WriteLine(sb.ToString());
    }

    public void Dispose()
    {

    }
  }
}
