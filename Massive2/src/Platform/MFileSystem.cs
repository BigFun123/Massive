using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//platform specific access to files

namespace Massive.Platform
{
  public class MFileSystem
  {
    public static string ProjectPath = @".\";    
    public static string AssetsPath = @".\Assets\";
    public static string ShadersPath = @".\Assets\Shaders\";
    public static string AppDataPath = @".\Assets\UserData";
    public static string CachePath = @".\Assets\UserData\Cache";
    public static string RegistryPath = "Assets\\Registry.json";

    public static void SetProjectPath(string path)
    {
      if (!path.EndsWith("\\")) path += "\\";
      MFileSystem.ProjectPath = path;
      MFileSystem.AssetsPath = Path.Combine(path, "Assets\\");
      MFileSystem.AppDataPath = Path.Combine(path, @"UserData");
      MFileSystem.CachePath = Path.Combine(path, @"UserData\Cache");
      if (!Directory.Exists(MFileSystem.AppDataPath)) Directory.CreateDirectory(MFileSystem.AppDataPath);
      if (!Directory.Exists(MFileSystem.CachePath)) Directory.CreateDirectory(MFileSystem.CachePath);
    }

    public static string GetFile(string sPath)
    {
      if (!File.Exists(sPath)) return "";
      return File.ReadAllText(sPath);
    }

    public static bool SaveFile(string sPath, string sData)
    {
      bool Result = false;
      try
      {
        File.WriteAllText(sPath, sData);
        Result = true;
      }
      catch (Exception e)
      {
        Result = false;
      }

      return Result;
    }
  }
}
