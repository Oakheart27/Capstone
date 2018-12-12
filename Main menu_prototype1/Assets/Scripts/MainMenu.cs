using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Text playerDisplay;
    public Button registerButton;
    public Button loginButton;   
    

    // If logged in, displays username
	private void Start()   
    {
        // Checks if logged in 
        if (DBManager.LoggedIn)
        {
            playerDisplay.text = "Welcome " + DBManager.username;
        }

        // Will hide buttons based on whether or not the user is logged in or not 
        registerButton.interactable = !DBManager.LoggedIn;
        loginButton.interactable = !DBManager.LoggedIn; 

    }
}
