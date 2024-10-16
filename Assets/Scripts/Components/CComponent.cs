using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class CComponent : MonoBehaviour {
    public int x;
    public int y;
    protected bool _high = false;
    public virtual bool high
    {
        get => _high;
        set
        {
            _high = value;
            Debug.Log("some component got a value");
            Debug.Log(value);
        }
    }
    protected Sprite defaultSprite;
    protected SpriteRenderer sprite;
    public CircuitGrid grid;
    public List<CComponent> neighbours = new(4);
    public Terminal[] terminals => GetComponentsInChildren<Terminal>();
    public Terminal[] inputs => Array.FindAll(terminals, (x) => !x.output && x is RealTerminal y);
    public Terminal[] outputs => Array.FindAll(terminals, (x) => x.output && x is RealTerminal y);
    public holdable holdable;

    public holdable.State state
    {
        get {
            if (holdable == null) return holdable.State.Pinned;
            else return holdable.state;
        }
    } 

    public void Start()
    {
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        holdable = GetComponent<holdable>();
        defaultSprite = sprite.sprite;
        high = false;
        StartCoroutine(UpdateNode());
    }

    public virtual IEnumerator UpdateNode()
    {
        while (true) {
            Array.ForEach(outputs, (x) => {
                if (x.high != high && !Camera.main.GetComponent<CameraUtil>().TutorialPause) x.high = high;
            });
            yield return new WaitForSeconds(Camera.main.GetComponent<CameraUtil>().UpdateSpeed);
        }
    }

    public virtual void Update()
    {
        if (!Camera.main.GetComponent<CameraUtil>().TutorialPause) {
            Vector2 mousepos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            if (Input.GetMouseButton(1) && state == holdable.State.Pinned && GetComponent<Collider2D>().OverlapPoint(mousepos))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    transform.Rotate(new Vector3(0, 0, -90 * Time.deltaTime));
                    foreach (Terminal t in gameObject.GetComponentsInChildren<Terminal>())
                    {
                        t.Free();
                    }
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    if (Mathf.Round(transform.rotation.eulerAngles.z / 90) * 90 == transform.rotation.eulerAngles.z) transform.Rotate(new Vector3(0, 0, -90));
                    else transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Round(transform.rotation.eulerAngles.z / 90) * 90));
                    foreach (Terminal t in gameObject.GetComponentsInChildren<Terminal>())
                    {
                        t.Free();
                    }
                }
            }
        }
    }

    public virtual void Init()
    {
        if (y >= CircuitGrid.height - 1) neighbours.Add(null);
        else neighbours.Add(grid.compObjects[x][y + 1].GetComponent<CComponent>());
        if (x >= CircuitGrid.width - 1) neighbours.Add(null);
        else neighbours.Add(grid.compObjects[x + 1][y].GetComponent<CComponent>());
        if (y < 1) neighbours.Add(null);
        else neighbours.Add(grid.compObjects[x][y - 1].GetComponent<CComponent>());
        if (x < 1) neighbours.Add(null);
        else neighbours.Add(grid.compObjects[x - 1][y].GetComponent<CComponent>());

    }

}
