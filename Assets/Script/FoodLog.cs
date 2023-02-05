using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using TMPro;

public class FoodLog : MonoBehaviour
{


    //public TextMeshPro foodInput;
    //public TextMeshProUGUI foodInput;
    public TextMeshProUGUI foodInput;
    public TextMeshProUGUI mealInput;
    public TMP_Text foodlist;


    // the name of the db -
    private string dbName = "URI=file:FodLog.db";

    // Start is called before the first frame update
    void Start()
    {
        // run the method to create the table
        CreateDB();
        // display existing food list
        DisplayFood();
    }

    void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS fooditems (foodname VARCHAR(30), meal VARCHAR(20)); ";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

    }

    public void AddFood()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO fooditems (foodname, meal) VALUES ('" + foodInput.text + "', '" + mealInput.text + "'); ";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void DeleteALLFood()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM fooditems; ";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void DeleteSingleFood()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM fooditems WHERE foodname = '" + foodInput.text + "'; ";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void DisplayFood()
    {
        foodlist.text = "";

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM fooditems ORDER BY meal; ";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())

                        foodlist.text += reader["foodname"] + "\t\t" + reader["meal"] + "\n";

                    reader.Close();
                }

            }
            connection.Close();
        }

    }
}