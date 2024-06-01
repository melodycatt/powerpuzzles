using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
public class TextWobble : MonoBehaviour
{
    
    public float speed = 1;
    public float max = 10;

    public decimal y = 90;
    public int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new(0,0, (float)((decimal)max * DecimalMath.Sin(y * DecimalMath.Pi / 180) * (decimal)Time.deltaTime)));
    }

    void FixedUpdate() {
        y += 1m * (decimal)speed;
        if (y == 360) {
            y = 0;
        }
    }
}
