using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class Time : MObject
  {
    public Stopwatch stopwatch = new Stopwatch();
    //public Stopwatch stopwatch_total = new Stopwatch();
    public static long FrameCount = 0;
    public static double DeltaTime; // s
    public static double DeltaTimeMS; // ms
    public static double PreviousTime; // ms
    public static double TotalTime; //ms

    public Time() : base(EType.Other, "Time")
    {
      stopwatch.Start();
    }
    
    public override void Update()
    {
      FrameCount++;      
      TotalTime = stopwatch.ElapsedMilliseconds;
      DeltaTimeMS = TotalTime - PreviousTime;
      PreviousTime = TotalTime;
      DeltaTime = DeltaTimeMS * 0.001;
    }
  }
}
