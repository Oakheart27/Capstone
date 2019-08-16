using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerLeftChair : MonoBehaviour
{
    public GameObject tableTop;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Worker")
        {
            tableTop.SendMessage("RemoveWorkerFromChair", other.gameObject);
        }
    }
}
