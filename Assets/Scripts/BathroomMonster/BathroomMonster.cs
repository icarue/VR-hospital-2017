using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomMonster : MonoBehaviour {

    enum MonsterStage
    {
        DoorOpen,
        DoorCloseChance,
        DoorClosed,
        MonsterMovesDown,
        UserFailed,
        
    }

    //Current stage
    MonsterStage currentStage = MonsterStage.DoorOpen;

    //Bathroom Door
    GameObject doorHinge;
    doorOpen door;

    //Open Door
    float doorOpenDuration;

    //Seconds for user to close the door
    public float minSecondsForUserToCloseDoor;
    public float maxSecondsForUserToCloseDoor;
    float secondsBeforeUserLose;

    //Disappearing properties
    float speedOfDisappearance = 10;
    Vector3 disappearDestination;
    //Seconds before jump scare starts
    float secondsBeforeJumpScareStarts;

    void Awake()
    {
        //Door
        doorHinge = GameObject.Find("DoorHinge");
        door = doorHinge.GetComponent<doorOpen>();
        doorOpenDuration = Random.Range(3, 5);

        //Seconds for user to close the door
        secondsBeforeUserLose = Random.Range(minSecondsForUserToCloseDoor, maxSecondsForUserToCloseDoor);

        //Disappearing bathroom monster disapearrance
        disappearDestination = transform.localPosition;
        disappearDestination.y = 15;


        //Seconds before jump scare starts
        secondsBeforeJumpScareStarts = Random.Range(5,8);
    }

    // Use this for initialization
    void Start () {
        door.setDoorAngleWithDuration(door.openDoorAngle, doorOpenDuration);
	}
	
	// Update is called once per frame
	void Update () {
        switch(currentStage)
        {
            case MonsterStage.DoorOpen:
                finishedOpenDoor();
                break;
            case MonsterStage.DoorCloseChance:
                doorClosingChance();
                break;
            case MonsterStage.DoorClosed:
                userPassed();
                break;
            case MonsterStage.MonsterMovesDown:
                monsterDisappears();
                break;
            case MonsterStage.UserFailed:
                userFailed();
                break;
        }
		
	}
    
    void finishedOpenDoor()
    {
        if (door.canInteractWithDoor)
        {
            currentStage = MonsterStage.DoorCloseChance;
        }
    }


    void doorClosingChance()
    {
        secondsBeforeUserLose -= Time.deltaTime;
        if (secondsBeforeUserLose < 0)
        {
            //User loses
            door.setUserLost();
            currentStage = MonsterStage.MonsterMovesDown;
            door.setUserLost();
            door.setDoorAngleWithDuration(door.loseDoorAngle, doorOpenDuration);
        }

        //Check if Door is closed
        if ((int)doorHinge.transform.rotation.eulerAngles.y == 0)
        {
            Debug.Log(secondsBeforeUserLose);
            //User able to close the door
            currentStage = MonsterStage.DoorClosed;
        }
    }

    void userPassed()
    {
        Destroy(gameObject);
    }


    void monsterDisappears()
    {
        float step = speedOfDisappearance * Time.deltaTime;
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, disappearDestination, step);

        if (gameObject.transform.localPosition == disappearDestination)
        {
            currentStage = MonsterStage.UserFailed;
        }
    }

    void userFailed()
    {
        secondsBeforeJumpScareStarts -= Time.deltaTime;
        if (secondsBeforeJumpScareStarts < 0)
        {
            GameObject.Find("CameraObject").GetComponent<JumpScare>().startJumpScare();
            Destroy(gameObject);
        }
    }
}
