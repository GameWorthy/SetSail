using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameWorthy;

public class Game : MonoBehaviour {

	private enum MenuState	{
		OFF,
		MAIN_MENU,
		IN_GAME,
		GAME_OVER,
		SETTINGS,
		MEDALS,
		UPGRADE_SHOP
	}

	private MenuState menuState = MenuState.OFF;

	[SerializeField] private int START_AT_LEVEL = 0;
	
	[SerializeField] private Menu menu = null;
	[SerializeField] private Ship ship = null;
	[SerializeField] private Text nauticMilesText = null;
	[SerializeField] private CoinUI coinUi = null;
	[SerializeField] private SpriteRenderer seaSprite = null;
	[SerializeField] private LevelNotifier levelNotifier = null;
	[SerializeField] private Continue continueButton = null;
	[SerializeField] private UpgradeShop upgradeShop = null;
	[SerializeField] private GameObject[] upgradeNotifications = null;
	[SerializeField] private AudioClip[] gameOverClips = null;

	private bool gameInProgress = false;
	private ObstacleLevel currentObstacle = null;
	private int currentLevel = 0;
	private float currentSpeed = 4;
	private float currentMiles = 0;
	private GameData gameData = null;
	private bool isTransitioning = false;
	private AudioSource audioSource = null;

	private static Game self = null;

	public int CurrentLevel {
		get { return currentLevel; }
		private set { currentLevel = value; }
	}

	public float CurrentSpeed {
		get { return currentSpeed; }
		private set { currentSpeed = value; }
	}

	public float CurrentMiles {
		get { return currentMiles; }
		private set { currentMiles = value; }
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
		int total = 1;

		if (self.currentLevel >= UpgradesDB.GetTrippeCoinLevel (self.gameData.coinLevel)) {
			total = 3;
		}
		else if(self.currentLevel >= UpgradesDB.GetDoubleCoinLevel (self.gameData.coinLevel)) {
			total = 2;
		}

		return UnityEngine.Random.Range (1, total + 1);
	}

	public static Game GetSelf () {
		return self;
	}

	void Awake() {
		if (self == null) {
			self = this;
		}
	}

	void Start() {

		Application.targetFrameRate = 60;

		MemoryCard.Initiate ();

		gameData = (GameData) MemoryCard.Load ("game");

		if (gameData == null) {
			gameData = new GameData();
		}

		Screen.orientation = ScreenOrientation.Portrait;
		menuState = MenuState.MAIN_MENU;

		MedalSystem.Initiate ();
		menu.UpdateMedalsText ();

		coinUi.UpdateText (gameData.coins);
		SetShipUpgradeStats ();
		
		audioSource = GetComponent<AudioSource> ();

		upgradeShop.PopulateUpgrades (gameData.turnLevel, gameData.foodLevel, gameData.coinLevel);

		ShowMenu ();

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
			ResetAllGameData ();
		}

		
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			gameData.coinLevel = 1;
			gameData.foodLevel = 1;
			gameData.turnLevel = 1;
			SetShipUpgradeStats();
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			gameData.coinLevel = 2;
			gameData.foodLevel = 2;
			gameData.turnLevel = 2;
			SetShipUpgradeStats();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			gameData.coinLevel = 3;
			gameData.foodLevel = 3;
			gameData.turnLevel = 3;
			SetShipUpgradeStats();
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			gameData.coinLevel = 4;
			gameData.foodLevel = 4;
			gameData.turnLevel = 4;
			SetShipUpgradeStats();
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			gameData.coinLevel = 5;
			gameData.foodLevel = 5;
			gameData.turnLevel = 5;
			SetShipUpgradeStats();
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			GameCoins += 100;
		}
	}

	public void StartGame() {
		if (gameInProgress) {
			return;
		}

		gameInProgress = true;

		menuState = MenuState.IN_GAME;

		if (currentObstacle != null) {
			currentObstacle.ForceExit();
		}

		CurrentLevel = START_AT_LEVEL;
		currentMiles = 0;
		currentSpeed = LevelMetadata.GetLevelSpeed (CurrentLevel+1);
		ColorSea(LevelMetadata.GetLevelSeaColor(CurrentLevel+1));

		ship.Live ();
		ship.MoveShip ();

		NextLevel ();

		menu.DeactivateHighScore ();
	}
	
	public void NextLevel() {
		if (!isTransitioning) {
			CurrentLevel++;
		}
		isTransitioning = false;
		levelNotifier.Present (LevelMetadata.GetLevelName(CurrentLevel));
		StartMovement ();
		NextObstacle ();
	}

	public void TransitionLevel () {
		isTransitioning = true;
		CurrentLevel++;
		levelNotifier.Present (LevelMetadata.GetLevelName(CurrentLevel));
		if (CurrentLevel == 6) {
			levelNotifier.PlayHell ();
		} else {
			levelNotifier.PlayLevelUp ();
		}
		StartMovement ();
		DOTween.To (() => currentSpeed, x => currentSpeed = x, LevelMetadata.GetLevelSpeed (CurrentLevel), 1f);
		ColorSea(LevelMetadata.GetLevelSeaColor(CurrentLevel));
	}

	public void NextObstacle() {
		int milestone = LevelMetadata.GetLevelMilestone (currentLevel);
		if (currentMiles >= milestone) {
			if(milestone >= 0) {
				currentObstacle = ObstacleLevel.ActivateTransitionObstacle(currentSpeed);
				currentObstacle.OnLevelEnter();
				ship.Refuel(20);
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

		audioSource.clip = gameOverClips[UnityEngine.Random.Range(0,gameOverClips.Length)];
		audioSource.Play();

		gameInProgress = false;
		isTransitioning = false;
		menuState = MenuState.GAME_OVER;
		ship.Die ();
		continueButton.UpdateInfo ();
		CheckForUpgradeNotification ();

		if (currentMiles > gameData.highScore) {
			gameData.highScore = (int)currentMiles;
			menu.ActivateHighScore();
		}

		levelNotifier.StopHell ();
		
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
		
		menu.SetHighScore (gameData.highScore);
		menu.SetCurrentScore ((int)currentMiles);
		gameData.totalMiles += (int)currentMiles;

		Save();
	}

	public void ContinueGame () {
		menuState = MenuState.IN_GAME;
		gameInProgress = true;

		//remove the current obstacle
		currentObstacle.ForceExit ();
		//put on the new obstacle
		NextObstacle ();
		//remove the recently added miles
		gameData.totalMiles -= (int)currentMiles;

		StartMovement ();

		ship.Live ();
		ship.transform.position = new Vector3 (0,ship.transform.position.y,ship.transform.position.z);
		ship.Refuel(50);

		if (CurrentLevel == 6) {
			levelNotifier.PlayHell ();
		}
	}

	public void ShowMenu() {
		CheckForUpgradeNotification ();

		StartMovement (LevelMetadata.GetLevelSpeed(1));

		upgradeShop.HideInfo ();

		ship.Live ();
		ship.MoveShip (0f);
		ship.RandomizeShip ();
		ColorSea (LevelMetadata.GetLevelSeaColor(1));
		menuState = MenuState.MAIN_MENU;
		if (currentObstacle != null) {
			currentObstacle.ForceExit();
		}
	}

	public void SetShipUpgradeStats() {
		ship.TurnSpeed = UpgradesDB.GetTurnValue(gameData.turnLevel);
		ship.FuelRate = UpgradesDB.GetFoodValue(gameData.foodLevel);
	}

	public void ShowSettings() {
		menuState = MenuState.SETTINGS;
	}

	public void ShowMedals() {
		menuState = MenuState.MEDALS;
	}

	public void ShowUpgradeShop() {
		upgradeShop.PopulateUpgrades (gameData.turnLevel, gameData.foodLevel, gameData.coinLevel);
		ship.MoveShip (-2.25f);
		menuState = MenuState.UPGRADE_SHOP;
	}

	private void StartMovement() {
		StartMovement(LevelMetadata.GetLevelSpeed (CurrentLevel));
	}

	private void StartMovement(int _speed) {
		SideLines.Speed = _speed;
		Wave.Speed = _speed;
	}

	private void StopMovement() {
		SideLines.Speed = 0;
		Wave.Speed = 0;
	}

	private void ColorSea(Color _color) {
		seaSprite.DOColor (_color, 1f);;
	}

	public static void Save() {
		MemoryCard.Save (self.gameData, "game");
	}

	public void UpgradeTurn() {
		gameData.turnLevel++;
		if (gameData.turnLevel > UpgradesDB.MAX_UPGRADE_LEVEL) {
			gameData.turnLevel = UpgradesDB.MAX_UPGRADE_LEVEL;
		}
		upgradeShop.PopulateUpgrades (gameData.turnLevel, gameData.foodLevel, gameData.coinLevel);
		SetShipUpgradeStats ();
	}

	public void UpgradeFood() {
		gameData.foodLevel++;
		if (gameData.foodLevel > UpgradesDB.MAX_UPGRADE_LEVEL) {
			gameData.foodLevel = UpgradesDB.MAX_UPGRADE_LEVEL;
		}
		upgradeShop.PopulateUpgrades (gameData.turnLevel, gameData.foodLevel, gameData.coinLevel);
		SetShipUpgradeStats ();
	}

	public void UpgradeCoin() {
		gameData.coinLevel++;
		if (gameData.coinLevel > UpgradesDB.MAX_UPGRADE_LEVEL) {
			gameData.coinLevel = UpgradesDB.MAX_UPGRADE_LEVEL;
		}
		upgradeShop.PopulateUpgrades (gameData.turnLevel, gameData.foodLevel, gameData.coinLevel);
		SetShipUpgradeStats ();
	}

	private void CheckForUpgradeNotification() {
		bool canUpgrade = upgradeShop.HasUpgradesAvailable ();
		foreach (GameObject gmo in upgradeNotifications) {
			gmo.SetActive(canUpgrade);
		}
	}

	public void ResetAllGameData () {
		gameData.highScore = 0;
		gameData.coinLevel = 0;
		gameData.foodLevel = 0;
		gameData.turnLevel = 0;
		GameCoins = 0;
		SetShipUpgradeStats ();
	}

}

[Serializable]
public class GameData {
	public int highScore = 0;
	public double totalMiles = 0;
	public int coins = 0;
	public int turnLevel = 0;
	public int foodLevel = 0;
	public int coinLevel = 0;
}

