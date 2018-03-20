using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance = null;

	[SerializeField]
	private GameObject[] monsters;
    [SerializeField]
    private GameObject[] ambients;
    [Header("Fear increase")]
    [SerializeField]
	private float Rate;
    [SerializeField]
    private GameObject lamp;
    float secondsPassed = 0;

	float timerUntilNextMonster;
	bool monsterActivated = false;
    bool ambientActivated = false;

    #region MonoDevelop
    public void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);

        InitGame();
	}

	
	
	// Update is called once per frame
	void Update () {
        //If it's anything other than in game - nothing will happen
		if (GameStatus.instance.currentStatus != Status.InGame) { return; }

		if (!monsterActivated) {
			timerUntilNextMonster -= Time.deltaTime;

            if (timerUntilNextMonster < 0)
            {
                monsterActivated = true;
                int index = selectMonster();
                monsters[index].GetComponent<Monster>().launchAttack();
            }

            //Ambient shows up while waiting for monster
            if (!ambientActivated)
            {
                ambientActivated = true;
                int index = selectAmbient();
                ambients[index].GetComponent<Ambient>().StartAmb();
            }
        } else
        {
            increaseFear();   
        } 
	}

    #endregion

    #region setup

    // Use this for initialization
    void InitGame()
    {
        setVariables();
        setupDelegates();
    }

    void setVariables()
    {
        timerUntilNextMonster = Random.Range(5, 10);
        setMonsterGameObjectActive(false);
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


    #endregion

    #region Fear

    void increaseFear()
    {
        secondsPassed += Time.deltaTime;
        if (secondsPassed > Rate && 
            !lamp.GetComponent<BedSideLight>().isLightOn)
        {
            //Increase camera shake
            GetComponent<FearShakeController>().increaseCameraShake();
            secondsPassed = 0;
        }
    }
    #endregion

    #region Monsters
    void setTimeUntilNextMonster() {
		timerUntilNextMonster = Random.Range (5, 10);
	}

	void setMonsterActivatedFalse() {
        //AUDIO
        AudioController.instance.STOP(TYPE.MONSTER);
		monsterActivated = false;
		setTimeUntilNextMonster ();
	}

    //Randomizer that chooses the monster
	int selectMonster() {
		int max = monsters.Length;
        return 3;// Random.Range (0, max);

	}
    #endregion

    #region Ambient


    void setAmbientActivatedFalse()
    {
        AudioController.instance.STOP(TYPE.AMBIENT);
        ambientActivated = false;
    }

    int selectAmbient()
    {
        int max = ambients.Length;
        return Random.Range(0, max);
    }

    #endregion


    #region Game States
    public void StartGame() {
		//Set State
		GameStatus.instance.currentStatus = Status.InGame;
		UserInterfaceController.instance.PlayGame ();
		//AUDIO
		AudioController.instance.PLAY(AudioController.instance.AUDIO.StartGame,TYPE.UI,1.0f);
	}

	public void EndGame() {
		//Set the State
		GameStatus.instance.currentStatus = Status.EndGame;
        UserInterfaceController.instance.GameOver();
		//AUDIO
		AudioController.instance.STOPALL();
	}

	public void ResetScene() {
        // Game Controller
        reset();
        //Fear Shake Controller
        GetComponent<FearShakeController>().resetShake();
        //Game Status
        GameStatus.instance.currentStatus = Status.MainMenu;
        //UI Controller
        UserInterfaceController.instance.setupUI();
        //Timer Controller
        GetComponent<TimerController>().resetTime();
	}

    private void reset()
    {
        setMonsterActivatedFalse();
        setVariables();

        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].GetComponent<Monster>().resetMonster();
            monsters[i].SetActive(false);
        }
    }

    private void setMonsterGameObjectActive(bool set)
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].SetActive(set);
        }
    }
    #endregion
}
