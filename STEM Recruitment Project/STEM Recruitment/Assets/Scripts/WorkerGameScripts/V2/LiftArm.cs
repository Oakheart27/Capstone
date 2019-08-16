using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftArm : MonoBehaviour
{
    private GameObject up, down;
    // Start is called before the first frame update
    void Start()
    {
        up = this.gameObject.transform.Find("Up").gameObject;
        down = this.gameObject.transform.Find("Down").gameObject;

        up.SetActive(false);
    }
    // Move arm up.
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Hand")
        {
            down.SetActive(false);
            up.SetActive(true);
        }
    }
    
}
