using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour {


    public Animator anim;
    public float speed;
    public bool isCurtainOpen { get; private set; }

    // Use this for initialization
    void Start()
    {
        isCurtainOpen = false;
        anim = GetComponent<Animator>();

        anim.speed = speed;

    }

    private void OnMouseDown()
    {
        anim.Play("curtainOpening");
        isCurtainOpen = true;
    }

    private void OnMouseExit()
    {
        anim.Play("curtainClosing");
        isCurtainOpen = false;
    }

    private void OnMouseUp()
    {
        anim.Play("curtainClosing");
        isCurtainOpen = false;
    }
    /*
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Press down");
                anim.Play("curtainOpening");
                isCurtainOpen = true;

            }

            if (Input.GetMouseButtonUp(0))
            {
                anim.Play("curtainClosing");
                isCurtainOpen = false;
            }
        }
        */
}
