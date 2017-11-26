using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class doorOpen : MonoBehaviour {

    protected int closedDoorAngle = 0;
    public float openDoorAngle;
    public float percentageChanceToGlitchDoor;

    public bool canInteractWithDoor = true;

    private void Awake()
    {
        if (percentageChanceToGlitchDoor > 1)
        {
            percentageChanceToGlitchDoor /= 100;
        }
    }

    //Tween with a set duration
    private void setAngleDur(float angle, float duration, Ease easey = Ease.OutSine){
		//rotationAngle = angle;
		gameObject.transform.DOKill();
		gameObject.transform.DORotate (new Vector3 (0.0f, angle, 0.0f), duration).SetEase(easey);
	}

	//Tween with speed in deg per second
	private void setAngleSpeed(float angle, float speed, Ease easey = Ease.OutSine){
		//rotationAngle = angle;
		gameObject.transform.DOKill();
		gameObject.transform.DORotate (new Vector3 (0.0f, angle, 0.0f), Mathf.Abs(gameObject.transform.localEulerAngles.y-angle)/speed).SetEase(easey);
	}

    public void tryToCloseTheDoor() {
        if (Random.Range(0, 1) > percentageChanceToGlitchDoor)
        {
            glitchToCertainAngle();
            canInteractWithDoor = false;
        }
    }

    protected void glitchToCertainAngle()
    {
        float timeTakenToCloseDoor = 1;
        float almostClosedAngle = Random.Range(1, 10);
        setAngleDur(almostClosedAngle,timeTakenToCloseDoor);
        Invoke("openDoorToSetAngle", timeTakenToCloseDoor);
    }


    protected void openDoorToSetAngle()
    {
        setAngleDur(openDoorAngle, 1);
    }

    private void Update()
    {
        int angle = (int)this.transform.rotation.eulerAngles.y;

        if (angle == closedDoorAngle) {
            canInteractWithDoor = false;
        }

        if (angle == (int)openDoorAngle) {
            canInteractWithDoor = true;
        }
    }


}
