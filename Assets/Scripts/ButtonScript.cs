using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        Texture2D texture2d = (Texture2D)Resources.Load("Icons/heritage");
        Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));//注意居中显示采用0.5f值
        spr.sprite = sp;
    }

    public void selected() {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        Texture2D texture2d = (Texture2D)Resources.Load("Icons/heritage_selected");
        Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));//注意居中显示采用0.5f值
        spr.sprite = sp;
     
    }

    public void open() {

    }
}
