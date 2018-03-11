using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour {

	[SerializeField]
	GameObject MainMenuPanel;
	[SerializeField]
	GameObject GameUI;


	[SerializeField]
	GameObject GamePlayUI;
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
		setupPanels ();
	}

	public void PlayGame() {
		GameUI.SetActive (true);
		GamePlayUI.SetActive (true);
		MainMenuPanel.SetActive (false);
	}

	public void SetGameOvePanel() {
		GameUI.SetActive (true);
		GamePlayUI.SetActive (false);
		GameOverScreen.SetActive (true);
		StartCoroutine ("SetPanelAfter");
	}

	public void setupPanels() {
		MainMenuPanel.SetActive (true);
		GameUI.SetActive (false);
		GamePlayUI.SetActive (false);
		GameOverScreen.SetActive (false);
	}

	IEnumerator SetPanelAfter(){ 
		yield return new WaitForSeconds (2);
		GameOverUI.SetActive (true);
	}
}
