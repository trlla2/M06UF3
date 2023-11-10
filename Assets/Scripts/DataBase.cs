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
    
    private  Plant[] plants;
    private IDbConnection conn;
    private string dbName = "entifarm.db";

    private void Start()
    {
        conn = new SqliteConnection(string.Format("URI=file:{0}", dbName));
        conn.Open();
        GetPlants();
    }

    public void GetPlants()
    {
        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM plants"; 
        IDataReader dataReader = cmd.ExecuteReader();
        int index = 0;
        while (dataReader.Read())
        {
            Plant p = new Plant();

            p.id = dataReader.GetInt32(0);
            p.name = dataReader.GetString(1); 
            p.time = dataReader.GetFloat(2);
            p.quantity = dataReader.GetInt32(3);
            p.sell = dataReader.GetFloat(4);
            p.buy = dataReader.GetFloat(5);
            if (!dataReader.IsDBNull(6))
            {
                p.season = dataReader.GetInt32(6);
            }

            plants[index] = p;
            index ++;
        }
    }
}
