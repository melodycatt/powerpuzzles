using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class holdable : MonoBehaviour
{
    private HingeJoint2D hinge;
    public Vector2 offset;
    public State state;
    private Rigidbody2D rb;
    public GameObject grid;

    public ShopManager shop;

    [Range(64.0f, 1024f)] public float BlockCount = 256;
    private Vector2 dim;
    private Vector2 size;
    private Vector2 count;

    public bool initd = false; 
    // Start is called before the first frame update
    void Start()
    {
        if (!initd) {
            float k = Camera.main.aspect;
            dim = new(Camera.main.pixelWidth, Camera.main.pixelHeight);
            count = new Vector2(BlockCount, BlockCount / k);
            // (256, 160)
            size = new Vector2(1.0f / count.x, 1.0f / count.y);
            Debug.Log(count);
            Debug.Log(size);
            grid = GameObject.Find("Grid");

            rb = GetComponent<Rigidbody2D>();
            Debug.Log(rb);
            initd = true;
        }
    }
    
    public void StartPublic() {
        if (!initd) {
            float k = Camera.main.aspect;
            dim = new(Camera.main.pixelWidth, Camera.main.pixelHeight);
            count = new Vector2(BlockCount, BlockCount / k);
            // (256, 160)
            size = new Vector2(1.0f / count.x, 1.0f / count.y);
            Debug.Log(count);
            Debug.Log(size);
            grid = GameObject.Find("Grid");

            rb = GetComponent<Rigidbody2D>();
            Debug.Log(rb);
            initd = true;
        }
    }

    public enum State
    {
        Free,
        Held,
        Pinned,
        Group
    }

    // Update is called once per frame
    void Update()
    {
        if (!Camera.main.GetComponent<CameraUtil>().TutorialPause) {            
            if (state != State.Group) {
                Vector2 mousepos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift) && !shop.hover)
                {
                    if (GetComponent<Collider2D>().OverlapPoint(mousepos) && !Camera.main.GetComponent<CameraUtil>().holding)
                    {
                        GetComponent<Collider2D>().isTrigger = false;
                        rb.gravityScale = 1;
                        state = State.Held;
                        hinge = gameObject.AddComponent<HingeJoint2D>();
                        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePos.z = transform.position.z;
                        float direction = Vector3.Dot((mousePos - transform.position).normalized, transform.right);
                        float distance = Mathf.Sqrt(Mathf.Pow((mousePos.x - transform.position.x), 2) + Mathf.Pow((mousePos.y - transform.position.y), 2)) / transform.localScale.x;
                        if (direction < 0)
                            distance *= -1;
                        hinge.autoConfigureConnectedAnchor = false;
                        hinge.anchor = new Vector2(distance, 0);
                        hinge.enableCollision = true;
                        hinge.connectedAnchor = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                        foreach (RealTerminal t in gameObject.GetComponentsInChildren<RealTerminal>())
                        {
                            t.Free();
                        }
                        Camera.main.GetComponent<CameraUtil>().holding = true;
                    }
                }
                else if ((Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) && state == State.Held)
                {
                    hinge.connectedAnchor = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                }
                if (Input.GetMouseButtonUp(0) && state == State.Held)
                {
                    if (grid.GetComponent<SpriteRenderer>().bounds.Contains(mousepos))
                    {
                        Pin(new(mousepos.x, mousepos.y, 0));
                    } else state = State.Free;
                    Destroy(hinge);
                    Debug.Log(state);
                    Camera.main.GetComponent<CameraUtil>().holding = false;
                }
            }
        }
    }

    public void Pin(Vector3 mousepos) {
        try {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            GetComponent<Collider2D>().isTrigger = true;
            state = State.Pinned;
            transform.SetPositionAndRotation(mousepos, Quaternion.identity);
        } catch (System.Exception e) {
            print(gameObject.name);
            print(initd);
            throw e;
        }
    }
}
