// Gwen Morris, Claudia Coronel, Samantha Earl
// Team Scikids
// CS476 Capstone

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager {

    // Variable initialization
    public static string username;
    public static int score; 

    // Checks if user is logged in 
    //
    public static bool LoggedIn
    {
        // Checks if null (not logged in)
        get
        {
            return username != null;
        }
    }

    // Resets username to null for log out 
    public static void LogOut()
    {
        username = null; 
    }
}
