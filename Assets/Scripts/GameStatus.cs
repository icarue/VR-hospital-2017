using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status{
    MainMenu,
    Paused,
    InGame,
    EndGame,
}

public class GameStatus : MonoBehaviour {

	public static GameStatus instance;
	public Status currentStatus;

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
        currentStatus = Status.MainMenu;
	}

	public bool isInGame(){
		return currentStatus == Status.InGame;
	}
	
	
}
