using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenV2 : MonoBehaviour
{
    public float speed = 0.0f;
    public float workerScreenX, thisScreenX;
    public Camera camera;

    private RectTransform rectComponent;
    private Image imageComp;
    private bool okToChange = false;
    private float min, max;
    private float t = 0.0f;
    private float y, z;

    // Use this for initialization
    void Start()
    {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;

        y = camera.transform.position.y;
        z = camera.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(camera.transform.position.x == thisScreenX)
        {
            StartCoroutine(GoBack());

            if (imageComp.fillAmount != 1f)
            {
                imageComp.fillAmount = imageComp.fillAmount + Time.deltaTime * speed;

            }

            else
            {
                imageComp.fillAmount = 0.0f;

            }
        }

        if(okToChange)
        {
            // animate the position of the game object...
            camera.transform.position = new Vector3(Mathf.Lerp(min, max, t), y, z);

            // .. and increase the t interpolater
            t += 0.5f * Time.deltaTime;

            if (t > 1.0f)
            {
                t = 0.0f;

                okToChange = false;

                Debug.Log("Camera changed to: " + camera.transform.position);
            }
        }
        
        
    }

    IEnumerator GoBack()
    {
        yield return new WaitForSeconds(5);

        min = thisScreenX;

        max = workerScreenX;

        okToChange = true;

    }
}
