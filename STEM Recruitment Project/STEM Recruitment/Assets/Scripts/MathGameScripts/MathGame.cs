using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathGame : MonoBehaviour
{
    public static MathGame instance = null;

    public Text equationTxt, scoreText;
    public GameObject numPrefab, objectSpawner;

    private int level, ans, ansChosen, score;
    private bool ready = false;
    float[] positions = { -450, -150, 150, 450 };
    //Sprite[] numberSprites;

    IEnumerator runLevelCoroutine;
    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        //Load spritesheet
       // numberSprites = Resources.LoadAll<Sprite>("Images/MathGame/0thru13");

        /*for (int i = 0; i < numberSprites.Length; i++)
        {
            Debug.Log("Loaded " + numberSprites[i].name + " from spritesheet.");
        }
        */
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        ans = -1;
        ansChosen = -1;
        runLevelCoroutine = RunLevel();
    }
    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(runLevelCoroutine);
    }

    IEnumerator RunLevel()
    {
        ready = false;

        if (level == 0)
        {
            CreateEquation(1, 4);
        }
        else if(level == 1)
        {
            CreateEquation(1, 9);
        }
        else
        {
            CreateEquation(1, 19);
        }
        
        yield return new WaitForSeconds(6f);

        equationTxt.text = "";
        DestroySprites();
        ans = -1;
        score = 0;
    }

    // Create a random equation between a min and max number.
    void CreateEquation(int min, int max)
    {
        System.Random rand = new System.Random();
        int first = rand.Next(min, max);
        int second = rand.Next(min, max);

        ans = first + second;

        equationTxt.text = first.ToString() + " + " + second.ToString();

        LoadSprites(max);
    }

    // Load all sprites that will spawn at the top of the screen
    void LoadSprites(int max)
    {
        Debug.Log("Loading sprites");
        System.Random rand = new System.Random();

        int[] randArr = new int[4];
        
        // Place the answer randomly in the array.
        int randAnsIndex = rand.Next(0, 4); 
        randArr[randAnsIndex] = ans;

        int i = 0;

        // Randomly place wrong numbers in the rest of array.
        while(i < 4)
        {
            if(i != randAnsIndex)
            {
                int randWrongAns = rand.Next(1, max); // Get random wrong number

                if(randWrongAns != ans && !ArrayContainsInt(randArr, randWrongAns))
                {
                    randArr[i] = randWrongAns;

                    i++;

                } // end inner if
            }// end outer if
            else
            {
                i++; // If we reached to the wrong answer index, skip over it.
            }
        }// end while loop
        
        // Go through the randomized array and load the corresponding sprite.
        for(int j = 0; j < 4; j++)
        {
            int num = randArr[j];

            string spriteStr = num.ToString();

            Vector3 localPos = new Vector3(positions[j], (float)11.5, -9);

            //Sprite numSprite = numberSprites[num];
            Sprite numSprite = Resources.Load<Sprite>("Images/MathGame/" + spriteStr);

            GameObject currentObj = Instantiate(numPrefab);

            currentObj.name = "num" + j.ToString();
            currentObj.transform.SetParent(objectSpawner.transform, true);

            currentObj.GetComponent<SpriteRenderer>().sprite = numSprite;

            currentObj.transform.localPosition = localPos;

            currentObj.SendMessage("SetNum", num);

            Debug.Log("Loading sprite: " + numSprite.name + ", at position: " + localPos + 
                ", with name: " + currentObj.name);
        }
    }

    void DestroySprites()
    {
        for(int i = 0; i < 4; i++)
        {
            string path = "/Canvas/ObjectSpawner/num" + i.ToString();

            GameObject objToDestroy = GameObject.Find(path);

            if (objToDestroy != null)
            {
                GameObject.Destroy(GameObject.Find(path));
            }
           
        }
    }

    bool ArrayContainsInt(int[] arr, int num)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            if(arr[i] == num)
            {
                return true;
            }
        }

        return false;
    }

    public void PressAnswer(int userAns)
    {
        StopCoroutine(runLevelCoroutine);

        DestroySprites();

        if (userAns == ans)
        {
            score += 10;
        }
        else
        {
            score -= 10;
        }

        scoreText.text = "Score: " + score.ToString();

        StartCoroutine(StartGame());
    }
    
}
