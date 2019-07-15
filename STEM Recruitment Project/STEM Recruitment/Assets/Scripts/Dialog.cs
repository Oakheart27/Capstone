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
    public string[] gregAns;
    public string[] lisaAns;
    public string[] tyroneAns;
    public string[] userFeedback;
    public string[] gScore1, gScore2, gScore3;
    public string[] lScore1, lScore2, lScore3;
    public string[] tScore1, tScore2, tScore3;
    public string[] noscore; 
    public string[] gfeed, lfeed, tfeed; 

    private int index = 0;
    public int counter = 1;
    public int anscount = 0; // determines what index of answers arrays to display 
    public int ans;
    public int totalcount = 0; // keeps track of how many questions have been answered 
    public float typingSpeed; // determines the speed of text apperance for questions 
    public int person1, person2, person3;
    public int final1, final2, final3;
    public int winner;
    public int temp = 1; 

    // Objects in Unity
    public GameObject continueBtn;
    public GameObject choiceBtn;
    public GameObject p1Btn;
    public GameObject p2Btn;
    public GameObject p3Btn;
    public GameObject gregsp, lisasp, tyronesp; 
    public GameObject resultsBtn;
    public GameObject p1, p2;
    public GameObject jobdescription, summary;
    public GameObject jobPanel, returnPanel;
    public GameObject returnBtn, feedBtn;
    public GameObject score1, score2, score3;

    public Text textDisplay; // Questions
    public Text gregR, lisaR, tyroneR; //  Responses of interviewies 
    public Text crit; 
    public Text result; // Displays who the user chose and summary of interviewies. 9286079754
    public Text feedback; // Dispalys developer feedback on user choice 
    public Text userReturn;
    
    //private string continueBtnStr = "Continue";
    // Start is called before the first frame update
    void Start()
    {
        // disables character name buttons
        p1Btn.GetComponent<Button>().interactable = false;
        p2Btn.GetComponent<Button>().interactable = false;
        p3Btn.GetComponent<Button>().interactable = false;

        returnBtn.GetComponent<Button>().interactable = false;
        resultsBtn.SetActive(false);

        // hides speech from characters
        gregR.enabled = false;
        lisaR.enabled = false;
        tyroneR.enabled = false;

        // hides feedback of character on feedback panel 
        crit.enabled = false;

        // hides speech bubble image
        gregsp.SetActive(false); 
        lisasp.SetActive(false);
        tyronesp.SetActive(false);

        feedBtn.GetComponent<Button>().interactable = false;

        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        //feedBtn.GetComponent<Button>().interactable = false;
        // Check if text displayed is current sentence index
        displayScore(); 
        if (textDisplay.text == sentences[index])
        {
            if (sentences[index] == "Choose a Person" || sentences[index] == "Choose who you would hire after looking at the summary above.") // checks if prompting for question 
            {
                continueBtn.GetComponent<Button>().interactable = false; // hides continue button
                // allows name buttons to be ineractable for choice 
                p1Btn.GetComponent<Button>().interactable = true;
                p2Btn.GetComponent<Button>().interactable = true;
                p3Btn.GetComponent<Button>().interactable = true;
            }
            else
            {
                continueBtn.SetActive(true);
                continueBtn.GetComponent<Button>().interactable = true;

                // name buttons uninteractable with user when not prompting person choice
                p1Btn.GetComponent<Button>().interactable = false;
                p2Btn.GetComponent<Button>().interactable = false;
                p3Btn.GetComponent<Button>().interactable = false;

                p1Btn.GetComponentInChildren<Text>().text = "Greg";
                p2Btn.GetComponentInChildren<Text>().text = "Lisa";
                p3Btn.GetComponentInChildren<Text>().text = "Tyrone";
            }

            if (sentences[index] == "Choose who you would hire after looking at the summary above.")
            {
                jobdescription.SetActive(false); 
                summary.SetActive(true);
                summary.GetComponentInChildren<Text>().text = "Greg answeered " + person1 + " correct, Lisa answered " + person2 + " correct, and Tyrone answered " + person3 + " correct.";

            }

            int temp = counter-1;

            if (temp != counter)
            {
                //Debug.Log("Temp: " + temp + " Counter: " + counter);
                continueBtn.GetComponent<Button>().interactable = true;
            }

            if (anscount == totalcount)
            {
                feedBtn.GetComponent<Button>().interactable = false; 
                //totalcount =+ 1; 
            }

            p1Btn.SetActive(true);
            p2Btn.SetActive(true);
            p3Btn.SetActive(true);

            if (sentences[index] == "Thank you for your time. Click on the results button.")
            {
                continueBtn.SetActive(false);
                continueBtn.GetComponent<Button>().interactable = false;
                resultsBtn.SetActive(true);
                endGame(); 
            }

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
        // Checks if user is choosing the person to hire
        if (sentences[index] == "Choose who you would hire then click the results button.")
        {
            if (btn.name == "p1Btn")
            {
                final1 += 10;
                Debug.Log("Updated player 1");
            }
            else if (btn.name == "p2Btn")
            {
                final2 += 10;
                Debug.Log("Updated Player 2");
            }
            else if (btn.name == "p3Btn")
            {
                final3 += 10;
                Debug.Log("Updated Player 3");
            }
            else
            {
                Debug.Log("No button press detected");
            }
        }
        // Checks if choosing who answered question best
        else {

            winner = 0; 

            if (btn.name == "p1Btn")
            {
                person1 += 1;
                winner += 5; 
                Debug.Log("Updated player 1");
             //   totalcount += 1;
                feedBtn.GetComponent<Button>().interactable = true;
                p1Btn.GetComponent<Button>().interactable = false;
                p2Btn.GetComponent<Button>().interactable = false;
                p3Btn.GetComponent<Button>().interactable = false;
            }

            else if (btn.name == "p2Btn")
            {
                person2 += 1;
                winner += 10; 
                Debug.Log("Updated Player 2");
            //    totalcount += 1;
                feedBtn.GetComponent<Button>().interactable = true;
                p1Btn.GetComponent<Button>().interactable = false;
                p2Btn.GetComponent<Button>().interactable = false;
                p3Btn.GetComponent<Button>().interactable = false;
            }
            else if (btn.name == "p3Btn")
            {
                person3 += 1;
                winner += 15; 
                Debug.Log("Updated Player 3");
            //    totalcount += 1;
                feedBtn.GetComponent<Button>().interactable = true;
                p1Btn.GetComponent<Button>().interactable = false;
                p2Btn.GetComponent<Button>().interactable = false;
                p3Btn.GetComponent<Button>().interactable = false;
            }
            else
            {
                Debug.Log("No button press detected");
            }
        }
        
        counter += 1;
        continueBtn.SetActive(true);
        continueBtn.GetComponent<Button>().interactable = true; // after choice made, returns continue button to screen
        
        p1Btn.GetComponentInChildren<Text>().text = "Greg";
        p2Btn.GetComponentInChildren<Text>().text = "Lisa";
        p3Btn.GetComponentInChildren<Text>().text = "Tyrone";

        p1Btn.GetComponent<Button>().interactable = false;
        p2Btn.GetComponent<Button>().interactable = false;
        p3Btn.GetComponent<Button>().interactable = false;

        p1Btn.SetActive(false);
        p2Btn.SetActive(false);
        p3Btn.SetActive(false);
        anscount += 1;
        temp = anscount - 1; 
        Debug.Log(person1 + ", " +  person2 + ", " + person3);

    }

    public void gregOver()
    {
        gregR.enabled = true; //enables response to answer 
        gregsp.SetActive(true); // hides speech bubble image
        Debug.Log(anscount);
        if (anscount -1 > 7) { anscount = 7; } // Stops array out of bounds error
        gregR.GetComponent<Text>().text = gregAns[anscount - 1];
        /*

        if (btn.name == "greg")
        {
            Debug.Log("In Greg");
            gregR.enabled = true;
            gregR.GetComponent<Text>().text = "bombskill";
            Debug.Log("HELLOW IM DONE");
        }
        if (btn.name == "lisa")
        {
            lisaR.enabled = true;
            lisaR.GetComponent<Text>().text = "HiIII";
        }
        if (btn.name == "tyrone")
        {
            tyroneR.enabled = true;
            tyroneR.GetComponent<Text>().text = "bro";
        }
        Debug.Log("MouseOver"); */

    }

    public void lisaOver()
    {
        lisaR.enabled = true;
        lisasp.SetActive(true);
        if (anscount - 1 > 7) { anscount = 7; } // Stops array out of bounds error
        lisaR.GetComponent<Text>().text = lisaAns[anscount -1]; 
    }

    public void tyroneOver()
    {
        tyroneR.enabled = true;
        tyronesp.SetActive(true);
        if (anscount -1 > 7) { anscount = 7; } // Stops array out of bounds error
        tyroneR.GetComponent<Text>().text = tyroneAns[anscount - 1]; 
    }

    public void Response()
    {
        if (anscount - 2 > 7) { anscount = 7; } // stops array out of bounds error 
        Debug.Log("Ans Response Value " + (anscount - 2)); 
        if (winner == 5)
        {
            crit.enabled = true;
            crit.GetComponent<Text>().text = gfeed[anscount -2];
        }
        else if (winner == 10)
        {
            crit.enabled = true;
            crit.GetComponent<Text>().text = lfeed[anscount - 2];
        }

        else if (winner == 15)
        {
            crit.enabled = true;
            crit.GetComponent<Text>().text = tfeed[anscount - 2];
        }
    }

    // removes critisizim text from screen 
    public void endfeed()
    {
        crit.enabled = false;
        crit.enabled = false;
        crit.enabled = false;
       // totalcount = +1;
    }

    public void mouseLeave()
    {
        // hides speech bubble image
        gregsp.SetActive(false); 
        lisasp.SetActive(false);
        tyronesp.SetActive(false);      
    }

   public void hidePannels()
    {
        returnPanel.SetActive(false);
        jobPanel.SetActive(false);
        returnBtn.GetComponent<Button>().interactable = false;
    }

    public void readJob()
    {
        returnPanel.SetActive(false);
        jobPanel.SetActive(true);
        returnBtn.GetComponent<Button>().interactable = true;
    }

    public void feedbackInfo()
    {
        jobPanel.SetActive(false); 
        returnPanel.SetActive(true);
        if (anscount - 1 > 7) { anscount = 7; } // Stops array out of bounds error
        userReturn.GetComponent<Text>().text = userFeedback[anscount - 1];
        returnBtn.GetComponent<Button>().interactable = true;
    }

    public void displayScore()
    {
        summary.GetComponentInChildren<Text>().text = "Greg answeered " + person1 + " correct, Lisa answered " + person2 + " correct, and Tyrone answered " + person3 + " correct.";
        if (anscount-2 >7) { temp = 7; }
        if (winner == 5)
        {
            Debug.Log("temp " + temp); 
            score1.GetComponentInChildren<Text>().text = gScore1[anscount - 2];
            score2.GetComponentInChildren<Text>().text = gScore2[anscount - 2];
            score3.GetComponentInChildren<Text>().text = gScore3[anscount - 2];
        }

        else if (winner == 10)
        {
            score1.GetComponentInChildren<Text>().text = lScore1[anscount - 2];
            score2.GetComponentInChildren<Text>().text = lScore2[anscount - 2];
            score3.GetComponentInChildren<Text>().text = lScore3[anscount - 2];
        }

        else if (winner == 15)
        {
            score1.GetComponentInChildren<Text>().text = tScore1[anscount - 2];
            score2.GetComponentInChildren<Text>().text = tScore2[anscount - 2];
            score3.GetComponentInChildren<Text>().text = tScore3[anscount - 2];
        }

        else
        {
            score1.GetComponentInChildren<Text>().text = noscore[0];
            score2.GetComponentInChildren<Text>().text = noscore[0];
            score3.GetComponentInChildren<Text>().text = noscore[0];
        }
    }

    //TODO Change endGame conditions to allow user to choice final hire. 
    public void endGame()
    {
        Debug.Log("We're in the endgame now!");
        userChoice = "Greg";
        if (final2 > final1 && final2 > final3)
        {
            userChoice = "Lisa";
        }

        if (final3 > final1 && final3 > final2)
        {
            userChoice = "Tyrone";
        }
        print("The user picked " + userChoice);
        result.text = "The person you said gave the best answer the most often is " + userChoice;

        if (userChoice == "Greg")
        {
            feedback.text = "Greg is very professionally dressed. He did take advantage of providing skills he has in the tell me about yourself sectiion. He doesn't have the " +
                " programming knowledge for the job.";
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