﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;

	[SerializeField]
	private GameObject[] monsters;

	float timerUntilNextMonster;
	bool monsterActivated = false;

	public void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != null)
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
		timerUntilNextMonster = Random.Range (5, 10);
		for (int i = 0; i < monsters.Length; i++) {
			monsters [i].GetComponent<Monster> ().onPlayerWin += setMonsterActivatedFalse;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!monsterActivated) {
			timerUntilNextMonster -= Time.deltaTime;
		} 

		if (timerUntilNextMonster < 0 && !monsterActivated) {
			monsterActivated = true;
			int index = selectMonster ();
			monsters [index].GetComponent<Monster> ().launchAttack ();
		}
	}

	void setTimeUntilNextMonster() {
		timerUntilNextMonster = Random.Range (5, 10);
	}

	void setMonsterActivatedFalse() {
		monsterActivated = false;
		setTimeUntilNextMonster ();
	}
		
	int selectMonster() {
		int max = monsters.Length;
		return Random.Range (0, max);
	}
}