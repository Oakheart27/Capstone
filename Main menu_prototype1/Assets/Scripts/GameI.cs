using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameI : MonoBehaviour {

    // Initialization of Variables
    public Text userName; 

	// Use this for initialization
	void Start () {

        // Checks if logged in 
        if (DBManager.LoggedIn)
        {
            userName.text = "Welcome " + DBManager.username;
        }
    }
}
