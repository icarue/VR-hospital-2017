using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInput : MonoBehaviour {

	// Use this for initialization
	void Awake () {
#if UNITY_EDITOR
        GetComponent<SmoothMouseLook>().enabled = true;
#else
        GetComponent<RotateCamera>().enabled = true;
        GetComponent<ZoomIn>().enabled = true;
        GetComponent<Gyroscope>().enabled = true;
#endif
    }

}
