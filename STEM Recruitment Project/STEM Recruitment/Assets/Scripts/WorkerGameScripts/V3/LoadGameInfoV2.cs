﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameInfoV2 : MonoBehaviour
{
    public GameObject bossCanvas, workerCanvas, loadingCanvas;
    public GameObject[] allWorkers;

    private Job jobInfo;
    private Worker[] allWorkerInfo;
    private int[] randomArray;
    private int randArrIndex = 0;
    private int numOfJobs;
    private string[] txtFileInfo;

    // Start is called before the first frame update
    void Start()
    {

        allWorkerInfo = new Worker[allWorkers.Length];

        string path = "ProjectGameInfo/"; // Full directory: Resources/ProjectGameInfo

        TextAsset[] allJobs = Resources.LoadAll<TextAsset>(path); // Get all files in directory

        numOfJobs = allJobs.Length; // Count number of available files to pick from

        randomArray = new int[numOfJobs];

        RandomizeArray();

        StartParsingFile();
    }
    
    void StartParsingFile()
    {

        string path = "ProjectGameInfo/";

        string jobStr = "Project" + randomArray[randArrIndex].ToString();
        
        // Load chosen project file
        TextAsset txtFile = Resources.Load<TextAsset>(path + jobStr);

        txtFileInfo = txtFile.text.Split(';');

        Debug.Log("Loaded " + jobStr);

        // Get rid of all newlines EXCEPT for job description - I'm allowing newlines here.
        for (int i = 0; i < txtFileInfo.Length; i++)
        {
            if (i != 2)
            {
                txtFileInfo[i] = txtFileInfo[i].Replace("\n", string.Empty);
            }

        }
        Debug.Log("Length: " + txtFileInfo.Length);
        
        OrganizeProjectInfo();

        //OrganizeWorkers(txtFileInfo);

        //SendJobDescription();

        //SendWorkersToScreen();
        
    }
    
    public void SendWorkerScreenInfo()
    {
        workerCanvas.SetActive(true);

        bossCanvas.SetActive(false);

        OrganizeWorkers();

        SendJobDescription();

        SendWorkersToScreen();
    }

    // Goes through the randomized array. Once array is complete, completely restarts scene. 
    public void RestartScene()
    {
        if (randArrIndex == numOfJobs - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            randArrIndex++;
            StartParsingFile();
        }
    }

    // Takes all available jobs, randomly sorts them in an array. This is to make sure that the
    // user does not get the same job twice in a row.
    void RandomizeArray()
    {
        System.Random rand = new System.Random();

        int i = 0;
        while (i != numOfJobs)
        {
            int randFile = rand.Next(1, numOfJobs + 1);

            if (!randomArray.Contains(randFile))
            {
                Debug.Log("Adding Project" + randFile + " to index " + i);
                randomArray[i] = randFile;

                i++;
            }
        }
    }

    // Organize all of the information for the project (job description & boss blurbs) & send to boss screen
    void OrganizeProjectInfo()
    {
        // Split up the boss blurb by ':'
        string[] bossBlurb = txtFileInfo[3].Split(':');

        // Add all info of job into job node
        jobInfo = new Job(txtFileInfo[1], txtFileInfo[2], bossBlurb);
        
        // Send the bossBlurb to canvas
        bossCanvas.SendMessage("ReceiveDialog", bossBlurb);

        // Send the job title to canvas
        bossCanvas.SendMessage("ReceiveTitle", txtFileInfo[1]);

        Debug.Log("Done organizing project info");
    } // end OrganizeProjectInfo()

    // Organize worker information and add to worker array
    void OrganizeWorkers()
    {
        int currentIndex = 6;

        int arrayIndex = 0;

        // Go through each worker and organize information
        for (int i = 1; i < 7; i++)
        {
            int id = i;
            string title = txtFileInfo[currentIndex + 1];
            string description = txtFileInfo[currentIndex + 2];
            bool status;

            // the worker's correctness is assigned with YES or NO
            if (WorkerIsCorrect(txtFileInfo[currentIndex + 3]))
            {
                status = true;
            }
            else
            {
                status = false;
            }

            string feedback = txtFileInfo[currentIndex + 4];

            Worker newWorker = new Worker(id, title, description, feedback, status);

            allWorkerInfo[arrayIndex] = newWorker;

            arrayIndex++;

            currentIndex += 5;
        }
    } // end OrganizeWorkers()

    // Sends job description to worker screen canvas
    void SendJobDescription()
    {
        GameObject jobDescriptTab = GameObject.Find("/WorkerCanvas/WorkerScreen/Canvas/JobDescriptTab").gameObject;

        jobDescriptTab.SendMessage("ReceiveDescript", jobInfo.GetDescript());

        Debug.Log(jobInfo.GetDescript());
    }

    void SendWorkersToScreen()
    {
        System.Random rand = new System.Random();

        Worker[] shuffledArr = new Worker[allWorkerInfo.Length];

        // Randomly sort info list
        int i = 0;
        while (i != allWorkerInfo.Length)
        {
            int randomIndex = rand.Next(0, allWorkerInfo.Length);

            if (shuffledArr.Contains(allWorkerInfo[randomIndex]))
            {
                continue;
            }
            else
            {
                shuffledArr[i] = allWorkerInfo[randomIndex];
                i++;
            }
        }

        // Use new random list to assign all workers with
        for (int j = 0; j < allWorkers.Length; j++)
        {
            GameObject thisWorker = allWorkers[j].gameObject.transform.Find("Canvas").gameObject;
            thisWorker.SendMessage("ReceiveTitle", shuffledArr[j].GetTitle());
            thisWorker.SendMessage("ReceiveDescription", shuffledArr[j].GetDescript());
            thisWorker.SendMessage("ReceiveFeedback", shuffledArr[j].GetFeedback());
            thisWorker.SendMessage("ReceiveStatus", shuffledArr[j].IsCorrect());

        }
    }

    // CHANGE LATER if I figure out why string comparison functions aren't working
    bool WorkerIsCorrect(string str)
    {
        char[] temp = new char[str.Length];

        using (StringReader sr = new StringReader(str))
        {
            sr.Read(temp, 0, str.Length);

            if (temp[1] == 'Y' || temp[1] == 'y')
            {
                return true;
            }
            else if (temp[1] == 'N' || temp[1] == 'n')
            {
                return false;
            }
            else
            {
                Debug.Log("ERROR! Yes/No has not been detected - " + temp[1]);
                return false;
            }
        }
    }
    // Job node for boss screen and job description panel
    public class Job
    {
        string title, description;
        string[] bossBlurbs;

        public Job(string newTitle, string newDescript, string[] newBlurbs)
        {
            title = newTitle;
            description = newDescript;
            bossBlurbs = newBlurbs;
        }

        public string GetTitle()
        {
            return title;
        }

        public string GetDescript()
        {
            return description;
        }

        public string[] GetBlurbs()
        {
            return bossBlurbs;
        }
    } // end Job node

    // Worker node for each worker's title, skill description, feedback, and right/wrong
    public class Worker
    {
        int id;
        string title, description, feedback;
        bool isCorrect;

        public Worker(int newID, string newTitle, string newDescript, string newFB, bool status)
        {
            id = newID;
            title = newTitle;
            description = newDescript;
            feedback = newFB;
            isCorrect = status;
        }

        public string GetTitle()
        {
            return title;
        }

        public string GetDescript()
        {
            return description;
        }

        public string GetFeedback()
        {
            return feedback;
        }

        public bool IsCorrect()
        {
            return isCorrect;
        }
    } // end Worker node
}
