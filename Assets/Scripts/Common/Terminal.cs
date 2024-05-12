using UnityEngine;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public class Terminal : MonoBehaviour
{
    public List<Wire> curves;
    private GameObject wire;
    public bool wiring = false;
    public bool output;
    public LayerMask terminal;
    public CComponent parnet;

    private bool _high;
    public bool high
    {
        get => _high;
        set
        {
            _high = value;

            if (output)
            {
                foreach (Wire curve in curves)
                {
                    curve.high = value;
                }
            } else if (parnet != null)
            {
                parnet.high = value;
            }
        }
    }

    public enum WireType
    {
        Normal
    }


    // Use this for initialization
    void Start()
	{
        parnet = transform.parent.GetComponent<CComponent>();
        wire = new GameObject("Wire");
        LineRenderer lr = wire.AddComponent<LineRenderer>();
        lr.startWidth = 0.075f;
        lr.endWidth = 0.075f;
        lr.sortingOrder = 10;
        wire.AddComponent<Wire>();
    }

    private void Update()
    {
        Vector2 mousepos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        if (Input.GetMouseButtonDown(0) && GetComponent<Collider2D>().OverlapPoint(mousepos))
        {
            if (output)
            {
                CreateWire(transform.position, mousepos, WireType.Normal);
                Debug.Log("curves");
                wiring = true;
            } else if (!output && curves.Count > 0)
            {
                wiring = true;
                curves[^1].end = null;
            }
        }
        else if ((Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) && wiring)
        {
            curves[^1].curve.UpdateEnd(mousepos, curves[^1].lr);
        }
        if (Input.GetMouseButtonUp(0) && wiring)
        {
            wiring = false;

            Collider2D[] candidateTerminals = {};
            Debug.Log(Physics2D.OverlapCircle(mousepos, .1f, terminal));
            if (Physics2D.OverlapCircle(mousepos, .1f, terminal))
            {
                Debug.Log("got to 70");
                candidateTerminals = Physics2D.OverlapCircleAll(mousepos, 0.35f, terminal);
                candidateTerminals = Array.FindAll(candidateTerminals, (x) => x.GetComponent<Terminal>().output == false);
                if (candidateTerminals.Length > 0)
                {
                    Terminal finalTerminal = candidateTerminals[0].GetComponent<Terminal>();
                    Vector3 TerminalPosition = candidateTerminals[0].transform.position;
                    foreach (Collider2D collider in candidateTerminals)
                    {
                        Debug.Log("got to 76 loop");
                        Debug.Log((collider.transform.position - (Vector3)mousepos).magnitude);
                        if ((collider.transform.position - (Vector3)mousepos).magnitude <= (TerminalPosition - (Vector3)mousepos).magnitude) {
                            Debug.Log("got to 78");
                            finalTerminal = Array.Find(collider.gameObject.GetComponents<Terminal>(), (x) => x.output == false);
                            TerminalPosition = finalTerminal.transform.position;
                        }
                    }
                    curves[^1].curve.UpdateEnd(TerminalPosition, curves[^1].lr);
                    finalTerminal.curves.Add(curves[^1]);
                    curves[^1].end = finalTerminal;
                    finalTerminal.high = high;
                    Debug.Log("got to 86");
                } else
                {
                    Debug.Log("got to 90 :(");
                    Destroy(curves[^1].gameObject);
                    curves[^1].start.curves.Remove(curves[^1]);
                    curves.Remove(curves[^1]);
                }
            }
            else
            {
                Debug.Log("got to 90 :(");
                Destroy(curves[^1].gameObject);
                curves[^1].start.curves.Remove(curves[^1]);
                curves.Remove(curves[^1]);
            }
        }
    }

    public void Free()
    {
        foreach (Wire i in curves)
        {
            Debug.Log("hi");
            if (output)
            {
                Debug.Log("ugh");
                i.end.curves.Remove(i);
            }
            else i.start.curves.Remove(i);
            Destroy(i.gameObject);
        }
        curves.Clear();
    }

    public void CreateWire(Vector2 s, Vector2 e, WireType type)
    {
        GameObject tempWire = Instantiate(wire);
        tempWire.transform.parent = transform;
        tempWire.GetComponent<Wire>().Init(s, e, this);
        curves.Add(tempWire.GetComponent<Wire>());
    }
}

