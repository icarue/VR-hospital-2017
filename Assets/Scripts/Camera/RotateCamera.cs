using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RotateCamera : MonoBehaviour {
    float yRotation; 
    float xRotation;
    float zRotation;

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
    }

    public void ResetCamera() 
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
    }


    // Update is called once per frame
    void Update()
    {
        xRotation = -Input.gyro.rotationRateUnbiased.x;
        yRotation = -Input.gyro.rotationRateUnbiased.y;
        zRotation = Input.gyro.rotationRateUnbiased.z;
        transform.Rotate(xRotation, yRotation, zRotation);
    }


}


