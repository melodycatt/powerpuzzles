using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

public class ComponentGroup
{
    public List<KeyValuePair<Gates, List<Tuple<int, int, int>>>> Logic;

    public void Save() {
        List<string> gates = Logic.Select((x) => x.Key.ToString()).ToList();
    }


    public int nInputs;
    public int nOutputs;
    public List<CComponent> Nodes;
    public ShopManager shop;

    public List<List<Tuple<int, int>>> Inputs;
    public List<List<Tuple<int, int>>> Outputs;
    
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
    public ComponentGroup(int nInputs, int nOutputs, List<List<Tuple<int, int>>> inputs, List<List<Tuple<int, int>>> outputs, List<KeyValuePair<ComponentGroup.Gates, List<Tuple<int, int, int>>>> Logic)
    {
        this.nOutputs = nOutputs;
        this.nInputs = nInputs;
        Inputs = inputs;
        Outputs = outputs;
        this.Logic = Logic;
        Nodes = new();
    }

    // Update is called once per frame
    public void Instantiate(List<Terminal> inputs, List<Terminal> outputs)
    {
        foreach( Gates node in Logic.Select((x) => x.Key)) {
            GameObject tempNode = GameObject.Instantiate(shop.Objects[(int)node].Value);
            tempNode.GetComponent<holdable>().StartPublic();
            tempNode.GetComponent<holdable>().shop = shop;
            tempNode.GetComponent<holdable>().Pin(new(0, 0, -20));
            tempNode.GetComponent<holdable>().state = holdable.State.Group;
            Nodes.Add(tempNode.GetComponent<CComponent>());
        } 
    }

    public IEnumerator Connect(List<Terminal> inputs, List<Terminal> outputs, List<Terminal> realoutputs) {
        yield return 0;
        yield return 0;
        yield return 0;
        int i = 0;
        foreach( List<Tuple<int, int, int>> node in Logic.Select((x) => x.Value)) {
            Terminal[] terminals = Nodes[i].inputs;
            foreach(Tuple<int, int, int> j in node) {
                Wire wire = terminals[j.Item1].ReturnWire();
                wire.transform.position = new Vector3(0, 0, -20);
                wire.start = Nodes[j.Item2].outputs[j.Item3];
                Nodes[j.Item2].outputs[j.Item3].curves.Add(wire);
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
                wire.end = Nodes[j.Item1].inputs[j.Item2];
                Nodes[j.Item1].inputs[j.Item2].curves.Add(wire);
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
                wire.start = Nodes[j.Item1].outputs[j.Item2];
                Nodes[j.Item1].outputs[j.Item2].curves.Add(wire);
                wire.end = outputs[i];
                wire.high = wire.start.high;
            }
            i++;
        }
    }
}
