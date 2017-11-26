using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class doorOpen : MonoBehaviour {

    public readonly int closedDoorAngle = 0;

    //Bathroom Door properties
    public float openDoorAngle;
    public float percentageChanceToGlitchDoor;
    public bool canInteractWithDoor { get; private set; }
    float timeTakenToCloseDoor = 1;
    public float loseDoorAngle { get; private set; }

    private void Awake()
    {
        loseDoorAngle = openDoorAngle + Random.Range(6, 10);

        if (percentageChanceToGlitchDoor > 1)
        {
            percentageChanceToGlitchDoor /= 100;
        }
    }


    /*                  Scripted Events                         */
    //Bathroom monster activated - first time door open
    public void setDoorAngleWithDuration(float angle, float duration, Ease easey = Ease.OutSine)
    {
        gameObject.transform.DOKill();
        gameObject.transform.DORotate(new Vector3(0.0f, angle, 0.0f), duration).SetEase(easey);
        canInteractWithDoor = false;
    }


    /*                  User Interaction Events                 */
    //When user interacts with the door and tries to close it
    public void tryToCloseTheDoor() {
        float change = Random.Range(0, 100);
        Debug.Log(change);
        if (change > percentageChanceToGlitchDoor)
        {
            glitchToCertainAngle();
        } else
        {
            setDoorAngleWithDuration(closedDoorAngle, timeTakenToCloseDoor);
        }
    }

    void glitchToCertainAngle()
    {
        float almostClosedAngle = Random.Range(1, 10);
        setDoorAngleWithDuration(almostClosedAngle,timeTakenToCloseDoor);
        Invoke("openDoorToSetAngle", timeTakenToCloseDoor);
    }


    void openDoorToSetAngle()
    {
        setDoorAngleWithDuration(openDoorAngle, 1);
    }

    private void Update()
    {
        int angle = (int)this.transform.rotation.eulerAngles.y;

        if (angle == (int)openDoorAngle) {
            canInteractWithDoor = true;
        }
    }


}
