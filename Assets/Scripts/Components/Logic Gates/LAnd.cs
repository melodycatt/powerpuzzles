using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class LAnd : CComponent
{
    public override bool high
    {
        get
        {
            return _high;
        }
        set
        {
            _high = Array.TrueForAll(inputs, (e) => e.high);// && !inputs.ToList().Exists(x => x.high);
            if (_high) sprite.sprite = on;
            else sprite.sprite = defaultSprite;
        }
    }

    public Sprite on;

    // Use this for initialization
    new void Start()
	{
        on = Resources.Load<Sprite>("Wires/and_b");
        base.Start();
	}
}

