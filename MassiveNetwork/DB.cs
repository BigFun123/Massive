using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveNetwork
{
  public static class DB
  {
    public const string INSTANCEID = "instanceid";
    public const string OWNERID = "ownerid";
    public const string TEMPLATEID = "templateid";
    public const string TEXTUREID = "textureid";
    public const string NAME = "name";
    public const string DESCRIPTION = "description";
    public const string TAG = "tag";
    public const string PERSIST = "persist";

    public const string RADIUS = "radius";
    public const string X = "x";
    public const string Y = "y";
    public const string Z = "z";
    public const string RX = "rx";
    public const string RY = "ry";
    public const string RZ = "rz";
    public const string RW = "rw";

    public const string DATECREATED = "date_created";
    public const string DATEMODIFIED = "date_modified";

    public const string USERID = "userid";
    public const string SCREENNAME = "screenname";

    public static DataTable CreateObjectTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add(DB.INSTANCEID);
      dt.Columns.Add(DB.OWNERID);
      dt.Columns.Add(DB.TEMPLATEID);
      dt.Columns.Add(DB.TEXTUREID);
      dt.Columns.Add(DB.NAME);
      dt.Columns.Add(DB.DESCRIPTION);
      dt.Columns.Add(DB.TAG);
      dt.Columns.Add(DB.PERSIST);
      dt.Columns.Add(DB.RADIUS);
      dt.Columns.Add(DB.X);
      dt.Columns.Add(DB.Y);
      dt.Columns.Add(DB.Z);
      dt.Columns.Add(DB.RX);
      dt.Columns.Add(DB.RY);
      dt.Columns.Add(DB.RZ);
      dt.Columns.Add(DB.RW);
      dt.Columns.Add(DB.DATECREATED);
      dt.Columns.Add(DB.DATEMODIFIED);

      return dt;
    }

    public static DataTable CreateUserTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add(DB.USERID);
      dt.Columns.Add(DB.SCREENNAME);

      return dt;
    }

  }
}
