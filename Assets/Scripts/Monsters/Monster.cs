using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a script that will be the baseline for enemy behaviours. 
//Enemies must have a timer til they activate, a method to make them attack, and a way for them to state they are finished.
public abstract class Monster : MonoBehaviour {

    protected Vector3 originalPosition;

    protected virtual void resetPosition()
    {
        transform.position = originalPosition;
    }

    //Camera Object
    [SerializeField]
	protected GameObject cameraObject;

	//Delegate
	public delegate void PlayerWins();
	public PlayerWins onPlayerWin;

	//Jumpscare
	protected float timeUntilJumpScare;

	//The attack itself. Huzzah!
	public virtual void launchAttack () {
		gameObject.SetActive (true);
		setupMonsterToStartAttack ();
		setTimeUntilJumpScare ();
	}

	protected virtual void gameOver() {
		timeUntilJumpScare -= Time.deltaTime;
		if (timeUntilJumpScare < 0){
			cameraObject.GetComponent<JumpScare>().startJumpScare();
			gameObject.SetActive (false);
		}
	}

	protected abstract void setTimeUntilJumpScare ();

	protected virtual void playerWins() {
		onPlayerWin ();
		gameObject.SetActive(false);
        resetMonster();

    }

	protected abstract void setupMonsterToStartAttack ();
    public abstract void resetMonster();
}
