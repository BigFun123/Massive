using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Massive.Network;
using MySql.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace MassiveServer.src
{
  public class MDatabase
  {
    //in the db, make sure to only allow entry for localhost users
    private MySqlConnection connection = null;
    public string DatabaseName = "wonderland";
    public string Hostname = "127.0.0.1";
    public string UserName = "root";
    public string Password = "root";
    public string Port = "3306";
    public string ResultText = "";
    string ConnectionString;
    public string ConfigFile = "mysql_config.json";

    public MDatabase()
    {
      LoadSettings();
      Setup();
    }

    public void Setup()
    {
      
      ConnectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}; port={4};",
        Hostname, DatabaseName, UserName, Password, Port);
    }

    public void SaveSettings()
    {
      string sData = DatabaseName + "|" + Hostname + "|" + UserName + "|" + Password + "|" + Port;
      File.WriteAllText(ConfigFile, sData);
    }

    void LoadSettings()
    {
      if (File.Exists(ConfigFile))
      {
        string sData = File.ReadAllText(ConfigFile);
        string[] items = sData.Split('|');
        DatabaseName = items[0];
        Hostname = items[1];
        UserName = items[2];
        Password = items[3];
        Port = items[4];
      }

    }

    public int QueryParam(string sQuery, Dictionary<string,string> Params)
    {
      int NumResults = 0;

      try
      {
        Setup();
        //we must use 'using' method for multithreaded operation
        using (var connection = new MySqlConnection(ConnectionString)) //actually pulls a connection from the pool
        {
          using (var cmd = connection.CreateCommand())
          {
            connection.Open();
            cmd.CommandText = sQuery;

            foreach( KeyValuePair<string,string> k in Params)
            {
              cmd.Parameters.AddWithValue("@"+k.Key, k.Value);
            }
            
            NumResults = cmd.ExecuteNonQuery();
            ResultText = NumResults + " rows affected";
          }
        }
      }
      catch (Exception e)
      {
        ResultText = e.Message;
        Console.WriteLine(e.Message);
      }
      finally
      {
        Disconnect();
      }


      return NumResults;
    }

      public int Query(string sQuery)
    {
     // Console.WriteLine(sQuery);
      ResultText = "Ok";
      //DataTable results = new DataTable();
      int NumResults = 0;

      try
      {
        Setup();
        //we must use 'using' method for multithreaded operation
        using (var connection = new MySqlConnection(ConnectionString)) //actually pulls a connection from the pool
        {
          using (var cmd = connection.CreateCommand())
          {
            connection.Open();
            cmd.CommandText = sQuery;
            NumResults = cmd.ExecuteNonQuery();
            ResultText = NumResults + " rows affected";
          }
        }
      }
      catch (Exception e)
      {
        ResultText = e.Message;
        Console.WriteLine(e.Message);
      }
      finally
      {
        Disconnect();
      }
      return NumResults;
    }



    //for UI table
    public DataTable QueryReader(string sQuery)
    {
      //Console.WriteLine(sQuery);
      ResultText = "Ok";
      DataTable results = new DataTable();
      try
      {
        Setup();
        using (var connection = new MySqlConnection(ConnectionString)) //actually pulls a connection from the pool
        {
          using (var cmd = connection.CreateCommand())
          {
            connection.Open();
            cmd.CommandText = sQuery;
            var reader = cmd.ExecuteReader();
            results.Load(reader);
            reader.Close();
          }
        }
      }
      catch (Exception e)
      {
        ResultText = e.Message;
      }
      finally
      {
        Disconnect();
      }
      /*
            while (reader.Read())
            {
              string someStringFromColumnZero = reader.GetString(0);
              string someStringFromColumnOne = reader.GetString(1);
              Console.WriteLine(someStringFromColumnZero + "," + someStringFromColumnOne);
            }
            */

      return results;
    }

    //for UI table
    public int QueryScalar(string sQuery)
    {
      ResultText = "Ok";
      int Result = 0;
      try
      {
        Setup();
        using (var connection = new MySqlConnection(ConnectionString)) //actually pulls a connection from the pool
        {
          using (var cmd = connection.CreateCommand())
          {
            connection.Open();
            cmd.CommandText = sQuery;
            Result = Convert.ToInt32(cmd.ExecuteScalar());
          }
        }
      }
      catch (Exception e)
      {
        ResultText = e.Message;
      }
      finally
      {
        Disconnect();
      }
      return Result;
    }

    public string DataTableToJSON(DataTable dt)
    {
      JsonSerializer serializer = new JsonSerializer();
      List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
      Dictionary<string, object> row;
      foreach (DataRow dr in dt.Rows)
      {
        row = new Dictionary<string, object>();
        foreach (DataColumn col in dt.Columns)
        {
          row.Add(col.ColumnName, dr[col]);
        }
        rows.Add(row);
      }
      return JsonConvert.SerializeObject(rows, Formatting.Indented);
    }

    public void Disconnect()
    {
      if (connection != null)
      {
        connection.Close();
        connection = null;
      }
    }
  }
}
