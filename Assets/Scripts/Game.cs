using UnityEngine;
using UnityEngine.UI;
using System;
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
	[SerializeField] private Ship ship = null;
	[SerializeField] private Text nauticMilesText = null;
	[SerializeField] private CoinUI coinUi = null;
	[SerializeField] SpriteRenderer seaSprite = null;
	private bool gameInProgress = false;
	private ObstacleLevel currentObstacle = null;
	private int currentLevel = 0;
	private float currentSpeed = 4;
	private float currentMiles = 0;
	private GameData gameData = null;

	private static Game self = null;

	public int CurrentLevel {
		get { return currentLevel; }
		private set { currentLevel = value; }
	}

	public static bool GameInProgress {
		get {return self.gameInProgress;}
		set {self.gameInProgress = value;}
	}

	public static int GameCoins {
		get { return self.gameData.coins; }
		set { 
			self.gameData.coins = value;
			self.coinUi.UpdateText(self.gameData.coins);
		}
	}

	public static int GetRandomCoinValue() {
		switch (self.CurrentLevel) {
		case 1:
			return 1;
		case 2:
			return UnityEngine.Random.Range(1,3);
		}

		return UnityEngine.Random.Range (1, 4);
	}

	void Awake() {
		if (self == null) {
			self = this;
		}
	}

	void Start() {

		MemoryCard.Initiate ();

		gameData = (GameData) MemoryCard.Load ("game");

		if (gameData == null) {
			gameData = new GameData();
		}

		Screen.orientation = ScreenOrientation.Portrait;
		menuState = MenuState.MAIN_MENU;
		ShowMenu ();

		MedalSystem.Initiate ();
		menu.UpdateMedalsText ();

		coinUi.UpdateText (gameData.coins);

	}

	void Update() {

		menu.SetAnimationState ((int)menuState);

		if (gameInProgress) {
			currentMiles += Time.deltaTime * currentSpeed * 0.5f;
			nauticMilesText.text = currentMiles.ToString ("N0");
		}

		if (Input.GetKeyDown (KeyCode.G)) {
			NextLevel();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			gameData.highScore = 0;
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
		currentMiles = 0;
		ship.Live ();
		ship.GoDown ();
		NextLevel ();
	}
	
	public void NextLevel() {
		CurrentLevel++;
		currentSpeed = LevelMetadata.GetLevelSpeed(CurrentLevel);
		ColorSea(LevelMetadata.GetLevelSeaColor(CurrentLevel));
		StartMovement ();
		NextObstacle ();
	}

	public void NextObstacle() {
		int milestone = LevelMetadata.GetLevelMilestone (currentLevel);
		if (currentMiles >= milestone) {
			if(milestone >= 0) {
				NextLevel();
				return;
			}
		}
		if(currentObstacle != null) {
			currentObstacle.OnLevelExit ();	
		}
		currentObstacle = ObstacleLevel.ActivateRandomObstacle (currentLevel, currentSpeed);
		currentObstacle.OnLevelEnter();
	}

	public void SetGameOver() {
		StopMovement ();

		gameInProgress = false;
		menuState = MenuState.GAME_OVER;
		ship.Die ();

		if (currentMiles > gameData.highScore) {
			gameData.highScore = (int)currentMiles;
			menu.ActivateHighScore();
		}
		
		//give medals
		if (currentMiles >= 320) {
			MedalSystem.TotalPlatinum++;
			menu.SetMedalColor (new Color(203f/255f,251f/255f,1,1));
		} else if (currentMiles >= 180) {
			MedalSystem.TotalGold++;
			menu.SetMedalColor (new Color(1,237f/255f,0,1));
		} else if (currentMiles >= 100) {
			MedalSystem.TotalSilver++;
			menu.SetMedalColor (new Color(203f/255f,203f/255f,203f/255f,1));
		} else if (currentMiles >= 45) {
			MedalSystem.TotalBronze++;
			menu.SetMedalColor (new Color(218f/255f,103f/255f,0f,1));
		} else {
			menu.SetMedalColor(new Color(0f,0f,0f,0f));
		}
		
		menu.UpdateMedalsText ();
		GameCoins += (int)currentMiles / 2;
		
		menu.SetHighScore (gameData.highScore);
		menu.SetCurrentScore ((int)currentMiles);
		gameData.totalMiles += (int)currentMiles;

		MemoryCard.Save (gameData, "game");
	}

	public void ShowMenu() {
		StartMovement ();
		ship.Live ();
		ship.GoUp ();
		ColorSea (LevelMetadata.GetLevelSeaColor(1));
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
		SideLines.Speed = currentSpeed;
		Wave.Speed = currentSpeed;
	}

	private void StopMovement() {
		SideLines.Speed = 0;
		Wave.Speed = 0;
	}

	private void ColorSea(Color _color) {
		seaSprite.color = _color;
	}
}

[Serializable]
public class GameData {
	public int highScore = 0;
	public double totalMiles = 0;
	public int coins = 0;
}

