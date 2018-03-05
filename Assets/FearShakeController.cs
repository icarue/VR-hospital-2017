using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class FearShakeController : MonoBehaviour {

    [SerializeField]
    float maxMagnitude;
    [SerializeField]
    float maxRoughness;

    float currentMag = 0;
    float currentRough = 0;

    bool switchBetweenMagAndRough = false;
    private void Start()
    {
        increaseCameraShake();
    }

    public void increaseCameraShake()
    {
        if (switchBetweenMagAndRough)
        {
            currentMag += 0.1f;
        } else
        {
            currentRough += 0.1f;
        }
        CameraShaker.Instance.StartShake(currentMag, currentRough, 0.5f);
    }

    public void resetShake()
    {
        currentMag = 0;
        currentRough = 0;
        CameraShaker.Instance.StartShake(currentMag, currentRough, 2);
    }
}
