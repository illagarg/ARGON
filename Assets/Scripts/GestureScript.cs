using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.MagicLeap;
using UnityEngine;

public class GestureScript : MonoBehaviour
{
    private GameObject cube; // Reference to our Cube
    private MLHandKeyPose[] gestures; // Holds the different hand poses we will look for
    private bool scaleFunc = false;
    private bool timeFunc = true;
    private bool large = false;
    private float prePos;
    private bool firstPos = true;

    // Start is called before the first frame update
    void Awake()
    {
        MLHands.Start(); // Start the hand tracking.
        gestures = new MLHandKeyPose[5]; //Assign the gestures we will look for.
        gestures[0] = MLHandKeyPose.Ok;
        gestures[1] = MLHandKeyPose.Fist;
        gestures[2] = MLHandKeyPose.OpenHandBack;
        gestures[3] = MLHandKeyPose.Finger;
        gestures[4] = MLHandKeyPose.Pinch;
        // Enable the hand poses.
        MLHands.KeyPoseManager.EnableKeyPoses(gestures, true, false);

        cube = GameObject.Find("Cube"); // Find our Cube in the scene.
        cube.SetActive(true);
    }

    private void OnDestroy()
    {
        MLHands.Stop();
    }

    bool GetGesture(MLHand hand, MLHandKeyPose type)
    {
        if (hand != null)
        {

            if (hand.KeyPose == type)
            {
                if (hand.KeyPoseConfidence > 0.9f)
                {
                    return true;
                }
            }

        }
        return false;
    }

    void MoveTimeBar(bool right)
    {
        if (right == true)
        {
            cube.transform.Translate(0.1f, 0, 0); //test
            //move time bar to the right
        }
        else
        {
            cube.transform.Translate(-0.1f, 0, 0); //test
            //move timr bar to the left
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scaleFunc == true)
        {
            if ((GetGesture(MLHands.Right, MLHandKeyPose.OpenHandBack) && GetGesture(MLHands.Left, MLHandKeyPose.Fist)) || (GetGesture(MLHands.Left, MLHandKeyPose.OpenHandBack) && GetGesture(MLHands.Right, MLHandKeyPose.Fist)))
            { }
            else if (large == false && (GetGesture(MLHands.Right, MLHandKeyPose.OpenHandBack) || GetGesture(MLHands.Left, MLHandKeyPose.OpenHandBack)))
            {
                cube.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
                large = true;

            }
            else if (large == true && (GetGesture(MLHands.Right, MLHandKeyPose.Fist) || GetGesture(MLHands.Left, MLHandKeyPose.Fist)))
            {
                cube.transform.localScale += new Vector3(-0.2f, -0.2f, -0.2f);
                large = false;

            }

        }
        if (timeFunc == true)
        {
            if (GetGesture(MLHands.Right, MLHandKeyPose.Pinch) && !GetGesture(MLHands.Left, MLHandKeyPose.Pinch))
            {
                if (firstPos == true)
                {
                    prePos = MLHands.Right.Index.KeyPoints[0].Position.x;
                    firstPos = false;
                }
                else
                {
                    if (MLHands.Right.Index.KeyPoints[0].Position.x - prePos > 0)
                    {
                        prePos = MLHands.Right.Index.KeyPoints[0].Position.x;
                        MoveTimeBar(true); //move to right
                    }
                    else if (MLHands.Right.Index.KeyPoints[0].Position.x - prePos < 0)
                    {
                        prePos = MLHands.Right.Index.KeyPoints[0].Position.x;
                        MoveTimeBar(false); //move to left
                    }
                }
            }
            else if (GetGesture(MLHands.Left, MLHandKeyPose.Pinch) && !GetGesture(MLHands.Right, MLHandKeyPose.Pinch))
            {
                if (firstPos == true)
                {
                    prePos = MLHands.Left.Index.KeyPoints[0].Position.x;
                    firstPos = false;
                }
                else
                {
                    if (MLHands.Left.Index.KeyPoints[0].Position.x - prePos > 0)
                    {
                        prePos = MLHands.Left.Index.KeyPoints[0].Position.x;
                        MoveTimeBar(true); //move to right
                    }
                    else if (MLHands.Left.Index.KeyPoints[0].Position.x - prePos < 0)
                    {
                        prePos = MLHands.Left.Index.KeyPoints[0].Position.x;
                        MoveTimeBar(false); //move to left
                    }
                }
            }
            else firstPos = true;
        }

    }


}