using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveNetwork
{
  public static class MDataTableHelper
  {
    public static T GetAs<T>(string item, DataRow dr)
    {
      int ord = dr.Table.Columns[item].Ordinal;
      return (T)dr[ord];
    }
  }
}