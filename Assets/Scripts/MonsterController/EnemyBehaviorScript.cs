using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a script that will be the baseline for enemy behaviours. 
//Enemies must have a timer til they activate, a method to make them attack, and a way for them to state they are finished.
public abstract class EnemyBehaviorScript : MonoBehaviour {

	//float delay = 0.0f;
	//bool active = false;
	public GameObject controller;
	EnemyControllerScript controllerScript;

	// Use this for initialization
	void Start () {
		controllerScript = controller.GetComponent<EnemyControllerScript> ();
	}



	// Update is called once per frame
	//TODO: Use coroutines instead of this spinlock stuff
	void Update () {
		/*if (active) {
			if (delay > 0.0f) {
				delay -= Time.deltaTime;
			} else {
				active = false;
				launchAttack ();
			}
		}*/
	}

	//This prepares the attack
	public IEnumerator prepareAttack (float delay){
		yield return new WaitForSeconds(delay);
		launchAttack();
		declareFinished ();
		yield return null;
		//active = true;
	}

	//The attack itself. Huzzah!
	public abstract void launchAttack ();


	//When the monster is defeated, signal back to the controller that it's done.
	public void declareFinished (){
		controllerScript.activeMonsters--;

	}
}
