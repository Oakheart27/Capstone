using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckWorkersV2 : MonoBehaviour
{
    public int numberOfChairs;
    public Text feedback;
    //public GameObject chairList;

    private Chair[] chairList;
    private int chairIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        chairList = new Chair[numberOfChairs];
    }
    
    public IEnumerator OkToCheck(bool ok)
    {
        yield return new WaitForSeconds(2.0f);

        if(ok)
        {
            for(int i = 0; i < numberOfChairs; i++)
            {
                GameObject feedbackPic = chairList[i].worker.gameObject.transform.Find("FeedbackPic").gameObject;

                feedbackPic.SetActive(true);
            }
        }
    }

    public void ReceiveChair(GameObject newChair)
    {
        chairList[chairIndex].chair = newChair;
        Debug.Log("Received " + newChair.name);
    }

    public void ReceiveWorker(GameObject newWorker)
    {
        chairList[chairIndex].worker = newWorker;
        Debug.Log("Received " + newWorker.name);
    }

    public void ReceiveCube(GameObject newCube)
    {
        chairList[chairIndex].cube = newCube;
        Debug.Log("Received " + newCube.name);
    }
    
    public void IncreaseIndex(int i)
    {
        chairIndex += i;
    }
    

    public class Chair
    {
        public GameObject chair, cube, worker;

        public Chair(GameObject newChair = null, GameObject newCube = null, GameObject newWorker = null)
        {
            chair = newChair;
            cube = newCube;
            worker = newWorker;
        }

    }
}
