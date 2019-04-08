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

            /*
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
        resultsBtn.SetActive(false); 

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
            resultsBtn.SetActive(false); 
        }
    }

    public void ButtonClick(Button btn)
    {
        if (btn.name == "p1Btn")
        {
            person1 += 1;
            print("Updated player 1");
        }
        else if (btn.name == "p2Btn") 
        {
            person2 += 1;
            print("Updtated Player 2");
        }
        else if (btn.name == "p3Btn")
        {
            person3 += 1;
            print("Updated Player 3");
        }
        else
        {
            print("No button press detected"); 
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
        print("We're in the endgame now!");
        userChoice = "Greg"; 
        if (person1 <person2)
        {
            userChoice = "Phil"; 
            if (person2 < person3)
            {
                userChoice = "Tyrone"; 
            }
        }
        print("The user picked " + userChoice);
        result.text = "The person you said gave the best answer the most often is " + userChoice; 

        if (userChoice == "Greg")
        {
            feedback.text = "Greg is very professionally dressed but doesn't believe he has any weakness. There are always things that people can improve on. He does " +
                "meet the requirement of having a degree but doesn't know all of the languages that the job requires while there is another candidate that has more experience" +
                "in the languages that the employers require."; 
        }
        if (userChoice == "Phil")
        {
            feedback.text = "Out of the three candidates, Phil seems to meet most of the requirements for the position of entry-level software engineer. He has a bachelor's degree" +
                "in Computer Science and knows a majority of the programming languages that are required for the field. He is also working to improve his weaknesses and work" +
                "ethic.";
        }
        if (userChoice == "Tyrone")
        {
            feedback.text = "Tyrone doesn't have the best attire for a job interview. He is also missing a Bachelors degree that is needed for the job. By comparison there " +
                "are better candidates that were interviewed."; 
        }
    }
}
