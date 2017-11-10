using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceControl : MonoBehaviour {

    private AudioSource aud;

    private float currentUpdateTime = 0f;
    private float clipLoudness;
    public float updateStep = 0.1f;


    private float[] clipSampleData;
    public int sampleDataLength = 1024;
	// Use this for initialization
	void Start () {

        aud = GetComponent<AudioSource>(); 
        aud.clip = Microphone.Start(Microphone.devices[0], true, 1, 44100);


        clipSampleData = new float[sampleDataLength];
	}
	
	// Update is called once per frame
	void Update () {

        if (Microphone.IsRecording(Microphone.devices[0])){
            //Debug.Log("Recording right now");
        }


        //Only update the volume every 10ms 
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            aud.clip.GetData(clipSampleData, aud.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            foreach (float sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
            Debug.Log("Clip loudness:" + clipLoudness);
        }
	}
}
