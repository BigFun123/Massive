using Massive.Network;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class ObjectSpawnedEvent
  {
    public DataTable SpawnedObject;
    public ObjectSpawnedEvent(DataTable inSpawned)
    {
      SpawnedObject = inSpawned;
    }
  }
}
