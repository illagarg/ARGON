
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.MagicLeap;
using UnityEngine;
using UnityEngine.UI;

public class GestureScript : MonoBehaviour
{
    //private GameObject cube; // Reference to our Cube
    private float value = 0;
    public GameObject controller;
    private bool large;

    private MLHandKeyPose[] gestures; // Holds the different hand poses we will look for
    private string mode;

    private MLHandKeyPose prevGesture;
    private MLHands prevSide;

    private Vector3 camPrevPos;
    private bool firstPos = true;


    private Vector3[] pos; //new
    private float margin = 0.08f;  //new
    //private int selectime = 5; //new, time chosed, 0-4 means finger(from thumb to pinkie), 5 means no choose
    //public GameObject sphereThumb, sphereIndex, sphereMiddle, sphereRing, spherePinkie;  //new




    // Start is called before the first frame update
    void Awake()
    {
        MLHands.Start(); // Start the hand tracking.
        gestures = new MLHandKeyPose[6]; //Assign the gestures we will look for.
        gestures[0] = MLHandKeyPose.Ok;
        gestures[1] = MLHandKeyPose.Fist;
        gestures[2] = MLHandKeyPose.OpenHandBack;
        gestures[3] = MLHandKeyPose.Finger;
        gestures[4] = MLHandKeyPose.Pinch;
        gestures[5] = MLHandKeyPose.L;
        // Enable the hand poses.
        MLHands.KeyPoseManager.EnableKeyPoses(gestures, true, false);

        //cube = GameObject.Find("Cube"); // Find our Cube in the scene.
        //cube.SetActive(true);
        mode = "scale";
        camPrevPos = this.transform.position;

        pos = new Vector3[6]; //new
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

    //void DisplayTime(Vector3 posThumb, Vector3 posIndex, Vector3 posMiddle, Vector3 posRing, Vector3 posPinkie)
    void DisplayTime()//new
    {
        //posThumb, posIndex, posMiddle, posRing, and posPinkie are positions of fingers, from thumb to pinkie
        //以下测试用，sphereThumb, sphereIndex, sphereMiddle, sphereRing, spherePinkie放小球在四个手指上，一定注意把小球挂在script上，否则报错！实际应用中如果有时间的模型同理。
        /*
        sphereThumb.transform.position = posThumb; //大拇指位置是posThumb，放最早的年份
        sphereIndex.transform.position = posIndex; //食指
        sphereMiddle.transform.position = posMiddle; //中指
        sphereRing.transform.position = posRing; //无名指
        spherePinkie.transform.position = posPinkie; //小拇指位置是posPinkie，放最晚的年份
        */
        controller.GetComponent<ControllerScript>().displayTime(pos);
    }


    void HideTime() //new
    {
        controller.GetComponent<ControllerScript>().hideTime();
    }

    void ShowTime() {
        controller.GetComponent<ControllerScript>().showTime();
    }


    void CheckGaze(Vector3 posThumb, Vector3 posIndex, Vector3 posMiddle, Vector3 posRing, Vector3 posPinkie)  //new, check if gaze at fingers, etc:gazing at Thumb, change the selectime to 0
    {
        //func of gaze
        //check the gaze point with the input points
        //posThumb, posIndex, posMiddle, posRing, and posPinkie are positions of fingers, from thumb to pinkie
        //if it near posThumb, change the selectime to 0
        //if it near posIndex, change the selectime to 1
        //if it near posMiddle, change the selectime to 2
        //if it near posRing, change the selectime to 3
        //if it near posPinkie, change the selectime to 4

    }

    // Update is called once per frame
    void Update()
    {
        //print(mode);
        //print(large);

        if (GetGesture(MLHands.Right, MLHandKeyPose.L) || GetGesture(MLHands.Left, MLHandKeyPose.L)) {
            controller.GetComponent<ControllerScript>().reposition(this.transform.position - camPrevPos);
        }

        else {  // Not making an "L" gesture

            if (mode == "Normal")
            {
                large &= (!GetGesture(MLHands.Right, MLHandKeyPose.NoHand) || !GetGesture(MLHands.Left, MLHandKeyPose.NoHand));

                if ((GetGesture(MLHands.Right, MLHandKeyPose.OpenHandBack) && GetGesture(MLHands.Left, MLHandKeyPose.Fist)) || (GetGesture(MLHands.Left, MLHandKeyPose.OpenHandBack) && GetGesture(MLHands.Right, MLHandKeyPose.Fist)))
                { }



                else if (!large && (GetGesture(MLHands.Right, MLHandKeyPose.OpenHandBack) || GetGesture(MLHands.Left, MLHandKeyPose.OpenHandBack)))
                {
                    // Open menu
                    controller.GetComponent<ControllerScript>().processEvent("open");
                    large = true;

                }
                else if (large && (GetGesture(MLHands.Right, MLHandKeyPose.Fist) || GetGesture(MLHands.Left, MLHandKeyPose.Fist)))
                {
                    //print("Closed hand");
                    // Close menus
                    controller.GetComponent<ControllerScript>().processEvent("close");
                    large = false;
                }

            }





            else if (mode == "BackDate")  //new
            {

                // 识别右手
                if (GetGesture(MLHands.Right, MLHandKeyPose.OpenHandBack) && !GetGesture(MLHands.Left, MLHandKeyPose.OpenHandBack)) //right hand operation
                {
                    ShowTime();
                    //pos[0] = MLHands.Right.Thumb.KeyPoints[0].Position; //大拇指
                    //pos[1] = MLHands.Right.Index.KeyPoints[0].Position; //食指
                    //pos[5] = MLHands.Left.Middle.KeyPoints[0].Position; //手掌中心
                    //pos[2] = new Vector3(pos[5].x, pos[5].y + 2 * margin, pos[5].z); //中指
                    //pos[3] = new Vector3(pos[1].x + 2 * margin, pos[1].y, pos[1].z); //无名指
                    //pos[4] = new Vector3(pos[3].x + 1 * margin, pos[3].y - 0.5f * margin, pos[3].z); //小拇指
                    //DisplayTime();
                    //CheckGaze(pos[0], pos[1], pos[2], pos[3], pos[4]);

                }

                // 识别左手
                else if (GetGesture(MLHands.Left, MLHandKeyPose.OpenHandBack) && !GetGesture(MLHands.Right, MLHandKeyPose.OpenHandBack)) //left hand operation
                {
                    ShowTime();
                    //pos[0] = MLHands.Left.Thumb.KeyPoints[0].Position; //大拇指
                    //pos[1] = MLHands.Left.Index.KeyPoints[0].Position; //食指
                    //pos[5] = MLHands.Left.Middle.KeyPoints[0].Position; //手掌中心
                    //pos[2] = new Vector3(pos[5].x, pos[5].y + 2 * margin, pos[5].z); //中指
                    //pos[3] = new Vector3(pos[1].x - 2 * margin, pos[1].y, pos[1].z); //无名指
                    //pos[4] = new Vector3(pos[3].x - 1 * margin, pos[3].y - 0.5f * margin, pos[3].z); //小拇指
                    //DisplayTime();
                    //CheckGaze(pos[0], pos[1], pos[2], pos[3], pos[4]);
                }

                if (GetGesture(MLHands.Right, MLHandKeyPose.Fist) || GetGesture(MLHands.Left, MLHandKeyPose.Fist))
                {
                    HideTime();
                    controller.GetComponent<ControllerScript>().selectFingerMenu();
                }

                else
                {
                    //HideTime();
                }

            }


        }
        camPrevPos = this.transform.position;

    }

    public void switchMode(string newMode) {
        mode = newMode;

    }

    public void dontclose() {
        large = false;
    }













    // Rubbish
    /*
    void MoveTimeBar(bool right)
    {
        if (right == true)
        {
            //move time bar to the right
            controller.GetComponent<ControllerScript>().dragTimeBar(1);

        }
        else
        {
            //move time bar to the left
            controller.GetComponent<ControllerScript>().dragTimeBar(-1);
        }
    }
    */


}






















