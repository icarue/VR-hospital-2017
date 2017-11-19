using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour {

    GameObject crawler;
    bool endGame = false;

    Vector3 spawnLocation;
    Vector3 spawnRotation;

    Vector3 destination;

    public float speedOfJumpScare;

    private void Start()
    {
        spawnLocation = new Vector3(0, -9, 2.86f);
        spawnRotation = new Vector3(0, -90, 0);

        destination = spawnLocation;
        destination.y = -4.8f;

        crawler = Resources.Load("crawler") as GameObject;

        startJumpScare();
    }

    public void startJumpScare()
    {
        crawler = GameObject.Instantiate(crawler, gameObject.transform);
        crawler.transform.localPosition = spawnLocation;
        crawler.transform.localRotation = Quaternion.Euler(spawnRotation);
        endGame = true;
    }

    // Update is called once per frame
    void Update () {
        if (endGame)
        {
            float step = speedOfJumpScare * Time.deltaTime;
            crawler.transform.localPosition = Vector3.MoveTowards(crawler.transform.localPosition, destination, step);
        }
	}
}
