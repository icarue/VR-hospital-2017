using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour {

    string curtainClosing = "CurtainClose";
    string curtainOpening = "OpenCurtain";
    Animator anim;
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
        Debug.Log("Curtain click");
        anim.Play(curtainClosing);
        isCurtainOpen = true;
    }

    private void OnMouseExit()
    {
        anim.Play(curtainOpening);
        isCurtainOpen = false;
    }

    private void OnMouseUp()
    {
        anim.Play(curtainOpening);
        isCurtainOpen = false;
    }

}
