using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSideMonster : Monster {

    enum MonsterStages
    {
        AudioSetup,
        ShowMonster,
        InteractiveMonster,
        HideMonster,
        JumpScare,
        UserWin
    }
    MonsterStages currentStage;

    [SerializeField]
    float MoveUpY;
    Vector3 moveUpDestination;
    [Header("Move Up Speed")]
    [SerializeField]
    float speed;
    [SerializeField]
    [Header("Stay Up For")]
    float seconds;

    float currentSeconds;

    #region Mono
    private void Awake()
    {
        base.originalPosition = transform.position;    
    }

    private void Update()
    {
        Debug.Log(cameraObject.transform.localEulerAngles.y);
        switch (currentStage)
        {
            case MonsterStages.AudioSetup:
                AudioUnderBedScratch();
                break;
            case MonsterStages.ShowMonster:
                IsMonsterMovingUp(true);
                break;
            case MonsterStages.InteractiveMonster:
                InterActiveMonster();
                break;
            case MonsterStages.HideMonster:
                IsMonsterMovingUp(false);
                break;
            case MonsterStages.UserWin:
                playerWins();
                break;
            case MonsterStages.JumpScare:
                gameOver();
                break;

        }
    }
    #endregion

    #region MonsterStages
    void AudioUnderBedScratch()
    {
        if (!AudioController.instance.isPlaying(TYPE.MONSTER))
        {
            currentStage = MonsterStages.ShowMonster;
        }
    }

    void IsMonsterMovingUp(bool up)
    {
        Vector3 current = transform.position;
        Vector3 destination = up? moveUpDestination : base.originalPosition;
        
        transform.position = Vector3.MoveTowards(current, destination, speed * Time.deltaTime);
        if (transform.position == destination)
        {
            currentStage = up ? MonsterStages.InteractiveMonster : MonsterStages.UserWin;
        }

    }

    void InterActiveMonster()
    {
        currentSeconds -= Time.deltaTime;
        if (currentSeconds < 0)
        {
            currentStage = MonsterStages.HideMonster;
        }
        float cameraAngle = cameraObject.transform.localEulerAngles.y;
        if (cameraAngle > 36 && cameraAngle < 95)
        {
            currentStage = MonsterStages.JumpScare;
        }
        
    }

    #endregion

    #region Abstract
    protected override void setupMonsterToStartAttack()
    {
        currentStage = (MonsterStages)0;
        moveUpDestination = transform.position + new Vector3(0, MoveUpY, 0);
        currentSeconds = seconds;
        //AUDIO
        AudioController.instance.PLAY(AudioController.instance.AUDIO.UnderTheBed, TYPE.MONSTER);

    }

    public override void resetMonster()
    {
        transform.position = base.originalPosition;
    }

    protected override void setTimeUntilJumpScare()
    {
        timeUntilJumpScare = 0;
    }
    #endregion

}
