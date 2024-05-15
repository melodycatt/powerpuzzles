using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CGroup : CComponent
{
    //public Tester Tester;

    public List<List<Tuple<int, int>>> Inputs;
    public List<List<Tuple<int, int>>> Outputs;

    public ShopManager shop;
    public int id;

    public int nInputs;
    public int nOutputs;

    public ComponentGroup componentGroup;

    public override bool high
    {
        get => _high;
        set
        {
            _high = value;

            //if (testing) Tester.Result(_high, id);
        }
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        defaultSprite = Resources.Load<Sprite>("Wires/lamp_a");
    }

    public new void Init() {
        GameObject terminal = Resources.Load<GameObject>("Terminal");
        float inputGap = sprite.bounds.size.y / nInputs;
        float outputGap = sprite.bounds.size.y / nOutputs;
        for (int i = 0 - (int)Math.Floor(nInputs / 2f); i < (int)Math.Floor(nInputs / 2f); i++) {
            GameObject tempTerminal = Instantiate(terminal);
            tempTerminal.transform.parent = transform;
            tempTerminal.transform.position = transform.position + new Vector3(sprite.bounds.size.x / 2, inputGap / 2 + i * inputGap);
        }
        for (int i = 0 - (int)Math.Floor(nOutputs / 2f); i < (int)Math.Floor(nOutputs / 2f); i++) {
            GameObject tempTerminal = Instantiate(terminal);
            tempTerminal.GetComponent<Terminal>().output = true;
            tempTerminal.transform.parent = transform;
            tempTerminal.transform.position = transform.position + new Vector3(-sprite.bounds.size.x / 2, outputGap / 2 + i * outputGap);
        }
        componentGroup = new ComponentGroup(nInputs, nOutputs, Inputs, Outputs);
        componentGroup.shop = shop;
        componentGroup.Instantiate(inputs.ToList(), outputs.ToList());
    }

    /* Update is called once per frame
    public override void UpdateNode(CComponent source, CComponent sender, bool remove)
    {
        base.updateNode(source, sender, remove);
        if(powerSources.Count > 0)
        {
            sprite.sprite = on;
        } else
        {
            sprite.sprite = defaultSprite;
        }
    }*/
}
