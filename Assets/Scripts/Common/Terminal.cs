using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;

public class Terminal : MonoBehaviour
{
    public List<Wire> curves = new();
    protected GameObject wire;
    public bool wiring = false;
    public bool output;
    public LayerMask terminal;
    public CComponent parnet;

    protected bool _high = false;
    public virtual bool high
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
                Debug.Log(value);
                if (!curves.Exists(x => x.high) || value) {
                    _high = value;
                } else {
                    _high = true;
                }
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
        wire = Resources.Load<GameObject>("Wire");
    }

    void RemoveOff() {
        if (!curves.Exists(x => x.high && x != curves[^1])) {
            Debug.Log("1");
            _high = false;
            parnet.high = false;
        }
    }

    private void Update()
    {
        if (!Camera.main.GetComponent<CameraUtil>().TutorialPause) {
            Vector2 mousepos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            if (Input.GetMouseButtonDown(0) && GetComponent<Collider2D>().OverlapPoint(mousepos))
            {
                if (output)
                {
                    CreateWire(transform.position, mousepos, WireType.Normal);
                    wiring = true;
                } else if (!output && curves.Count > 0)
                {
                    wiring = true;
                    curves[^1].end = null;
                    RemoveOff();
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
                if (Physics2D.OverlapCircle(mousepos, .1f, terminal))
                {
                    candidateTerminals = Physics2D.OverlapCircleAll(mousepos, 0.35f, terminal);
                    candidateTerminals = Array.FindAll(candidateTerminals, (x) => !x.GetComponent<Terminal>().output);
                    if (candidateTerminals.Length > 0)
                    {
                        Terminal finalTerminal = candidateTerminals[0].GetComponent<Terminal>();
                        Vector3 TerminalPosition = candidateTerminals[0].transform.position;
                        foreach (Collider2D collider in candidateTerminals)
                        {
                            if ((collider.transform.position - (Vector3)mousepos).magnitude <= (TerminalPosition - (Vector3)mousepos).magnitude) {
                                finalTerminal = Array.Find(collider.gameObject.GetComponents<Terminal>(), (x) => !x.output);
                                TerminalPosition = finalTerminal.transform.position;
                            }
                        }
                        curves[^1].curve.UpdateEnd(TerminalPosition, curves[^1].lr);
                        finalTerminal.high = curves[^1].high;
                        if (!finalTerminal.curves.Contains(curves[^1])) finalTerminal.curves.Add(curves[^1]);
                        curves[^1].end = finalTerminal;
                    } else
                    {
                        Destroy(curves[^1].gameObject);
                        if (curves[^1].start != this)
                        {
                            curves[^1].start.curves.Remove(curves[^1]);
                        }
                        curves.Remove(curves[^1]);
                    }
                }
                else
                {
                    Destroy(curves[^1].gameObject);
                    if (curves[^1].start != this)
                    {
                        curves[^1].start.curves.Remove(curves[^1]);
                    }
                    curves.Remove(curves[^1]);
                }
            }
        }
    }

    public void Free()
    {
        foreach (Wire i in curves)
        {
            if (output)
            {
                i.end.curves.Remove(i);
            }
            else i.start.curves.Remove(i);
            Destroy(i.gameObject);
        }
        curves.Clear();
    }

    public void CreateWire(Vector3 s, Vector3 e, WireType type)
    {
        GameObject tempWire = Instantiate(wire);
        tempWire.transform.parent = transform;
        tempWire.GetComponent<Wire>().Init(s, e, this);
        curves.Add(tempWire.GetComponent<Wire>());
    }

    public Wire ReturnWire()
    {
        GameObject tempWire = Instantiate(wire);
        tempWire.transform.parent = transform;
        curves.Add(tempWire.GetComponent<Wire>());
        return tempWire.GetComponent<Wire>();
    }
}