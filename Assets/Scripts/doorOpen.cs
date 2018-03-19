using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class doorOpen : MonoBehaviour {

    public readonly int closedDoorAngle = 0;

    //Bathroom Door properties
    public float openDoorAngle;
    public float glitchChance;
    public bool canInteractWithDoor { get; private set; }
    public bool userStillPlaying { get; private set; }
    public float loseDoorAngle { get; private set; }

	//Door interaction properties
	private float glitchDoorCloseDuration = 0.5f;

    private void Awake()
    {
        setupDoor();
    }

    public void setupDoor()
    {
        userStillPlaying = true;
        loseDoorAngle = openDoorAngle + Random.Range(6, 10);
    }

    public void setUserLost() {
        userStillPlaying = false;
    }

    /*                  Scripted Events                         */
    //Bathroom monster activated - first time door open
    public void setDoorAngleWithDuration(float angle, float duration, Ease easey = Ease.InSine)
    {
        gameObject.transform.DOKill();
        gameObject.transform.DORotate(new Vector3(0.0f, angle, 0.0f), duration).SetEase(easey);
        canInteractWithDoor = false;

        if (angle == openDoorAngle && userStillPlaying)
        {
            Invoke("invokeInteractionToTrue", duration);
        }
    }


    /*                  User Interaction Events                 */
    //When user interacts with the door and tries to close it
    public void tryToCloseTheDoor() {
		//TOOD - Test this
        float change = Random.Range(0, 100);
        if (change < glitchChance)
        {
            //Unable to close door
            glitchToCertainAngle();
        } else
        {
            setDoorAngleWithDuration(closedDoorAngle, Random.Range(2,4));
			//AUDIO
			AudioController.instance.PLAY(AudioController.instance.AUDIO.DoorClose, TYPE.UI);
        }
    }

    void glitchToCertainAngle()
    {
        float almostClosedAngle = Random.Range(1, 3);
		//Fail to close door
		setDoorAngleWithDuration(almostClosedAngle,glitchDoorCloseDuration);
		StartCoroutine("setGlitchedDoorAngle");
    }

    void invokeDoorToOpenDoorAngle() {
        setDoorAngleWithDuration(openDoorAngle,Random.Range(2,4));
    }

	IEnumerator setGlitchedDoorAngle(){
		//Wait for Door to almost close
		yield return new WaitForSeconds (glitchDoorCloseDuration);
		//AUDIO
		AudioController.instance.PLAY(AudioController.instance.AUDIO.DoorCloseFail, TYPE.UI);
		float duration = Random.Range (2, 4);
		setDoorAngleWithDuration (openDoorAngle, duration);
	}

    void invokeInteractionToTrue()
    {
        canInteractWithDoor = true;
    }


}
