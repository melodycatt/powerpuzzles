using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;

public class CameraUtil : MonoBehaviour
{
	public TextMeshProUGUI bitsCounter;
	public ShopManager shop;

	public bool holding;

	public bool TutorialPause = false;

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

	public void toggleShop() {
		if (!shop.enabled) shop.hover = false;
		shop.enabled = !shop.enabled;
		shop.gameObject.SetActive(!shop.gameObject.activeInHierarchy);

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			toggleShop();
		}
		if (Input.GetKeyDown(KeyCode.F)) {
			GameObject NOT = GameObject.Find("NOT CRASH PLEASE");
			CComponent LNOT = NOT.GetComponent<LNot>();
			Debug.Log(NOT);
			Debug.Log(LNOT);
			print(LNOT.outputs[0]);
			LNOT.outputs[0].high = false;
			print(LNOT.outputs[0]);
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

