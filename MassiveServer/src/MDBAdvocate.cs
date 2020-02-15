using Massive.Network;
using MassiveNetwork;
using MassiveServer.src;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveServer
{
  public class MDBAdvocate : MDatabase
  {

    public int GetObjectCount()
    {
      string sQuery = "SELECT COUNT(*) FROM objects;";
      int results = QueryScalar(sQuery);
      return results;
    }

    public bool SetTexture(string UserID, string InstanceID, string TextureID)
    {
      string sQuery = string.Format(
        @"UPDATE objects SET `textureid`='{0}' WHERE `instanceid`='{1}' AND `ownerid`='{2}';",
        TextureID, InstanceID, UserID);
      int results = Query(sQuery);
      return results == 0 ? false : true;
    }

    public int DeleteObject(string InstanceID, string UserID)
    {
      string sQuery = string.Format(
        @"DELETE FROM objects WHERE `instanceid`='{0}' AND `ownerid`='{1}';",
        InstanceID, UserID);
      return Query(sQuery);
    }

    public string DumpUser(string UserID)
    {
      string sQuery = string.Format(
        @"SELECT * FROM objects WHERE `ownerid`='{0}';",
        UserID);
      DataTable dt = QueryReader(sQuery);
      return DataTableToJSON(dt);
    }

    public DataTable GetTable(string sTableName)
    {
      string sQuery = string.Format(
        @"SELECT * FROM {0};", sTableName);
      DataTable dt = QueryReader(sQuery);
      return dt;
    }

    public bool MoveObject(string UserID, string InstanceID, double[] Locus, double[] Rotation)
    {
      string sQuery = string.Format(
@"UPDATE objects SET `x`={0},`y`={1},`z`={2},`rx`={3},`ry`={4},`rz`={5},`rw`={6} WHERE `instanceid`='{7}' AND `ownerid`='{8}';",
        Locus[0], Locus[1], Locus[2],
        Rotation[0], Rotation[1], Rotation[2], Rotation[3],
        InstanceID, UserID);
      int num = Query(sQuery);
      return num == 0 ? false : true;
    }

    public bool SetProperty(string UserID, string InstanceID, string PropertyTag)
    {
      string sQuery = string.Format(
       @"UPDATE objects SET `tag`=@tag WHERE `instanceid`=@instanceid AND `ownerid`=@ownerid;",
         PropertyTag, InstanceID, UserID);
      //int num = Query(sQuery);

      Dictionary<string, string> parms = new Dictionary<string, string>();
      parms.Add("tag", PropertyTag);
      parms.Add("instanceid", InstanceID);
      parms.Add("ownerid", UserID);
      int num = QueryParam(sQuery, parms);

      return num == 0 ? false : true;
    }

    public bool ChangeAvatar(string UserID, string AvatarID)
    {
      int num = 0;
      string sQuery = string.Format(
        @"UPDATE objects SET `templateid`='{0}' WHERE `instanceid`='{1}' AND `ownerid`='{1}';",
        AvatarID, UserID);
      num = Query(sQuery);
      sQuery = string.Format(
        @"UPDATE users SET `avatarid`='{0}' WHERE `userid`='{1}';",
        AvatarID, UserID);
      num += Query(sQuery);
      return num == 0 ? false : true;
    }

    public MServerObject GetObject(string InstanceID)
    {
      MServerObject mso = new MServerObject();
      //if (Objects.ContainsKey(InstanceID)) return Objects[InstanceID];
      string sQuery = string.Format("SELECT 1 FROM `objects` WHERE `instanceid`='{0}'", InstanceID);
      DataTable dt = QueryReader(sQuery);

      DataRow row = dt.Rows[0];
      mso.OwnerID = row.ItemArray[0].ToString();

      return mso;
    }

    public DataTable GetObjectsNear(string UserID, double x, double y, double z)
    {
      List<MServerObject> msl = new List<MServerObject>();

      string sQuery = string.Format("SELECT * FROM `objects`;");
      DataTable dt = QueryReader(sQuery);
      //string sJSON = DataTableToJSON(dt);
      //TODO: proximity select
      return dt;
    }

    public void RemoveUserFromUniverse(string UserID)
    {
      string sQuery = string.Format(
        @"DELETE FROM objects WHERE `instanceid`='{0}' AND `persist`=0;",
        UserID);
      Query(sQuery);
    }

    public void AddPlayer(MUserAccount m)
    {
      string sQuery = string.Format(
@"INSERT IGNORE into users (userid, screenname, avatarid, email, password, ip) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');",
        m.UserID, m.UserName, m.AvatarID, m.Email, m.Password, m.ClientIP);
      Query(sQuery);

      /*sQuery = string.Format(
        @"INSERT into `objects` (`instanceid`,`ownerid`,`templateid`,`name`,`persist`) 
        VALUES('{0}', '{1}', '{2}', '{3}', 0)
        ON DUPLICATE KEY UPDATE `name`='{3}';",
        m.UserID, m.UserID, m.AvatarID, m.UserName);
      Query(sQuery);*/
    }

    public MUserAccount GetPlayerByEmail(string Email, string Password)
    {
      MUserAccount mu = new MUserAccount();
      mu.Email = Email;
      mu.Password = Password;
      string sQuery = string.Format("SELECT * from users where email='{0}' and password='{1}';", Email, Password);
      DataTable dt = QueryReader(sQuery);
      if (dt.Rows.Count == 0) return null;

      //DataRow row = dt.Rows[0]["userid"];
      //mu.UserID = row.ItemArray.GetValue("userid"].ToString();
      mu.UserID = dt.Rows[0]["userid"].ToString();
      mu.UserName = dt.Rows[0]["screenname"].ToString();
      mu.AvatarID = dt.Rows[0]["avatarid"].ToString();
      mu.TotalObjects = (int)dt.Rows[0]["totalobjects"];
      mu.Credit = (int)dt.Rows[0]["wallet"];
      return mu;
    }

    public MUserAccount GetPlayerByUserID(string UserID)
    {
      MUserAccount mu = new MUserAccount();
      string sQuery = string.Format("SELECT * from users where userid='{0}';", UserID);
      DataTable dt = QueryReader(sQuery);
      if (dt.Rows.Count == 0) return null;

      //DataRow row = dt.Rows[0]["userid"];
      //mu.UserID = row.ItemArray.GetValue("userid"].ToString();
      mu.UserID = dt.Rows[0]["userid"].ToString();
      return mu;
    }


    public bool AddToWorld(DataTable dt)
    {
      int n = 0;
      foreach (DataRow dr in dt.Rows)
      {
        string sQuery = string.Format(
@"INSERT IGNORE into objects (`instanceid`, `ownerid`, `templateid`, `textureid`, `name`, `persist`, x,y,z, rx, ry, rz, rw) 
VALUES('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12});",
         dr[DB.INSTANCEID], dr[DB.OWNERID], dr[DB.TEMPLATEID], dr[DB.TEXTUREID], dr[DB.NAME], dr[DB.PERSIST],
         dr[DB.X], dr[DB.Y], dr[DB.Z], dr[DB.RX], dr[DB.RY], dr[DB.RZ], dr[DB.RW]
         );
        n += Query(sQuery);
      }
      return n == 0 ? false : true;
    }

    public string RegisterUser(MUserAccount m)
    {
      MUserAccount mu = GetPlayerByEmail(m.Email, m.Password);

      if (mu != null)
      {      
        return mu.UserID;
      }

      string s = UidGen.GUID();
      m.UserID = s;
      string sQuery = string.Format(
      @"INSERT into users (`screenname`,`avatarid`,`email`,`password`, `userid`, `ip`) 
          VALUES('{0}','{1}','{2}','{3}','{4}','{5}');",
      m.UserName, m.AvatarID, m.Email, m.Password, m.UserID, m.ClientIP);

      Query(sQuery);
      return m.UserID;
    }

    public string UpdatePlayer(MUserAccount m)
    {          
        string sQuery = string.Format(
        @"UPDATE users 
          SET screenname = '{0}', avatarid = '{1}', email='{2}', password='{3}' 
          WHERE `userid`='{4}';",
        m.UserName, m.AvatarID, m.Email, m.Password, m.UserID);

      Query(sQuery);
      return m.UserID;
    }

    public string UpdatePlayerIP(MUserAccount m)
    {
      if (m == null) return "";
      string sQuery = string.Format(
      @"UPDATE users SET ip = '{0}' WHERE `userid`='{1}';",
      m.ClientIP, m.UserID);

      Query(sQuery);
      return m.UserID;
    }

    public string UpdatePlayerUsage(MUserAccount m)
    {
      if (m == null) return "";

      string sCountQuery = String.Format("SELECT COUNT(*) from `objects` where ownerid='{0}'", m.UserID);
      int ObjectsOwned = QueryScalar(sCountQuery);
      string DateMYSQL = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
      string sQuery = string.Format(
      @"UPDATE users SET date_accessed = '{0}',totalobjects='{1}' WHERE `userid`='{2}';",
      DateMYSQL, ObjectsOwned, m.UserID);

      Query(sQuery);



      return m.UserID;
    }

    /// <summary>
    /// Removes all inactive objects from the object table
    /// </summary>
    public void FlushPlayers()
    {
      string sQuery = string.Format(
        @"DELETE from `objects` where `persist`=0;
        "
      );
      Query(sQuery);
    }
  }
}
