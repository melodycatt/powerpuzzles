using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using AYellowpaper.SerializedCollections;

public class Level : MonoBehaviour
{
    [SerializeField]
    [TextArea(3, 40)]
    string Orders;
    [SerializeField]
    int Bits;

    [SerializedDictionary("Inputs", "Outputs")]
    public SerializedDictionary<bool[], bool[]> Table = new () 
    {
        { 
            new bool[] { false, false}, 
            new bool[] { false } 
        },
        {
            new bool[] { false, true },
            new bool[] { true }
        },
        {
            new bool[] { true, false },
            new bool[] { true }
        },
        {
            new bool[] { true, true },
            new bool[] { true }
        },
    };

    public string[] hints = new string[] {
        "For the set button, try wiring the input to an OR gate, and then wiring the output of that OR gate back to its own input.\nThat way, once the OR gate receives a high input, it will power itself forever.\nNext, find a way to include the reset input so that it breaks that infinite loop.",
        "To break the loop, add some gates before the OR gate connects back to itself, so it doesn't directly connect to itself anymore. That way, you can interrupt the high signal with the reset input before it gets back to power the OR gate again.",
        "To make the reset button interrupt the high signal, first wire the OR gate to an input of an AND gate. Next make sure the other input is high UNLESS the reset input is high.",
        "To make the other input of the AND gate stay high unless the reset input is high, just wire the reset input to a NOT gate, and wire that to the AND gate."
    };
    public bool hinting;
    public int hintI;

    TextMeshProUGUI tmp;
    Image image;

    [SerializeField]
    Button hint;

    // Start is called before the first frame update
    void Start()
    {
        image = transform.GetChild(1).GetComponent<Image>();
        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        transform.localScale = new(0.5f, 0.5f, 0.5f);
        GetComponent<RectTransform>().anchoredPosition = new(-278, 0); 
        tmp.transform.localScale = new(1, 1, 1);
        tmp.GetComponent<RectTransform>().anchoredPosition = new(-1000, 500);
        tmp.margin = new(0,0, -780, 0);
        tmp.text = Orders;
    }

    public void Hint() {
        gameObject.SetActive(true);
        hinting = true;
        hintI++;
        hint.interactable = false;
        transform.localScale = new(0.25f, 0.25f, 0.25f);
        GetComponent<RectTransform>().anchoredPosition = new(-139, 0); 
        tmp.transform.localScale = new(2, 2, 2);
        tmp.GetComponent<RectTransform>().anchoredPosition = new(-2480, 0);
        tmp.margin = new(0,0, -960, 0);
        tmp.text = hints[hintI]; 
    }

    // Update is called once per frame
    void Update()
    {
        if (hinting) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                hinting = false;
                if (hintI < hints.Count() - 1) {
                    hint.interactable = true;
                    gameObject.SetActive(false);
                }
            }
        }

        else if(Input.GetKeyDown(KeyCode.Space)) {
            if (!hinting) {
                Camera.main.GetComponent<CameraUtil>().bits = Bits;
            }
            Camera.main.GetComponent<CameraUtil>().table.Table = Table;
            gameObject.SetActive(false);
        }
    }
}
