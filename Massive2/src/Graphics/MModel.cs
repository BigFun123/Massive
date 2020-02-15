using Assimp;
using Assimp.Configs;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MModel : MSceneObject
  {    
    private Scene _modelScene;
    private Vector3 m_sceneCenter, m_sceneMin, m_sceneMax;
    public string Filename;
   
   // List<MMesh> meshes = new List<MMesh>();

    public MModel(string inname = "Model") : base(EType.Model, inname)
    {

    }

    public void Load(string sPath)
    {
      Filename = sPath;
      string sFullPath = Path.Combine(Globals.ResourcePath, sPath);
      //string sFullPath =  sPath;
      if (!File.Exists(sFullPath))
      {
        Console.WriteLine("File:" + sFullPath + " not found");
        return;
      }

      AssimpContext importer = new AssimpContext();
      importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));
      try { 
      _modelScene = importer.ImportFile(sFullPath);
        processNode(_modelScene.RootNode, _modelScene, this);
        ComputeBoundingBox();
      }
      catch( Exception e)
      {
        Globals.Log(this, e.Message);
        Console.WriteLine(e.Message);
        if ( e.InnerException != null)
        {
          Console.WriteLine(e.InnerException.Message);
        }
      }      
    }

    // processes a node in a recursive fashion. Processes each individual mesh located at the node and repeats this process on its children nodes (if any).
    void processNode(Node node, Scene scene, MObject parent)
    {
      MMesh NewNode = null;
      if (parent == null) parent = this;
      // process each mesh located at the current node
      for (int i = 0; i < node.MeshCount; i++)
      {
        // the node object only contains indices to index the actual objects in the scene. 
        // the scene contains all the data, node is just to keep stuff organized (like relations between nodes).
        Mesh m = scene.Meshes[node.MeshIndices[i]];        
        // meshes.Add(processMesh(m, scene));
        Matrix4d tr = FromMatrixd(node.Transform);
        // tr.Transpose();
        NewNode = processMesh(m, scene, node.Name, tr);
        if ( parent != null )
        {
          NewNode.OwnerID = parent.OwnerID;
          NewNode.FaceCount = m.FaceCount;
          parent.Add(NewNode);
        }
      }
      // after we've processed all of the meshes (if any) we then recursively process each of the children nodes
      for (int i = 0; i < node.ChildCount; i++)
      {
        processNode(node.Children[i], scene, NewNode);
      }
    }

    MMesh processMesh(Mesh mesh, Scene scene, string sName, Matrix4d trans)
    {
      
      // data to fill
      List<TexturedVertex> vertices = new List<TexturedVertex>();
      List<int> indices = new List<int>();
      List<MTexture> textures = new List<MTexture>();

      // Walk through each of the mesh's vertices
      for (int i = 0; i < mesh.VertexCount; i++)
      {
        TexturedVertex vertex = new TexturedVertex();
        Vector3 vector = new Vector3(); // we declare a placeholder vector since assimp uses its own vector class that doesn't directly convert to glm's vec3 class so we transfer the data to this placeholder glm::vec3 first.
                                        // positions
        vector.X = mesh.Vertices[i].X;
        vector.Y = mesh.Vertices[i].Y;
        vector.Z = mesh.Vertices[i].Z;
        vertex._position = vector;
        // normals
        if (mesh.HasNormals)
        {
          vector.X = mesh.Normals[i].X;
          vector.Y = mesh.Normals[i].Y;
          vector.Z = mesh.Normals[i].Z;
          vertex._normal = vector;
        }
        // texture coordinates
        if (mesh.HasTextureCoords(0)) // does the mesh contain texture coordinates?
        {
          Vector2 vec = new Vector2();
          // a vertex can contain up to 8 different texture coordinates. We thus make the assumption that we won't 
          // use models where a vertex can have multiple texture coordinates so we always take the first set (0).
          vec.X = mesh.TextureCoordinateChannels[0][i].X;
          vec.Y = 1 - mesh.TextureCoordinateChannels[0][i].Y;
          vertex._textureCoordinate = vec;
        }
        else
          vertex._textureCoordinate = new Vector2(0.0f, 0.0f);

        if (mesh.HasTangentBasis)
        {
          // tangent
          vector.X = mesh.Tangents[i].X;
          vector.Y = mesh.Tangents[i].Y;
          vector.Z = mesh.Tangents[i].Z;
         /// vertex.Tangent = vector;
          // bitangent
          vector.X = mesh.BiTangents[i].X;
          vector.Y = mesh.BiTangents[i].Y;
          vector.Z = mesh.BiTangents[i].Z;
         /// vertex.BiTangent = vector;
        }
        vertices.Add(vertex);
      }
      // now wak through each of the mesh's faces (a face is a mesh its triangle) and retrieve the corresponding vertex indices.
      for (int i = 0; i < mesh.FaceCount; i++)
      {
        Face face = mesh.Faces[i];
        // retrieve all indices of the face and store them in the indices vector
        for (int j = 0; j < face.IndexCount; j++)
          indices.Add(face.Indices[j]);
      }
      // process materials
      Material material = scene.Materials[mesh.MaterialIndex];
      // we assume a convention for sampler names in the shaders. Each diffuse texture should be named
      // as 'texture_diffuseN' where N is a sequential number ranging from 1 to MAX_SAMPLER_NUMBER. 
      // Same applies to other texture as the following list summarizes:
      // diffuse: texture_diffuseN
      // specular: texture_specularN
      // normal: texture_normalN

      // 1. diffuse maps
      /*
      List<MTexture> diffuseMaps = loadMaterialTextures(material, TextureType_DIFFUSE, "texture_diffuse");
      textures.insert(textures.end(), diffuseMaps.begin(), diffuseMaps.end());
      // 2. specular maps
      List<Texture> specularMaps = loadMaterialTextures(material, aiTextureType_SPECULAR, "texture_specular");
      textures.insert(textures.end(), specularMaps.begin(), specularMaps.end());
      // 3. normal maps
      List<Texture> normalMaps = loadMaterialTextures(material, aiTextureType_HEIGHT, "texture_normal");
      textures.insert(textures.end(), normalMaps.begin(), normalMaps.end());
      // 4. height maps
      List<Texture> heightMaps = loadMaterialTextures(material, aiTextureType_AMBIENT, "texture_height");
      textures.insert(textures.end(), heightMaps.begin(), heightMaps.end());
      */

      // return a mesh object created from the extracted mesh data
      MMesh m = new MMesh(sName);
     // trans.Transpose();
      m.transform.Position = trans.ExtractTranslation();
      m.transform.Rotation = trans.ExtractRotation();
      m.transform.Scale = trans.ExtractScale();
      m.AddMaterial(this.material);
      m.SetupMesh(vertices, indices, textures);
      return m;
    }

  /*  public override void Render(Matrix4d viewproj)
    {
      if (!Enabled) return;

      // Matrix4 model = MTransform.GetFloatMatrix(GetMatrix());
      mvp = MTransform.GetFloatMatrix(GetMatrix() * viewproj);
      model = MTransform.GetFloatMatrix(GetMatrix());

      if (Globals.ShaderOverride == null)
      {
        material.shader.Bind();
        material.shader.SetMat4("mvp", mvp);

        //Vector3 pos = mvp.Inverted().ExtractTranslation();
        //Matrix4 ppos = Matrix4.CreateTranslation(pos);
        //material.shader.SetMat4("model", ppos);
        material.shader.SetMat4("model", model);
        material.texture.Bind();
       // for (int i = 0; i < meshes.Count(); i++)
        //  meshes[i].Draw(material.shader);
      }
      else
      {
        Globals.ShaderOverride.SetMat4("mvp", mvp);
        //  Matrix4d m4 = Matrix4d.CreateTranslation((GetMatrix() * viewproj).ExtractTranslation());
        //Vector3 pos = mvp.ExtractTranslation();
        //Matrix4 ppos = Matrix4.CreateTranslation(pos);
        Globals.ShaderOverride.SetMat4("model", model);
       // for (int i = 0; i < meshes.Count(); i++)
         // meshes[i].Draw(Globals.ShaderOverride);
      }
      base.Render(viewproj);
    }
    */


    public override void Setup()
    {
      base.Setup();
    }

    private void ComputeBoundingBox()
    {
      m_sceneMin = new Vector3(1e10f, 1e10f, 1e10f);
      m_sceneMax = new Vector3(-1e10f, -1e10f, -1e10f);
      Matrix4 identity = Matrix4.Identity;

      ComputeBoundingBox(_modelScene.RootNode, ref m_sceneMin, ref m_sceneMax, ref identity);

      m_sceneCenter.X = (m_sceneMin.X + m_sceneMax.X) / 2.0f;
      m_sceneCenter.Y = (m_sceneMin.Y + m_sceneMax.Y) / 2.0f;
      m_sceneCenter.Z = (m_sceneMin.Z + m_sceneMax.Z) / 2.0f;
    }

    private void ComputeBoundingBox(Node node, ref Vector3 min, ref Vector3 max, ref Matrix4 trafo)
    {
      Matrix4 prev = trafo;
      trafo = Matrix4.Mult(prev, FromMatrix(node.Transform));

      if (node.HasMeshes)
      {
        foreach (int index in node.MeshIndices)
        {
          Mesh mesh = _modelScene.Meshes[index];
          for (int i = 0; i < mesh.VertexCount; i++)
          {
            Vector3 tmp = FromVector(mesh.Vertices[i]);
            Vector3.TransformVector(ref tmp, ref trafo, out tmp);
            //tmp = trafo * tmp;

            min.X = Math.Min(min.X, tmp.X);
            min.Y = Math.Min(min.Y, tmp.Y);
            min.Z = Math.Min(min.Z, tmp.Z);

            max.X = Math.Max(max.X, tmp.X);
            max.Y = Math.Max(max.Y, tmp.Y);
            max.Z = Math.Max(max.Z, tmp.Z);
          }
        }
      }

      for (int i = 0; i < node.ChildCount; i++)
      {
        ComputeBoundingBox(node.Children[i], ref min, ref max, ref trafo);
      }
      trafo = prev;
    }

    private Vector3 FromVector(Vector3D vec)
    {
      Vector3 v;
      v.X = vec.X;
      v.Y = vec.Y;
      v.Z = vec.Z;
      return v;
    }

    private Color4 FromColor(Color4D color)
    {
      Color4 c;
      c.R = color.R;
      c.G = color.G;
      c.B = color.B;
      c.A = color.A;
      return c;
    }

    private Matrix4 FromMatrix(Matrix4x4 mat)
    {
      Matrix4 m = new Matrix4();
      m.M11 = mat.A1;
      m.M12 = mat.A2;
      m.M13 = mat.A3;
      m.M14 = mat.A4;
      m.M21 = mat.B1;
      m.M22 = mat.B2;
      m.M23 = mat.B3;
      m.M24 = mat.B4;
      m.M31 = mat.C1;
      m.M32 = mat.C2;
      m.M33 = mat.C3;
      m.M34 = mat.C4;
      m.M41 = mat.D1;
      m.M42 = mat.D2;
      m.M43 = mat.D3;
      m.M44 = mat.D4;
      return m;
    }

    private Matrix4d FromMatrixd(Matrix4x4 mat)
    {
      Matrix4d m = new Matrix4d();
      m.M11 = mat.A1;
      m.M12 = mat.A2;
      m.M13 = mat.A3;
      m.M14 = mat.A4;
      m.M21 = mat.B1;
      m.M22 = mat.B2;
      m.M23 = mat.B3;
      m.M24 = mat.B4;
      m.M31 = mat.C1;
      m.M32 = mat.C2;
      m.M33 = mat.C3;
      m.M34 = mat.C4;
      m.M41 = mat.D1;
      m.M42 = mat.D2;
      m.M43 = mat.D3;
      m.M44 = mat.D4;
      return m;
    }
  }
}
