using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSideLight : MonoBehaviour {

    public bool isLightOn { get; private set; }
    [SerializeField]
    GameObject spotLight;

    private void Start()
    {
        isLightOn = false;
    }

    private void OnMouseDown()
    { 
        isLightOn = !isLightOn;
        spotLight.SetActive(isLightOn);
		if (isLightOn) {
			GameController.instance.GetComponent<FearShakeController> ().resetShake ();
			//AUDIO
			AudioController.instance.PLAY (AudioController.instance.AUDIO.SwitchOnLight, TYPE.UI, 1.0f);
		} else {
			//AUDIO
			AudioController.instance.PLAY(AudioController.instance.AUDIO.SwitchOffLight,TYPE.UI,1.0f);
		}
			
    }

}
