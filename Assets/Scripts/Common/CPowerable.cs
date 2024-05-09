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

    // Update is called once per frame

    public virtual void updateNode(CComponent source, CComponent sender, bool remove)
    {
        Debug.Log("teehee2");
        if (remove)
        {
            foreach (CComponent i in neighbours)
            {
                if (i is CPowerable powerableI)
                {
                    if (i == sender || !powerableI.powerSources.Contains(source)) continue;
                    powerableI.powerSources.Remove(source);
                    powered = false;
                    powerableI.updateNode(source, this, true);
                }
            }
        }
        else
        {
            foreach (CComponent i in neighbours)
            {
                if (i is CPowerable powerableI)
                {
                    if (i == sender || powerableI.powerSources.Contains(source)) continue;
                    powerableI.powerSources.Add(source);
                    powered = true;
                    powerableI.updateNode(source, this, false);
                }
            }
        }
    }
}
