using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : doorOpen {

    private void OnMouseDown()
    {
        if (canInteractWithDoor)
        {
            tryToCloseTheDoor();
        }
    }
}
