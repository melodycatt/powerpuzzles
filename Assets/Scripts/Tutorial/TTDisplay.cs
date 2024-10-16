using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TTDisplay : MonoBehaviour
{
    public int Inputs;
    public int Outputs;
    public bool inLast;
    public bool outNext;

    // Start is called before the first frame update
    void Start()
    {
        // +14 for border, +15 for each column, +5 for each column - 1
        Color dg = new(0, 0.36f, 0.03f);
        Color32[] colors = new Color32[100 * 100];
        Array.Fill(colors, Color.white);
        for (int i = 0; i < 100; i++) {
            for (int j = i; j < i + 700; j += 100) {
                colors[j] = dg;
            }
        }
        for (int i = 9900; i < 10000; i++) {
            for (int j = i; j > i - 700; j -= 100) {
                colors[j] = dg;
            }
        }
        for (int i = 100; i <= 9800; i += 100) {
            for (int j = i; j < i + 7; j++) {
                colors[j] = dg;
            }
        }
        for (int i = 199; i <= 9899; i += 100) {
            for (int j = i; j > i - 7; j--) {
                colors[j] = dg;
            }
        }
        Texture2D bmp = new Texture2D(100, 100)
        {
            filterMode = FilterMode.Point
        };
        bmp.SetPixels32(colors);
        bmp.Apply();
        Sprite sprite = Sprite.Create(bmp, new Rect(0, 0, bmp.width, bmp.height), new(0.5f, 0.5f));
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
