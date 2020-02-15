using Assimp;
using Assimp.Configs;
using Massive.Platform;
using Massive.Tools;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// From Blender:
/// FBX Export
/// SCALE: 0.01
/// Mesh:
/// Z Forward  (Exporter is -Z Forward by Default)
/// Y Up
/// Armature:
///  Y
///  X
/// </summary>


namespace Massive.Graphics.Character
{
  public class MAnimatedModel : MSceneObject
  {
    const uint MAX_BONES = 100; //must match animation shader
    //List<MAnimatedMesh> Meshes;
    public Scene scene; //assimp
    Matrix4x4 m_global_inverse_transform;
    int m_num_bones = 0;
    Dictionary<string, uint> m_bone_mapping; // maps a bone name and their index
    List<BoneMatrix> m_bone_matrices = new List<BoneMatrix>();        
    public MAnimationController _animationController;    
    Matrix4x4[] transforms = new Matrix4x4[MAX_BONES];
    public Vector3d BoneOffset = Vector3d.Zero;


    public static Matrix4[] debug_transforms = new Matrix4[MAX_BONES];

    public MAnimatedModel(EType type = EType.AnimatedModel, string sName = "AnimatedModel")
      : base(type, sName)
    {
     // Meshes = new List<MAnimatedMesh>();
      m_bone_mapping = new Dictionary<string, uint>();
      _animationController = new MAnimationController();


      for (int i = 0; i < MAX_BONES; i++)
      {
        transforms[i] = Matrix4x4.Identity;
      }
    }

    public override void SetMaterial(MMaterial m)
    {
      base.SetMaterial(m);
      foreach( MObject mo in Modules)
      {
        if ( mo is MAnimatedMesh)
        {
          MAnimatedMesh mao = (MAnimatedMesh)mo;
          mao.SetMaterial(material);
        }
      }
      //foreach( MAnimatedMesh mesh in Meshes)
      //{
//        mesh.SetMaterial(material);
  //    }
    }

    public void CopyTo(MAnimatedModel m)
    {
      //do not call base(MSceneObject) CopyTo here because it is handled by SpawnHandler and adds extra Physics
      //base.CopyTo(m);
      m.SetMaterial(material);
      //m.Meshes = Meshes;
      m.scene = scene;
      m.BoneOffset = BoneOffset;
      m.m_global_inverse_transform = m_global_inverse_transform;
      m.transforms = transforms;
      m.m_num_bones = m_num_bones;
      m.m_bone_matrices = m_bone_matrices;
      for (int i = 0; i < MAX_BONES; i++)
      {
        m.transforms[i] = Matrix4x4.Identity;
      }
      m.m_bone_mapping = m_bone_mapping;          
      //controller must not be linked otherwise all animations have the same controller
      m._animationController = new MAnimationController();
      m.CalculateDrawMatrices(Matrix4d.Identity, Matrix4d.Identity);
      m._animationController.Setup(m);
      _animationController.CopyTo(m._animationController);
      _animationController.PlayAnimation("idle", 1);
      
    }

    public void Load(string sName)
    {
      string sFileName = sName;
      string sFullPath = Path.Combine(MFileSystem.AssetsPath, sFileName);
      AssimpContext importer = new AssimpContext();
      importer.SetConfig(new NormalSmoothingAngleConfig(35.0f));
      try
      {
        scene = importer.ImportFile(sFullPath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }

      m_global_inverse_transform = scene.RootNode.Transform;
      m_global_inverse_transform.Inverse();

      processNode(scene.RootNode, scene);
      _animationController.PlayAnimation("idle", 1);
    }

    void processNode(Node node, Scene scene)
    {
      MAnimatedMesh mesh;
      for (int i = 0; i < scene.MeshCount; i++)
      {
        Mesh ai_mesh = scene.Meshes[i];
        mesh = processMesh(ai_mesh, scene);

        Matrix4d tr = FromMatrixd(node.Transform);
        mesh.transform.Position = tr.ExtractTranslation();
        mesh.transform.Rotation = tr.ExtractRotation();
        mesh.transform.Scale = tr.ExtractScale();
        mesh.AddMaterial(this.material);

        //Meshes.Add(mesh); //accumulate all meshes in one vector        
        Add(mesh);
      }
    }

    MAnimatedMesh processMesh(Mesh mesh, Scene scene)
    {
      Console.WriteLine("bones: ", mesh.BoneCount, " vertices: ", mesh.VertexCount);

      List<AnimatedVertex> vertices = new List<AnimatedVertex>();
      List<int> indices = new List<int>();

      //List<MTexture> textures = new List<MTexture>();
      VertexBoneData[] bones_id_weights_for_each_vertex = new VertexBoneData[mesh.VertexCount];
      for (int i = 0; i < bones_id_weights_for_each_vertex.Length; i++)
      {
        bones_id_weights_for_each_vertex[i].InitVertexBoneData();
      }

      //vertices
      for (int i = 0; i < mesh.VertexCount; i++)
      {
        AnimatedVertex vertex = new AnimatedVertex();
        Vector3 vector;
        vector.X = mesh.Vertices[i].X;
        vector.Y = mesh.Vertices[i].Y;
        vector.Z = mesh.Vertices[i].Z;
        vertex._position = vector;

        if (mesh.Normals != null)
        {
          vector.X = mesh.Normals[i].X;
          vector.Y = mesh.Normals[i].Y;
          vector.Z = mesh.Normals[i].Z;
          vertex._normal = vector;
        }
        else
        {
          vertex._normal = Vector3.Zero;
        }

        // in assimp model can have 8 different texture coordinates
        // we only care about the first set of texture coordinates
        if (mesh.HasTextureCoords(0))
        {
          Vector2 vec = new Vector2(0, 0);
          vec.X = mesh.TextureCoordinateChannels[0][i].X;
          vec.Y = mesh.TextureCoordinateChannels[0][i].Y;
          vertex._textureCoordinate = vec;
        }
        else
        {
          vertex._textureCoordinate = new Vector2(0.0f, 0.0f);
        }
        vertices.Add(vertex);
      }

      //int[] indices = mesh.GetIndices();

      //indices
      for (int i = 0; i < mesh.FaceCount; i++)
      {
        Face face = mesh.Faces[i];
        // retrieve all indices of the face and store them in the indices vector
        for (int j = 0; j < face.IndexCount; j++)
          indices.Add(face.Indices[j]);
      }

      //Indices = indices.ToArray();


      //material
      /*
      if (mesh.MaterialIndex >= 0)
      {
        //all pointers created in assimp will be deleted automaticaly when we call import.FreeScene();
        Material material = scene.Materials[mesh.MaterialIndex];
        List<MTexture> diffuse_maps = LoadMaterialTexture(material, aiTextureType_DIFFUSE, "texture_diffuse");
        bool exist = false;
        for (int i = 0; (i < textures.size()) && (diffuse_maps.size() != 0); i++)
        {
          if (textures[i].path == diffuse_maps[0].path) // ������ ���� �������� 1 �������� ������� � 1 ������� � ����� ����
          {
            exist = true;
          }
        }
        if (!exist && diffuse_maps.size() != 0) textures.push_back(diffuse_maps[0]); //������ �������� �� 1 �������� !!!
                                                                                     //textures.insert(textures.end(), diffuse_maps.begin(), diffuse_maps.end());

        vector<Texture> specular_maps = LoadMaterialTexture(material, aiTextureType_SPECULAR, "texture_specular");
        exist = false;
        for (int i = 0; (i < textures.size()) && (specular_maps.size() != 0); i++)
        {
          if (textures[i].path == specular_maps[0].path) // ������ ���� �������� 1 �������� ������� � 1 ������� � ����� ����
          {
            exist = true;
          }
        }
        if (!exist && specular_maps.size() != 0) textures.push_back(specular_maps[0]); //������ �������� �� 1 �������� !!!
                                                                                       //textures.insert(textures.end(), specular_maps.begin(), specular_maps.end());

      }
      */

      // Dictionary<string, uint>  m_bone_mapping = new Dictionary<string, uint>();

      // load bones

      for (int i = 0; i < mesh.BoneCount; i++)
      {
        uint bone_index = 0;
        string bone_name = mesh.Bones[i].Name;

        //cout << mesh->mBones[i]->mName.data << endl;

        if (!m_bone_mapping.ContainsKey(bone_name)) // ��������� ��� �� � ������� ��������
        {
          // Allocate an index for a new bone
          bone_index = (uint)m_num_bones;
          m_num_bones++;
          //BoneMatrix bi = new BoneMatrix();
          //m_bone_matrices.Add(bi);

          BoneMatrix bbi = new BoneMatrix();
          //m_bone_matrices[bone_index];
          bbi.offset_matrix = mesh.Bones[i].OffsetMatrix;
         // bbi.offset_matrix.Inverse();
          m_bone_matrices.Add(bbi);

          //m_bone_matrices[bone_index] = bbi;
          m_bone_mapping[bone_name] = (uint)bone_index;

          //cout << "bone_name: " << bone_name << "			 bone_index: " << bone_index << endl;
        }
        else
        {
          bone_index = (uint)m_bone_mapping[bone_name];
        }

        //bones_id_weights_for_each_vertex = new List<VertexBoneData>(mesh.Bones[i].VertexWeightCount);

        for (int j = 0; j < mesh.Bones[i].VertexWeightCount; j++)
        {
          int vertex_id = mesh.Bones[i].VertexWeights[j].VertexID; // �� ������� �� ������ ����� �������� �����
          float weight = mesh.Bones[i].VertexWeights[j].Weight;
          bones_id_weights_for_each_vertex[vertex_id].addBoneData(bone_index, weight); // � ������ ������� ����� ����� � �� ���

          // ������ ������� vertex_id �� ������ ����� � �������� bone_index  ����� ��� weight
          //cout << " vertex_id: " << vertex_id << "	bone_index: " << bone_index << "		weight: " << weight << endl;
        }
      }


      return new MAnimatedMesh(vertices, indices.ToArray(), bones_id_weights_for_each_vertex);
    }

    public override void Setup()
    {
      initShaders(material.shader.ProgramID);
      _animationController.Setup(this);
     // _debug = new MPhysicsDebug();
      base.Setup();
    }

    void initShaders(int shader_program)
    {
      for (uint i = 0; i < MAX_BONES; i++) // get location all matrices of bones
      {
        string name = "bones[" + i + "]";// name like in shader
        // m_bone_location[i] = GL.GetUniformLocation(shader_program, name);
      }

      // rotate head AND AXIS(y_z) about x !!!!!  Not be gimbal lock
      //rotate_head_xz *= glm::quat(cos(glm::radians(-45.0f / 2)), sin(glm::radians(-45.0f / 2)) * glm::vec3(1.0f, 0.0f, 0.0f));
    }

    void DebugBones(Matrix4d viewproj, Matrix4d parentmodel, Matrix4x4[] transforms)
    {
      if (Globals.RenderPass != Globals.eRenderPass.ShadowDepth)
      {
        GL.Clear(ClearBufferMask.DepthBufferBit);

        Vector3d v = Globals.Avatar.GetPosition();
        Vector3d v1 = Vector3d.Zero;
        Vector3d v2 = Vector3d.Zero;

       // _debug.UserColorCoding = true;
        Matrix4 test = Matrix4.CreateTranslation(0, 0.5f, 0);
        float f = 0;
        for (int i = 0; i < m_bone_matrices.Count; i++)
        {
          //(MBone b in Bones)

          f += 0.1f;
          v1 = v2;
          v2 = MassiveTools.Vector3dFromVector3(
            TKMatrix(transforms[i]).ExtractTranslation()            
            );

          Vector3d r1 = v1 + v;
          Vector3d r2 = v2 + v;
         // _debug.DrawLine(ref r1, ref r2, i == 0 ? OpenTK.Graphics.Color4.Red : OpenTK.Graphics.Color4.White);
        }

       // _debug.Render(viewproj, parentmodel);
      }
    }

    public override void Update()
    {
      _animationController.Update();
      Animation ani = _animationController.GetCurrentAnimation();
      if (ani != null)
      {
        _animationController.TotalTime = ani.DurationInTicks;
        CalcAnimation2(scene.RootNode, ani, Matrix4x4.Identity);
        for (uint i = 0; i < m_num_bones; i++)
        {
          transforms[i] = m_bone_matrices[(int)i].final_world_transform
            * Matrix4x4.FromTranslation(new Vector3D((float)BoneOffset.X, (float)BoneOffset.Y, (float)BoneOffset.Z));
        }
      }

      base.Update();
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      //if (Globals.RenderPass == Globals.eRenderPass.Pick) return;

      int bonesLocation = 0;
      if (Globals.RenderPass == Globals.eRenderPass.ShadowDepth)
      { 
        Globals.ShaderOverride.SetInt("HasBones", 1);
        bonesLocation = Globals.ShaderOverride.GetLocation("bones");
      }
      else
      {
        material.Bind();
        bonesLocation = material.shader.GetLocation("bones");
      }

     
      
      //upload animated bones to GPU
      GL.UniformMatrix4(bonesLocation, m_num_bones, true, ref transforms[0].A1);

      base.Render(viewproj, parentmodel);

      if (Globals.RenderPass == Globals.eRenderPass.ShadowDepth)
      {       
        Globals.ShaderOverride.SetInt("HasBones", 0);
      }
    }

    Matrix4x4 FindRotation(NodeAnimationChannel n)
    {
      Matrix4x4 result = Matrix4x4.Identity;
      for (int c = 0; c < n.RotationKeyCount; c++)
      {
        QuaternionKey q = n.RotationKeys[c];

        q.Value = new Assimp.Quaternion(q.Value.W, q.Value.X, q.Value.Y, q.Value.Z);

        VectorKey pk = n.PositionKeys[c];
        VectorKey ps = n.ScalingKeys[c];

        if (_animationController.FrameCounter <= q.Time)
        {
          result =
               (Matrix4x4)q.Value.GetMatrix()
             * (Matrix4x4.FromTranslation(pk.Value))              
              * (Matrix4x4.FromScaling(ps.Value))
          ;
          break;
        }
      }
      return result;
    }


    public void CalcAnimation2(Node node, Animation ani, Matrix4x4 parent_transform)
    {
      NodeAnimationChannel n = FindChannel(ani, node.Name);

      Matrix4x4 node_transform = node.Transform;

      string node_name = node.Name;

      if (n != null)
      {
        node_transform = FindRotation(n);
      }
      Matrix4x4 global_transform = node_transform * parent_transform ;
      if (m_bone_mapping.ContainsKey(node_name))
      {
        uint bone_index = m_bone_mapping[node_name];
        BoneMatrix bi = m_bone_matrices[(int)bone_index];
        //bi.final_world_transform = m_global_inverse_transform * global_transform * m_bone_matrices[(int)bone_index].offset_matrix;
        bi.final_world_transform = m_bone_matrices[(int)bone_index].offset_matrix * global_transform * m_global_inverse_transform;
        m_bone_matrices[(int)bone_index] = bi;
        //counter++;
      }

      for (int i = 0; i < node.ChildCount; i++)
      {
        Node child = node.Children[i];
        CalcAnimation2(child, ani, global_transform);
      }
    }
    
    NodeAnimationChannel FindChannel(Animation ani, string NodeName)
    {
      for (int i = 0; i < ani.NodeAnimationChannelCount; i++)
      {
        NodeAnimationChannel n = ani.NodeAnimationChannels[i];
        if (n.NodeName == NodeName) return n;
      }
      return null;
    }

    

    public static OpenTK.Matrix4 TKMatrix(Assimp.Matrix4x4 input)
    {
      return new OpenTK.Matrix4(input.A1, input.B1, input.C1, input.D1,
                                 input.A2, input.B2, input.C2, input.D2,
                                 input.A3, input.B3, input.C3, input.D3,
                                 input.A4, input.B4, input.C4, input.D4);
    }

    public static Matrix4x4 FromTKMatrix(Matrix4d input)
    {
      Matrix4 fm = MTransform.GetFloatMatrix(input);

      return new Matrix4x4(fm.M11, fm.M21, fm.M31, fm.M41,
                           fm.M12, fm.M22, fm.M32, fm.M42,
                           fm.M13, fm.M23, fm.M33, fm.M43,
                           fm.M14, fm.M24, fm.M34, fm.M44);
    }

    public static OpenTK.Matrix4d TKMatrixd(Assimp.Matrix4x4 input)
    {
      return new OpenTK.Matrix4d(input.A1, input.B1, input.C1, input.D1,
                                 input.A2, input.B2, input.C2, input.D2,
                                 input.A3, input.B3, input.C3, input.D3,
                                 input.A4, input.B4, input.C4, input.D4);
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

    Assimp.Quaternion nlerp(Assimp.Quaternion a, Assimp.Quaternion b, float blend)
    {
      //cout << a.w + a.x + a.y + a.z << endl;
      a.Normalize();
      b.Normalize();

      Assimp.Quaternion result;
      float dot_product = a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
      float one_minus_blend = 1.0f - blend;

      if (dot_product < 0.0f)
      {
        result.X = a.X * one_minus_blend + blend * -b.X;
        result.Y = a.Y * one_minus_blend + blend * -b.Y;
        result.Z = a.Z * one_minus_blend + blend * -b.Z;
        result.W = a.W * one_minus_blend + blend * -b.W;
      }
      else
      {
        result.X = a.X * one_minus_blend + blend * b.X;
        result.Y = a.Y * one_minus_blend + blend * b.Y;
        result.Z = a.Z * one_minus_blend + blend * b.Z;
        result.W = a.W * one_minus_blend + blend * b.W;
      }
      result.Normalize();
      return result;
    }
  }
}
