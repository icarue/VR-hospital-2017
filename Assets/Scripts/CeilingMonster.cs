using System.Collections;
using System.Collections.Generic;
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
		OpenTile,
		UserInteraction,
		HideMonster,
		JumpScare,
		UserWin
    }

    MonsterStages currentStage ;

    void Awake()
    {
        CeilingOriginal = CeilingTile.transform.localPosition;
    }

    protected override void setTimeUntilJumpScare()
    {
        timeUntilJumpScare = Random.Range(2, 4);
    }

    protected override void setupMonsterToStartAttack()
    {
        MonsterStages currentStage = MonsterStages.OpenTile;
        CeilingPositionSetup();
        TimersSetup();
        MonsterSetup();
    }

    void CeilingPositionSetup()
    {
        CeilingTile.transform.localPosition = CeilingOriginal;
        CeilingTarget = CeilingOriginal + amountCeilingMoves;
    }

    void TimersSetup()
    {
        TimeUntilUserLoses = Random.Range(5, 8);
        TimeUntilUserWins = Random.Range(5, 8);
        currentTimeUntilWin = TimeUntilUserWins;
    }

    void MonsterSetup()
    {
        monsterOriginal = transform.position;
        monsterTarget = monsterOriginal + amountMonsterMoves;
    }

    void Start()
    {
        blanket = blanketObj.GetComponent<BlanketCover>();
    }

    // Update is called once per frame
    void Update () {
        switch (currentStage)
        {
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


    void OpenTile()
    {
        Vector3 current = CeilingTile.transform.localPosition;
        CeilingTile.transform.localPosition = Vector3.MoveTowards(current, CeilingTarget, ceilingSpeed * Time.deltaTime);
        if (current == CeilingTarget)
        {
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
}
