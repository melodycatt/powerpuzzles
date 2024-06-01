using UnityEngine;
using System.Collections;
using System;

public class LNot : CComponent
{
    public override bool high
    {
        get
        {
            return _high;
        }
        set
        {
            _high = !value;
            if (_high) sprite.sprite = on;
            else sprite.sprite = defaultSprite;
        }
    }
    public Sprite on;

    // Use this for initialization
    new void Start()
	{
        on = Resources.Load<Sprite>("Wires/not_b");
        base.Start();
	}
}

