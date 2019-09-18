﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleWorker : MonoBehaviour
{
    public GameObject rightHand, leftHand;

    private bool isFilled = false;
    private bool readyToPlace = false;
    private bool readyToRemove = false;
    private bool blocked = false;
    private GameObject worker = null; //worker in chair
    private GameObject newWorker = null; //temporary worker that touched chair
    private GameObject placementCube;
    private GameObject workerScreen;
    void Start()
    {
        // Get the cube, so I know where to place workers.
        placementCube = this.transform.Find("Cube").gameObject;

        // Get full worker screen
        workerScreen = GameObject.Find("/WholeGame/WorkerScreen");  
    }

    private void Update()
    {
        if(readyToPlace && UserLetGo())
        {
            PutWorkerInChair(newWorker);

            readyToPlace = false;

            Debug.Log(this.name + " has " + worker.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Worker")
        {
            newWorker = other.gameObject;

            readyToPlace = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Worker")
        {
            WorkerLeftChair(other.gameObject);
        }
    }

    void PutWorkerInChair(GameObject workerObj)
    {
        Debug.Log("PutWorkerInChair called.");

        // If the chair is filled, kick previous worker out.
        if(isFilled && (workerObj != worker) && !blocked)
        {
            Debug.Log("Kicking out " + worker.name);

            workerScreen.SendMessage("DeleteWorker", worker);

            worker.SendMessage("SendBack", true); // Send previous worker back

            BlockAllOtherChairs(true);
        }
        
        Debug.Log("Placing worker " + workerObj.name + " in " + this.name);

        workerScreen.SendMessage("AddWorker", workerObj);

        workerObj.transform.position = placementCube.transform.position;

        worker = workerObj;

        newWorker = null;

        isFilled = true;
    }

    void WorkerLeftChair(GameObject workerObj)
    {
        if(isFilled && workerObj == worker)
        {
            isFilled = false;

            worker = null;

            workerScreen.SendMessage("DeleteWorker", workerObj);
        }
    }

    bool UserLetGo()
    {
        if (rightHand.transform.position.x - leftHand.transform.position.x > 350)
        {
            return true;
        }

        else if ((rightHand.transform.position.y - leftHand.transform.position.y > 350) ||
                (leftHand.transform.position.y - rightHand.transform.position.y > 350))
        {
            return true;
        }

        return false;
    }

    void BlockAllOtherChairs(bool status)
    {
        for(int i = 1; i <= 3; i++)
        {
            string otherChairStr = "Chair" + i.ToString();

            GameObject otherChair = GameObject.Find("/WholeGame/WorkerScreen/" + otherChairStr);

            if(otherChair != this.gameObject)
            {
                otherChair.SendMessage("BlockThisChair", status);
            }
        }
    }

    public void BlockThisChair(bool status)
    {
        blocked = status;
    }
}
