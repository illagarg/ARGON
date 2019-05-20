using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerMenuScript : MonoBehaviour
{
    private bool gazed;
    private Color origColor;
   
    
    // Start is called before the first frame update
    void Start()
    {
        gazed = false;
        origColor = this.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void selected()
    {
        this.GetComponent<Image>().color = new Color(0, 0, 255);
        gazed = true;
    }

    public void chosen()
    {
        this.GetComponent<Image>().color = new Color(0, 255, 0);
    }

    public void unselected()
    {
        this.GetComponent<Image>().color = origColor;
        gazed = false;
    }

    public void adjustPos(Vector3 pos) {
        this.transform.position = pos;
    }
}
