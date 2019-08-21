using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillChairsV2 : MonoBehaviour
{
    /// <summary>
    /// Designed to be placed on the top of the table. Each chair needs 
    /// to be labeled as "Chair#", starting from 1. 
    /// </summary>
     
    public int numberOfChairs;
    public GameObject workerScreenScripts;
    public Text feedback;
    public Button nextButton;

    private Chair[] chairList;

    // Start is called before the first frame update
    void Start()
    {
        chairList = new Chair[numberOfChairs];

        // Find all chairs in the WorkerScreen game object.
        for(int i = 1; i <= numberOfChairs; i++)
        {
            string chairStr = "/WholeGame/WorkerScreen/Chair" + i.ToString();

            GameObject newChairObj = GameObject.Find(chairStr);

            GameObject newCube = newChairObj.transform.Find("Cube").gameObject;

            // Add chair and cube to the chair list. Worker's default is null.
            Chair newChair = new Chair(newChairObj, newCube);

            chairList[i - 1] = newChair;
            
        }

       // nextButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(GetNextAvailableChair() == -1)
        {
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            nextButton.gameObject.SetActive(false);
        }
    }

    // Since this script is attached to table top, OnTriggerEnter will trigger once worker touches
    // the table top.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Worker")
        {
            int availableChair = GetNextAvailableChair();

            if(availableChair == -1)
            {
                Debug.Log("All chairs are filled!");

                feedback.text = "All chairs are filled! Either drag a worker out or reset the entire table.";

                workerScreenScripts.SendMessage("ResetWorker", other.gameObject);
            }

            else
            {
                other.transform.position = chairList[availableChair].cube.transform.position;

                chairList[availableChair].worker = other.gameObject;
            }
        }
    }
    
    public void RemoveWorkerFromChair(GameObject workerToRemove)
    {
        for(int i = 0; i < numberOfChairs; i++)
        {
            if(chairList[i].worker == workerToRemove)
            {
                chairList[i].worker = null;
                break;
            }
        }
    }

    private int GetNextAvailableChair()
    {
        for (int i = 0; i < numberOfChairs; i++)
        {
            //Debug.Log("Chair " + i + " status: " + chairs[i].isFilled);
            if (chairList[i].worker == null)
            {
                //Debug.Log("Returning chair " + i);
                return i;
            }
        }

        return -1;
    }
    
    // Sends entire chair list to CheckWorkersV2.
    public void SendChairsToCheck(GameObject scripts)
    {
        for(int i = 0; i < numberOfChairs; i++)
        {
            scripts.SendMessage("ReceiveChair", chairList[i].chair);
            scripts.SendMessage("ReceiveWorker", chairList[i].worker);
            scripts.SendMessage("ReceiveCube", chairList[i].cube);
            scripts.SendMessage("IncreaseIndex", 1);
        }

        scripts.SendMessage("OkToCheck", true);
    }

    public class Chair
    {
        public GameObject chair, cube, worker;

        public Chair(GameObject newChair, GameObject newCube, GameObject newWorker = null)
        {
            chair = newChair;
            cube = newCube;
            worker = newWorker;
        }
        
    }
}
