using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GameWorthy;


public class Game : MonoBehaviour {
	private enum MenuState	{
		OFF,
		MAIN_MENU,
		IN_GAME,
		GAME_OVER,
		SETTINGS,
		MEDALS
	}

	private MenuState menuState = MenuState.OFF;

	[SerializeField] private Menu menu = null;
	[SerializeField] private SideLines sideLine = null;

	private bool gameInProgress = false;
	private int highestScore;
	private int totalScore;//sum of all games
	private ObstacleLevel currentObstacle = null;
	private int currentLevel = 0;
	private float currentSpeed = 4;

	public int CurrentLevel {
		get { return currentLevel; }
		private set { currentLevel = value; }
	}

	public bool GameInProgress {
		get {return gameInProgress;}
		set {gameInProgress = value;}
	}

	void Start() {
		//TODO:Create a saver class to deal with encryption saving data
		highestScore = PlayerPrefs.GetInt ("highest_score",0);
		totalScore = PlayerPrefs.GetInt ("total_score",0);


		Screen.orientation = ScreenOrientation.Portrait;
		menuState = MenuState.MAIN_MENU;
		ShowMenu ();

		MedalSystem.Initiate ();
		menu.UpdateMedalsText ();
	}

	void Update() {

		menu.SetAnimationState ((int)menuState);

		if (Input.GetKeyDown (KeyCode.G)) {
			NextLevel();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			highestScore = 0;
		}
	}

	public void StartGame() {
		if (gameInProgress) {
			return;
		}
		CurrentLevel = 0;
		gameInProgress = true;
		menuState = MenuState.IN_GAME;
		if (currentObstacle != null) {
			currentObstacle.ForceExit();
		}
		NextLevel ();
	}
	
	public void NextLevel() {
		CurrentLevel++;
		StartMovement ();
		NextObstacle ();
	}

	public void NextObstacle() {
		currentObstacle = ObstacleLevel.ActivateRandomObstacle (currentLevel, currentSpeed);
	}

	public void SetGameOver() {
		StopMovement ();
		gameInProgress = false;
		menuState = MenuState.GAME_OVER;

		if (currentLevel > highestScore) {
			highestScore = currentLevel;
			PlayerPrefs.SetInt("highest_score", highestScore);
			menu.ActivateHighScore();
		}
		
		//give medals
		if (currentLevel >= 45) {
			MedalSystem.TotalPlatinum++;
			menu.SetMedalColor (new Color(203f/255f,251f/255f,1,1));
		} else if (currentLevel >= 30) {
			MedalSystem.TotalGold++;
			menu.SetMedalColor (new Color(1,237f/255f,0,1));
		} else if (currentLevel >= 18) {
			MedalSystem.TotalSilver++;
			menu.SetMedalColor (new Color(203f/255f,203f/255f,203f/255f,1));
		} else if (currentLevel >= 6) {
			MedalSystem.TotalBronze++;
			menu.SetMedalColor (new Color(218f/255f,103f/255f,0f,1));
		} else {
			menu.SetMedalColor(new Color(0f,0f,0f,0f));
		}
		
		menu.UpdateMedalsText ();
		
		menu.SetHighScore (highestScore);
		menu.SetCurrentScore (currentLevel);
		totalScore += currentLevel;
		PlayerPrefs.SetInt ("total_score", totalScore);
	}

	public void ShowMenu() {
		StartMovement ();
		menuState = MenuState.MAIN_MENU;
		if (currentObstacle != null) {
			currentObstacle.ForceExit();
		}
	}

	public void ShowSettings() {
		menuState = MenuState.SETTINGS;
	}

	public void ShowMedals() {
		menuState = MenuState.MEDALS;
	}

	private void StartMovement() {
		sideLine.SetSpeed (currentSpeed - 1);
		Wave.Speed = currentSpeed;
	}

	private void StopMovement() {
		sideLine.SetSpeed (0);
		Wave.Speed = 0;
	}
}
