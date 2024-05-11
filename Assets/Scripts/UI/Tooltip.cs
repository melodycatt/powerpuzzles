using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public RectTransform tooltip;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveUI();
    }

    public void MoveUI()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = 100;
        tooltip.position = Camera.main.ScreenToWorldPoint(pos);
    }
}
