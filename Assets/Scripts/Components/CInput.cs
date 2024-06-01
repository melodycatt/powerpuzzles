using UnityEngine;
using System;

public class CInput : CComponent
{
	public override bool high
    {
        get
        {
            return _high;
        }
        set
        {
            if(Array.Find(terminals, x => x.output) != null) Array.Find(terminals, x => x.output).high = value;
            _high = value;
        }
    }
	public Sprite on;

	// Use this for initialization
	new void Start()
	{
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Debug.Log(sprite);
        defaultSprite = Resources.Load<Sprite>("Wires/input_a");
        on = Resources.Load<Sprite>("Wires/input_b");
		sprite.sprite = defaultSprite;
        high = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("pen");
        high = !high;
        if (high) sprite.sprite = on;
        else sprite.sprite = defaultSprite;
    }
}

