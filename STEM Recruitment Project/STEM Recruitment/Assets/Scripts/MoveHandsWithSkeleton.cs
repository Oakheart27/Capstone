using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandsWithSkeleton : MonoBehaviour
{
    public nuitrack.JointType[] typeJoint;
    GameObject[] CreatedJoint;
    public GameObject rightHand, leftHand;
    //public Canvas canvas; 
    public Camera camera;
    public float ZPosition;
    public float multiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        NuitrackManager.DepthSensor.SetMirror(true);
    }

    private void OnDestroy()
    {
        NuitrackManager.DepthSensor.SetMirror(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentUserTracker.CurrentUser != 0)
        {
            nuitrack.Skeleton skeleton = CurrentUserTracker.CurrentSkeleton;

            // Get right hand stuff
            nuitrack.Joint rightJoint = skeleton.GetJoint(typeJoint[0]);
            Vector3 rightPos = rightJoint.ToVector3();
            Vector3 rightNewPos = new Vector3((rightPos.x + camera.transform.position.x) * multiplier, (rightPos.y + camera.transform.position.y) * multiplier, ZPosition);
            //rightNewPos.z = 275;
            rightHand.transform.position = rightNewPos;

            // Get left hand stuff
            nuitrack.Joint leftJoint = skeleton.GetJoint(typeJoint[1]);
            Vector3 leftPos = leftJoint.ToVector3();
            Vector3 leftNewPos = new Vector3((leftPos.x + camera.transform.position.x) * multiplier, (leftPos.y + camera.transform.position.y) * multiplier, ZPosition);
            //leftNewPos.z = 275;
            leftHand.transform.position = leftNewPos;

            //Debug.Log("Right: " + rightHand.transform.position + ", Left: " + leftHand.transform.position);
        }
        else
        {
            //Debug.Log("User not found");
        }

    }
}
