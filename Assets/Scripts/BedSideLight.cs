using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSideLight : MonoBehaviour {

    public bool isLightOn { get; private set; }
    [SerializeField]
    GameObject spotLight;

    private void OnMouseDown()
    {
        spotLight.SetActive(isLightOn);
        isLightOn = !isLightOn;
    }

}
