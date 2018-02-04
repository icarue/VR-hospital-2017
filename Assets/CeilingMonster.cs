using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingMonster : Monster {

    enum MonsterStages
    {
		OpenTile,
		UserInteraction,
		HideMonster,
		JumpScare,
		UserWin
    }

    protected override void setTimeUntilJumpScare()
    {
        throw new NotImplementedException();
    }

    protected override void setupMonsterToStartAttack()
    {
        throw new NotImplementedException();
    }
		
	
	// Update is called once per frame
	void Update () {
		
	}
}
