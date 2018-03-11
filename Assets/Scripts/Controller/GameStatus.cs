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
	public Status currentStatus = Status.MainMenu;

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

	public bool isInGame(){
		return currentStatus == Status.InGame;
	}
}
