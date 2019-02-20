using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;
using System.Data;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DBManager : MonoBehaviour
{
    private bool isLoggedIn;
    private int playerID;
    
    public bool getStatus()
    {
        string path = getTxtPath();

        if(File.Exists(path))
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
        if(!File.Exists(getTxtPath()))
        {
            Debug.Log("Error: File does not exist (DBManager.getID");
            return -1;
        }

        else
        {
            using(TextReader reader = File.OpenText(getTxtPath()))
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

        while(reader.Read())
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

        if(File.Exists(path))
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
        if(Application.isEditor)
        {
            return Application.dataPath + "/Plugins/status.txt";
        }
        else
        {
            return Application.persistentDataPath + "/status.txt";
        }
    }

}
