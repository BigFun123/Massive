using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Massive.Network;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Tools;
using OpenTK;

namespace Massive
{
  public class MNetworkObject : MObject
  {
    // EventHandler<ErrorEvent> NetworkErrorEvent;    
    Vector3d OldPos = new Vector3d();
    public double DistanceThreshold = 0.1;

    private int targetPort = 50895;
    public int TargetPort { get => targetPort; set => targetPort = value; }

    private string targetIP = "127.0.0.1";
    public string TargetIP { get => targetIP; set => targetIP = value; }

    double Freq = 3;
    double FreqAccum = 0;
    double FreqAccumSync = 0;

    public static int UniqueID = 0;

    public MNetworkObject()
      : base(EType.NetworkObject, "NetworkObject")
    {

    }

    public override void Setup()
    {
      base.Setup();
      //UniversalID = Helper.GUID();
      InstanceID = (++UniqueID).ToString();
    }

    public override void Update()
    {
      FreqAccum += Time.DeltaTime;
      FreqAccumSync += Time.DeltaTime;

      MSceneObject p = (MSceneObject)Parent;

      if (FreqAccum > Freq)
      {        
        double dist = Vector3d.Distance(p.transform.Position, OldPos);
        if (dist > DistanceThreshold)
        {
          SendState();
          OldPos = p.transform.Position;
        }
        FreqAccum = 0;
      }

      //always send full state periodically
      /*if (FreqAccumSync > Freq * 30)
      {
        SendState();
        FreqAccumSync = 0;
      }
      */
    }

    public void ReceiveState(MPosMessage m)
    {


      //string spx = sState[4];
      //string spy = sState[5];
      //string spz = sState[6];

      /*
      string srx = sState[7];
      string sry = sState[8];
      string srz = sState[9];
      string srw = sState[10];
      */
      //Info("Received from :" + sender + " " + spx + "," + spy + "," + spz);
      MSceneObject so = (MSceneObject)Parent;
      //so.transform.Position = new Vector3d(double.Parse(spx), double.Parse(spy), double.Parse(spz));
      so.transform.Position = new Vector3d(m.Position[0], m.Position[1], m.Position[2]);
      //so.transform.Rotation = new Quaterniond(double.Parse(srx), double.Parse(sry), double.Parse(srz), double.Parse(srw));
    }

    public void SendState()
    {
      if ((Globals.Network.Connected == false) || (Enabled == false)) return;

      try
      {
        MSceneObject mp = (MSceneObject)Parent;        
        MassiveNetwork.GetInstance().PositionRequest(mp.InstanceID, mp.transform.Position, mp.transform.Rotation);
      }
      catch (Exception ex)
      {
        Globals.Network.Enabled = false; //temporary disable object to give Server time to reset, or this machine to reconnect
        Console.WriteLine(ex.Message);
        Globals.Log(this, ex.Message);        

        if (ex.InnerException != null)
        {
          Console.WriteLine(ex.InnerException.Message);
        }
      }
    }
  }
}
