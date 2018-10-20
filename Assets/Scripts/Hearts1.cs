using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts1 : MonoBehaviour {
    int hearts1 = 3;
    public Animator animator;
    // Use this for initialization
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hearts1 = GameObject.Find("Tommy").GetComponent<CharacterScript>().hearts;
        
        if (hearts1 == 0)
        {

            animator.Play("Hearts 0");
        }
    }
}
