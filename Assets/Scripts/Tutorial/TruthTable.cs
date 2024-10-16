using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TruthTable : MonoBehaviour
{
    public Dictionary<bool[], bool[]> Table;

    public Image tick;
    public Image cross;
    public Button test;

    public List<CInput> Inputs {
        get {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Input");
            List<CInput> output = new();
            foreach (GameObject i in gameObjects)
            {
                output.Add(i.GetComponent<CInput>());
            }
            return output;
        }
    }
    public List<COutput> Outputs
    {
        get
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Output");
            List<COutput> output = new();
            foreach (GameObject i in gameObjects)
            {
                output.Add(i.GetComponent<COutput>());
            }
            return output;
        }
    }

    public void StartCheck() {
        StartCoroutine(Check());
    }

    public IEnumerator Check()
    {
        bool superAllG = true;
        foreach (KeyValuePair<bool[], bool[]> t in Table)
        {
            for (int i = 0; i < Inputs.Count; i++)
            {
                print(t.Key[i]);
                Inputs[i].high = t.Key[i];
            }
            yield return new WaitForSeconds(1);
            for (int i = 0; i < Outputs.Count; i++)
            {
                print(Outputs[i].high);
                print(Outputs[i]);
                print(t.Value[i]);
                superAllG = Outputs[i].high == t.Value[i];
            }
        }
        yield return superAllG;
        Debug.Log("AllG Bruh");
        if (superAllG) {
            tick.enabled = true;
            test.interactable = false;
            StartCoroutine(SAG(superAllG));
        } else {
            cross.enabled = true;
            StartCoroutine(SAG(superAllG));
        }
    }

    public IEnumerator SAG(bool allg) {
        yield return new WaitForSeconds(1);
        if (allg) {
            tick.enabled = false;
        } else {
            cross.enabled = false;
        }
        yield return 0;
    }
}
