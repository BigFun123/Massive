using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class TableEvent : EventArgs
  {
    public DataTable Table;

    public TableEvent(DataTable dt)
    {
      Table = dt;
    }
  }
}
