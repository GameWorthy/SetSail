using System.Collections;

public class UpgradesDB {

	public const int MAX_UPGRADE_LEVEL = 5;

	/*****TURN*******/
	private static object[,] turnUpgrade = new object[,] {
		//index 0: index
		//index 1: turn_value
		//index 2: upgrade_prie
		//index 3: description
		{0,1.0f,-1, ""},
		{1,1.5f,50, "Remove rust from the helm."},
		{2,2.0f,150, "Polish the hull."},
		{3,2.7f,250, "Patch the holes in the sail, catch the wind!"},
		{4,3.5f,400, "Greese the helm for faster turning."},
		{5,4.5f,500, "Healm 9001, the best healm in the realm."}

	};
	
	public static float GetTurnValue(int _upgradeLevel) {
		_upgradeLevel = _upgradeLevel >= MAX_UPGRADE_LEVEL ? MAX_UPGRADE_LEVEL : _upgradeLevel;
		return (float)turnUpgrade[_upgradeLevel,1];
	}
	
	public static int GetTurnPrice(int _upgradeLevel) {
		_upgradeLevel = _upgradeLevel >= MAX_UPGRADE_LEVEL ? MAX_UPGRADE_LEVEL : _upgradeLevel;
		return (int)turnUpgrade[_upgradeLevel,2];
	}
	
	public static string GetTurnDescription(int _upgradeLevel) {
		_upgradeLevel = _upgradeLevel >= MAX_UPGRADE_LEVEL ? MAX_UPGRADE_LEVEL : _upgradeLevel;
		return (string)turnUpgrade[_upgradeLevel,3];
	}
	
	/*****FOOD*******/
	private static object[,] foodUpgrade = new object[,] {
		//index 0: index
		//index 1: food_value
		//index 2: upgrade_price
		//index 3: description
		{0,3.0f,-1, ""},
		{1,2.0f,150, "Extra rations."},
		{2,1.75f,250, "Buy food crates."},
		{3,1.25f,500, "Buy food barrels."},
		{4,1.0f,750, "Build a storage room."},
		{5,0.5f,1000, "Hire a chef."}
	};
	
	public static float GetFoodValue(int _upgradeLevel) {
		_upgradeLevel = _upgradeLevel >= MAX_UPGRADE_LEVEL ? MAX_UPGRADE_LEVEL : _upgradeLevel;
		return (float)foodUpgrade[_upgradeLevel,1];
	}
	
	public static int GetFoodPrice(int _upgradeLevel) {
		_upgradeLevel = _upgradeLevel >= MAX_UPGRADE_LEVEL ? MAX_UPGRADE_LEVEL : _upgradeLevel;
		return (int)foodUpgrade[_upgradeLevel,2];
	}
	
	public static string GetFoodDescription(int _upgradeLevel) {
		_upgradeLevel = _upgradeLevel >= MAX_UPGRADE_LEVEL ? MAX_UPGRADE_LEVEL : _upgradeLevel;
		return (string)foodUpgrade[_upgradeLevel,3];
	}

	
	/*****COINS*******/
	private static object[,] coinsUpgrade = new object[,] {
		//index 0: index
		//index 1: double_level
		//index 2: tripple_level
		//index 3: upgrade_prie
		//index 4: description
		//
		{0,999,999,-1, ""},
		{1,4,999,100, "Double Coins Starts At Level 4."},
		{2,3,999,250, "Double Coins Starts At Level 3."},
		{3,3,6,500, "Triple Coins Starts At Level 6."},
		{4,3,5,1000, "Tripple Coins Starts At Level 5."},
		{5,2,4,2000, "Double Coins Starts At Level 2 and Triple Coins Starts At Level 4."}
	};
	
	public static int GetDoubleCoinLevel(int _upgradeLevel) {
		_upgradeLevel = _upgradeLevel >= MAX_UPGRADE_LEVEL ? MAX_UPGRADE_LEVEL : _upgradeLevel;
		return (int)coinsUpgrade[_upgradeLevel,1];
	}
	
	public static int GetTrippeCoinLevel(int _upgradeLevel) {
		_upgradeLevel = _upgradeLevel >= MAX_UPGRADE_LEVEL ? MAX_UPGRADE_LEVEL : _upgradeLevel;
		return (int)coinsUpgrade[_upgradeLevel,2];
	}

	public static int GetCoinUpgradePrice(int _upgradeLevel) {
		_upgradeLevel = _upgradeLevel >= MAX_UPGRADE_LEVEL ? MAX_UPGRADE_LEVEL : _upgradeLevel;
		return (int)coinsUpgrade[_upgradeLevel,3];
	}
	
	public static string GetCoinDescription(int _upgradeLevel) {
		_upgradeLevel = _upgradeLevel >= MAX_UPGRADE_LEVEL ? MAX_UPGRADE_LEVEL : _upgradeLevel;
		return (string)coinsUpgrade[_upgradeLevel,4];
	}
}
