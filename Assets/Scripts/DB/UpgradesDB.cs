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
		{1,1.5f,100, "Remove rust from the helm."},
		{2,2.0f,250, "Greese the helm for faster turning."},
		{3,2.7f,500, "Fix the holes in your sales, it helps for turning."},
		{4,3.5f,1000, "Polish the hull."},
		{5,4.5f,2000, "Healm MZ3000, the best healm in the market."}
	};
	
	public static float GetTurnValue(int _upgradeLevel) {
		return (float)turnUpgrade[_upgradeLevel,1];
	}
	
	public static int GetTurnPrice(int _upgradeLevel) {
		return (int)turnUpgrade[_upgradeLevel,2];
	}
	
	public static string GetTurnDescription(int _upgradeLevel) {
		return (string)turnUpgrade[_upgradeLevel,3];
	}
	
	/*****FOOD*******/
	private static object[,] foodUpgrade = new object[,] {
		//index 0: index
		//index 1: food_value
		//index 2: upgrade_prie
		//index 3: description
		{0,3.0f,-1, ""},
		{1,2.5f,100, "Get an extra food barrel."},
		{2,2.0f,250, "Build an extra food room."},
		{3,1.5f,500, "Hire a chef."},
		{4,0.6f,1000, "Purchase fishing rods."},
		{5,0.2f,2000, "Farm in the boat."}
	};
	
	public static float GetFoodValue(int _upgradeLevel) {
		return (float)foodUpgrade[_upgradeLevel,1];
	}
	
	public static int GetFoodPrice(int _upgradeLevel) {
		return (int)foodUpgrade[_upgradeLevel,2];
	}
	
	public static string GetFoodDescription(int _upgradeLevel) {
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
		{1,4,999,100, "Double Coins Shows Up From Level 4."},
		{2,3,999,250, "Double Coins Shows Up From Level 3."},
		{3,3,6,500, "Triple Coins Shows Up From Level 6."},
		{4,3,5,1000, "Tripple Coins Shows Up From Level 5."},
		{5,2,4,2000, "Doble Coins Shows Up From Level 2 and Triple Coins Shows Up From Level 4."}
	};
	
	public static int GetDoubleCoinLevel(int _upgradeLevel) {
		return (int)coinsUpgrade[_upgradeLevel,1];
	}
	
	public static int GetTrippeCoinLevel(int _upgradeLevel) {
		return (int)coinsUpgrade[_upgradeLevel,2];
	}

	public static int GetCoinUpgradePrice(int _upgradeLevel) {
		return (int)coinsUpgrade[_upgradeLevel,3];
	}
	
	public static string GetCoinDescription(int _upgradeLevel) {
		return (string)coinsUpgrade[_upgradeLevel,4];
	}
}
