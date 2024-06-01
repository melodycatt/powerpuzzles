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
            if (_high) sprite.sprite = on;
            else sprite.sprite = defaultSprite;
        }
    }
    public Sprite on;

    // Use this for initialization
    new void Start()
	{
        on = Resources.Load<Sprite>("Wires/xor_b");
        base.Start();
	}
}

