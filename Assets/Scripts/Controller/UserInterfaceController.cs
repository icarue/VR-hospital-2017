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
	[SerializeField]
	GameObject GameOverPanel;

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
		MainMenuPanel.SetActive (false);
		GameOverUI.SetActive (false);
	}

	public void SetGameOvePanel() {
		GameUI.SetActive (false);
		GameOverUI.SetActive (true);
		GameOverScreen.SetActive (true);

		StartCoroutine ("SetPanelAfter");
	}

	IEnumerator SetPanelAfter(){ 
		yield return new WaitForSeconds (2);
		GameOverPanel.SetActive (true);
	}

	public void setupPanels() {
		MainMenuPanel.SetActive (true);
		GameUI.SetActive (false);
		GameOverUI.SetActive (false);
	}


}
