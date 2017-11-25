using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class doorOpen : MonoBehaviour {

	public float rotationAngle;
	public float rotationalSpeed;

	//Tween with a set duration
	public void setAngleDur(float angle, float duration, Ease easey = Ease.OutSine){
		//rotationAngle = angle;
		gameObject.transform.DOKill();
		gameObject.transform.DORotate (new Vector3 (0.0f, angle, 0.0f), duration).SetEase(easey);
	}

	//Tween with speed in deg per second
	public void setAngleSpeed(float angle, float speed, Ease easey = Ease.OutSine){
		//rotationAngle = angle;
		gameObject.transform.DOKill();
		gameObject.transform.DORotate (new Vector3 (0.0f, angle, 0.0f), Mathf.Abs(gameObject.transform.localEulerAngles.y-angle)/speed).SetEase(easey);
	}

    public void glitchToCertainAngle(float angle, float speed)
    {
        
    }


}
