using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowJobDescription : MonoBehaviour
{
    public GameObject bubble;

    private void Start()
    {
        // bubble = this.transform.Find("Bubble").gameObject;
        bubble.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hand")
        {
            Debug.Log("Hand collided with box");
            bubble.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Hand")
        {
            Debug.Log("Hand left box");
            bubble.SetActive(false);
        }
    }
}
