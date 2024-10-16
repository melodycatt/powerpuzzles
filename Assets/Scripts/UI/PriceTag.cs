using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceTag : MonoBehaviour
{
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle(bool toggle) {
        foreach (Transform i in transform) {
            i.gameObject.SetActive(toggle);
        }
        image.enabled = toggle;
    }
}
