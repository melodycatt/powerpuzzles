using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLamp : CPowerable
{
    public bool testing = false;
    //public Tester Tester;

    public int id;

    public override bool high
    {
        get => _high;
        set
        {
            _high = value;
            if (high) sprite.sprite = on;
            else sprite.sprite = defaultSprite;

            //if (testing) Tester.Result(_high, id);
        }
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        defaultSprite = Resources.Load<Sprite>("Wires/lamp_a");
        on = Resources.Load<Sprite>("Wires/lamp_b");
    }

    // Update is called once per frame
    public override void updateNode(CComponent source, CComponent sender, bool remove)
    {
        base.updateNode(source, sender, remove);
        if(powerSources.Count > 0)
        {
            sprite.sprite = on;
        } else
        {
            sprite.sprite = defaultSprite;
        }
    }
}
