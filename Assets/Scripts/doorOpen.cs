using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class doorOpen : MonoBehaviour {

	float rotationAngle = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			setAngleDur (90f, 2f);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			setAngleDur (0f, 0.2f, Ease.InSine);
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			setAngleSpeed (90f, 45f);
		} else if (Input.GetKeyDown (KeyCode.E)) {
			setAngleSpeed (0f, 450f, Ease.InSine);
		}



	}

	//Tween with a set duration
	public void setAngleDur(float angle, float duration, Ease easey = Ease.OutSine){
		//rotationAngle = angle;
		gameObject.transform.DOKill();
		gameObject.transform.DORotate (new Vector3 (0.0f, angle, 0.0f), duration).SetEase(easey);

		Debug.Log (gameObject.transform.localEulerAngles.y);

	}

	//Tween with speed in deg per second
	public void setAngleSpeed(float angle, float speed, Ease easey = Ease.OutSine){
		//rotationAngle = angle;
		gameObject.transform.DOKill();
		gameObject.transform.DORotate (new Vector3 (0.0f, angle, 0.0f), Mathf.Abs(gameObject.transform.localEulerAngles.y-angle)/speed).SetEase(easey);

		Debug.Log (gameObject.transform.localEulerAngles.y);

	}


}
