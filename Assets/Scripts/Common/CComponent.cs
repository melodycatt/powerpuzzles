using System;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
    protected Sprite defaultSprite;
    protected SpriteRenderer sprite;
    public CircuitGrid grid;
    public List<CComponent> neighbours = new(4);
    public Terminal[] terminals;
    public Terminal[] inputs => Array.FindAll(terminals, (x) => !x.output);

    public void Start()
    {
        terminals = GetComponentsInChildren<Terminal>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        defaultSprite = sprite.sprite;
        high = false;
        InvokeRepeating(nameof(UpdateNode), 0.05f, 0.05f);
    }

    public virtual void UpdateNode()
    {
        if (Array.Find(terminals, x => x.output) != null) Array.Find(terminals, x => x.output).high = high;
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
