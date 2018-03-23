using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour {

	[SerializeField]
	GameObject MenuUI;
	[SerializeField]
	GameObject GameUI;
	[SerializeField]
	GameObject GameOverUI;


	[SerializeField]
	GameObject GameOverScreen;
	[SerializeField]
	GameObject GameOverPanel;

	[SerializeField]
	GameObject GameOverWinTitle;
	[SerializeField]
	GameObject GameOverLoseTitle;
	bool didWin;

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

	void Start()
    {
        setupUI();
    }

    void OnLevelWasLoaded(int level)
    {
        setupUI();
    }

    public void PlayGame() {
        setupUI();
    }

	public void GameOver(bool win) {
        setupUI();
		didWin = win;
        GameOverScreen.SetActive(true);
        StartCoroutine ("SetPanelAfter");
	}

    IEnumerator SetPanelAfter()
    {
        yield return new WaitForSeconds(2);
        GameOverPanel.SetActive(true);

		GameOverWinTitle.SetActive (didWin);
		GameOverLoseTitle.SetActive (!didWin);

    }

    void disableGameOver()
    {
        GameOverPanel.SetActive(false);
        GameOverScreen.SetActive(false);
    }

    public void setupUI() {
        disableAllUI();
        disableGameOver();
        switch (GameStatus.instance.currentStatus)
        {
            case Status.MainMenu:
                MenuUI.SetActive(true);
                break;
            case Status.InGame:
                GameUI.SetActive(true);
                break;
            case Status.EndGame:
                GameOverUI.SetActive(true);
                break;
            default:
                MenuUI.SetActive(true);
                break;
        }
    }

    void disableAllUI()
    {
        GameOverUI.SetActive(false);
        GameUI.SetActive(false);
        MenuUI.SetActive(false);
    }

    
}
