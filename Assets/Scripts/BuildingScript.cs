using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public GameObject infoWindow;
    // Start is called before the first frame update
    void Start()
    {
        infoWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void displayInfo() {
        infoWindow.SetActive(true);
    }
    public void hideInfo() {
        infoWindow.SetActive(false);
    }
}
