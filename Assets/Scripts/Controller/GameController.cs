using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;

	[SerializeField]
	private GameObject[] monsters;
    [SerializeField]
    private GameObject[] ambients;
	[SerializeField]
	private GameObject[] gameObjectsToActiveOnPlay;
    [SerializeField]
    private float secondsRateToIncreaseFear;
    [SerializeField]
    private GameObject lamp;
    float secondsPassed = 0;

	float timerUntilNextMonster;
	bool monsterActivated = false;
    bool ambientActivated = false;

    //Game Status
    GameStatus status;

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
        setVariables();
        setupDelegates();
        activateGameObjects();
    }

    void setVariables()
    {
        timerUntilNextMonster = Random.Range(5, 10);
        status = GetComponent<GameStatus>();
    }

    void setupDelegates()
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].GetComponent<Monster>().onPlayerWin += setMonsterActivatedFalse;
        }

        for (int i = 0; i < ambients.Length; i++)
        {
            ambients[i].GetComponent<Ambient>().onEnd += setAmbientActivatedFalse;
        }
    }

    //Any Deactivated gameobjects will be activated
	void activateGameObjects(){
		for (int i = 0; i < gameObjectsToActiveOnPlay.Length; i++) {
			gameObjectsToActiveOnPlay [i].SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
        //If it's anything other than in game - nothing will happen
        if (status.currentStatus != Status.InGame) { return; }

		if (!monsterActivated) {
			timerUntilNextMonster -= Time.deltaTime;

            if (timerUntilNextMonster < 0)
            {
                monsterActivated = true;
                int index = selectMonster();
                monsters[index].GetComponent<Monster>().launchAttack();
            }
        } else
        {
            increaseFear();   
        } 
	}

    void increaseFear()
    {
        secondsPassed += Time.deltaTime;
        if (secondsPassed > secondsRateToIncreaseFear && 
            !lamp.GetComponent<BedSideLight>().isLightOn)
        {
            //Increase camera shake
            GetComponent<FearShakeController>().increaseCameraShake();
            secondsPassed = 0;
        }
    }


	void setTimeUntilNextMonster() {
		timerUntilNextMonster = Random.Range (5, 10);
	}

	void setMonsterActivatedFalse() {
		monsterActivated = false;
		setTimeUntilNextMonster ();
	}

    void setAmbientActivatedFalse()
    {
        ambientActivated = false;
    }
	

    //Randomizer that chooses the monster
	int selectMonster() {
		int max = monsters.Length;
		return Random.Range (0, max);
	}
}
