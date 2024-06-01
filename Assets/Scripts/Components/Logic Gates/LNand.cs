﻿using UnityEngine;
using System.Collections;
using System;

public class LNand : CComponent
{
    public override bool high
    {
        get
        {
            return _high;
        }
        set
        {
            _high = !Array.TrueForAll(inputs, (e) => e.high);
            if (_high) sprite.sprite = on;
            else sprite.sprite = defaultSprite;
        }
    }
    public Sprite on;

    // Use this for initialization
    new void Start()
	{
        on = Resources.Load<Sprite>("Wires/nand_b");
        base.Start();
	}

}

