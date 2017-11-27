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

    private void Awake()
    {
        timeForCurtainsToStayOpen = Random.Range(5, 8);
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
                break;
            case MonsterStages.JumpScare:
                break;
            case MonsterStages.UserWin:
                break;
        }
	}

    void monsterMoveIntoPosition()
    {
        currentStage = MonsterStages.UserInteraction;
    }

    void userInteraction()
    {
        timeForCurtainsToStayOpen -= Time.deltaTime;

    }
}
