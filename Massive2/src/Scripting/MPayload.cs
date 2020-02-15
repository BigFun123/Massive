using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MPayload : MObject
  {
    public MObject _this;

    public MPayload() : base(EType.Script, "Script")
    {

    }

    public void Init(MObject pref)
    {
      _this = pref;
      if (_this != null)
      {
        Console.WriteLine("Initialized from dll:" + _this.Name);
      }
      else
      {
        Console.WriteLine("Init called, with no linking MassiveObject");
      }
    }

    public void OnSelect(MSceneObject mo)
    {
      Console.WriteLine("Selected:" + mo.Name);
    }
  }
}
