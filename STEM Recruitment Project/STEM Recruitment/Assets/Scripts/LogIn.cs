using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;
using System.Data; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogIn : MonoBehaviour {
     
    public InputField nameField;
    public InputField passwordField;
    public Text errorDisplay;
    public Button submitButton;
    private int id; // ID for future use.

    
    // Use this for initialization
    void Start () {
        submitButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        string conn = LoadConnectionString(); // Once button is clicked, open database

        Debug.Log(conn);

        IDbConnection dbconn = new SqliteConnection(conn);

        dbconn.Open();

        //returnAll(dbconn);

        if (nameField.text == null || passwordField.text == null) // Make sure there is an entry for username and password
        {
            errorDisplay.text = "Please enter a username or password.";
        }
        else
        {
            if (accountExists(nameField.text, passwordField.text, dbconn))
            {
                errorDisplay.text = "Login Successful. Welcome back " + nameField.text;

                id = getID(nameField.text, dbconn);
                Debug.Log("ID: " + id);
            }
            else
            {
                Debug.Log("Couldn't find account");
            }
        }
    }

    bool accountExists(string username, string password, IDbConnection dbconn)
    {
        // Counting all instances of given username
        IDbCommand checkUName = dbconn.CreateCommand();

        checkUName.CommandText = "SELECT count(*) FROM users WHERE username = '" + username + "';";

        int UCount = Convert.ToInt32(checkUName.ExecuteScalar());

        // Counting all instances of given password
        IDbCommand checkPass = dbconn.CreateCommand();

        checkPass.CommandText = "SELECT count(*) FROM users WHERE password = '" + password + "';";

        int PCount = Convert.ToInt32(checkPass.ExecuteScalar());

        if (UCount == 0 || PCount == 0) // If username or password is not found, return false
        {
            errorDisplay.text = "Incorrect username or password";

            return false;
        }

        return true;
    }

    int getID(string username, IDbConnection dbconn)
    {
        IDbCommand idQuery = dbconn.CreateCommand();

        idQuery.CommandText = "SELECT id FROM users WHERE username = '" + username + "';";

        int id = Convert.ToInt32(idQuery.ExecuteScalar());

        return id;
    }
    string LoadConnectionString()
    {
        if(Application.isEditor)
        {
            return "URI=file:" + Application.dataPath + "/Plugins/prototypedb.s3db;";
        }
        // TODO: Get build to find database
        else
        {
            return "URI=file:" + Application.persistentDataPath + "/prototypedb.s3db;";
        }
        
    } // end LoadConnectionString()

    //////// This method is just for testing purposes. It iterates through database and prints everything in it. //////
    void returnAll(IDbConnection dbconn)
    {
        IDataReader reader;

        IDbCommand command = dbconn.CreateCommand();

        command.CommandText = "SELECT * FROM users";

        reader = command.ExecuteReader();

        while (reader.Read())
        {
            Debug.Log("id: " + reader[0].ToString() + " username: " + reader[1].ToString() + " password: " + reader[2].ToString());

        }
    }// end returnAll
}
