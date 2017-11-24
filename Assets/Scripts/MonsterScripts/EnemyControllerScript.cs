using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class EnemyControllerScript : MonoBehaviour {

	public EnemyBehaviorScript[] monsterArray;

	public float[,] timingArray; 
	//timings[2][5] is the timing of the 2nd monster on the 5th marker
	//If a marker is -1.0f, do not summon this monster. Else, summon this monster after x seconds
	//At every marker, some combination of monsters will be summoned with a delay. (delay can be 0)
	//Proceeds to next marker only when all previous marker's monsters are finished.

	public int activeMonsters;
	//At each marker, this will count the number of active monsters.
	//Finished monsters will count this down until it is 0, then script will continue.

	// Use this for initialization
	void Start () {
		//Checks if all monsters have an EnemyBehaviourScript
		/*for (int i = 0; i < monsterArray.Length; i++) {
			if (monsterArray [i].GetComponent<EnemyBehaviourScript> () == null)
				print ("Not a monster!");
				throw new UnityException ("This isn't a monster!");
		}*/

		//TODO: Find a way to input timings in a less sketchy and primitive way
		timingArray = new float[,] {
			{ 1.0f, 2.0f, 5.0f }, //Monster 1
			{0.0f, -1.0f, 0.0f } //Monster 2
		};

		//for each marker, summon the appropriate monsters. Then wait for all monsters to finish and iterate.

		StartCoroutine (summonMonsters (1));

	}

	IEnumerator summonMonsters(int marker){
		for (int i = 0; i < timingArray.GetLength (1); i++) {
			for (int j = 0; j < timingArray.GetLength (0); j++) {
				if (timingArray [j, i] >= 0.0f) {
					activeMonsters++;
					StartCoroutine (monsterArray [j].GetComponent<EnemyBehaviorScript> ().prepareAttack (timingArray [j, i]));
					
				}
			}
			//This waits until either all monsters are defeated or the player is ded like fred.
			yield return new WaitUntil (() => (activeMonsters == 0 || false)); //TODO: replace false with player failure state

			if (activeMonsters <= 0) {
				//yield return null;
			} else  //Player failure
				yield return null;
		}


	}

}
