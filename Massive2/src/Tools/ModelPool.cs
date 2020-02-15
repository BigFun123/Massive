using Assimp;
using Assimp.Configs;
using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class ModelPool : MObject
  {
    static Dictionary<string, MModel> Pool = new Dictionary<string, MModel>();

    public ModelPool() : base(EType.Other, "MeshPool")
    {
      Pool = new Dictionary<string, MModel>();
    }

    public static MModel GetMesh(string sFilename)
    {
      //AssimpContext importer = new AssimpContext();
      //importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));

      if (Pool.ContainsKey(sFilename))
      {
        return Pool[sFilename];
      }

      MModel model = new MModel();
      MMaterial mat = (MMaterial)MScene.MaterialRoot.FindModuleByName(MMaterial.DEFAULT_MATERIAL);
      model.AddMaterial(mat);
      model.Load(sFilename);
      Pool.Add(sFilename, model);
      return model;
    }
  }
}
