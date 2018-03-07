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
        if (isLightOn)
        {
            GameController.instance.GetComponent<FearShakeController>().resetShake();
        }
    }

}
