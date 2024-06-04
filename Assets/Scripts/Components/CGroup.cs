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

    [SerializeField]
    public ComponentGroup componentGroup;

    public override bool high
    {
        get => _high;
        set
        {
            for (int i = 0; i < inputs.Count(); i++) {
                HiddenInputs[i].high = inputs[i].high;
            }

            //if (testing) Tester.Result(_high, id);
        }
    }

    public List<Terminal> HiddenInputs;
    public List<Terminal> HiddenOutputs;
    public Transform hiddenInputHolder;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        hiddenInputHolder = transform.Find("Hidden Input Holder");
		nInputs = 2;
		nOutputs = 1;

        List<KeyValuePair<ComponentGroup.Gates, List<Tuple<int, int, int>>>> Logic = new()
        {
            new(ComponentGroup.Gates.NOT, new() { }),
            new(ComponentGroup.Gates.AND, new() { new(0,0,0), new(1,2,0) }),
            new(ComponentGroup.Gates.OR, new() { new(0,1,0) })
        };

        Outputs = new()
        {
            new() {new(2,0)},
        };

        Inputs = new()
        {
            new() {new(2,1)},
            new() {new(0,0)},
        };

// LOGIC [(NOT, [()]), (AND, [(0, 0, 0), (1, 2, 0)]), (OR, [(0, 1, 0)])
// INPUTS [[(2,1)], [(0, 0)]]
// OUTPUTS [[(2, 0)]]

		Init(Logic);
    }

    public void Init(List<KeyValuePair<ComponentGroup.Gates, List<Tuple<int, int, int>>>> Logic) {
        GameObject terminal = Resources.Load<GameObject>("Terminal");
        float inputGap = sprite.bounds.size.y / nInputs;
        float outputGap = sprite.bounds.size.y / nOutputs;
        if (nInputs == 1) inputGap = 0;
        if (nOutputs == 1) outputGap = 0;
        //1: 0 - 0, i< 1 good
        // 2: 0 - 1, i < 1 good
        // 3: 0 - 1, i < 2 goof
        // 4: 0 - 2, i < 2 good
        // 5: 0 - 2, i< 3 good
        print(inputGap);
        for (int i = (int)Math.Floor(nInputs / 2f); i > (int)-Math.Ceiling(nInputs / 2f); i--) {
            print(i);
            print(inputGap / 2 + i * inputGap);
            GameObject tempTerminal = Instantiate(terminal);
            tempTerminal.transform.parent = transform;
            tempTerminal.GetComponent<Terminal>().parnet = this;
            tempTerminal.transform.localPosition = new Vector3(sprite.bounds.size.x / 2, i * inputGap - inputGap / 2, 0);

            GameObject hiddenInput = Instantiate(terminal);
            Destroy(hiddenInput.GetComponent<RealTerminal>());
            hiddenInput.transform.position = new Vector3(0, 0, -20);
            hiddenInput.AddComponent<HiddenTerminal>();
            hiddenInput.transform.parent = hiddenInputHolder;
            hiddenInput.GetComponent<HiddenTerminal>().parnet = this;
            hiddenInput.GetComponent<HiddenTerminal>().output = true;

            HiddenInputs.Add(hiddenInput.GetComponent<HiddenTerminal>());
        }
        for (int i = (int)Math.Floor(nOutputs / 2f); i > (int)-Math.Ceiling(nOutputs / 2f); i--) {
            GameObject tempTerminal = Instantiate(terminal);
            tempTerminal.transform.parent = transform;
            tempTerminal.GetComponent<Terminal>().parnet = this;
            tempTerminal.GetComponent<Terminal>().output = true;
            tempTerminal.transform.localPosition = new Vector3(-sprite.bounds.size.x / 2, i * outputGap - outputGap / 2, 0);

            GameObject hiddenInput = Instantiate(terminal);
            Destroy(hiddenInput.GetComponent<RealTerminal>());
            hiddenInput.transform.position = new Vector3(0, 0, -20);
            hiddenInput.AddComponent<HiddenTerminal>();
            hiddenInput.transform.parent = hiddenInputHolder;
            hiddenInput.GetComponent<HiddenTerminal>().parnet = this;

            HiddenOutputs.Add(hiddenInput.GetComponent<HiddenTerminal>());
        }
        componentGroup = new ComponentGroup(nInputs, nOutputs, Inputs, Outputs, Logic);
        componentGroup.shop = shop;
        componentGroup.Instantiate(HiddenInputs, HiddenOutputs);
        StartCoroutine(componentGroup.Connect(HiddenInputs, HiddenOutputs, outputs.ToList()));
    }

    public void HiddenHigh (bool high) {
        _high = high;
        for (int i = 0; i < outputs.Count(); i++) {
            outputs[i].high = HiddenOutputs[i].high;
        }
    }

    public override void UpdateNode()
    {
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
