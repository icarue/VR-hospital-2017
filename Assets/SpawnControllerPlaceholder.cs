using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControllerPlaceholder : MonoBehaviour {

    //1
    GameObject monster;
	// Use this for initialization
	void Start () {
        if (Random.Range(0, 100) % 2 == 0)
        {
            monster = Resources.Load("CorriderCrawler") as GameObject;
        }
        else
        {
            monster = Resources.Load("BathroomCrawler") as GameObject;

        }

        GameObject _monster = Instantiate(monster, monster.transform.position, monster.transform.rotation);
        this.enabled = false;
	}
	
}
