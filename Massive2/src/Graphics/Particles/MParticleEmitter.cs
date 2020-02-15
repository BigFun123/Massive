using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive2.src.Graphics.Particles
{
  public class MParticleEmitter : MSceneObject
  {
    const int MaxParticles = 100000;
    MParticle[] ParticlesContainer = new MParticle[MaxParticles];
    int LastUsedParticle = 0;

    public MParticleEmitter() 
      : base (EType.Particle, "ParticleEmitter")
    {

    }

    public void Emit()
    {
      MParticle p = new MParticle();
      
    }
    // Finds a Particle in ParticlesContainer which isn't used yet.
    // (i.e. life < 0);
    int FindUnusedParticle()
    {

      for (int i = LastUsedParticle; i < MaxParticles; i++)
      {
        if (ParticlesContainer[i].life < 0)
        {
          LastUsedParticle = i;
          return i;
        }
      }

      for (int i = 0; i < LastUsedParticle; i++)
      {
        if (ParticlesContainer[i].life < 0)
        {
          LastUsedParticle = i;
          return i;
        }
      }

      return 0; // All particles are taken, override the first one
    }
    void SortParticles()
    {
      //Sort(&ParticlesContainer[0], &ParticlesContainer[MaxParticles]);
    }
  }


}
