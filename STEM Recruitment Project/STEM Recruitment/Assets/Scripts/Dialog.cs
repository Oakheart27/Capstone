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
    public int clickcnt = 0;
    public int cClick = 0; 
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
    public GameObject jobdescription; 
    public GameObject jobPanel, scorePanel;
    public GameObject feedBtn;
    public GameObject score1, score2, score3;

    public Text textDisplay; // Questions
    public Text gregR, lisaR, tyroneR; //  Responses of interviewies 
    public Text crit; // responses to the answers for each interview question 
    public Text result; // Displays who the user chose and summary of interviewies. 9286079754
    public Text feedback, feedFinal; // Dispalys developer feedback on user choice 
    //public Text userReturn;
    public Text proconSum, proconFinal; // lists the pros and cons of each character 
    
    //private string continueBtnStr = "Continue";
    // Start is called before the first frame update
    void Start()
    {
        // disables character name buttons
        p1Btn.GetComponent<Button>().interactable = false;
        p2Btn.GetComponent<Button>().interactable = false;
        p3Btn.GetComponent<Button>().interactable = false;

        //returnBtn.GetComponent<Button>().interactable = false;
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
       // if (textDisplay.text == sentences[index])
        //{
            if (sentences[index] == "Choose who you would hire after looking at the summary above.") // checks if prompting for question 
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
                p1Btn.GetComponent<Button>().interactable = true;
                p2Btn.GetComponent<Button>().interactable = true;
                p3Btn.GetComponent<Button>().interactable = true;

                Debug.Log("CLICK count: " + clickcnt);
                Debug.Log("Continue CLICK: " + cClick); 
                p1Btn.GetComponentInChildren<Text>().text = "Greg";
                p2Btn.GetComponentInChildren<Text>().text = "Lisa";
                p3Btn.GetComponentInChildren<Text>().text = "Tyrone";
            }
            /*
            if (sentences[index] == "Choose who you would hire.")
            {
                jobdescription.SetActive(false); 
                //summary.SetActive(true);
                //summary.GetComponentInChildren<Text>().text = "Greg answeered " + person1 + " correct, Lisa answered " + person2 + " correct, and Tyrone answered " + person3 + " correct.";

            }*/

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

            /*
            p1Btn.SetActive(true);
            p2Btn.SetActive(true);
            p3Btn.SetActive(true);
            */
            if (sentences[index] == "Thank you for your time. Click on the results button.")
            {
                continueBtn.SetActive(false);
                continueBtn.GetComponent<Button>().interactable = false;
                resultsBtn.SetActive(true);
                endGame(); 
            }

        }
    //}

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

    public void continueClick()
    {
        cClick += 1; 
    }

    /*
    public void finalCount(Button btn)
    {
        Button currentbtn = btn; 
        while (sentences[index] != "Thank you for your time. Click on the results button.")
        {
            ButtonClick(currentbtn); 
        }
        if (sentences[index] == "Thank you for your time. Click on the results button.")
            {
                if (btn.name == "p1Btn")
                {
                    final1 += 10;
                    Debug.Log("Updated player 1 FINAL");
                }
                else if (btn.name == "p2Btn")
                {
                    final2 += 10;
                    Debug.Log("Updated Player 2 FINAL");
                }
                else if (btn.name == "p3Btn")
                {
                    final3 += 10;
                    Debug.Log("Updated Player 3 FINAL");
                }
                else
                {
                    Debug.Log("No button press detected FINAL");
                }
                Debug.Log(" Count of FINAL: Final1 " + final1 + " final2 " + final2 + " final3 " + final3);
            }

    }*/

    public void ButtonClick(Button btn)
    {
        // Checks if user is choosing the person to hire

        if (sentences[index] == "Choose who you would hire.")
        {
            if (btn.name == "p1Btn")
            {
                final1 += 10;
                Debug.Log("Updated player 1 FINAL");
            }
            else if (btn.name == "p2Btn")
            {
                final2 += 10;
                Debug.Log("Updated Player 2 FINAL");
            }
            else if (btn.name == "p3Btn")
            {
                final3 += 10;
                Debug.Log("Updated Player 3 FINAL");
            }
            else
            {
                Debug.Log("No button press detected FINAL");
            }
            Debug.Log(" Count of FINAL: Final1 " + final1 + " final2 " + final2 + " final3 " + final3);
        }
        // Checks if choosing who answered question best
        else
        {
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
            clickcnt += 1;
        }
        
        counter += 1;
        continueBtn.SetActive(true);
        continueBtn.GetComponent<Button>().interactable = true; // after choice made, returns continue button to screen
        
        p1Btn.GetComponentInChildren<Text>().text = "Greg";
        p2Btn.GetComponentInChildren<Text>().text = "Lisa";
        p3Btn.GetComponentInChildren<Text>().text = "Tyrone";

        /*
        p1Btn.GetComponent<Button>().interactable = false;
        p2Btn.GetComponent<Button>().interactable = false;
        p3Btn.GetComponent<Button>().interactable = false;

        p1Btn.SetActive(false);
        p2Btn.SetActive(false);
        p3Btn.SetActive(false);*/
        anscount += 1;
        temp = anscount - 1; 
        Debug.Log(person1 + ", " +  person2 + ", " + person3);

    }

    // determines what happens when the mouse moves over characters 
    public void gregOver()
    {
        gregR.enabled = true; //enables response to answer 
        gregsp.SetActive(true); // hides speech bubble image
        Debug.Log(anscount);
        if (cClick > 2) { cClick = 2; } // Stops array out of bounds error
        gregR.GetComponent<Text>().text = gregAns[cClick];
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
        if (cClick > 2) { cClick = 2; } // Stops array out of bounds error
        lisaR.GetComponent<Text>().text = lisaAns[cClick]; 
    }

    public void tyroneOver()
    {
        tyroneR.enabled = true;
        tyronesp.SetActive(true);
        if (cClick > 2) { cClick = 2; } // Stops array out of bounds error
        tyroneR.GetComponent<Text>().text = tyroneAns[cClick]; 
    }

    public void Response()
    {
        if (cClick > 2) { cClick = 2; } // stops array out of bounds error 
        Debug.Log("Ans Response Value " + (cClick)); 
        if (winner == 5)
        {
            crit.enabled = true;
            crit.GetComponent<Text>().text = gfeed[cClick];
        }
        else if (winner == 10)
        {
            crit.enabled = true;
            crit.GetComponent<Text>().text = lfeed[cClick];
        }

        else if (winner == 15)
        {
            crit.enabled = true;
            crit.GetComponent<Text>().text = tfeed[cClick];
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
       // finalPanel.SetActive(false);
        jobPanel.SetActive(false);
        scorePanel.SetActive(false); 
    }

    public void readJob()
    {
        //finalPanel.SetActive(false);
        jobPanel.SetActive(true);
        scorePanel.SetActive(false); 
        //returnBtn.GetComponent<Button>().interactable = true;
    }

    public void seeScore()
    {
        jobPanel.SetActive(false);
        scorePanel.SetActive(true); 
    }

    public void feedbackInfo()
    {
        jobPanel.SetActive(false); 
        //finalPanel.SetActive(true);
        scorePanel.SetActive(true); 
        if (anscount - 1 > 2) { anscount = 2; } // Stops array out of bounds error
        //userReturn.GetComponent<Text>().text = userFeedback[anscount - 1];
        //returnBtn.GetComponent<Button>().interactable = true;
    }

    public void displayScore()
    {
        //summary.GetComponentInChildren<Text>().text = "Greg answeered " + person1 + " correct, Lisa answered " + person2 + " correct, and Tyrone answered " + person3 + " correct.";
        if (cClick >2) { cClick = 2; }
        if (winner == 5)
        {
            Debug.Log("temp " + temp); 
            score1.GetComponentInChildren<Text>().text = gScore1[cClick];
            score2.GetComponentInChildren<Text>().text = gScore2[cClick];
            score3.GetComponentInChildren<Text>().text = gScore3[cClick];
        }

        else if (winner == 10)
        {
            score1.GetComponentInChildren<Text>().text = lScore1[cClick];
            score2.GetComponentInChildren<Text>().text = lScore2[cClick];
            score3.GetComponentInChildren<Text>().text = lScore3[cClick];
        }

        else if (winner == 15)
        {
            score1.GetComponentInChildren<Text>().text = tScore1[cClick];
            score2.GetComponentInChildren<Text>().text = tScore2[cClick];
            score3.GetComponentInChildren<Text>().text = tScore3[cClick];
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
        Debug.Log("Final1 " + final1 + " final2 " + final2 + " final3 " + final3); 

        if (final1 == 0 && final2 == 0 && final3 == 0)
        {
            Debug.Log("Failure");
            userChoice = "ERROR"; 
        }

        if (final1 > final2 && final1 > final3)
        {
            userChoice = "Greg";
            Debug.Log("GREG"); 
        }

        if (final2 > final1 && final2 > final3)
        {
            userChoice = "Lisa";
            Debug.Log("LISA");
        }

        if (final3 > final1 && final3 > final2)
        {
            userChoice = "Tyrone";
            Debug.Log("TYRONE");
        }

        print("The user picked " + userChoice);
        result.text = "The person you chose is " + userChoice;

        if (userChoice == "Greg")
        {
            feedback.text = "Greg is honest and enthusiastic, but not very experienced! The candidate takes half the summer to learn the proper techniques. Since you " +
                "had to take the time to get this person up to speed, you don’t end up having the time to apply for a big grant you had been planning on. Oh well, there’s always next year!";
            proconSum.text = "Greg is enthusiastic (+)" + "\n" // new line character 
                + "inexperienced will need a LOT of supervision to become a fully contributing member of the field team (-)"; 
        }
        if (userChoice == "Lisa")
        {
            feedback.text = "Lisa turns out to be a bit full of themselves. They seem to learn quickly, freeing you up to apply for that big grant to fund more wildlife projects. " +
                "But - you discover at the end of the summer that the data they’ve been collecting is inaccurate. You figure out how to fix the errors, but it costs you valuable planning " + 
                "time for other projects!";
            proconSum.text = "Lisa is confident (+)" + "\n" + "but not self - reflective enough to check their work for accuracy - leading to data collection mistakes(-)";
        }
        if (userChoice == "Tyrone")
        {
            feedback.text = "Tyrone is enthusiastic and capable of learning what they don't know - and quick! They take on every project thoughtfully throughout the whole summer." + 
                "Unfortunately, Tyrone doesn't get along well with two other more experienced team members. You are hoping this person will come back for another summer before " + 
                "they graduate, but are unsure if the team dynamics will work against productivity next time around!";
            proconSum.text = "Tyrone is enthusiastic" + "\n" + "confident(but not too much so)" + "\n" + "quick to learn(+)" + "\n" + "does not get along well with the team(-)"; 
        }
    }

    public void finalGreg()
    {
        feedFinal.text = "Greg is honest and enthusiastic, but not very experienced! The candidate takes half the summer to learn the proper techniques. Since you " +
                "had to take the time to get this person up to speed, you don't end up having the time to apply for a big grant you had been planning on. Oh well, there’s always next year!";
        proconFinal.text = "Greg is enthusiastic (+)" + "\n" // new line character 
            + "inexperienced will need a LOT of supervision to become a fully contributing member of the field team (-)";
    }

    public void finalLisa()
    {
        feedFinal.text = "Lisa turns out to be a bit full of themselves. They seem to learn quickly, freeing you up to apply for that big grant to fund more wildlife projects. " +
                "But - you discover at the end of the summer that the data they’ve been collecting is inaccurate. You figure out how to fix the errors, but it costs you valuable planning " +
                "time for other projects!";
        proconFinal.text = "Lisa is confident (+)" + "\n" + "but not self - reflective enough to check their work for accuracy - leading to data collection mistakes(-)";
    }
   
    public void finalTyrone()
    {
        feedFinal.text = "Tyrone is enthusiastic and capable of learning what they don't know - and quick! They take on every project thoughtfully throughout the whole summer." +
                "Unfortunately, Tyrone doesn't get along well with two other more experienced team members. You are hoping this person will come back for another summer before " +
                "they graduate, but are unsure if the team dynamics will work against productivity next time around!";
        proconFinal.text = "Tyrone is enthusiastic" + "\n" + "confident(but not too much so)" + "\n" + "quick to learn(+)" + "\n" + "does not get along well with the team(-)";

    }

    public void newChoice()
    {
        /*
        final1 = 0;
        final2 = 0;
        final3 = 0;
        sentences[index] = "Choose who you would hire.";
        textDisplay.text = sentences[index];
        //Update();
        resultsBtn.SetActive(true);*/
    }
}