using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class capsuleinterro : MonoBehaviour
{
    public Transform harkster;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        harkster = transform.GetChild(0).transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        print(Input.GetAxis("Mouse X"));
        harkster.Rotate(Input.GetAxis("Mouse Y") * -5, Input.GetAxis("Mouse X") * 5, 0);
        harkster.rotation = Quaternion.Euler(harkster.rotation.eulerAngles.x, harkster.rotation.eulerAngles.y, 0);

        if (Input.GetKey(KeyCode.W)) {
            print(1000 * Time.deltaTime * harkster.forward.y);
            print(harkster.forward.y);
            
            rb.AddForce(new(0, 10000 * Time.deltaTime * harkster.forward.y, 0), ForceMode.Acceleration);
        }
        // a
    }
}
