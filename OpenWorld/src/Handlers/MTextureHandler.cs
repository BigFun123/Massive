using Massive;
using Massive.Events;
using Massive.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Handlers
{
  public class MTextureHandler
  {
    public MTextureHandler()
    {
      Globals.Network.TextureHandler += Network_TextureHandler;
      MMessageBus.TextureRequestHandler += MMessageBus_TextureRequestHandler;
    }

    //sends local testure changes to the server
    private void MMessageBus_TextureRequestHandler(object sender, TextureRequestEvent e)
    {
      Globals.Network.TextureRequest(e.InstanceID, e.TextureID);
    }

    private void Network_TextureHandler(object sender, Massive.Events.TextureEvent e)
    {
      MSceneObject mo = (MSceneObject)MScene.ModelRoot.FindModuleByInstanceID(e.InstanceID);
      MObject o = MScene.MaterialRoot.FindModuleByName(e.TextureID);
      
      if (o != null && o.Type == MObject.EType.Material)
      {
        MMaterial mat = (MMaterial)o;
        mo.SetMaterial(mat);
      }
      else
      {
        if (MassiveTools.IsURL(e.TextureID))
        {
          MMaterial mat = (MMaterial)new MMaterial("URLShader");
          MShader DefaultShader = (MShader)MScene.MaterialRoot.FindModuleByName(MShader.DEFAULT_SHADER);
          mat.AddShader(DefaultShader);
          mat.ReplaceTexture(Globals.TexturePool.GetTexture(e.TextureID));
          MScene.MaterialRoot.Add(mat);
          mo.SetMaterial(mat);
          mo.material.Setup();
          MMessageBus.ChangedTexture(this, e.InstanceID, e.TextureID);
        }
        else
        {
          Console.WriteLine("Object " + e.InstanceID + " was null");
          MMessageBus.Error(this, "Could not find Material:" + e.TextureID);
        }
      }
    }
  }
}
