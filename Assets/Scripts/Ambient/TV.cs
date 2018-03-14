using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Ambient
{
    float waitTime;
    void OnEnable()
    {
        waitTime = Random.Range(5, 7);
    }
    public override void StartAmb()
    {
        gameObject.SetActive(true);
        StartCoroutine(waitAndDeactivate());
		//AUDIO
		AudioController.instance.PLAY(AudioController.instance.AUDIO.StaticTV,TYPE.AMBIENT,1.0f);
    }

    IEnumerator waitAndDeactivate()
    {
        yield return new WaitForSeconds(waitTime);
		//AUDIO
		AudioController.instance.STOP (TYPE.AMBIENT);
        endAmb();
        gameObject.SetActive(false);
    }
}
