using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
	public LineRenderer lr;
	public BezierCurve curve;
	public Terminal end;
	public Terminal start;

	private bool _high;
	public bool high
	{
		get => _high;
		set
		{
			_high = value;
			if (end != null) end.high = value;
			if(value)
			{
                lr.material.SetColor("_Color", new Color(0.1f, 0.65f, 0));
            } else
			{
                lr.material.SetColor("_Color", new Color(0.8f, 0.8f, 0.8f));
            }
			curve.Render(lr);
        }
	} 

	public enum WireType
	{
		Normal
	}

	public void Start()
	{

	}

	private void Update()
	{
	}

	public void Init(Vector3 s, Vector3 e, Terminal start)
	{
		this.start = start;
		curve = new BezierCurve(s, e);
		lr = GetComponent<LineRenderer>();
        lr.positionCount = 50;
        Material material = new Material(Shader.Find("Unlit/pree"));
        material.SetColor("_Color", new Color(0.8f, 0.8f, 0.8f)); //new Color(0.1f, 0.65f, 0)
        material.renderQueue = 5000;
        lr.material = material;
        lr.sortingOrder = 1;
        lr.sortingLayerName = "wire";
        lr.numCapVertices = 5;
		high = start.high;
        curve.Render(lr);
    }

}

