using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Data;
using Mono.Data.Sqlite;
using Unity.VisualScripting;
using System.Data.Common;
using System.Security.Cryptography;

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
    private IDataReader reader;

    private IDbConnection conn;
    private string dbName = "entifarm.db";
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
    }
    private void Start()
    {
        conn = new SqliteConnection(string.Format("URI=file:{0}", dbName));
        conn.Open();
    }

    private void Update()
    {
        GetPlants();
    }

    ArrayList GetPlants()
    {
        ArrayList plants = new ArrayList();

        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM plants"; 
        IDataReader dataReader = cmd.ExecuteReader();
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
            //c.season = reader.GetInt32(6); 

            plants.Add(c);

        }

        return plants;
    }
}
