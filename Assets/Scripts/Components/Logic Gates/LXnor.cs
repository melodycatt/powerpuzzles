using UnityEngine;
using System.Linq;
using System;

public class LXnor : CComponent
{
    public override bool high
    {
        get
        {
            return _high;
        }
        set
        {
            _high = inputs.Count((e) => e.high) != 1;
            if (_high) sprite.sprite = on;
            else sprite.sprite = defaultSprite;
        }
    }
    public Sprite on;

    // Use this for initialaization
    new void Start()
    {
        on = Resources.Load<Sprite>("Wires/xnor_b");
        base.Start();
    }

}

