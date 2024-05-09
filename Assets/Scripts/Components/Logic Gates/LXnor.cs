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
        }
    }

    // Use this for initialaization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

