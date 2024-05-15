using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI test;
    public string[] values = { "e", "mememe" };
    public int valI = 0;
    public bool placing = false;
    public bool hover = false;
    public List<KeyValuePair<int, GameObject>> Objects = new();
    public CameraUtil cameraUtil;
    // Start is called before the first frame update
    void Start()
    {
        cameraUtil = Camera.main.GetComponent<CameraUtil>();
        Objects.Add(new(10, Resources.Load<GameObject>("OR")));
        Objects.Add(new(15, Resources.Load<GameObject>("AND")));
        Objects.Add(new(15, Resources.Load<GameObject>("XOR")));
        Objects.Add(new(5, Resources.Load<GameObject>("NOT")));
        Objects.Add(new(15, Resources.Load<GameObject>("NOR")));
        Objects.Add(new(20, Resources.Load<GameObject>("NAND")));
        Objects.Add(new(20, Resources.Load<GameObject>("XNOR")));
    }

    // Update is called once per frame
    void Update()
    {
        if (placing && Input.GetMouseButtonDown(0) && !hover && cameraUtil.bits - Objects[valI].Key >= 0)
        {
            GameObject temp = Instantiate(Objects[valI].Value);
            cameraUtil.bits -= Objects[valI].Key;
            temp.GetComponent<holdable>().shop = this;
            temp.transform.position = new(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }

    public void Switch(int pee)
    {
        placing = true;
        valI = (valI + 1) % Objects.Count;
    }

    public void Select(int valI)
    {
        placing = true;
        this.valI = (valI) % Objects.Count;
    }

    public void Off()
    {
        placing = false;
    }

    public void Set_Hover(bool value)
    {
        hover = value;
    }
}
