using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Server
{
  public static class MRudeWords
  {
    public static bool IsRude(string s)
    {
      if (string.IsNullOrEmpty(s)) return false;
      s = s.ToLower();
      if (s.IndexOf("fuck") > -1) return true;
      if (s.IndexOf("shit") > -1) return true;
      if (s.IndexOf("cunt") > -1) return true;
      if (s.IndexOf("whore") > -1) return true;
      if (s.IndexOf("cock") > -1) return true;
      if (s.IndexOf("pussy") > -1) return true;
      if (s.IndexOf("slut") > -1) return true;
      if (s.IndexOf("faggot") > -1) return true;
      if (s.IndexOf("penis") > -1) return true;
      if (s.IndexOf("vagina") > -1) return true;
      if (s.IndexOf("clitoris") > -1) return true;
      if (s.IndexOf("nigger") > -1) return true;
      if (s.IndexOf("kaffir") > -1) return true;
      return false;
    }
  }
}
