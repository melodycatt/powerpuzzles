using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
using Unity.VisualScripting;

public class CameraUtil : MonoBehaviour
{
	public TextMeshProUGUI bitsCounter;
	public ShopManager shop;
	public TruthTable table;
	public bool holding;

	public int inputsN;
	public int outputsN;

	public List<CInput> Inputs;
	public List<COutput> Outputs;

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
		float height = Camera.main.orthographicSize * 2;
		float inputGap = height / inputsN;
		float outputGap = height / outputsN;
		GameObject Input = Resources.Load<GameObject>("Input");
		GameObject Output = Resources.Load<GameObject>("Output");
		for (float i = Camera.main.orthographicSize - inputGap / 2; i >= -Camera.main.orthographicSize + inputGap / 2; i-= inputGap) {
			CInput tempIn = Instantiate(Input).GetComponent<CInput>();
			Inputs.Add(tempIn);
			tempIn.transform.position = new(6.75f, i, 0);
		}
		for (float i = Camera.main.orthographicSize - outputGap / 2; i >= -Camera.main.orthographicSize + outputGap / 2; i-= outputGap) {
			COutput tempOut = Instantiate(Output).GetComponent<COutput>();
			Outputs.Add(tempOut);
			tempOut.transform.position = new(-6.75f, i, 0);
		}
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
			table.Table = new Dictionary<bool[], bool[]>
			{
				{ 
					new bool[] { true, false}, 
					new bool[] { true, true } 
				},
				{
					new bool[] { true, true },
					new bool[] { true, false }
				},
				{
					new bool[] { false, true },
					new bool[] { false, false }
				},
				{
					new bool[] { false, false },
					new bool[] { false, true }
				},
			};
			StartCoroutine(table.Check());
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

