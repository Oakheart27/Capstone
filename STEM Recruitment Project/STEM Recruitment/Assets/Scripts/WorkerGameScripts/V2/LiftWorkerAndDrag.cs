using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftWorkerAndDrag : MonoBehaviour
{
    public GameObject rightHand, leftHand;
    private GameObject workerRightArm, workerLeftArm;
    private bool isFalling;
    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        workerRightArm = this.transform.Find("RightArm").gameObject;
        workerLeftArm = this.transform.Find("LeftArm").gameObject;
        isFalling = false;

        this.transform.Find("FeedbackPic").gameObject.SetActive(false);
        originalPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if(isFalling)
        {
            this.transform.Translate(Vector3.down * 100.0f * Time.deltaTime, Space.World);
        }

        if(ArmGrabbed(workerRightArm) && ArmGrabbed(workerLeftArm))
        {
            //using the position of the right and left hands to move the whole object
            Vector3 pos1 = leftHand.transform.position;
            Vector3 pos2 = rightHand.transform.position;

            Vector3 midPoint = (pos2 - pos1) / 2;

            this.transform.position = midPoint + pos1;

            if (UserLetGo())
            {
                LetArmGo(workerRightArm);
                LetArmGo(workerLeftArm);
                isFalling = true;
            }
        }
        
        if(ArmGrabbed(workerRightArm) && !ArmGrabbed(workerLeftArm))
        {
            LetArmGoAfterTime(workerRightArm, workerLeftArm);
        }

        if (ArmGrabbed(workerLeftArm) && !ArmGrabbed(workerRightArm))
        {
            LetArmGoAfterTime(workerLeftArm, workerRightArm);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Table")
        {
            isFalling = false;
        }
        
        if (other.gameObject.tag == "Floor")
        {
            isFalling = false;
            this.transform.position = originalPos;
        }
    }
    
    private bool ArmGrabbed(GameObject arm)
    {
        GameObject upArm = arm.transform.Find("Up").gameObject;

        if(upArm.activeSelf)
        {
            return true;
        }
        return false;
    }

    private void LetArmGo(GameObject arm)
    {
        GameObject upArm, downArm;
        upArm = arm.transform.Find("Up").gameObject;
        downArm = arm.transform.Find("Down").gameObject;

        upArm.SetActive(false);
        downArm.SetActive(true);
    }

    IEnumerator LetArmGoAfterTime(GameObject arm1, GameObject arm2)
    {
        GameObject upArm1, downArm1, upArm2;

        upArm1 = arm1.transform.Find("Up").gameObject;
        downArm1 = arm1.transform.Find("Down").gameObject;

        upArm2 = arm2.transform.Find("Up").gameObject;

        yield return new WaitForSeconds(2);

        if(upArm1.activeSelf == true && upArm2.activeSelf == false)
        {
            upArm1.SetActive(false);
            downArm1.SetActive(true);
        }
    }

    bool UserLetGo()
    {
        if (rightHand.transform.position.x - leftHand.transform.position.x > 350)
        {
            // Debug.Log("DragWithHandlebars -> User let go with distance at " + 
            //     (rightHand.transform.position.x - leftHand.transform.position.x));
            return true;
        }
        else if ((rightHand.transform.position.y - leftHand.transform.position.y > 350) ||
                (leftHand.transform.position.y - rightHand.transform.position.y > 350))
        {
            //Debug.Log("DragWithHandlebars -> User let go with distance at " +
            //    (rightHand.transform.position.y - leftHand.transform.position.y));
            return true;
        }

        return false;
    }
}
