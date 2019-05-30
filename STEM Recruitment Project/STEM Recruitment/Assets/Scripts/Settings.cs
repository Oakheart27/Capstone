using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Button quitBtn;
    public Button lowBtn, medBtn, highBtn;
    private int sensVal;
    private int newSensVal;
    // Start is called before the first frame update
    void Start()
    {
        // Deactivate and hide quit button.
        quitBtn.interactable = false;
        quitBtn.gameObject.SetActive(false);

        // Save previous sensitivity value.
        SensitivitySettings sensSet = GetComponent<SensitivitySettings>();

        sensVal = sensSet.GetSensitivityVal();
    }

    // Update is called once per frame
    void Update()
    {
        // Save previous sensitivity value.
        SensitivitySettings sensSet = GetComponent<SensitivitySettings>();

        newSensVal = sensSet.GetSensitivityVal();

        // If the user changed the sensitivity, show the quit button.
        if (sensVal != newSensVal)
        {
            quitBtn.interactable = true;
            quitBtn.gameObject.SetActive(true);
        }

        HideCurrentSensSetting();
    }

    public void HideCurrentSensSetting()
    {
        if(sensVal == 4)
        {
            lowBtn.interactable = false;
        }

        if (sensVal == 3)
        {
            medBtn.interactable = false;
        }

        if (sensVal == 1)
        {
            highBtn.interactable = false;
        }
    }
    // Maybe move to different file? Here temporarily.
    public void QuitGame()
    {
        Application.Quit();
    }

}
