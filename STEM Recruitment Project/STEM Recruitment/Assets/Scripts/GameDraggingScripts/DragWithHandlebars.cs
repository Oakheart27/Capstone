using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragWithHandlebars : MonoBehaviour
{
    public GameObject leftHand, rightHand, leftBar, rightBar, outline;
    //public Text scoreText;
    //private int score;
    private Vector3 originalPosition, outlinePos;
    private GameObject cube;
    private RigidbodyConstraints constraints;
    private bool isCorrect;
    
    void Start()
    {
        originalPosition = this.transform.position;
        cube = this.transform.Find("Cube").gameObject;
        outlinePos = outline.transform.position;

        isCorrect = false;
        
        //score = int.Parse(scoreText.text);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = this.transform.position;

        if(isCorrect)
        {
            // Move the picture to its outline
            this.transform.position = outlinePos;

            // Freeze the picture
            constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            
            // Turn bars to black to prevent user from moving the picture from the outline.
            turnToBlack(rightBar);
            turnToBlack(leftBar);
        }

        // If the picture is not being grabbed and it's not in its correct position, reset
        // the picture to its original position.
        else
        {
            this.transform.position = originalPosition;
        }

        // If we're in the range of the picture's outline
        if (currentPos.x <= outlinePos.x + 50 || currentPos.x <= outlinePos.x - 50)
        {
            if (currentPos.y < outlinePos.y + 50 || currentPos.y <= outlinePos.y - 50)
            {
                isCorrect = true;

                //Debug.Log(this.name + " CORRECT");
                
            }

        }

        if (isGrabbed(rightBar) && isGrabbed(leftBar))
        {
            //using the position of the right and left hands to move the whole object
            Vector3 pos1 = leftHand.transform.position;
            Vector3 pos2 = rightHand.transform.position;
            
            Vector3 midPoint = (pos2 - pos1)/2;

            this.transform.position = midPoint + pos1;
        }

    }
    // Method determines if a bar is being grabbed if at least one segment of the bar is green.
    // Class GrabHandles changes segments from black to green.
    bool isGrabbed(GameObject bar)
    {
        GameObject top, bottom, side;
        Material topMat, bottomMat, sideMat;

        top = bar.transform.Find("Top").gameObject;
        topMat = top.GetComponent<Renderer>().material;

        bottom = bar.transform.Find("Bottom").gameObject;
        bottomMat = bottom.GetComponent<Renderer>().material;

        side = bar.transform.Find("Side").gameObject;
        sideMat = side.GetComponent<Renderer>().material;
        
        if (topMat.color == Color.green || 
            bottomMat.color == Color.green || 
            sideMat.color == Color.green)
        {
            return true;
        }

        return false;
    }

    // Resets all segments to black to prevent player from moving object.
    void turnToBlack(GameObject bar)
    {
        GameObject top, bottom, side;
        Material topMat, bottomMat, sideMat;

        top = bar.transform.Find("Top").gameObject;
        topMat = top.GetComponent<Renderer>().material;

        bottom = bar.transform.Find("Bottom").gameObject;
        bottomMat = bottom.GetComponent<Renderer>().material;

        side = bar.transform.Find("Side").gameObject;
        sideMat = side.GetComponent<Renderer>().material;

        topMat.color = Color.black;
        bottomMat.color = Color.black;
        sideMat.color = Color.black; 
    }
    
}
