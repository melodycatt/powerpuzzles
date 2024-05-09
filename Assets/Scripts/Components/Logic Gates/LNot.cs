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
        }
    }

    // Use this for initialization
    new void Start()
	{
        base.Start();
	}

	// Update is called once per frame
	void Update()
	{
	    
	}
}

