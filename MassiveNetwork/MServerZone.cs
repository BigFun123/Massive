using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class MServerZone : IMSerializable
  {
    private string name;
    public string Name { get => name; set => name = value; }
    public bool ServerZone = false;
    public string Group;
    public string Description;
    public string OwnerID;
    public Vector3d_Server Position;
    public double[] Rotation = new double[] { 0, 0, 0, 1 };

    public MServerZone(string inOwnerID, string inName, string inGroup, Vector3d_Server v)
    {
      OwnerID = inOwnerID;
      Name = inName;
      Group = inGroup;
      Position = v;
    }

    public void CopyTo(MServerZone z)
    {
      z.Name = name;
      z.ServerZone = ServerZone;
      z.Group = Group;
      z.Description = Description;
      z.OwnerID = OwnerID;
      z.Position = Position;
      z.Rotation = Rotation;
    }
  }
}
