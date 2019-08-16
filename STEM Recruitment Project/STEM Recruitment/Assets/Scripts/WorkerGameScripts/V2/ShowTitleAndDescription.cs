using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTitleAndDescription : MonoBehaviour
{
    public Text feedback;
    public string workerDescription;

    private int timesHovered = 0;
    private Text title;

    // Start is called before the first frame update
    void Start()
    {
        title = this.GetComponentInChildren<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hand")
        {
            // The first time a player hovers over a worker, only the worker's title
            // appears. Title is permanently enabled afterwards.
            if(timesHovered == 0)
            {
                title.enabled = true;
                timesHovered++;
            }

            // If the title is already enabled, show the worker description.
            if(timesHovered == 1)
            {
                feedback.text = workerDescription;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Hand")
        {
        // Worker description disappears when hand leaves worker.
            if (timesHovered == 1)
            {
                feedback.text = "";
            }
        }
    }
}
