using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;
using System.Data;
using UnityEngine.TestTools;


namespace Tests
{
    public class SQLiteTests
    {
        /*
        // A Test behaves as an ordinary method
        [Test]
        public static void CreateNewDatabase()
        {
            Registration register = new Registration();
            
            // LoadConnectionString calls makeDatabase if database does not already exist.
            string conn = register.LoadConnectionString(); 
            
            //IDbConnection dbconn = new SqliteConnection(conn);

            bool dbExists = File.Exists(conn);

            Assert.AreEqual(true, dbExists);
        }

        [Test]
        public void AddNewAccount()
        {
            Registration register = new Registration();
            string username = "TestUsername2";
            string password = "TestPassword2";

            string conn = register.LoadConnectionString();

            IDbConnection dbconn = new SqliteConnection(conn);

            dbconn.Open();
             
            register.addAccount(username, password, dbconn);

            IDbCommand command = dbconn.CreateCommand();

            command.CommandText = "SELECT EXISTS(SELECT username FROM user WHERE username = '" + username + "');";

            int check = Convert.ToInt32(command.ExecuteScalar());

            Assert.AreEqual(1, check);

            dbconn.Close();

        }

        [Test]
        public void LoginToAccount()
        {
            Assert.AreEqual(true, true);
        }

        [Test]
        public void LoggedIn()
        {
            Assert.AreEqual(1, 1);
        }*/
        
     

    } // end LoadConnectionString()
    /*
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
        public IEnumerator SQLiteTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }*/
}
