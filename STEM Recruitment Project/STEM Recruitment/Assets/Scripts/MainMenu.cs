using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //DBManager dbManage;
    public TextMeshProUGUI welcomeMessage;

    // Use this for initialization
    void Start()
    {
        DBManager dbManage = GetComponent<DBManager>();
        welcomeMessage = GetComponent<TextMeshProUGUI>();

        Debug.Log("Status: " + dbManage.getStatus());

       /* if(dbManage.getStatus())
        {
            welcomeMessage.text += dbManage.getUsername(dbManage.getID());
        }*/

    }
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

        // Log out of account
        DBManager dbManage = GetComponent<DBManager>();
        dbManage.logOut();

        // Quits game
        Application.Quit(); 

    }

}
