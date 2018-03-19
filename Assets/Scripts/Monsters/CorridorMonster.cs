using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorMonster : Monster {

    enum MonsterStages
    {
		UserInteraction,
        DoorOpen,
        JumpScare,
        UserWin
    }

	MonsterStages currentStage;

    //User interaction
    float timeForCurtainsToStayOpen;
    float currentTimeToStayOpen;
    float timeForUserToLose;

    //Curtains
	[SerializeField]
	private GameObject localCurtain;
    Curtains curtain;

    //Door
	[SerializeField]
	private GameObject LeftDoorHinge;
    doorOpen door;
    float openDoorAngle = -40;
    float timeForDoorToOpen;

    //Monster Object
    [SerializeField]
    GameObject monster;

    private void Awake()
    {
        base.originalPosition = transform.position;
        door = LeftDoorHinge.GetComponent<doorOpen>();
        curtain = localCurtain.GetComponent<Curtains>();
    }

	protected override void setupMonsterToStartAttack ()
	{
		//Curtains
		timeForCurtainsToStayOpen = UnityEngine.Random.Range(5, 8);
		currentTimeToStayOpen = timeForCurtainsToStayOpen;
		timeForUserToLose = timeForCurtainsToStayOpen + UnityEngine.Random.Range(3, 5);

		//Door
		timeForDoorToOpen = UnityEngine.Random.Range(2, 3);

		currentStage = MonsterStages.UserInteraction;
	}

	protected override void setTimeUntilJumpScare(){
		timeUntilJumpScare = UnityEngine.Random.Range(0, 2)+timeForDoorToOpen;
	}
		
    // Update is called once per frame
    void Update () {
        switch (currentStage)
        {
            case MonsterStages.UserInteraction:
                userInteraction();
                break;
            case MonsterStages.DoorOpen:
                doorOpen();
                break;
            case MonsterStages.JumpScare:
				gameOver();
                break;
            case MonsterStages.UserWin:
				playerWins();
                break;
        }
	}
		
    void userInteraction()
    {
        if(curtain.isCurtainOpen){
            currentTimeToStayOpen -= Time.deltaTime;   
        } else {
            currentTimeToStayOpen = timeForCurtainsToStayOpen;
            timeForUserToLose -= Time.deltaTime;
        }

        if (currentTimeToStayOpen < 0){
            //User Wins
            currentStage = MonsterStages.UserWin;
        }

        if (timeForUserToLose < 0) {
            //User Loses
            currentStage = MonsterStages.DoorOpen;
        }
    }

    void doorOpen(){
        door.setDoorAngleWithDuration(openDoorAngle,timeForDoorToOpen);
		//AUDIO
		AudioController.instance.PLAY(AudioController.instance.AUDIO.DoorCreak,TYPE.MONSTER);
        currentStage = MonsterStages.JumpScare;
        //only deactive mesh
        setModelActive(false);
    }

    public override void resetMonster()
    {
        //Set Door
        LeftDoorHinge.transform.eulerAngles = new Vector3(0,-90,0);

        //Turn on all Models again
        setModelActive(true);

        //Stage
        currentStage = MonsterStages.UserInteraction;
    }


    void setModelActive(bool on)
    {
        monster.SetActive(on);
    }

}
