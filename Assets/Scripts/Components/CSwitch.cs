using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CSwitch : CComponent {
    private static Sprite on;
    private bool toggled = false;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        defaultSprite = Resources.Load<Sprite>("Wires/switch_a");
        on = Resources.Load<Sprite>("Wires/switch_b");
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        toggled = !toggled;
        if (toggled)
        {
            sprite.sprite = on;
            foreach (var i in neighbours.OfType<CPowerable>())
            {
                Debug.Log(i);
                i.powerSources.Add(this);
                i.updateNode(this, this, false);
            }
        }
        else
        {
            sprite.sprite = defaultSprite;
            foreach (var i in neighbours.OfType<CPowerable>())
            {
                Debug.Log(i);
                i.powerSources.Remove(this);
                i.updateNode(this, this, true);
            }
        }
    }
}
