using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Massive.Network;
using Npgsql;

namespace Massive.Server
{

  public class MPostGISAdapter
  {
    string ConnectionString;
    NpgsqlConnection Connection;
    int Counter = 0;

    public MPostGISAdapter()
    {
      ConnectionString = @"UserID=massive;Password=massivepg;Host=massivepg.ctpdjiwptubz.us-east-1.rds.amazonaws.com;Port=5432;Database=postgres;
         Pooling=true;MinPoolSize=0;MaxPoolSize=100;";
      // NpgsqlConnection.GlobalTypeMapper.UseNetTopologySuite();    
      NpgsqlConnection.GlobalTypeMapper.UseGeoJson();
      
    }

    public void Close()
    {
      if (Connection != null)
      {
        Connection.Close();
      }
    }

    public void Setup()
    {
      string query = "CREATE EXTENSION postgis;";
      string table = @"CREATE TABLE ROADS(ID int4, ROAD_NAME varchar(25), geom geometry(LINESTRING, 4326));";
      string alter = @"ALTER TABLE roads ADD COLUMN geom2 geometry(LINESTRINGZ,4326);";
    }

    public void Connect()
    {
      Connection = new NpgsqlConnection(ConnectionString);
      try
      {
        Connection.Open();        
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }

    public DataTable Query(string s, out string Result)
    {
      Counter++;
      Result = "OK \n";
      DataTable results = new DataTable();
      
      try
      {
        using (var cmd = new NpgsqlCommand(s, Connection))
        {
         // cmd.UnknownResultTypeList = new[] { false, false, true, false };
          using (var reader = cmd.ExecuteReader())
          {
            results.Load(reader);            
          }
        }
      }
      catch (Exception e)
      {
        Result = e.Message;
      }

      //return Counter + ": " + Result;
      return results;
    }

    public void GetRows()
    {
      // Retrieve all rows
      using (var cmd = new NpgsqlCommand("SELECT name FROM wonderland.test", Connection))
      using (var reader = cmd.ExecuteReader())
        while (reader.Read())
          Console.WriteLine(reader.GetString(0));
    }

    public void AddObject(MServerObject mo)
    {
      // Insert some data
      using (var cmd = new NpgsqlCommand())
      {
        cmd.Connection = Connection;
        cmd.CommandText = "INSERT INTO objects (name, data, geom) VALUES (@p, 'wwhat', ST_GeomFromText('POINT(18.060316 -33.432044 22.1)', 4326))";
        cmd.Parameters.AddWithValue("p", "Hello world");
        int Result = cmd.ExecuteNonQuery();
        Console.WriteLine("Add:" + Result);
      }
    }

    public void GetClosest()
    {
      string query = @"SELECT * FROM wonderland.test 
         ORDER BY geom <-> st_setsrid(st_makepoint(18.060316, -33.412044, 22.1), 4326)      
      LIMIT 10;";

      using (var cmd = new NpgsqlCommand())
      {
        cmd.Connection = Connection;
        cmd.CommandText = query;
        cmd.UnknownResultTypeList = new[] { false, false, true, false };
        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            Console.WriteLine(reader.ToString()); //.GetString(0)
            EntityRow[] values = new EntityRow[10];
            reader.GetValues(values);
            Console.WriteLine(reader[0].ToString());
            Console.WriteLine(reader[1].ToString());
            Console.WriteLine(reader[2].ToString());
            Console.WriteLine(reader[3].ToString());
          }
        }
      }

    }

  }

}
