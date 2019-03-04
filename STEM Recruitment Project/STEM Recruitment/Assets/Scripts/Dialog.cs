using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    // Intsance variables 
    public string[] sentences;
    public string[] followup; 
    private int index;
    public int counter;  
    public float typingSpeed;
    public int person1, person2, person3; 

    // Objects in Unity 
    public GameObject continueBtn;
    public GameObject choiceBtn;
    public GameObject p1, p2; 

    //public Button p1Btn;
    //public Button p2Btn;
    //public Button p3Btn;
    public Text textDisplay;

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
            counter += 1; 
            continueBtn.SetActive(true);

        }

        if (counter == size)
        {
            print("Inside the if");
            p1.SetActive(false);
            p2.SetActive(false); 
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

        Debug.Log(person1 + ", " +  person2 + ", " + person3);
    }
}
