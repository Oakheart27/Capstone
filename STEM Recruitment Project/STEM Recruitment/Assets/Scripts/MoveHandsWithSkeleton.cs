using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandsWithSkeleton : MonoBehaviour
{
    public nuitrack.JointType[] typeJoint;
    GameObject[] CreatedJoint;
    public GameObject rightHand, leftHand;
    public Canvas canvas; 
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
            Vector3 rightNewPos = rightJoint.ToVector3() + canvas.transform.position;
            rightNewPos.z = -21;
            rightHand.transform.position = rightNewPos;

            // Get left hand stuff
            nuitrack.Joint leftJoint = skeleton.GetJoint(typeJoint[1]);
            Vector3 leftNewPos = leftJoint.ToVector3() + canvas.transform.position;
            leftNewPos.z = -21;
            leftHand.transform.position = leftNewPos;

            //Debug.Log("Right: " + rightHand.transform.position + ", Left: " + leftHand.transform.position);
        }
        else
        {
            //Debug.Log("User not found");
        }

    }
}
