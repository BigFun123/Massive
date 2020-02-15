using Massive.Network;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveNetwork
{
  public class MTableMessage : IMSerializable
  {
    public DataTable SpawnTable;

    public MTableMessage(DataTable in_spawnables)
    {
      SpawnTable = in_spawnables;
    }
  }
}
