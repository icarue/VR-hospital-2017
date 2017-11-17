using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour {

	float lightsOnDur = 0.05f;
	float lightsOffDur = 1f;
	bool isLightOn = false;
	float timer;
	Light lightBulb;

	void Start() {
		timer = lightsOffDur;
		lightBulb = gameObject.GetComponent<Light> ();
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			setTimer ();
			timer = isLightOn? lightsOnDur: lightsOffDur;

			switchLights ();
		}	
	}

	void setTimer() {
		//This timer has to change depening if it's lights on/off
		lightsOffDur = Random.Range(0.01f, 1f);
		lightsOnDur = Random.Range (0.05f, 0.5f);
	}

	void switchLights() {
		if (isLightOn)
			lightBulb.intensity = 5;
		else
			lightBulb.intensity = 0;

		isLightOn = !isLightOn;
	}
}
