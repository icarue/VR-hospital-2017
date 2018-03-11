using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour {
    //Beginning
    [SerializeField]
    int startTime;
    [SerializeField]
    int end;
    int currentTime;

    TextMesh textMesh;
    float time;
    [SerializeField]
    float RealLifeSecondsPerMinute;

    [SerializeField]
    GameObject timerText;

	// Use this for initialization
	void Start () {
        textMesh = timerText.GetComponent<TextMesh>();
	}

    void setup()
    {
        time = 0;
        currentTime = startTime;
    }
	
	// Update is called once per frame
	void Update () {
		if (GameStatus.instance.currentStatus != Status.InGame) {
			return;
		}

        time += Time.deltaTime;
        if (time > RealLifeSecondsPerMinute)
        {
            time = 0;
            incrementTime();
            setTextMesh();
        }
	}

    void setTextMesh()
    {
        textMesh.text = currentTime + ":00";
    }

    void incrementTime()
    {
        currentTime++;
        if (currentTime > 12)
        {
            currentTime = 1;
        }
    }

    public void resetTime()
    {
        setup();
        setTextMesh();
    }
}
