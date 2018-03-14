using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour {

    string curtainClosing = "CurtainClose";
    string curtainOpening = "CurtainOpen";
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
        anim.Play(curtainOpening);
        isCurtainOpen = true;
    }

    private void OnMouseExit()
    {
        anim.Play(curtainClosing);
        isCurtainOpen = false;
    }

    private void OnMouseUp()
    {
        anim.Play(curtainClosing);
        isCurtainOpen = false;
    }

}
