using UnityEngine;
using System.Linq;
using System;
// fuck
public class LXor : CComponent
{
    public override bool high
    {
        get
        {
            return _high;
        }
        set
        {
            _high = inputs.Count((e) => e.high) == 1;
        }
    }

    // Use this for initialization
    new void Start()
	{
        base.Start();
	}
}

