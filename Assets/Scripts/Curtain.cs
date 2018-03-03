using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtainmovement : MonoBehaviour
{
    public Animator anim;
    public float speed;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        anim.speed = speed;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("curtainAnimationOpen");

        }

        if (Input.GetMouseButtonUp(0))
        {
            anim.Play("curtainAnimationClose");
        }
    }
}


   