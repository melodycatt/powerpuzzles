using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class CircuitGrid : MonoBehaviour
{
    //real
    public int blocksX = 100;
    private int blocksY;
    private int blockSize;
    private SpriteRenderer sr;
    //fake
    public static int width = 5;
    public static int height = 3;
    private static GameObject Empty;
    private static GameObject Switch;
    private static GameObject Wire;
    private static GameObject Lamp;

    public List<List<GameObject>> compObjects = Enumerable.Range(1, width).Select(i => new List<GameObject>()).ToList();
    private static Dictionary<CompTypes, GameObject> comptypes;
    public List<CompTypes> comps = Enumerable.Repeat(CompTypes.Empty, width * height).ToList();
    public enum CompTypes
    {
        Empty,
        Switch,
        Wire,
        Lamp
    }
    // Start is called before the first frame update
    void Start() {
        // snap grid
        sr = GetComponent<SpriteRenderer>();
        Camera cam = Camera.main;
        Vector3 min = sr.bounds.min;
        Vector3 max = sr.bounds.max;
        Vector3 screenMin = cam.WorldToScreenPoint(min);
        Vector3 screenMax = cam.WorldToScreenPoint(max);
        int screenWidth = (int)(screenMax.x - screenMin.x);
        int screenHeight = (int)(screenMax.y - screenMin.y);
        this.blockSize = screenWidth / blocksX;
        this.blocksY = screenHeight / blockSize;


        /*
        blockSize = 10;
        Empty = Resources.Load<GameObject>("GridSlot");
        Switch = Resources.Load<GameObject>("Switch");
        Wire = Resources.Load<GameObject>("Wire1");
        Lamp = Resources.Load<GameObject>("Lamp");
        comptypes = new(3) { { CompTypes.Empty, Empty }, { CompTypes.Switch, Switch }, { CompTypes.Wire, Wire }, { CompTypes.Lamp, Lamp } };

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject current = Instantiate(comptypes[comps[width * j + i]], new Vector3((i - Mathf.Floor(width / 2)) * 1.5f, (j - Mathf.Floor(height / 2)) * 1.5f, 0), Quaternion.identity);
                compObjects[i].Add(current);
                current.GetComponent<CComponent>().x = i;
                current.GetComponent<CComponent>().y = j;
                current.GetComponent<CComponent>().grid = this;
            }
        }
        foreach (List<GameObject> i in compObjects)
        {
            Debug.Log(compObjects.IndexOf(i));
            foreach(GameObject j in i)
            {
                Debug.Log(i.IndexOf(j));
                j.GetComponent<CComponent>().Init();
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
