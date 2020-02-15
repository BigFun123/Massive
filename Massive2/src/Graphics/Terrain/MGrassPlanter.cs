using Massive;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MGrassPlanter
  {
    public Matrix4[,] Grassmats;
    public const int LOD0Size = 1024;
    public bool IsComplete = false;

    public MGrassPlanter()
    {
      Grassmats = new Matrix4[LOD0Size, LOD0Size];
    }

    public void PrepareGrass(MTerrainTile tile)
    {
      MTerrainTile Tile = tile;
      Matrix4d TreeRotation = Matrix4d.CreateFromQuaternion(Quaterniond.FromEulerAngles(tile.transform.Up()));

      float numxinterps = LOD0Size / Tile.x_res;
      float numzinterps = LOD0Size / Tile.z_res;

      for (int x = 0; x < LOD0Size; x++)
        for (int z = 0; z < LOD0Size; z++)
        {
          Vector3d PlantingPos = new Vector3d((x / numxinterps), 0, (z / numzinterps));
          //PlantingPos = Tile.GetInterpolatedPointOnSurfaceFromGrid2(PlantingPos);
          //double y = tile.ElevationAtPoint(x / numxinterps, z / numzinterps);
          //PlantingPos = new Vector3d(x, y, z);
          PlantingPos = Tile.GetInterpolatedPointOnSurfaceFromGrid2(PlantingPos);
          Matrix4d TreeScale = Matrix4d.Scale(10, 10, 10);
          Matrix4d TreePosition = Matrix4d.CreateTranslation(PlantingPos);
          Matrix4 final = MTransform.GetFloatMatrix(TreeScale * TreeRotation * TreePosition);
          Grassmats[x, z] = final;
        }

      IsComplete = true;
      Console.WriteLine("Grass Planted:" + Tile.TileX + "," + Tile.TileY);
    }
  }
}
