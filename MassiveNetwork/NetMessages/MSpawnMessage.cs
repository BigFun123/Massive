using Massive.Network;
using System.Data;

namespace MassiveNetwork
{
  public class MSpawnMessage : IMSerializable
  {
    public DataTable SpawnTable;

    public MSpawnMessage(DataTable in_spawnables)
    {
      SpawnTable = in_spawnables;
    }
  }
}
