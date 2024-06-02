using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class TruthTable : MonoBehaviour
{
    public Dictionary<bool[], bool[]> Table;

    public List<CInput> Inputs {
        get {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Inputs");
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
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Outputs");
            List<COutput> output = new();
            foreach (GameObject i in gameObjects)
            {
                output.Add(i.GetComponent<COutput>());
            }
            return output;
        }
    }

    public TruthTable(Dictionary<bool[], bool[]> table)
    {
        Table = table;
    }

    public IEnumerator Check()
    {
        bool superAllG = true;
        foreach (KeyValuePair<bool[], bool[]> t in Table)
        {
            for (int i = 0; i < Inputs.Count; i++)
            {
                Inputs[i].high = t.Key[i];
            }
            yield return new WaitForSeconds(1);
            for (int i = 0; i < Outputs.Count; i++)
            {
                superAllG = Outputs[i].high != t.Value[i];
            }
        }
        yield return superAllG;
    }
}