using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject crawler = Resources.Load("BathroomCrawler") as GameObject;
        GameObject.Instantiate(crawler);
	}

}
