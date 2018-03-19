using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour {

    doorOpen door;

    private void Awake()
    {
        door = GameObject.Find("DoorHinge").GetComponent<doorOpen>();
    }

    private void OnMouseDown()
    {
        if (door.canInteractWithDoor && door.userStillPlaying)
        {
            //When completed, can't click this.
            door.tryToCloseTheDoor();
        }
    }
}
