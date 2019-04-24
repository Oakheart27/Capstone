using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;
using System.Data;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DBManager 
{
    private bool isLoggedIn;
    private int playerID;

    public bool getStatus()
    {
        string path = getTxtPath();

        if (File.Exists(path))
        {
            return true;
        }

        return false;
    }

    public void setStatus(bool status)
    {
        isLoggedIn = status;
    }


    public int getID()
    {
        if (!File.Exists(getTxtPath()))
        {
            Debug.Log("Error: File does not exist (DBManager.getID");
            return -1;
        }

        else
        {
            using (TextReader reader = File.OpenText(getTxtPath()))
            {
                int id = int.Parse(reader.ReadLine());
                return id;
            }
        }
    }

    public string getUsername()
    {
        string conn = loadConnectionString();

        string returnUsername = "";

        int id = getID();

        SqliteConnection dbconn = new SqliteConnection(conn);

        dbconn.Open();

        SqliteCommand command = new SqliteCommand(dbconn);

        command.CommandText = "SELECT username FROM user WHERE id = " + id + ";";

        SqliteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            returnUsername += reader["username"];
        }

        Debug.Log(returnUsername);
        dbconn.Close();
        return returnUsername;

    }

    string loadConnectionString()
    {
        if (Application.isEditor)
        {
            return "URI=file:" + Application.dataPath + "/Plugins/prototypedb.s3db;";
        }
        else
        {
            return "URI=file:" + Application.persistentDataPath + "/prototypedb.s3db;";
        }

    }

    // When user logs in/registers, a temporary file is made 
    public void writeID(int userID)
    {
        string path = getTxtPath();

        if (File.Exists(path))
        {
            Debug.Log("Error: file already exists");

            logOut();

        }

        File.Create(path).Dispose();

        TextWriter writer = new StreamWriter(path);

        // Writes userID to file
        writer.Write(userID);

        writer.Close();
    }

    static void readStatus()
    {
        string path = getTxtPath();

        Debug.Log(path);

        StreamReader reader = new StreamReader(path);

        string status = reader.ReadToEnd();

        Debug.Log(status);

        reader.Close();
    }

    public void logOut()
    {
        string path = getTxtPath();

        File.Delete(path);
    }

    static string getTxtPath()
    {
        if (Application.isEditor)
        {
            return Application.dataPath + "/Plugins/status.txt";
        }
        else
        {
            return Application.persistentDataPath + "/status.txt";
        }
    }

    public void addScoreToDB(int score, int userID, int gameID)
    {
        if(getID() < 0)
        {
            return;
        }

        string path = loadConnectionString();

        IDbConnection dbconn = new SqliteConnection(path);

        dbconn.Open();

        // Check to see if user has already played game before
        IDbCommand checkUser = dbconn.CreateCommand();
        checkUser.CommandText = "SELECT userID FROM score WHERE userID = " + userID + ";";
        Debug.Log(checkUser.CommandText);
        int check = Convert.ToInt32(checkUser.ExecuteScalar());

        // Check if score is higher or lower than score already in database
        IDbCommand checkScore = dbconn.CreateCommand();
        checkScore.CommandText = "SELECT userScore FROM score WHERE userID = " + userID + ";";
        Debug.Log(checkScore.CommandText);
        int check2 = Convert.ToInt32(checkScore.ExecuteScalar());

        // If user is already in table...
        if (check > 0)
        {
            // If score is lower than new score...
            if(check2<score)
            {
                // Add to table
                IDbCommand updateScore = dbconn.CreateCommand();

                updateScore.CommandText = "UPDATE score SET userScore = " + score + " WHERE userID = " + userID + " AND gameID = " + gameID + ";";

                updateScore.ExecuteNonQuery();
            }
        }


        // If user is not in table ...
        else
        {
            IDbCommand addNewUser = dbconn.CreateCommand();

            addNewUser.CommandText = "INSERT INTO score(gameID, userID, userScore) VALUES (" + gameID +
                ", " + userID + ", " + score + ");";

            Debug.Log(addNewUser.CommandText);

            addNewUser.ExecuteNonQuery();

            Debug.Log("Score of " + score + " was added.");
        }
    }

    public int getHighScore(int userID, int gameID)
    {
        if(getStatus() == false)
        {
            return -1;
        }

        string path = loadConnectionString();

        IDbConnection dbconn = new SqliteConnection(path);

        dbconn.Open();

        IDbCommand getScoreCmd = dbconn.CreateCommand();

        getScoreCmd.CommandText = "SELECT userScore FROM score WHERE userID = " + userID + ";";

        int score = Convert.ToInt32(getScoreCmd.ExecuteScalar());

        return score;


    }

}
