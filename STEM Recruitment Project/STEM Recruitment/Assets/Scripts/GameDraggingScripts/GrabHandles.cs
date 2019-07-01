
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHandles : MonoBehaviour
{
    //public Material greenMaterial, blackMaterial;
    private Material currentMaterial, otherMat;
    public GameObject otherSeg;

    public void Start()
    {
        currentMaterial = this.GetComponent<Renderer>().material;
        currentMaterial.color = Color.black;

        otherMat = otherSeg.GetComponent<Renderer>().material;
        otherMat.color = Color.black;
    }
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Hand")
        {
            currentMaterial.color = Color.green;
            otherMat.color = Color.green;
        }

    }

    public void OnCollisionExit(Collision collision)
    {
        currentMaterial.color = Color.black;
        otherMat.color = Color.black;
    }
}