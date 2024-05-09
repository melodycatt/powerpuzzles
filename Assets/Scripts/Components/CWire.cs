using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWire : CPowerable
{
    private int type = 1;
    private Animator animator;
    public List<RuntimeAnimatorController> controllers;
    new void Start()
    {
        /*for (int i = 1; i < 12; i++ )
        {
            controllers.Add(Resources.Load<RuntimeAnimatorController>($"Wires/{i}b_anim.controller"));
        }*/
        base.Start();
        sprite.sprite = Resources.Load<Sprite>($"Wires/{type}a");
        defaultSprite = sprite.sprite;
        animator = transform.GetChild(0).GetComponent<Animator>();
        Debug.Log(animator);
    }

    new public void Init()
    {
        base.Init();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Wires/1b_anim.controller");
    }

    public override void updateNode(CComponent source, CComponent sender, bool remove)
    {
        Debug.Log("teehee");
        base.updateNode(source, sender, remove);
        animator.enabled = !animator.enabled;
        sprite.sprite = defaultSprite;
        Debug.Log("teehee3");
    }

 
}
