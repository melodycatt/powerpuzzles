using UnityEngine;
using System;
using System.Collections;

public class CLengthener : CComponent
{
    public bool ready = true;
	public override bool high
    {
        get
        {
            return _high;
        }
        set
        {
            Debug.Log("hi");
            if (value)
            {
                _high = true;
                sprite.sprite = on;
            }
            else
            {
                if(ready) StartCoroutine(Extend());
            }
        }
    }
	public Sprite on;

	// Use this for initialization
	new void Start()
	{
        base.Start();
        on = Resources.Load<Sprite>("Wires/input_b");
		sprite.sprite = defaultSprite;
        _high = false;
    }

    private void OnMouseDown()
    {
    }

    public IEnumerator Extend()
    {
        ready = false;
        yield return new WaitForSeconds(0.5f);
        _high = false;
        sprite.sprite = defaultSprite;
        ready = true;
    }
}

