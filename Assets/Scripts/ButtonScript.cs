using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private float value = 0;
    private bool gazed = false;

    private string iconName;

    // Start is called before the first frame update
    void Start()
    {
        iconName = GetComponent<SpriteRenderer>().sprite.name;
    }

    // Update is called once per frame
    void Update()
    {
        //SpriteRenderer spr = GetComponent<SpriteRenderer>();
        //Texture2D texture2d = (Texture2D)Resources.Load("Icons/heritage");
        //Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));
        //spr.sprite = sp;

    }

    public void selected() {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        Texture2D texture2d = (Texture2D)Resources.Load("Icons/"+ iconName + "_selected");
        Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));
        spr.sprite = sp;

        gazed = true;
    }

    public void unselected() {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        Texture2D texture2d = (Texture2D)Resources.Load("Icons/" + iconName);
        Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));
        spr.sprite = sp;

        gazed = false;
    }

    public bool isGazed() {
        return gazed;
    }

    public IEnumerator openButton()
    {

        float multiplier = Time.deltaTime * 5;
        while (true)
        {
            this.transform.localScale += Vector3.one * multiplier;
            this.GetComponent<SpriteRenderer>().color -= new Color(0,0,0,multiplier);
            yield return null;
            if (this.GetComponent<SpriteRenderer>().color.a < 0)
            {

                disableSelf();
                break;
            }
        }
    }


    public IEnumerator fadeButton()
    {

        float multiplier = Time.deltaTime * 5;
        while (true)
        {
            this.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, multiplier);
            yield return null;
            if (this.GetComponent<SpriteRenderer>().color.a < 0)
            {

                disableSelf();
                break;
            }
        }
    }


    public IEnumerator comeBackButton()
    {
        enableSelf();
        // Start from transparent
        this.transform.localScale = Vector3.one * 2;
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        float multiplier = Time.deltaTime * 5;
        while (true)
        {
            this.transform.localScale -= Vector3.one * multiplier;
            this.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, multiplier);
            yield return null;
            if (this.GetComponent<SpriteRenderer>().color.a >= 1)
            {
                break;
            }
        }
    }


    public void disableSelf() {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Collider>().enabled = false;
    }

    public void enableSelf()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<Collider>().enabled = true;
    }
}
