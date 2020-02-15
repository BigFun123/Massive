using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld
{
  public class MBuildingBlock
  {
    public const string MATERIAL_TYPE = "MMaterial";
    public const string MDOEL_TYPE = "MModel";

    private string name = "Block";
    private string textureID = "TEXTURE01";
    private string materialID = "MATERIAL01";
    private string templateID;
    private string type = "MModel";
    private string physicsShape = "Box";
    private double[] size = new double[] { 1, 1, 1 };
    private string model;
    private string path;
    private double weight = 1;
    private string subModule;
    private bool isTransparent = false;
    private double[] boneOffset = new double[] { 0, 0, 0 };
    private double[] rotationOffset = new double[] { 0, 0, 0 };

    public string Name { get => name; set => name = value; }    
    public string TemplateID { get => templateID; set => templateID = value; }
    public string Type { get => type; set => type = value; }
    public string TextureID { get => textureID; set => textureID = value; }
    public double[] Size { get => size; set => size = value; }
    public string Model { get => model; set => model = value; }
    public double Weight { get => weight; set => weight = value; }
    public string MaterialID { get => materialID; set => materialID = value; }
    public string PhysicsShape { get => physicsShape; set => physicsShape = value; }
    public string Path { get => path; set => path = value; }
    public string SubModule { get => subModule; set => subModule = value; }
    public bool IsTransparent { get => isTransparent; set => isTransparent = value; }
    public double[] BoneOffset { get => boneOffset; set => boneOffset = value; }
    public double[] RotationOffset { get => rotationOffset; set => rotationOffset = value; }

    public MBuildingBlock(string sName)
    {
      name = sName;
    }
  }
}
