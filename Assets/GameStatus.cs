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

    public Status currentStatus { get; private set; }

	// Use this for initialization
	void Start () {
        currentStatus = Status.MainMenu;
	}

    void setGameStatus(Status stat)
    {
        currentStatus = stat;
    }
	
	
}
