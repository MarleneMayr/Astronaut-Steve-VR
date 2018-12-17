using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour {

    public GameObject obj;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = obj.GetComponent<Animator>();
        anim.Play("slime_idle");
    }
    
}
