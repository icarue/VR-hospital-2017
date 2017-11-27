﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour {


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
            Debug.Log("Press down");
            anim.Play("curtainOpening");

        }

        if (Input.GetMouseButtonUp(0))
        {
            anim.Play("curtainClosing");
        }
    }
}