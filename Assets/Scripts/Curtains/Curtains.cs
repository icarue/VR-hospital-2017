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
		//AUDIO
		AudioController.instance.PLAY(AudioController.instance.AUDIO.CurtainsClose,TYPE.UI);
        anim.Play(curtainClosing);
        isCurtainOpen = true;
    }

    private void OnMouseExit()
    {
		//AUDIO
		if (!AudioController.instance.isPlaying(TYPE.UI) && isCurtainOpen){
			AudioController.instance.PLAY(AudioController.instance.AUDIO.CurtainsOpen,TYPE.UI);
		}
        anim.Play(curtainOpening);
        isCurtainOpen = false;
    }

    private void OnMouseUp()
    {
		//AUDIO
		AudioController.instance.PLAY(AudioController.instance.AUDIO.CurtainsOpen,TYPE.UI);
        anim.Play(curtainOpening);
        isCurtainOpen = false;
    }

}
