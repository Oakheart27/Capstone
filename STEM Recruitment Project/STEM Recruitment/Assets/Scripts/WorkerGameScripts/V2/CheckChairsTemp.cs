using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckChairsTemp : MonoBehaviour
{
    public Camera cam;
    public GameObject[] chairs;
    public GameObject[] allWorkers;
    public Text feedbackText;

    private bool okToCheck = false;
    private bool wrongAnsPresent = false;
    private bool reset = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(okToCheck)
        {
            for(int i = 0; i < chairs.Length; i++)
            {
                GameObject chairCube = chairs[i].transform.Find("Cube").gameObject;

                Vector3 cubePos = chairCube.transform.position;

                for(int j = 0; j < allWorkers.Length;  j++)
                {
                    if(allWorkers[j].transform.position == cubePos)
                    {
                        GameObject feedback = allWorkers[j].transform.Find("FeedbackPic").gameObject;

                        feedback.SetActive(true);

                        if(feedback.GetComponent<SpriteRenderer>().sprite.name == "Wrong")
                        {
                            wrongAnsPresent = true;
                        }
                        
                        break;
                    }
                }
                
            }

            okToCheck = false;
        }

        if(wrongAnsPresent)
        {
            feedbackText.text = "Oops! Some of your answers were wrong. Press the reset button to try again.";
            
        }

        if(reset)
        {
            for (int i = 0; i < chairs.Length; i++)
            {
                GameObject chairCube = chairs[i].transform.Find("Cube").gameObject;

                Vector3 cubePos = chairCube.transform.position;

                for (int j = 0; j < allWorkers.Length; j++)
                {
                    if (allWorkers[j].transform.position == cubePos)
                    {
                        GameObject feedback = allWorkers[j].transform.Find("FeedbackPic").gameObject;

                        feedback.SetActive(false);
                        
                        break;
                    }
                }

            }

            reset = false;
        }
    }

    public void ReadyToCheck()
    {
        okToCheck = true;
        reset = true;
    }

    public void Reset()
    {
        if(wrongAnsPresent)
        {
            wrongAnsPresent = false;

            feedbackText.text = "";
        }
    }
}
