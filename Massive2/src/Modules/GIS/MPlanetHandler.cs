using Massive;
using Massive.Events;
using Massive.Network;
using Massive.GIS;
using Massive.Tools;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * 
 * Universe Reference Date : 2000/01/01
 * Universe Reference point : Sol Center "0,0,0"
 * Units: Meters, kg
 * 
 * */

namespace Massive
{
  public class MPlanetHandler : MObject
  {
    public static List<MAstroBody> Bodies = new List<MAstroBody>();
    public static MAstroBody CurrentNear;
    const string CloudTexURL = @"Textures\Planets\Clouds.png";
    const string OpaqueCloudTexURL = @"Textures\Planets\Clouds_2048.jpg";
    const string SkyTexURL = @"Textures\Planets\sky2.jpg";

    //MCube GravityIndicator;
    double ScaleFactor = 1000;
    MMaterial MSkyMaterial;

    public static MPlanetHandler _Instance;

    public MPlanetHandler() : base(MObject.EType.Null, "Planet")
    {
      _Instance = this;
    }

    public static MAstroBody Get(string sName)
    {
      foreach( MAstroBody m in Bodies)
      {
        if (m.Name == sName) return m;
      }
      return null;
    }

    public override void Setup()
    {
      //TODO:
      //ADD URL TEXTURES!

      Bodies.Add(new MAstroBody("Sol", "The Sun", 1.988544E+30, new Vector3d(0, 0, 0),
      new Vector3d(6.955E+5, 6.955E+5, 6.955E+5) * ScaleFactor,
      "Textures\\Planets\\2k_sun.jpg", false, 0, false, false)); // x 5 for gas emission shader

      Bodies.Add(new MAstroBody("Mercury", "The planet Mercury", 3.302E+23,
        new Vector3d(-3.676448098104388E+07, -5.914211791485628E+07, -3.979251580149613E+06) * 1000,
        new Vector3d(2440, 2440, 2440) * ScaleFactor, "Textures\\Planets\\2k_mercury.jpg"
        , false, 0, false, true));
      //VX = 3.256023326434234E+01 VY = -2.128637611764578E+01 VZ = 5.328388929915358E-01

      Bodies.Add(new MAstroBody("Venus", "The planet Venus. Friggin awesom.", 48.685E+23,
        new Vector3d(-1.048122216556790E+08, 2.393654051492437E+07, -6.939236908324338E+06) * 1000,
         new Vector3d(6051.8, 6051.8, 6051.8) * ScaleFactor,
         "Textures\\Planets\\2k_venus.jpg"
         , false, 0, false, true));
      //VX = -8.048894150515352E+00 VY = -3.423458014481214E+01 VZ = 6.724535965214056E-01

      Bodies.Add(new MAstroBody("Earth", "The planet Earth. Mostly harmless.", 5.97219E+24,
        new Vector3d(1.272267138537290E+7, 1.463568132767451E+8, -7.583510712786615E+6) * 1000,
        new Vector3d(6371, 6371, 6371) * ScaleFactor, "Textures\\Planets\\Albedo.jpg",
        true, (6365) * ScaleFactor, false, false));
      //VX = -2.999254798461512E+01 VY = 2.409743982501453E+00 VZ = -3.489312789986800E+00

      Bodies.Add(new MAstroBody("Mars", "The planet Mars. Soft and chewy.", 6.4185E+23,
        new Vector3d(1.995782587374064E+8, -5.523248829579433E+7, 2.040911708145680E+7) * 1000,
        new Vector3d(3389.9, 3389.9, 3389.9) * ScaleFactor, "Textures\\Planets\\2k_mars.jpg"
        , false, 0, false, true));
      //VX= 7.888925502949942E+00 VY= 2.512145341232943E+01 VZ=-1.428163670266684E-01

      Bodies.Add(new MAstroBody("Jupiter", "The planet Jupiter, by Jupiter", 1898.13E+24,
        new Vector3d(6.889448949554296E+08, 2.740877921731672E+08, 4.453849463134830E+07) * 1000,
        new Vector3d(71492, 71492, 71492) * ScaleFactor,
        "Textures\\Planets\\2k_jupiter.jpg"
        , false, 0, false, true));

      Bodies.Add(new MAstroBody("Saturn", "The planet Saturn. The laziest planet in the sol system", 5.68319E+26,
        new Vector3d(1.177516875361957E+09, 7.071279503136320E+08, 3.202538221937501E+07) * 1000,
        new Vector3d(60268, 60268, 60268) * ScaleFactor,
        "Textures\\Planets\\2k_saturn.jpg",
        false, 0, true, true));

      Bodies.Add(new MAstroBody("Uranus", "The planet Uranus. Not a gas giant.", 86.8103E+24,
        new Vector3d(1.553968700866261E+09, -2.526552315539761E+09, 2.925724853501363E+08) * 1000,
        new Vector3d(25362, 25362, 25362) * ScaleFactor,
        "Textures\\Planets\\2k_uranus.jpg"
        , false, 0, false, true));

      Bodies.Add(new MAstroBody("Neptune", "The planet Neptune.", 102.41E+24,
        new Vector3d(1.460744159765920E+09, -4.239590984479527E+09, 4.426341416587460E+08) * 1000,
        new Vector3d(24766, 24766, 24766) * ScaleFactor,
        "Textures\\Planets\\2k_neptune.jpg"
        , false, 0, false, true));

      //VX = 5.092374353910485E+00 VY = 1.848518616013199E+00 VZ = 2.857105569330195E-01

      Bodies.Add(new MAstroBody("Moon", "The Moon", 734.9E+20,
        new Vector3d(1.235183178863080E+07, 1.462044997645861E+08, -7.578605371678978E+06) * ScaleFactor,
        new Vector3d(1738.0, 1738.0, 1738.0) * ScaleFactor, "Textures\\Planets\\2k_moon.jpg"
        , false, 0, false, false));
      //VX=-2.965755272661235E+01 VY= 1.496658883251257E+00 VZ=-3.397496558449063E+00

      AddGravityIndicator();

      InitializePlanets();
    }

    void AddGravityIndicator()
    {
      MGravityIndicator mg = new MGravityIndicator();
      mg.SetMaterial((MMaterial)MScene.MaterialRoot.FindModuleByName(MMaterial.DEFAULT_MATERIAL));
      MScene.Overlay.Add(mg);
    }

    public override void Update()
    {
      MGravityCalculator.CalculateGravityAtAvatar();
      base.Update();
    }

    public static Vector3d GetUpAt(Vector3d pos)
    {
      if (_Instance == null) return new Vector3d(0, 1, 0);
      return MGravityCalculator.GetGravityVector(pos);
    }

    void CreateShaders()
    {
      MShader SkyShader = Helper.CreateShader("SkyShader");
      SkyShader.Load("default_v.glsl",
        "sky_f.glsl",
        "Terrain\\eval.glsl",
        "Terrain\\control.glsl"
        );
      SkyShader.Bind();
      SkyShader.SetInt("material.diffuse", MShader.LOCATION_DIFFUSE);
      SkyShader.SetInt("material.specular", MShader.LOCATION_SPECULAR);
      SkyShader.SetInt("material.multitex", MShader.LOCATION_MULTITEX);
      SkyShader.SetInt("material.normalmap", MShader.LOCATION_NORMALMAP);
      SkyShader.SetInt("material.shadowMap", MShader.LOCATION_SHADOWMAP);

      MSkyMaterial = new MMaterial("SKY01M");
      MSkyMaterial.AddShader(SkyShader);
      MSkyMaterial.SetDiffuseTexture(Globals.TexturePool.GetTexture(SkyTexURL));
      MScene.MaterialRoot.Add(MSkyMaterial);
    }

    public void InitializePlanets()
    {
      //GravityIndicator = Helper.CreateCube(MScene.AstroRoot, "GravitySphere", Vector3d.Zero);
      //GravityIndicator.transform.Scale = new Vector3d(0.05, 0.1, 0.1);
      //GravityIndicator.OwnerID = "MasterAstronomer";
      //MScene.AstroRoot.Add(GravityIndicator);
      //GravityIndicator.SetMaterial((MMaterial)MScene.MaterialRoot.FindModuleByName(MMaterial.DEFAULT_MATERIAL));
      CreateShaders();
      
      for (int i=0; i< Bodies.Count; i++)
      {
        MAstroBody m = Bodies[i];
        //m.Radius = m.Radius * 0.5;
        if (m.IsTemplate == true) continue;
        m.ListIndex = i;
        Vector3d pos = m.Position + new Vector3d(-m.Radius.X, 0, 0) * (m.HasRings == true ? 3.0 : 1.1);
        MServerZone zone = new MServerZone("MASTER_ASTRONOMER", m.Name, "Astronomical",
          MassiveTools.ToVector3_Server(pos));
        zone.Rotation = MassiveTools.ArrayFromQuaterniond(Quaterniond.Identity);
        zone.Description = m.Description + " \nRadius:" + m.Radius;
        MMessageBus.AddZone(this, zone);
        //Extensions.LookAt(m.Position + m.Radius * 2, m.Position), m.Name));

        MSceneObject mo;
        //planet files contain uvs for mercator
        if (m.HasAtmosphere)
        {
          CurrentNear = m;
          mo = Helper.CreateModel(MScene.AstroRoot, m.Name, @"Models\planets\earth.3ds", Vector3d.Zero);
          //mo = Helper.CreateSphere(MScene.AstroRoot, 3, "Planet");
          //mo.transform.Scale = m.Radius * 1.00055;
          mo.transform.Scale = m.Radius;
        }
        else
        {
          mo = Helper.CreateModel(MScene.AstroRoot, m.Name, @"Models\planets\planet_sphere2.3ds", Vector3d.Zero);
          mo.transform.Scale = m.Radius;
        }

        if (m.HasRings)
        {
          MModel ring = Helper.CreateModel(MScene.Priority2, m.Name + "_rings", @"Models\planets\planet_rings.3ds", Vector3d.Zero);
          ring.transform.Position = m.Position;
          ring.transform.Rotation = Quaterniond.FromEulerAngles(0, 0, 5 * Math.PI / 180);
          ring.transform.Scale = m.Radius;
          ring.InstanceID = m.Name;
          ring.TemplateID = m.Name;
          ring.DistanceThreshold = m.Radius.X * 12;
          ring.OwnerID = "MasterAstronomer";
          MMaterial ringmat = new MMaterial(m.Name + "_mat");
          ringmat.AddShader((MShader)MScene.MaterialRoot.FindModuleByName(MShader.DEFAULT_SHADER));
          MTexture ringtex = Globals.TexturePool.GetTexture(@"Textures\Planets\saturn_rings.png");
          ringmat.SetDiffuseTexture(ringtex);
          ring.SetMaterial(ringmat);

          MPhysicsObject ringpo = new MPhysicsObject(ring, "Physics", 0, MPhysicsObject.EShape.ConcaveMesh, false, m.Radius);
          ringpo.SetLinearFactor(0, 0, 0);
          ringpo.SetRestitution(0.5);
        }

        mo.transform.Position = m.Position;
        //mo.transform.Scale = m.Radius * 1.9999;

        mo.InstanceID = m.Name;
        mo.TemplateID = m.Name;
        mo.OwnerID = "MasterAstronomer";
        mo.DistanceThreshold = m.Radius.X * 110002; //distance at which it becomes visible
        //MModel mod = (MModel)mo.FindModuleByType(EType.Model);
        //mod.DistanceThreshold = mo.DistanceThreshold;
        //MMesh met = (MMesh)mod.FindModuleByType(EType.Mesh);
        //if ( met != null ) { 
        //met.DistanceThreshold = mo.DistanceThreshold;
        //}
        mo.Tag = m;
        m.Tag = mo;

        //now that we have a 3d model, apply some progressive textures (will auto-switch with smooth transition in shader)
        MMaterial mat = new MMaterial(m.Name + "_mat");
        mat.AddShader((MShader)MScene.MaterialRoot.FindModuleByName(MShader.DEFAULT_SHADER));
        MTexture tex = Globals.TexturePool.GetTexture(m.TextureName);
        mat.SetDiffuseTexture(tex);
        mo.SetMaterial(mat);
        MTexture tex2 = Globals.TexturePool.GetTexture("Textures\\terrain\\sand01b.jpg");
        mat.SetMultiTexture(tex2);
        MTexture tex3 = Globals.TexturePool.GetTexture("Textures\\terrain\\water.jpg");
        mat.SetNormalMap(tex3);

        double dia = 2.0 * Math.PI * m.Radius.X * 0.0000001;
        //double dia = 1;
        mat.Tex2CoordScale = new Vector2((float)dia, (float)dia);
        mo.SetMaterial(mat);

        MScene.MaterialRoot.Add(mat);

        if (m.HasAtmosphere)
        {
          if (Settings.DrawTerrains == true)
          {
            m.AddDynamicTerrain(); //adds tile based terrain (from e.g. tile service)
          }

          //MPhysicsObject po = new MPhysicsObject(mo, "Physics", 0, MPhysicsObject.EShape.ConcaveMesh, 
          //false, m.Radius);
          MPhysicsObject po = new MPhysicsObject(mo, "Physics", 0, MPhysicsObject.EShape.Sphere,
            false, m.Radius);
          //po.SetLinearFactor(0, 0, 0);
          //po.SetRestitution(0.5);
          //MSphere moc = Helper.CreateSphere(MScene.AstroRoot, 3, m.Name+ "Clouds", Vector3d.Zero);

          MModel moc = Helper.CreateModel(MScene.AstroRoot, m.Name + "_clouds", @"Models\planets\clouds.3ds", Vector3d.Zero);
          moc.CastsShadow = false;
          moc.transform.Position = m.Position;
          moc.transform.Scale = m.Radius;
          moc.DistanceThreshold = m.Radius.X * 3;
          //moc.transform.Scale = m.Radius*2.1;
          moc.InstanceID = m.Name;
          moc.TemplateID = m.Name;
          moc.OwnerID = "MasterAstronomer";
          moc.Tag = m;

          MMaterial cmat = new MMaterial("CloudMat");
          cmat.AddShader((MShader)MScene.MaterialRoot.FindModuleByName(MShader.DEFAULT_SHADER));
          cmat.Opacity = 1;
          cmat.IsSky = 1;
          // = new MTexture("CloudTex");
          MTexture ctex = Globals.TexturePool.GetTexture(CloudTexURL);
          ctex.Additive = false;
          cmat.SetDiffuseTexture(ctex);
          moc.SetMaterial(cmat);
          MScene.MaterialRoot.Add(cmat);

          MModel sky = Helper.CreateModel(MScene.AstroRoot, m.Name + "_sky", @"Models\sky.3ds", Vector3d.Zero);
          sky.CastsShadow = false;
          sky.transform.Position = m.Position;
          sky.transform.Scale = m.Radius;
          //moc.transform.Scale = m.Radius*2.1;
          sky.InstanceID = m.Name;
          sky.TemplateID = m.Name;
          sky.OwnerID = "MasterAstronomer";
          sky.Tag = m;
          sky.SetMaterial(cmat);
          sky.DistanceThreshold = m.Radius.X * 4;
          MObjectAnimation ani = new MObjectAnimation();
          ani.AngleOffset = Quaterniond.FromEulerAngles(0, 0.002, 0);
          ani.Speed = 1;
          sky.Add(ani);

          sky.SetMaterial(MSkyMaterial);

          /*MMaterial csky = new MMaterial("Skymat");
          csky.AddShader((MShader)MScene.MaterialRoot.FindModuleByName(MShader.DEFAULT_SHADER));
          csky.Opacity = 0.5;
          csky.IsSky = 1;
          // = new MTexture("CloudTex");
          MTexture cSkyTex = Globals.TexturePool.GetTexture(OpaqueCloudTexURL);
          cSkyTex.Additive = false;
          csky.SetDiffuseTexture(cSkyTex);
          sky.SetMaterial(csky);
          MScene.MaterialRoot.Add(csky);
          */



          /* MSphere water = Helper.CreateSphere(MScene.ModelRoot, 5, "Water");
           water.transform.Position = m.Position;
           water.transform.Scale = m.Radius * 2.00;
           MMaterial waterman = new MMaterial("Watermat");
           MShader shader = new MShader("watershader");
           shader.Load("ocean_vs.glsl", "ocean_fs.glsl");
           shader.Bind();
           shader.SetInt("diffuseTexture", 0);
           shader.SetInt("shadowMap", 1);
           waterman.AddShader(shader);
           water.SetMaterial(waterman);
           */
          //water.SetMaterial((MMaterial)MScene.MaterialRoot.FindModuleByName(MMaterial.DEFAULT_MATERIAL));          
        }
        else
        {
          MPhysicsObject po = new MPhysicsObject(mo, "Physics", 0, MPhysicsObject.EShape.Sphere, false,
            m.Radius * 0.999);
          po.SetLinearFactor(0, 0, 0);
          po.SetRestitution(0.5);
        }
        m.Setup();
        //Console.WriteLine("Created:" + mo.Name + ":" + (mo.transform.Position) + " R:" + m.Radius);
      }

    }
  }
}
