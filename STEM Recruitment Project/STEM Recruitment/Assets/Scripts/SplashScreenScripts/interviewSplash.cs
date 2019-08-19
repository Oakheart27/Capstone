using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class interviewSplash : MonoBehaviour
{
    public Text jobdes, questions;
    public string newScene;

    IEnumerator Start()
    {
        jobdes.canvasRenderer.SetAlpha(0.0f);
        questions.canvasRenderer.SetAlpha(0.0f);


        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(newScene);
    }

    void FadeIn()
    {
        jobdes.CrossFadeAlpha(1.0f, 1.5f, false);
        questions.CrossFadeAlpha(1.0f, 1.5f, false);

    }
    void FadeOut()
    {
        jobdes.CrossFadeAlpha(0.0f, 2.5f, false);
        questions.CrossFadeAlpha(0.0f, 2.5f, false);

    }
}
