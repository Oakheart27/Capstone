using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CheckWorkers : MonoBehaviour
{
    public GameObject[] CorrectWorkers, IncorrectWorkers;
    public Text feedback;
    private Vector3[] incorrectInitPositions;

    private void Start()
    {
        for(int i = 0; i < IncorrectWorkers.Length; i++)
        {
            Debug.Log(IncorrectWorkers[i] + " position: " + IncorrectWorkers[i].transform.position);
            //incorrectInitPositions[i] = IncorrectWorkers[i].transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Worker")
        {
            if(IncorrectWorkers.Contains(collision.gameObject))
            {
                feedback.text = "Wrong!";

                // Set the game object's position to the corresponding vector position inincorrectInitPositions.
                collision.gameObject.transform.position = incorrectInitPositions[getItemIndex(collision.gameObject, IncorrectWorkers)];
            }
            else if(CorrectWorkers.Contains(collision.gameObject))
            {
                feedback.text = "Correct!";
            }
            else
            {
                Debug.Log("Item not in correct or incorrect lists");
            }
        }
    }

    private int getItemIndex(GameObject item, GameObject[] list)
    {
        for(int i = 0; i < list.Length; i++)
        {
            if(list[i] == item)
            {
                return i;
            }
        }
        return -1;
    }
}
