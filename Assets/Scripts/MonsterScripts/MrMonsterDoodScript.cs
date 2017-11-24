using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrMonsterDoodScript : EnemyBehaviorScript {

	public override void launchAttack (){
		print("'sup, I'm Mr Monster Dood. " + gameObject.name);
	}

}
