using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorMonster : MonoBehaviour {

    enum MonsterStages
    {
        MoveIn,
        UserInteraction,
        DoorOpen,
        JumpScare,
        UserWin
    }

    MonsterStages currentStage = MonsterStages.MoveIn;

    //User interaction
    float timeForCurtainsToStayOpen;
    float currentTimeToStayOpen;
    float timeForUserToLose;

    //Curtains
    Curtains curtain;

    //Door
    doorOpen door;
    float openDoorAngle = -90;
    float timeForDoorToOpen;

    //Jumpscare
    float timeUntilJumpScare;

    private void Awake()
    {
        door = GameObject.Find("LeftDoorHinge").GetComponent<doorOpen>();
        curtain = GameObject.Find("LocalCurtain").GetComponent<Curtains>();
        //Curtains
        timeForCurtainsToStayOpen = Random.Range(5, 8);
        currentTimeToStayOpen = timeForCurtainsToStayOpen;
        timeForUserToLose = timeForCurtainsToStayOpen + Random.Range(3, 5);

        //Door
        timeForDoorToOpen = Random.Range(2, 4);
        timeUntilJumpScare = Random.Range(3, 5)+timeForDoorToOpen;
    }

    // Update is called once per frame
    void Update () {
        switch (currentStage)
        {
            case MonsterStages.MoveIn:
                monsterMoveIntoPosition();
                break;
            case MonsterStages.UserInteraction:
                userInteraction();
                break;
            case MonsterStages.DoorOpen:
                doorOpen();
                break;
            case MonsterStages.JumpScare:
                jumpScare();
                break;
            case MonsterStages.UserWin:
                userWin();
                break;
        }
	}

    void monsterMoveIntoPosition()
    {
        currentStage = MonsterStages.UserInteraction;
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
    }

    void userWin(){
        Destroy(gameObject);
    }

    void jumpScare(){
        timeUntilJumpScare -= Time.deltaTime;
        if (timeUntilJumpScare < 0){
            GameObject.Find("CameraObject").GetComponent<JumpScare>().startJumpScare();
            Destroy(gameObject);
        }
    }
}
