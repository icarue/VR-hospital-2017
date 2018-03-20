using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningHallway : Ambient
{
    float RandomWaitForSeconds;
	public override void StartAmb()
    {
        RandomWaitForSeconds = Random.Range(5, 8);
        //AUDIO
        AudioController.instance.PLAY(AudioController.instance.AUDIO.RunningOutside, TYPE.AMBIENT, 0.3f);
        StartCoroutine("WaitAndDeactivate");
    }

    IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(RandomWaitForSeconds);
        endAmb();
    }

}
