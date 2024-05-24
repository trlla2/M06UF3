using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Data;
using Mono.Data.Sqlite;
using Unity.VisualScripting;
using System.Data.Common;
using System.Security.Cryptography;
using System.Numerics;
using UnityEngine.UIElements;

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

public class User
{
    public int id = 0;
    public string user = "";
    public string password = "";
    public int id_savedGame = 0;
}

public class cell
{
    public int id = 0;
    public int x = 0;
    public int y = 0;
    public float time = 0.0f;
    public int id_plant = 0;
}

public class DataBase : MonoBehaviour
{
    public static DataBase DB;
    private IDbConnection conn;

   // private IDataReader reader;

    private string dbName = "entifarm.db";

    public List<Plant> plants = new List<Plant>();
    public List<Plant> userPlants = new List<Plant>();
    public User user = new User();

    private IDataReader reader;

    [Header("New Users Stats")]
    [SerializeField] private float startMoney = 25f;
    [SerializeField] private int startRowCell = 1; //SIZE en la BDD

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
        //GetUserPlants();
    }
   

    public List<Plant> GetPlants()
    {

        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM plants;";
        reader = cmd.ExecuteReader();
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

    public void GetUserPlants(int userId)
    {
       

        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT plants.id_plant, plants.plant, plants.time, plants.quantity * COUNT(*) as total_quantity, plants.sell, plants.buy FROM plants_users LEFT JOIN plants ON plants.id_plant = plants_users.id_plant WHERE plants_users.id_user = \"" + userId + "\" GROUP BY plants.id_plant;";
        reader = cmd.ExecuteReader();

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
    }

    public bool LoginUser(string username, string password)
    {

        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM users WHERE user = \"" + username + "\" AND password = \"" + password + "\";";
        reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            user.id = reader.GetInt32(0);
            user.user = reader.GetString(1);
            user.password = reader.GetString(2);
            reader.Close();
            
            cmd.CommandText = "SELECT id_savedgame FROM saved_games WHERE id_user = " + user.id + ";";
            reader = cmd.ExecuteReader();  //GET ID SavedGame
            reader.Read();
            user.id_savedGame = reader.GetInt32(0);
            reader.Close();
            
            GetUserPlants(user.id);

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetUser(string username)
    {

        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM users WHERE user = \"" + username + "\";";
        reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            user.id = reader.GetInt32(0);
            user.user = reader.GetString(1);
            user.password = reader.GetString(2);

            return true;
        }
        else
        {
            return false;
        }
    }

    
    public void RegisterUser(string username, string password)
    {
        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO users (user,password) VALUES ( \"" + username + "\", \"" + password + "\");";
        reader = cmd.ExecuteReader();
        reader.Close();
        
        
        
        cmd.CommandText = "SELECT id_user FROM users WHERE user = \"" + username + "\";";
        reader = cmd.ExecuteReader();  //GET ID USER
        reader.Read();
        
        int idUser = reader.GetInt32(0);
        
        reader.Close();

        cmd.CommandText = "INSERT INTO saved_games ( time, size, money, id_user) VALUES ( 0 , " + startRowCell + ", " + startMoney + ", " + idUser + ");";
        reader = cmd.ExecuteReader();// Create Saved Game
        reader.Close();


        cmd.CommandText = "SELECT id_savedgame FROM saved_games WHERE id_user = " + idUser + ";";
        reader = cmd.ExecuteReader();  //GET ID SavedGame
        reader.Read();
        
        int idSavedGame = reader.GetInt32(0);
        
        reader.Close();


        for (int x = 0; x < startRowCell; x++)
        {
            for (int y = 0; y < 4; y++) // 4 = colum number of cell grid
            {
                cmd.CommandText = "INSERT INTO savedgames_cells ( x, y, time, id_plant, id_savedgame) VALUES ( " + x + " , " + y + ", " + 0 + ", " + 0 + ", " + idSavedGame + ");";
                reader = cmd.ExecuteReader();// Create Cells
                reader.Close();
            }
        }
    }

    public int GetRows()
    {
        int rows = startRowCell;
        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT size FROM saved_games WHERE id_user = " + user.id + "";
        reader = cmd.ExecuteReader();
        if (reader.Read()) 
        { 
            rows = reader.GetInt32(0);
        }

        reader.Close();

        return rows;
    }

    public float GetMoney()
    {
        float money = startMoney;
        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT money FROM saved_games WHERE id_user = " + user.id + "";
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            money = reader.GetFloat(0);
        }

        return money;
    }

    public int GetSavedGameID()
    {
        int savedGameID = 0;
        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT saved_games FROM users WHERE id_user = \"" + user.id + "\";";
        reader = cmd.ExecuteReader();  //GET ID SavedGame
        if (reader.Read())
        {
            savedGameID = reader.GetInt32(0);
        }
        return savedGameID;
    }

    public cell GetCell(int x, int y)
    {
        cell cell = new cell();

        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT id_savedgame_cell ,time,  id_plant FROM savedgames_cells WHERE x = " + x + " AND y = " + y + " AND id_savedgame = " + user.id_savedGame + ";";
        reader = cmd.ExecuteReader();


        if (reader.Read())
        {
            cell.id = reader.GetInt32(0);
            cell.time = reader.GetFloat(1);
            cell.id_plant = reader.GetInt32(2);
        }

        return cell;
    }

}
