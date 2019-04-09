using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MathGameProgress : MonoBehaviour
{
    public static MathGameProgress instance = null;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text responseText;

    [SerializeField]
    Text currentScore;

    int firstNum;
    int secondNum;
    int ans;
    int givenAns;
    int score = 0;
    string strAns;

    string[] myObjectsNames = new string[] { "button_score_1(Clone)",
                                             "button_score_2(Clone)",
                                             "button_score_3(Clone)",
                                             "button_score_4(Clone)"};

    private void Start()
    {
        CreateEquation();
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void CreateEquation()
    {
        RandomEquation();
        scoreText.text = firstNum.ToString() + "+" + secondNum.ToString() + "=";
    }

    void RandomEquation()
    {
        firstNum = Random.Range(0, 3);
        secondNum = Random.Range(0, 3);
        ans = firstNum + secondNum;
        if (ans == 0)
        {
            RandomEquation();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text += strAns;
        if (givenAns == ans)
        {

            responseText.text = "CORRECT";
            score += 5;
            currentScore.text = "Your Score: " + score.ToString();
            StartCoroutine(TimeStop());
            //responseText.text = "";
            //CreateEquation();

        }
        else
        {
            responseText.text = "INCORRECT, NEXT";
            score -= 5;
            currentScore.text = "Your Score: " + score.ToString();
            StartCoroutine(TimeStop());
            //responseText.text = "";
            //CreateEquation();
        }

    }

    public void AddNumber(string strVal, int intVal)
    {
        strAns = strVal;
        givenAns = intVal;
        UpdateScoreText();

    }

    IEnumerator TimeStop()
    {
        yield return new WaitForSeconds(3f);
        DeleteAll();
        responseText.text = "";
        CreateEquation();
    }

    public void DeleteAll()
    {
        foreach (string name in myObjectsNames)
        {
            GameObject go = GameObject.Find(name);
            //if the tree exist then destroy it
            if (go)
                Destroy(go.gameObject);
        }
    }

}
