using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddToScore : MonoBehaviour
{
    // Need to get from Editor
    public GameObject[] images;
    public GameObject[] outlines;
    
    public TextMeshPro feedback;

    // Private lists to keep track of everything
    private MyObject[] myImages;

    // Other needed values.
    private int score;
    private int scoreVal = 5;
    private int numOfItems;
    private RigidbodyConstraints constraints;

    // Start is called before the first frame update
    void Start()
    {
        feedback.text = "Score: 0";

        numOfItems = images.Length;

        Debug.Log("Num of items: " + numOfItems);
        
        // Go through all images.
        for(int i = 0; i < numOfItems; i++)
        {
            // Create new cusstom object & find position of image
            MyObject newObj = new MyObject();
            Vector3 newPos = images[i].transform.position;

            // Add all attributes to new object
            newObj.setObject(images[i]);
            newObj.setInitPos(newPos);
            newObj.setFirstTime(true);

            myImages[i] = newObj;
        }
        
        
        //firstTimes = new bool[numOfItems];
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < numOfItems; i++)
        {
            if(images[i].transform.position != myImages[i].getPos() && myImages[i].isFirstTime())
            {
                if(images[i].transform.position == outlines[i].transform.position)
                {
                    // Give feedback
                    feedback.text = "You did it!";

                    // Set current and answer to the same position.
                    this.transform.position = outlines[i].transform.position;

                    // Freeze current position
                    constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;

                    // Set answer's outline to green.
                    outlines[i].GetComponent<Outline>().effectColor = Color.green;

                    myImages[i].setFirstTime(false);
                    //Debug.Log("Current: " + currentPos + ", Answer: " + answerPos);
                }
            }
        }
    }

}

public class MyObject : MonoBehaviour
{
    public GameObject myObject;
    public Vector3 objectPos;
    public bool firstTime;

    public void setObject(GameObject newObj)
    {
        myObject = newObj;
    }

    public GameObject getObject()
    {
        return myObject;
    }

    public void setInitPos(Vector3 pos)
    {
        objectPos = pos;
    }

    public Vector3 getPos()
    {
        return objectPos;
    }

    public void setFirstTime(bool status)
    {
        firstTime = status;
    }

    public bool isFirstTime()
    {
        return firstTime;
    }
    
}
