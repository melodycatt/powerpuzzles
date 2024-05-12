using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TooltipInfo : MonoBehaviour
{
    public string Name;
    public string Description;
    public Collider2D trigger;
    public Tooltip tooltip;
    public Vector3 mousepos;
    public LayerMask tooltips;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<Collider2D>();
        tooltip = Camera.main.GetComponent<Tooltip>();
    }

    // Update is called once per frame
    void Update()
    {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (trigger.OverlapPoint(mousepos) && Input.GetKey(KeyCode.LeftControl) && !tooltip.tooltipEnabled)
        {
            tooltip.tooltipEnabled = true;
            tooltip.texts[0].text = Name;
            tooltip.texts[1].text = Description;
        }
        else if ((Physics2D.OverlapPoint(mousepos, tooltips) == null || !Input.GetKey(KeyCode.LeftControl)) && tooltip.tooltipEnabled)
        {
            tooltip.tooltipEnabled = false;
        };
    }
}
