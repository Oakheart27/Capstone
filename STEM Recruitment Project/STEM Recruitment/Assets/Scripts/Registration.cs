using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;
using System.Data;
using UnityEngine.UI;


public class Registration : MonoBehaviour
{

    public Text errorDisplay;
    public InputField nameField;
    public InputField passwordField;
    public Button submitButton;

    // Start is called before the first frame update
    void Start()
    {
        submitButton.onClick.AddListener(TaskOnClick);
    } // end start()

    void TaskOnClick()
    {
        string conn = LoadConnectionString(); // Once button is clicked, open database

        Debug.Log(conn);

        IDbConnection dbconn = new SqliteConnection(conn);

        dbconn.Open();

        returnAll(dbconn);

        if (nameField.text == null || passwordField.text == null) // Make sure there is an entry for username and password
        {
            errorDisplay.text = "Please enter a username or password.";
        }

        else
        {
            if (verifyInputs(nameField.text, passwordField.text, dbconn)) // Make sure inputs have correct character length and do not already exist in database
            {
                addAccount(nameField.text, passwordField.text, dbconn);
            }
            else
            {
                Debug.Log("Couldn't add account");
            }

        }

        dbconn.Close();
    } // end TaskOnClick()

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

    void addAccount(string username, string password, IDbConnection dbconn)
    {
        IDbCommand addUserQuery = dbconn.CreateCommand();

        addUserQuery.CommandText = "INSERT INTO users (username, password) VALUES ('" + username + "', '" + password + "');";

        addUserQuery.ExecuteNonQuery();

        //Debug.Log("Added to database. New entries are: "); // Test

        errorDisplay.text = "Account added. Welcome " + username;

        //returnAll(dbconn); // Test

    } // end addAccount

    string LoadConnectionString()
    {
        if (Application.isEditor) // If using editor, go to database in plugins folder
        {
            return "URI=file:" + Application.dataPath + "/Plugins/prototypedb.s3db;";
        }
        else // If using build, get filepath (in Users/<your_name>/AppData/LocalLow/SciKids)
        {
            string filePath = Application.persistentDataPath + "/prototypedb.s3db";

            if (!File.Exists(filePath)) //if database doesn't exit, create database
            {
                Debug.Log(Application.persistentDataPath);

                makeDatabase(filePath);

            }

            return "URI=file:" + filePath;
        }
    } // end LoadConnectionString()

    // Creates the database if doesn't exist
    void makeDatabase(string filePath)
    {
        SqliteConnection.CreateFile(filePath);

        SqliteConnection dbconn = new SqliteConnection("URI=file:" + filePath);

        dbconn.Open();

        // Create 1st table - users
        string command = "CREATE TABLE IF NOT EXISTS users(id INTEGER PRIMARY KEY, username VARCHAR(30) NOT NULL, password VARCHAR(30) NOT NULL);";

        Debug.Log(command);

        SqliteCommand makeTableQuery = dbconn.CreateCommand();

        makeTableQuery.CommandText = command;

        makeTableQuery.ExecuteNonQuery();

        dbconn.Close();
    } // end makeDatabase()

    bool verifyInputs(string username, string password, IDbConnection dbconn)
    {
        //////// Make sure username is 8-30 characters and password is 5-30 characters ////////////
        if (username.Length < 8)
        {
            errorDisplay.text = "Username must be at least 8 characters long";

            return false;
        }

        if (password.Length < 5)
        {
            errorDisplay.text = "Password must be at least 5 characters long";

            return false;
        }

        if (username.Length > 30 || password.Length > 30)
        {
            errorDisplay.text = "Username and Password must be under 30 characters";

            return false;
        }

        //////// Make sure username and password are unique ///////////
        // Counting all instances of given username
        IDbCommand checkUName = dbconn.CreateCommand();

        checkUName.CommandText = "SELECT count(*) FROM users WHERE username = '" + username + "';";

        int UCount = Convert.ToInt32(checkUName.ExecuteScalar());

        // Counting all instances of given password
        IDbCommand checkPass = dbconn.CreateCommand();

        checkPass.CommandText = "SELECT count(*) FROM users WHERE password = '" + password + "';";

        int PCount = Convert.ToInt32(checkPass.ExecuteScalar());

        // If either the username or password return a value greater than 0, that entry is in the database. So return false.
        if (UCount > 0 || PCount > 0)
        {
            errorDisplay.text = "Username or Password already exists.";

            return false;

        }

        return true;
    } // end verfiyInputs()
}