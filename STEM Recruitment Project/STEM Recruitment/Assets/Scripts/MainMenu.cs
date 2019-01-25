using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Functions to load new scenes 
    public void Modules ()
    {
        // Call to new scene
        SceneManager.LoadScene("Modules");

    }

    public void LogIn()
    {
        // Call to new scene
        SceneManager.LoadScene("LogIn");

    }

    public void Register()
    {
        // Call to new scene
        SceneManager.LoadScene("Registration");

    }

    public void AboutUs()
    {
        // Call to new scene
        SceneManager.LoadScene("AboutUs");

    }

    public void Settings()
    {
        // Call to new scene
        SceneManager.LoadScene("Settings");

    }

    public void Back()
    {
        // Call to new scene
        SceneManager.LoadScene("Home");

    }

    // Exits game 
    public void quitGame()
    {
        // Displays message in console of Unity
        Debug.Log("Succesful Quit");

        // Quits game
        Application.Quit(); 

    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
