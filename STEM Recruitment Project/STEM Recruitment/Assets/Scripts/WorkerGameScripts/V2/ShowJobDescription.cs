using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowJobDescription : MonoBehaviour
{
    public GameObject bubble;

    Text jobDescription;
    string textToShow;
    private void Start()
    {
        bubble.SetActive(false);

        jobDescription = bubble.transform.Find("Text").GetComponent<Text>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hand")
        {
            Debug.Log("Hand collided with tab, calling DelayBubble()");
            StartCoroutine(DelayBubble());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Hand")
        {
            StopAllCoroutines();

            Debug.Log("DelayBubble stopped");

            bubble.SetActive(false);

            jobDescription.text = "";
        }
    }

    IEnumerator DelayBubble()
    {
        Debug.Log("DelayBubble called");

        yield return new WaitForSeconds(1);
        
        bubble.SetActive(true);

        jobDescription.enabled = true;

        jobDescription.text = textToShow;
    }

    public void ReceiveDescript(string newDescript)
    {
        textToShow = newDescript;
    }
}
