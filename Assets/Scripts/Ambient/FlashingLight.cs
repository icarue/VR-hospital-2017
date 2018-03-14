using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : Ambient {

    public float maxIntensity = 1f;
	float lightsOnDur = 0.05f;
	float lightsOffDur = 1f;
	bool isLightOn = false;
	float timer;
	Light lightBulb;

    //Ambient Properties
    float waitTime;

	void Awake() {
		timer = lightsOffDur;
		lightBulb = gameObject.GetComponent<Light> ();
    }

    private void OnEnable()
    {
        waitTime = Random.Range(5, 7);
    }

    public override void StartAmb()
    {
        gameObject.SetActive(true);
		//AUDIO
		AudioController.instance.PLAY (AudioController.instance.AUDIO.BrokenLight, TYPE.AMBIENT, 1.0f);
    }

    IEnumerator waitAndDeactivate()
    {
        yield return new WaitForSeconds(waitTime);
		//AUDIO
		AudioController.instance.STOP(TYPE.AMBIENT);
        endAmb();
        gameObject.SetActive(false);
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
			lightBulb.intensity = maxIntensity;
		else
			lightBulb.intensity = 0;

		isLightOn = !isLightOn;
	}
}
