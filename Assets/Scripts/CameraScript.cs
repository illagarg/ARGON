using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Vector3 lookingDirection;
    private GameObject obj;
    private float timer;

    public GameObject controller;

    public GameObject fingerMenus;

    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray();
        lookingDirection = new Vector3();

        obj = null;
    }

    // Update is called once per frame
    void Update()
    {
        lookingDirection = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, lookingDirection, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);
            if (hit.collider.gameObject != obj) {

                if (obj != null && obj.tag == "Button") {
                    obj.GetComponent<ButtonScript>().unselected();
                }
                else if (obj != null && obj.tag == "FingerMenu")
                {
                    obj.GetComponent<FingerMenuScript>().unselected();
                }


                if (hit.collider.gameObject.tag == "Button") {
                    hit.collider.gameObject.GetComponent<ButtonScript>().selected();
                }

                else if (hit.collider.gameObject.tag == "FingerMenu")
                {
                    if (hit.collider.gameObject == obj) {
                        timer += Time.deltaTime;
                    }
                    hit.collider.gameObject.GetComponent<FingerMenuScript>().selected();
                    controller.GetComponent<ControllerScript>().choose(hit.collider.gameObject);
                }


                obj = hit.collider.gameObject;
            }
        }
        else {
            if (obj != null && obj.tag == "Button")
            {
                obj.GetComponent<ButtonScript>().unselected();
            }
            else if (obj != null && obj.tag == "FingerMenu")
            {
                obj.GetComponent<FingerMenuScript>().unselected();
            }
            obj = null;
        }

    }

    public GameObject getGameObject() {
        return obj;
    }
}
