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
    CameraShakeInstance shaker;

    private void Start()
    {
        shaker = new CameraShakeInstance(5, 5);
    }

    public void increaseCameraShake()
    {

        if (switchBetweenMagAndRough)
        {
            currentMag += 0.5f;
        } else
        {
            currentRough += 0.5f;
        }
        shaker.StartFadeOut(2f);
        shaker = CameraShaker.Instance.StartShake(currentMag, currentRough, 0.5f);
        switchBetweenMagAndRough = !switchBetweenMagAndRough;
    }

    public void resetShake()
    {
        currentMag = 0;
        currentRough = 0;
        shaker.StartFadeOut(2f);
    }
}
