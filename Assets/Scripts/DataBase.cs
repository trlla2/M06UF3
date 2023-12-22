using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Data;
using Mono.Data.Sqlite;
using Unity.VisualScripting;
using System.Data.Common;
using System.Security.Cryptography;
using System.Numerics;

public class Plant
{
    public int id =0;
    public string name= "";
    public float time =0.0f ;
    public int quantity = 0;
    public float sell = 0f;
    public float buy =0f;
    public int season = 0;
}
public class DataBase : MonoBehaviour
{
    public static DataBase DB;
    private IDbConnection conn;

   // private IDataReader reader;

    private string dbName = "entifarm.db";

    public List<Plant> plants = new List<Plant>();


    private void Awake()
    {
        if(DB != null && DB != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DB = this;
            DontDestroyOnLoad(this.gameObject);
        }
        conn = new SqliteConnection(string.Format("URI=file:{0}", dbName));
        conn.Open();
        GetPlants();
        GetUserPlants();
    }
   

    public List<Plant> GetPlants()
    {

        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM plants;";
        IDataReader reader = cmd.ExecuteReader();
        if (reader == null)
        {
            return plants;
        }

        while (reader.Read())
        {

            Plant c = new Plant();

            c.id = reader.GetInt32(0);
            c.name = reader.GetString(1);
            c.time = reader.GetFloat(2);
            c.quantity = reader.GetInt32(3);
            c.sell = reader.GetFloat(4);
            c.buy = reader.GetFloat(5);
            if (!reader.IsDBNull(6))
            {
                c.season = reader.GetInt32(6); 
            }
            plants.Add(c);

        }

        return plants;
    }

    public List<Plant> GetUserPlants()
    {
        List<Plant> userPlants = new List<Plant>();

        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT plants.id_plant, plants.plant, plants.time, plants.quantity * COUNT(*) as total_quantity, plants.sell, plants.buy FROM plants_users LEFT JOIN plants ON plants.id_plant = plants_users.id_plant WHERE plants_users.id_user = 1 GROUP BY plants.id_plant;";
        IDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Plant c = new Plant();

            c.id = reader.GetInt32(0);
            c.name = reader.GetString(1);
            c.time = reader.GetFloat(2);
            c.quantity = reader.GetInt32(3);
            c.sell = reader.GetFloat(4);
            c.buy = reader.GetFloat(5);
            if (!reader.IsDBNull(6))
            {
                c.season = reader.GetInt32(6);
            }
            userPlants.Add(c);
        }

        return userPlants;
    }
}
