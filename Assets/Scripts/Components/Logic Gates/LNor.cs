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

