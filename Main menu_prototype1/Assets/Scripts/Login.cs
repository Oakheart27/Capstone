using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class Login : MonoBehaviour {

    // Initialization of variables 
    public InputField nameField;
    public InputField passwordField;

    public Text errorDisplay;

    public Button submitButton;

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());

    }

    IEnumerator LoginPlayer ()
    {

        WWWForm form = new WWWForm(); // Creates WWWForm 

        // Addition of fields for login
        form.AddField("name", nameField.text); 
        form.AddField("password", passwordField.text);

        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
        yield return www;

        // Check for successful information pull (0)
        if (www.text[0] == '0')
        {
            DBManager.username = nameField.text;

            // pulls score from string and converts to int
            DBManager.score = int.Parse(www.text.Split('\t')[1]);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0); // return to main menu page 
        }

        else
        {
            // Display error information 
            Debug.Log("User login failed. Error #" + www.text);
            errorDisplay.text = "Error #" + www.text;

        }
    }

    // Method to check minimum character of fields for login panel 
    public void VerifyInputs()
    {
        // Checks if username 8+ characters and password is 5+ characters 
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 5);
    }
}
