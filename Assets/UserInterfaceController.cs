using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour {

	[SerializeField]
	GameObject MainMenuPanel;
	[SerializeField]
	GameObject GameUI;
	[SerializeField]
	GameObject GameOverUI;
	[SerializeField]
	GameObject GameOverScreen;

	public static UserInterfaceController instance;

	public void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != null)
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}

	void Start() {
		SetActivePanels ();
	}

	public void PlayGame() {
		SetActivePanels ();
	}

	public void SetActivePanels() {
		bool active = GameStatus.instance.isInGame ();
		MainMenuPanel.SetActive (!active);
		GameUI.SetActive (active);
	}

	public void SetGameOvePanel() {
		GameUI.SetActive (false);
		StartCoroutine ("FadeInPanels");
	}


	IEnumerator FadeInPanels(){
		yield return new WaitForSeconds (2);
		GameOverScreen.SetActive (true);
		GameOverUI.SetActive (true);
	}
}
