using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TruthTable = Scripts.Utility.TruthTable;
using UnityEngine.Windows;

public class Tester : MonoBehaviour
{
    private List<COutput> Outputs;
    private List<CInput> Inputs;

    

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        int idI = 0;
        foreach (GameObject obj in scene.GetRootGameObjects())
        {
            if (obj.GetComponent<COutput>() != null)
            {
                Outputs.Add(obj.GetComponent<COutput>());
                obj.GetComponent<COutput>().id = idI;
                idI++;
            }
            if (obj.GetComponent<CInput>())
            {
                Inputs.Add(obj.GetComponent<CInput>());
            }
        }
        TruthTable test = new TruthTable(2, (inputs) => { return inputs.Count(e => e) == 1; }, (inputs) => { return inputs.Count(e => e) == 1; });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartTesting()
    {
        foreach(COutput ouptur in Outputs) 
        {
            ouptur.testing = true;
        }
    }

    public void Result(bool value, int id)
    {
        
    }
}
