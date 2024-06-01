using UnityEngine;
using System;

public class LNor : CComponent
{
    public override bool high
    {
        get
        {
            return _high;
        }
        set
        {
            _high = !Array.Exists(inputs, (e) => e.high);
            if (_high) sprite.sprite = on;
            else sprite.sprite = defaultSprite;
        }
    }
    public Sprite on;

    // Use this for initialaization
    new void Start()
    {
        on = Resources.Load<Sprite>("Wires/nor_b");
        base.Start();
    }

}

