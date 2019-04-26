using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    // Intsance variables 
    public int size;
    public string[] sentences;
    public string userChoice; 
    //public string[] followup; 
    private int index;
    public int counter;
    public int qnum; 
    public float typingSpeed;
    public int person1, person2, person3; 

    // Objects in Unity 
    public GameObject continueBtn;
    public GameObject choiceBtn;
    public GameObject p1Btn;
    public GameObject p2Btn;
    public GameObject p3Btn;
    public GameObject resultsBtn; 
    public GameObject p1, p2; 

    public Text textDisplay;
    public Text result;
    public Text feedback;

    //private string continueBtnStr = "Continue";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        // Check if text displayed is current sentence index
        if (textDisplay.text == sentences[index])
        { 
            continueBtn.SetActive(true);
            
            /*l
            p1Btn.SetActive(true);
            p2Btn.SetActive(true);
            p3Btn.SetActive(true);*/
         
            if (qnum < 3)
            {
                resultsBtn.SetActive(false);
            }
            else { resultsBtn.SetActive(true); }

        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueBtn.SetActive(false); // hide continue button
        continueBtn.GetComponentInChildren<Text>().text = "Continue";
        resultsBtn.SetActive(false);
        resultsBtn.GetComponentInChildren<Text>().text = "Results";
        // checks if at the end of sentece
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueBtn.SetActive(false); // hide continue button
            continueBtn.GetComponentInChildren<Text>().text = "Continue";
            resultsBtn.SetActive(false);
            resultsBtn.GetComponentInChildren<Text>().text = "Results";
        }
    }

    public void ButtonClick(Button btn)
    {
        if (btn.name == "p1Btn")
        {
            person1 += 1;
            Debug.Log("Updated player 1");
        }
        else if (btn.name == "p2Btn") 
        {
            person2 += 1;
            Debug.Log("Updated Player 2");
        }
        else if (btn.name == "p3Btn")
        {
            person3 += 1;
            Debug.Log("Updated Player 3");
        }
        else
        {
            Debug.Log("No button press detected"); 
        }
        qnum += 1;
        /*
        p1Btn.SetActive(false);
        p2Btn.SetActive(false);
        p3Btn.SetActive(false);*/
        Debug.Log(person1 + ", " +  person2 + ", " + person3);
    }

    public void endGame()
    {
        Debug.Log("We're in the endgame now!");
        userChoice = "Greg"; 
        if (person2 > person1 && person2 > person3)
        {
            userChoice = "Lisa"; 
        }

        if (person3 > person1 && person3 > person2)
        {
            userChoice = "Tyrone";
        }
        print("The user picked " + userChoice);
        result.text = "The person you said gave the best answer the most often is " + userChoice; 

        if (userChoice == "Greg")
        {
            feedback.text = "Greg is very professionally dressed. He did take advantage of providing skills he has in the tell me about yourself sectiion. He doesn't have the " +
                "\programming knowledge for the job "; 
        }
        if (userChoice == "Lisa")
        {
            feedback.text = "Out of the three candidates, Lisa seems to meet most of the requirements for the position of entry-level software engineer. She has a bachelor's degree" +
                " in Computer Science and knows a majority of the programming languages that are required for the field.";
        }
        if (userChoice == "Tyrone")
        {
            feedback.text = "Tyrone does not have the best attire for a job interview. He is also missing a Bachelors degree that is needed for the job. He knows one of the languages " +
                "but, By comparison there are better candidates that were interviewed."; 
        }
    }
}
