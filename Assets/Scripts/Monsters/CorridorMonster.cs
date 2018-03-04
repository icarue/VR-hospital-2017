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


	void Start() {
		door = LeftDoorHinge.GetComponent<doorOpen>();
		curtain = localCurtain.GetComponent<Curtains>();
	}

	protected override void setupMonsterToStartAttack ()
	{
		//Curtains
		timeForCurtainsToStayOpen = Random.Range(5, 8);
		currentTimeToStayOpen = timeForCurtainsToStayOpen;
		timeForUserToLose = timeForCurtainsToStayOpen + Random.Range(3, 5);

		//Door
		timeForDoorToOpen = Random.Range(2, 3);

		currentStage = MonsterStages.UserInteraction;
	}

	protected override void setTimeUntilJumpScare(){
		timeUntilJumpScare = Random.Range(0, 2)+timeForDoorToOpen;
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
        currentStage = MonsterStages.JumpScare;
		//only deactive mesh
        foreach (GameObject model in GameObject.FindGameObjectsWithTag("CorridorMonster")) {
            model.SetActive(false);
        }
    }

}
