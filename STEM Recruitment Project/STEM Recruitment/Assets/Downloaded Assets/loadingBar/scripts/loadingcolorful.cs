using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingcolorful : MonoBehaviour {

    private RectTransform rectComponent;
    private Image imageComp;
    public float speed = 0.0f;
    public Vector3 newPosition, currentPosition;
    public Camera camera;
    private bool okToChange = true;
    // Use this for initialization
    void Start () {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if(camera.transform.position == currentPosition)
        {
            if(okToChange)
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
            
        }
        
    }

    IEnumerator GoBack()
    {
        yield return new WaitForSeconds(5);

        camera.transform.position = newPosition;

        okToChange = false;

    }
}
