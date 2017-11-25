using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomMonster : MonoBehaviour {

    enum MonsterStage
    {
        DoorOpen,
        DoorCloseChance,
        DoorClosed,
        UserFailed
    }

    //Current stage
    MonsterStage currentStage = MonsterStage.DoorOpen;

    //Bathroom Door
    GameObject doorHinge;
    doorOpen door;

    //Open Door
    public float doorAngle;
    float doorOpenDuration;

    //Open more Door
    public float loseDoorAngle;

    //Seconds for user to close the door
    float secondsBeforeUserLose;

    //Seconds before jump scare starts
    float secondsBeforeJumpScareStarts;

    void Awake()
    { 
        //Door
        doorHinge = GameObject.Find("DoorHinge");
        door = doorHinge.GetComponent<doorOpen>();
        doorOpenDuration = Random.Range(3, 5);

        //Seconds for user to close the door
        secondsBeforeUserLose = Random.Range(5,8);

        //Seconds before jump scare starts
        secondsBeforeJumpScareStarts = Random.Range(5,10);
    }

    // Use this for initialization
    void Start () {
        door.setAngleDur(doorAngle, doorOpenDuration);
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
            case MonsterStage.UserFailed:
                userFailed();
                break;
        }
		
	}
    
    void finishedOpenDoor()
    {
        int currentAngle = (int)doorHinge.transform.localRotation.eulerAngles.y;
        if (currentAngle == (int)doorAngle)
        {
            currentStage = MonsterStage.DoorCloseChance;
        }
    }


    void doorClosingChance()
    {
        secondsBeforeUserLose -= Time.deltaTime;
        if (secondsBeforeUserLose < 0)
        {
            currentStage = MonsterStage.UserFailed;
            door.setAngleDur(loseDoorAngle, doorOpenDuration);
        }

        //Check if Door is closed
        if (doorHinge.transform.rotation.y == 0)
        {
            currentStage = MonsterStage.DoorClosed;
        }
    }

    void userPassed()
    {
        Destroy(gameObject);
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
