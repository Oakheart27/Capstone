using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCollider : MonoBehaviour
{
    int num = 0;
    GameObject gameScripts;
    bool active = true;
    private void Awake()
    {
        gameScripts = GameObject.Find("/GameScripts");    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!active) return;

        active = false;

        gameScripts.SendMessage("PressAnswer", num);

        Destroy(gameObject);
    }

    public void SetNum(int newNum)
    {
        num = newNum;
    }
}
