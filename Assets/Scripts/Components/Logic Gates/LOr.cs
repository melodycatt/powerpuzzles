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
        }
    }

    // Use this for initialization
    new void Start()
	{
        base.Start();
	}
}

