using MassiveNetwork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// a serializable and transmittable version of the game object
/// </summary>

namespace Massive.Network
{
  public class MServerObject
  {
    public const int iSTATICSTORAGE = 1;
    public const int iDYNAMICSTORAGE = 0;

    public int StaticStorage;
    public string Type;
    public string OwnerID;
    public string InstanceID;
    public const string MODELURL = "MODELURL";
    public string TemplateID;
    public string Name;
    public string Description;
    public string Tag;
    public string TextureID;
    public DateTime DateCreated;
    public DateTime DateModified;
    public double[] Position = new double[3] { 0, 0, 0 };
    public double[] Rotation = new double[4] { 0, 0, 0, 1 };
    public double[] Scale = new double[3];
    public double[] BoneOffset = new double[3];
    public double Radius = 200;

    public string Serialize()
    {
      return JsonConvert.SerializeObject(this);
    }

    public static MServerObject Deserialize(string sData)
    {
      return JsonConvert.DeserializeObject<MServerObject>(sData);
    }

    public double Distance(double x2, double y2, double z2)
    {
      return Math.Sqrt((x2 - Position[0]) * (x2 - Position[0])
        + (y2 - Position[1]) * (y2 - Position[1])
        + (z2 - Position[2]) * (z2 - Position[2]));
    }

    public static MServerObject UnpackFromDataRow(DataColumnCollection header, DataRow dr)
    {
      DataColumn dc = header[DB.PERSIST];
      if (dc == null) return null;
      MServerObject mso = new MServerObject();

      mso.StaticStorage = Convert.ToInt32(dr[DB.PERSIST]);
      mso.InstanceID = (string)(dr[DB.INSTANCEID].ToString());
      mso.TemplateID = (string)(dr[DB.TEMPLATEID].ToString());
      mso.TextureID= (string)(dr[DB.TEXTUREID].ToString());
      mso.OwnerID = (string)(dr[DB.OWNERID].ToString());
      mso.Name = (string)(dr[DB.NAME].ToString());
      mso.Tag = (string)(dr[DB.TAG].ToString());
      mso.Description = (string)dr[DB.DESCRIPTION].ToString();
      mso.Radius = Convert.ToDouble(dr[DB.RADIUS]);
      mso.Position[0] = Convert.ToDouble(dr[DB.X]);
      mso.Position[1] = Convert.ToDouble(dr[DB.Y]);
      mso.Position[2] = Convert.ToDouble(dr[DB.Z]);
      mso.Rotation[0] = Convert.ToDouble(dr[DB.RX]);
      mso.Rotation[1] = Convert.ToDouble(dr[DB.RY]);
      mso.Rotation[2] = Convert.ToDouble(dr[DB.RZ]);
      mso.Rotation[3] = Convert.ToDouble(dr[DB.RW]);
      return mso;
    }
  }
}
