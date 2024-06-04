using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class HiddenTerminal : Terminal
{
    public override bool high
    {
        get => _high;
        set
        {
            _high = value;
            if (output)
            {
                foreach (Wire curve in curves)
                {
                    curve.high = value;
                }
            } else if (parnet != null)
            {
                if (parnet is CGroup gparnet) gparnet.HiddenHigh(value);
            }
        }
    }

    void Start()
	{
        parnet = transform.parent.parent.GetComponent<CComponent>();
        wire = Resources.Load<GameObject>("Wire");
    }

}