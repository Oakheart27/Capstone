using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ScaleScene : MonoBehaviour
{
    public Camera cam;
    public GameObject rightHand, leftHand, star;
    public GameObject[] otherObjectsNotOnCanvas;
    
    private float[] allOtherSizes;
    private float originalHandSize, originalStarSize, originalCamSize;

    // Start is called before the first frame update
    void Start()
    {
        originalHandSize = rightHand.transform.localScale.x;
        originalStarSize = star.transform.localScale.x;
        originalCamSize = cam.orthographicSize;

        string filePath = GetFilePath();

        try
        {
            StreamReader reader = File.OpenText(filePath);

            string infoStr = reader.ReadLine(); // Should only be 1 line

            string[] strList = infoStr.Split(','); // Split line by commas

            reader.Close();

            float[] floatList = new float[strList.Length]; // array to hold all values

            for(int i = 0; i < strList.Length; i++)
            {
                floatList[i] = float.Parse(strList[i]); // convert each string into float
            }

            float newHandSize = rightHand.transform.localScale.x * floatList[1];

            float newStarSize = star.transform.localScale.x * floatList[1];

            cam.orthographicSize = floatList[0];
            
            rightHand.transform.localScale = new Vector3(newHandSize, newHandSize, newHandSize);

            leftHand.transform.localScale = new Vector3(newHandSize, newHandSize, newHandSize);

            star.transform.localScale = new Vector3(newStarSize, newStarSize, newStarSize);
            
        }

        catch(Exception e)
        {
            Debug.Log("Could not read/write settings file. ");
            Debug.Log(e);
        }
    }

    // TEMPORARY - I'm worried that the scale will continuously decrease, so I'm resetting 
    // everything when the scene is destroyed.
    private void OnDestroy()
    {
        rightHand.transform.localScale = new Vector3(originalHandSize, originalHandSize, originalHandSize);
        leftHand.transform.localScale = new Vector3(originalHandSize, originalHandSize, originalHandSize);
        star.transform.localScale = new Vector3(originalStarSize, originalStarSize, originalStarSize);
        cam.orthographicSize = originalCamSize;
    }

    string GetFilePath()
    {
        if (Application.isEditor)
        {
            return Application.dataPath + "/Plugins/camera_size.txt";
        }
        else
        {
            return Application.persistentDataPath + "/camera_size.txt";
        }
    }
}
