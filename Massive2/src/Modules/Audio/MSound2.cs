using Massive.Platform;
using OpenTK;
using OpenTK.Audio.OpenAL;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Massive
{
  public class MSound2 : MObject
  { 
    Timer timer = new Timer(10000);
    //Process
    public int SoundBO, source;
    public Vector3 DeltaPos;
    public float Volume = 1;
    public bool Loop { get; set; }
    private string currentFile = "";
    public string CurrentFile { get => currentFile; set => currentFile = value; }

    public unsafe MSound2()
      : base(EType.Sound, "Audio")
    {
      DeltaPos = Vector3.Zero;
    }

    public void CopyTo(MSound2 so)
    {
      so.currentFile = CurrentFile;
      so.Loop = Loop;
      so.Volume = Volume;
    }

    public override void Dispose()
    {
      AL.DeleteBuffer(SoundBO);
    }

    public override void Setup()
    {
      string sFile = Path.Combine(MFileSystem.AssetsPath, currentFile);      
      if (!File.Exists(sFile))
      {
        Error = "File not found:" + sFile;
        return;
      }

      Console.WriteLine(new OpenTK.Audio.AudioContext());

      AL.GenBuffers(1, out SoundBO);
      AL.GenSources(1, out source);
      
      int channels, bits_per_sample, sample_rate;
      byte[] sound_data = LoadWave(File.Open(sFile, FileMode.Open), out channels, out bits_per_sample, out sample_rate);

      if ( sound_data == null )
      {
        Error = "sound_data NULL";
      }

      if ( channels == 2)
      {
        Error = "WARNING: Sound has 2 channels, won't 3d. " + sFile;
      }

      /*
      int sampleFreq = 44100;
      double dt = 2 * Math.PI / sampleFreq;
      double amp = 0.5;

      int freq = 220;
      var dataCount = sampleFreq / freq;

      var sinData = new short[dataCount];
      for (int i = 0; i < sinData.Length; ++i)
      {
        sinData[i] = (short)(amp * short.MaxValue * Math.Sin(i * dt * freq));
      }
      AL.BufferData(buffers, ALFormat.Mono16, sinData, sinData.Length, sampleFreq);
      AL.Source(source, ALSourcei.Buffer, buffers);
      AL.Source(source, ALSourceb.Looping, true);
      */

      AL.BufferData(SoundBO, GetSoundFormat(channels, bits_per_sample), sound_data, sound_data.Length, sample_rate);
      AL.Source(source, ALSourcei.Buffer, SoundBO);
     // AL.Source(source, ALSourceb.Looping, Loop);

      Vector3 spos = Vector3.Zero;
      AL.Source(source, ALSource3f.Position, ref spos);
     // AL.Source(source, ALSource);
      float maxd = 1000000;
      AL.Source(source, ALSourcef.MaxDistance, maxd);

      //Play();
    }

    public override void Update()
    {
      if ( Parent.Renderable)
      {
        MSceneObject so = (MSceneObject)Parent;
        DeltaPos = MTransform.GetVector3(so.transform.Position - Globals.GlobalOffset);
        AL.Source(source, ALSource3f.Position, ref DeltaPos);
      }
    }

    public void Play()
    {
      DeltaPos = Vector3.Zero;
      AL.Source(source, ALSource3f.Position, ref DeltaPos);
      AL.Source(source, ALSourcef.Gain, Volume);
      AL.Source(source, ALSourceb.Looping, Loop);
      AL.SourcePlay(source);
    }

    public override void OnStop()
    {
      AL.SourceStop(source);
      base.OnStop();
    }

    public void Pause()
    {
      AL.SourcePause(source);
    }

    // Loads a wave/riff audio file.
    public static byte[] LoadWave(Stream stream, out int channels, out int bits, out int rate)
    {
      if (stream == null)
        throw new ArgumentNullException("stream");

      using (BinaryReader reader = new BinaryReader(stream))
      {
        // RIFF header
        string signature = new string(reader.ReadChars(4));
        if (signature != "RIFF")
          throw new NotSupportedException("Specified stream is not a wave file.");

        int riff_chunck_size = reader.ReadInt32();

        string format = new string(reader.ReadChars(4));
        if (format != "WAVE")
          throw new NotSupportedException("Specified stream is not a wave file.");

        // WAVE header
        string format_signature = new string(reader.ReadChars(4));
        if (format_signature != "fmt ")
          throw new NotSupportedException("Specified wave file is not supported.");

        int format_chunk_size = reader.ReadInt32();
        int audio_format = reader.ReadInt16();
        int num_channels = reader.ReadInt16();
        int sample_rate = reader.ReadInt32();
        int byte_rate = reader.ReadInt32();
        int block_align = reader.ReadInt16();
        int bits_per_sample = reader.ReadInt16();

        string data_signature = new string(reader.ReadChars(4));
        if (data_signature != "data")
          throw new NotSupportedException("Specified wave file is not supported.");

        int data_chunk_size = reader.ReadInt32();

        channels = num_channels;
        bits = bits_per_sample;
        rate = sample_rate;

        return reader.ReadBytes((int)reader.BaseStream.Length);
      }
    }

    public static ALFormat GetSoundFormat(int channels, int bits)
    {
      switch (channels)
      {
        case 1: return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
        case 2: return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
        default: throw new NotSupportedException("The specified sound format is not supported.");
      }
    }


  }
}
