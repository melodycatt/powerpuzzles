using UnityEngine;
using System;
using System.Collections.Generic;

public class CGroupTest : MonoBehaviour 
{
    public int nInputs;
    public int nOutputs;
    public List<List<Tuple<int, int>>> Inputs;
    public List<List<Tuple<int, int>>> Outputs;
    
	public void TestCGroup() {
		CGroup _CGroup = GameObject.Find("CGroup").GetComponent<CGroup>();
		_CGroup.nInputs = 2;
		_CGroup.nOutputs = 1;

        List<KeyValuePair<ComponentGroup.Gates, List<Tuple<int, int, int>>>> Logic = new()
        {
            new(ComponentGroup.Gates.NOT, new() { }),
            new(ComponentGroup.Gates.AND, new() { new(0,0,0), new(1,2,0) }),
            new(ComponentGroup.Gates.OR, new() { new(0,1,0) })
        };

        _CGroup.Outputs = new()
        {
            new() {new(2,0)},
        };

        _CGroup.Inputs = new()
        {
            new() {new(2,1)},
            new() {new(0,0)},
        };

// LOGIC [(NOT, [()]), (AND, [(0, 0, 0), (1, 2, 0)]), (OR, [(0, 1, 0)])
// INPUTS [[(2,1)], [(0, 0)]]
// OUTPUTS [[(2, 0)]]

		_CGroup.Init(Logic);
	}
}