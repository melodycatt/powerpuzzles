using UnityEngine;
using System;

public class LOr : CComponent
{
    public override bool high
    {
        get
        {
            return _high;
        }
        set
        {
            _high = Array.Exists(inputs, (e) => e.high);
            if (_high) sprite.sprite = on;
            else sprite.sprite = defaultSprite;
        }
    }
    public Sprite on;

    // Use this for initialization
    new void Start()
	{
        on = Resources.Load<Sprite>("Wires/or_b");
        base.Start();
	}
}

