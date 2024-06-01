using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CPowerable : CComponent
{
    protected static Sprite on;
    public bool powered = false;
    // s
    public List<CComponent> powerSources;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        defaultSprite = Resources.Load<Sprite>("Wires/switch_a");
        on = Resources.Load<Sprite>("Wires/switch_b");
    }
}
