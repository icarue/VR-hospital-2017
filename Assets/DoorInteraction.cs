using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour {

    public GameObject doorHinge;
    public doorOpen door { set; get; }

    private void OnMouseDown()
    {
        Debug.Log("Pressing door");
         doorHinge.GetComponent<doorOpen>().setAngleDur(0, 4);
    }


    //TODO function to set the angle to a randomize 0 - 10 angle
    //Function shoudl also set the duration between 1-3

    //Door open should have function to "glitch out"

}
