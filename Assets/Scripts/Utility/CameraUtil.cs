using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.U2D;

public class CameraUtil : MonoBehaviour
{
	[Range(0.05f, 1)]
	public float UpdateSpeed = 0.05f;
	public TextMeshProUGUI bitsCounter;
	public ShopManager shop;
	public TruthTable table;
	public bool holding;

	public int inputsN;
	public int outputsN;

	public List<CInput> Inputs;
	public List<COutput> Outputs;

	public bool TutorialPause = false;

	public GameObject robot;

	[SerializeField]
	private int _bits = 1000;
	public int startingBits = 2147483647;
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

	public void Reset() {
		ResetPuts(inputsN, outputsN);
		foreach(GameObject i in robot.GetComponent<Robot>().CurrentComps) {
			Destroy(i);
		}
		robot.GetComponent<Robot>().CurrentComps.Clear();
		bits = startingBits;
	}

	public void ResetPuts(int inputsN, int outputsN) {
		foreach(CInput i in Inputs) {
			Destroy(i.gameObject);
		}
		Inputs.Clear();
		foreach(COutput i in Outputs) {
			Destroy(i.gameObject);
		}
		Outputs.Clear();
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
	}

	public void ClearPuts() {
		foreach(CInput i in Inputs) {
			Destroy(i.gameObject);
		}
		Inputs.Clear();
		foreach(COutput i in Outputs) {
			Destroy(i.gameObject);
		}
		Outputs.Clear();
	}

	public void toggleShop() {
		shop.enabled = !shop.enabled;
		if (!shop.enabled) shop.hover = false;
		shop.gameObject.SetActive(!shop.gameObject.activeInHierarchy);
		//robot.SetActive(!robot.activeInHierarchy);
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

