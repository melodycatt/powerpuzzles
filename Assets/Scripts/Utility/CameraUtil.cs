using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
using Sirenix.OdinInspector;

public class CameraUtil : SerializedMonoBehaviour
{
	public TextMeshProUGUI bitsCounter;
	public ShopManager shop;

	public int _bits = 1000;
	public int bits
	{
		get => _bits;
		set
		{
			_bits = value;
			bitsCounter.text = value.ToString();
		}
	}
	// Use this for initialization
	void Start()
	{
		bitsCounter.text = _bits.ToString();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			shop.enabled = !shop.enabled;
			shop.gameObject.SetActive(!shop.gameObject.activeInHierarchy);
			
		}
	}

	public void TestCGroup(List<List<Tuple<int, int>>> Inputs, List<List<Tuple<int, int>>> Outputs) {
		CGroup _CGroup = GameObject.Find("CGroup").GetComponent<CGroup>();
		_CGroup.nInputs = 3;
		_CGroup.nOutputs = 2;

		_CGroup.Inputs = Inputs;
		_CGroup.Outputs = Outputs;
		_CGroup.Init();
	}
}

