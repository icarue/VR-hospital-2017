using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class JumpScare : MonoBehaviour {
    bool endGame = false;

    Vector3 spawnLocation;
    Vector3 spawnRotation;

    Vector3 destination;

    float speedOfJumpScare = 50f;

    float Magnitude = 2f;
    float Roughness = 10f;
    float FadeInTime = 0.1f;

    [SerializeField]
    GameObject crawler;

    CameraShakeInstance shaker;

    void resetScene()
    {
        crawler.SetActive(false);
        spawnLocation = new Vector3(0, -9, 2.86f);
        spawnRotation = new Vector3(0, -90, 0);

        destination = spawnLocation;
        destination.y = -4.8f;
    }

    private void Start()
    {
        resetScene();
    }

    public void startJumpScare()
    {
        //Turn on crawler
        crawler.SetActive(true);

        //Set it's position/rotation
        crawler.transform.localPosition = spawnLocation;
        crawler.transform.localRotation = Quaternion.Euler(spawnRotation);
        endGame = true;

        //Shake the screen
        shakeEnemy();

        //Black Screen
		StartCoroutine("SetGameOverScreen");
	
        //Add Audio
        crawler.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update () {
        if (endGame)
        {
            float step = speedOfJumpScare * Time.deltaTime;
            crawler.transform.localPosition = Vector3.MoveTowards(crawler.transform.localPosition, destination, step);
            if (crawler.transform.localPosition == destination)
            {
                endGame = false;
                
            }
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(Vector3.zero), step);
        }
    }

	IEnumerator SetGameOverScreen() {
		yield return new WaitForSeconds (1);
        shaker.StartFadeOut(1f);
        resetScene();
        GameController.instance.EndGame ();
	}

    void shakeEnemy()
    {
        shaker = CameraShaker.Instance.StartShake(Magnitude, Roughness, FadeInTime);
    }
}
