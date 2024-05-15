using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public RectTransform tooltip;
    public Vector3 offset;
    public Texture2D cursor;
    private bool _enabled = false;

    public TextMeshProUGUI[] texts;

    private string _name;
    public string Name
    {
        get => _name;
    }


    public bool tooltipEnabled
    {
        get => _enabled;
        set
        {
            tooltip.gameObject.SetActive(value);
            _enabled = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        texts = tooltip.GetComponentsInChildren<TextMeshProUGUI>();
        //Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        MoveUI();
    }

    public void MoveUI()
    {
        if (tooltipEnabled)
        {
            Vector3 pos = Input.mousePosition + offset;
            pos.z = 100;
            tooltip.position = Camera.main.ScreenToWorldPoint(pos);
        }
    }
}
