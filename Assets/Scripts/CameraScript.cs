using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Vector3 lookingDirection;

    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray();
        lookingDirection = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        lookingDirection = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, lookingDirection, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);
        }
    }
}
