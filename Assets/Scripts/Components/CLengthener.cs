using UnityEngine;
using System;
using System.Collections;

public class CLengthener : CComponent
{
	public override bool high
    {
        get
        {
            return _high;
        }
        set
        {
            if (value) _high = value;
            else
            {
                StartCoroutine(Extend());
            }
        }
    }
	public Sprite on;

	// Use this for initialization
	new void Start()
	{
        terminals = GetComponentsInChildren<Terminal>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        defaultSprite = Resources.Load<Sprite>("Wires/input_a");
        on = Resources.Load<Sprite>("Wires/input_b");
		sprite.sprite = defaultSprite;
        high = false;
    }

    // Update is called once per frame
    void Update()
	{
			
	}

    private void OnMouseDown()
    {
        high = !high;
        if (high) sprite.sprite = on;
        else sprite.sprite = defaultSprite;
    }

    public IEnumerator Extend()
    {
        yield return new WaitForSeconds(0.5f);
        _high = false;
    }
}

