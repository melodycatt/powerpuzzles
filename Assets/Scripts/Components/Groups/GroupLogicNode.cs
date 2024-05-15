using System;
using System.Collections.Generic;
using UnityEngine;

public class GroupLogicNode : LogicNode
{

    public new List<KeyValuePair<List<int>, Func<List<bool>, bool>>> Logic;
    public int nInputs;
    public int nOutputs;
    public List<KeyValuePair<List<int>, bool>> outputs;


    // Start is called before the first frame update
    public GroupLogicNode(int nInputs, int nOutputs) : base(0, null)
    {
        this.nOutputs = nOutputs;
        this.nInputs = nInputs;
        outputs = new(nOutputs);
    }

    // Update is called once per frame
    public override void Return()
    {
        foreach (KeyValuePair<List<int>, Func<List<bool>, bool>> node in Logic)
        {
            List<bool> tempInputs = new List<bool>();
            foreach (int i in node.Key) tempInputs.Add(inputs[i]);
            inputs.Add(node.Value(tempInputs));
        }
        List<KeyValuePair<List<int>, bool>> tempOutputs = new();
        foreach (KeyValuePair<List<int>, bool> output in outputs)
        {
            bool tempValue = false;
            foreach (int i in output.Key)
            {
                tempValue = inputs[i] || tempValue;
            }
            tempOutputs.Add(new(output.Key, tempValue));
        }
    }



    static bool OR(List<bool> inputs)
    {
        return inputs[0] || inputs[1];
    }

    static bool AND(List<bool> inputs)
    {
        return inputs[0] && inputs[1];
    }
    static bool XOR(List<bool> inputs)
    {
        return (inputs[0] && !inputs[1]) || (!inputs[0] && inputs[1]);
    }
    static bool NOT(List<bool> inputs)
    {
        return !inputs[0];
    }
    static bool NOR(List<bool> inputs)
    {
        return !(inputs[0] || inputs[1]);
    }
    static bool NAND(List<bool> inputs)
    {
        return !(inputs[0] && inputs[1]);
    }
    static bool XNOR(List<bool> inputs)
    {
        return !((inputs[0] && !inputs[1]) || (!inputs[0] && inputs[1]));
    }
}
