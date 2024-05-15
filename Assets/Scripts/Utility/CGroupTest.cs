using UnityEngine;
using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class CGroupTest : SerializedMonoBehaviour 
{
    public int nInputs;
    public int nOutputs;
    public List<List<Tuple<int, int>>> Inputs;
    public List<List<Tuple<int, int>>> Outputs;
    
	public void TestCGroup() {
		CGroup _CGroup = GameObject.Find("CGroup").GetComponent<CGroup>();
		_CGroup.nInputs = 3;
		_CGroup.nOutputs = 2;

		_CGroup.Inputs = Inputs;
		_CGroup.Outputs = Outputs;
		_CGroup.Init();
	}
}