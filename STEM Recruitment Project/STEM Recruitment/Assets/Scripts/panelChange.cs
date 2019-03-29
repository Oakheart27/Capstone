using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class panelChange : MonoBehaviour
{
    public GameObject gamePanel;
    public GameObject choicePanel;
    public GameObject resultPanel; 
    //public GameObject backBtn;
    int test = 1; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changePanel()
    {
        gamePanel.gameObject.SetActive(false);
        choicePanel.gameObject.SetActive(true);
    }
    
    public void backPanel()
    {
        gamePanel.gameObject.SetActive(true);
        choicePanel.gameObject.SetActive(false); 
    }

    public void endPanel()
    {
        gamePanel.gameObject.SetActive(false);
        resultPanel.gameObject.SetActive(true);
    }

    public void gameReturn()
    {
        gamePanel.gameObject.SetActive(true);
        resultPanel.gameObject.SetActive(false);
    }
}
