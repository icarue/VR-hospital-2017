using System;
using UnityEngine;

public class CeilingMonster : Monster {
    [SerializeField]
    private GameObject CeilingTile;
    [SerializeField]
    private GameObject blanketObj;
    BlanketCover blanket;

    //Ceiling Tiles
    Vector3 CeilingOriginal;
    Vector3 CeilingTarget;
    [SerializeField]
    private Vector3 amountCeilingMoves;
    [SerializeField]
    private float ceilingSpeed;

    //Interaction time
    private float TimeUntilUserLoses;
    private float TimeUntilUserWins;
    private float currentTimeUntilWin;

    //Monster Properties
    Vector3 monsterOriginal;
    [SerializeField]
    Vector3 amountMonsterMoves;
    Vector3 monsterTarget;
    [SerializeField]
    private float monsterHideSpeed;


    enum MonsterStages
    {
		AudioSetup,
		OpenTile,
		UserInteraction,
		HideMonster,
		JumpScare,
		UserWin
    }

    MonsterStages currentStage ;
    #region Mono 
    void Awake()
    {
        blanket = blanketObj.GetComponent<BlanketCover>();
        base.originalPosition = transform.position;
        CeilingOriginal = CeilingTile.transform.localPosition;
    }

    #endregion


    protected override void setTimeUntilJumpScare()
    {
        timeUntilJumpScare = UnityEngine.Random.Range(2, 4);
    }

    protected override void setupMonsterToStartAttack()
    {
		currentStage = (MonsterStages)0;
        CeilingPositionSetup();
        TimersSetup();
        MonsterSetup();
		//Start Audio
		//AUDIO
		AudioController.instance.PLAY (AudioController.instance.AUDIO.RunningAttic, TYPE.MONSTER);
    }

    void CeilingPositionSetup()
    {
        CeilingTile.transform.localPosition = CeilingOriginal;
        CeilingTarget = CeilingOriginal + amountCeilingMoves;
    }

    void TimersSetup()
    {
        TimeUntilUserLoses = UnityEngine.Random.Range(5, 8);
        TimeUntilUserWins = UnityEngine.Random.Range(5, 8);
        currentTimeUntilWin = TimeUntilUserWins;
    }

    void MonsterSetup()
    {
        monsterOriginal = transform.position;
        monsterTarget = monsterOriginal + amountMonsterMoves;
    }


    // Update is called once per frame
    void Update () {
        switch (currentStage)
        {
			case MonsterStages.AudioSetup:
				audioRunningInAttic ();
				break;
            case MonsterStages.OpenTile:
                OpenTile();
                break;
            case MonsterStages.UserInteraction:
                UserInteraction();
                break;
            case MonsterStages.HideMonster:
                HideMonster();
                break;
            case MonsterStages.JumpScare:
                gameOver();
                break;
            case MonsterStages.UserWin:
                playerWins();
                break;
        }
	}

	#region Monster Stages
	void audioRunningInAttic () {
		if (!AudioController.instance.isPlaying (TYPE.MONSTER)) {
			currentStage = MonsterStages.OpenTile;
			//AUDIO - Run audio of tile sliding
			AudioController.instance.PLAY(AudioController.instance.AUDIO.SlidingTile, TYPE.MONSTER);
		}
	}

    void OpenTile()
    {
        Vector3 current = CeilingTile.transform.localPosition;
        CeilingTile.transform.localPosition = Vector3.MoveTowards(current, CeilingTarget, ceilingSpeed * Time.deltaTime);
        if (current == CeilingTarget)
        {
            //AUDIO
            AudioController.instance.STOP(TYPE.MONSTER);
            currentStage = MonsterStages.UserInteraction;
        }
    }

    protected override void playerWins()
    {
        Vector3 current = CeilingTile.transform.localPosition;
        CeilingTile.transform.localPosition = Vector3.MoveTowards(current, CeilingOriginal, ceilingSpeed * Time.deltaTime);
        if (CeilingTile.transform.localPosition == CeilingOriginal) { 
            base.playerWins();
        }
    }

    void UserInteraction()
    {
        if (blanket.isBlanketUp)
        {
            currentTimeUntilWin -= Time.deltaTime;
        } else
        {
            currentTimeUntilWin = TimeUntilUserWins;
            TimeUntilUserLoses -= Time.deltaTime;
        }

        if (currentTimeUntilWin < 0)
        {
            currentStage = MonsterStages.UserWin;
            //AUDIO
            AudioController.instance.PLAY(AudioController.instance.AUDIO.SlidingTile, TYPE.MONSTER);
        }

        if (TimeUntilUserLoses < 0)
        {
            currentStage = MonsterStages.HideMonster;
        }
    }

    void HideMonster()
    {
        Vector3 current = transform.position;
        transform.position = Vector3.MoveTowards(current, monsterTarget, 0.5f * monsterHideSpeed * Time.deltaTime);
        if (current == monsterTarget)
        {
            currentStage = MonsterStages.JumpScare;
        }
    }

	#endregion

    public override void resetMonster()
    {
        //Set Ceiling Tile
        CeilingTile.transform.localPosition = CeilingOriginal;


        //Reset Monster position
        transform.position = base.originalPosition;
        Debug.Log("Set Original Position");
        Debug.Log(base.originalPosition);

        //Set Current stage
		currentStage = (MonsterStages)0;
    }
}
