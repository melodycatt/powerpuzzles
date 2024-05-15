using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class ComponentGroup
{

    public List<KeyValuePair<Gates, List<Tuple<int, int, int>>>> Logic;
    public int nInputs;
    public int nOutputs;
    public List<CComponent> Nodes;
    public ShopManager shop;
    public List<List<Tuple<int, int>>> Inputs;
    public List<List<Tuple<int, int>>> Outputs;
    
    public List<GameObject> nodes;

    public enum Gates {
        OR,
        AND,
        XOR,
        NOT,
        NOR,
        NAND,
        XNOR,
    }

    // Start is called before the first frame update
    public ComponentGroup(int nInputs, int nOutputs, List<List<Tuple<int, int>>> inputs, List<List<Tuple<int, int>>> outputs)
    {
        this.nOutputs = nOutputs;
        this.nInputs = nInputs;
        Inputs = inputs;
        Outputs = outputs;
    }

    // Update is called once per frame
    public void Instantiate(List<Terminal> inputs, List<Terminal> outputs)
    {
        foreach( Gates node in Logic.Select((x) => x.Key)) {
            GameObject tempNode = GameObject.Instantiate(shop.Objects[(int)node].Value);
            tempNode.GetComponent<holdable>().shop = shop;
            tempNode.GetComponent<holdable>().Pin(new(0, 0, -20));
            tempNode.GetComponent<holdable>().state = holdable.State.Group;
            Nodes.Add(tempNode.GetComponent<CComponent>());
        } 
        int i = 0;
        foreach( List<Tuple<int, int, int>> node in Logic.Select((x) => x.Value)) {
            Terminal[] terminals = Nodes[i].GetComponentsInChildren<Terminal>();
            foreach(Tuple<int, int, int> j in node) {
                Wire wire = terminals[j.Item1].ReturnWire();
                wire.transform.position = new Vector3(0, 0, -20);
                wire.start = Nodes[j.Item2].terminals.Where(x => x.output).ToList()[j.Item3];
                wire.end = terminals[j.Item1];
                wire.high = wire.start.high;
            }
            i++;
        }
        i = 0;
        foreach(List<Tuple<int, int>> input in Inputs) {
            foreach(Tuple<int, int> j in input) {
                Wire wire = inputs[i].ReturnWire();
                wire.transform.position = new Vector3(0, 0, -20);
                wire.end = Nodes[j.Item1].terminals.Where(x => !x.output).ToList()[j.Item2];
                wire.start = inputs[i];
                wire.high = wire.start.high;
            }
            i++;
        }
        i = 0;
        foreach( List<Tuple<int, int>> output in Outputs) {
            foreach(Tuple<int, int> j in output) {
                Wire wire = outputs[i].ReturnWire();
                wire.transform.position = new Vector3(0, 0, -20);
                wire.start = Nodes[j.Item1].terminals.Where(x => x.output).ToList()[j.Item2];
                wire.end = outputs[i];
                wire.high = wire.start.high;
            }
            i++;
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
