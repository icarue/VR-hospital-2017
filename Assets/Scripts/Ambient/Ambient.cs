using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ambient : MonoBehaviour {

    //Delegate
    public delegate void EndAmbient();
    public EndAmbient onEnd;

    public abstract void StartAmb();
    protected virtual void endAmb() {
        onEnd();
    }
}
