using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickByHovering : MonoBehaviour
{
    private Button myButton;
    private string originalButtonText;
    private int timeLeft; 

    // Call this method on EventTrigger -> PointerEnter.
    public void clickButton(Button thisButton)
    {
        myButton = thisButton;
        
        StartCoroutine(wait(3)); 

        timeLeft = 3;

        originalButtonText = myButton.GetComponentInChildren<TextMeshProUGUI>().text;

        StartCoroutine(showTime());
    }

    // Method waits for 3 seconds then invokes a click.
    IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
      
        myButton.onClick.Invoke();
    }
    
    // Method adds countdown to button text
    IEnumerator showTime()
    {
        while(timeLeft>=0)
        {
            Debug.Log(timeLeft);

            myButton.GetComponentInChildren<TextMeshProUGUI>().text = originalButtonText + " (" + timeLeft + ")";

            yield return new WaitForSeconds(1);

            timeLeft--;
        }
    }

    // Stops everything and changes button text to original. Call when pointer exits button.
    public void stop()
    {
        StopAllCoroutines();
        myButton.GetComponentInChildren<TextMeshProUGUI>().text = originalButtonText;
    }
}
   
