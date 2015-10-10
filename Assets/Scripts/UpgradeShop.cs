using UnityEngine;
using System.Collections;

public enum UpgradeTypes {
	YAW,
	FOOD,
	COINS
}

public class UpgradeShop : MonoBehaviour {

	//YEW, FOOD and COINS
	[SerializeField] private UpgradeMenu[] upgradeMenus = null;

	public void PopulateUpgrades(int _turnLevel, int _foodLevel, int _coinLevel) {
		upgradeMenus [(int)UpgradeTypes.YAW].UpdateUI (_turnLevel);
		upgradeMenus [(int)UpgradeTypes.FOOD].UpdateUI (_foodLevel);
		upgradeMenus [(int)UpgradeTypes.COINS].UpdateUI (_coinLevel);
	}

	public void PurchaseUpgrade(UpgradeTypes _upgradeType, int _price) {
		Game.GameCoins -= _price;
		switch (_upgradeType) {
			case UpgradeTypes.YAW:
				Game.GetSelf().UpgradeTurn();
				break;
			case UpgradeTypes.FOOD:
				Game.GetSelf().UpgradeFood();
				break;
			case UpgradeTypes.COINS:
				Game.GetSelf().UpgradeCoin();
				break;
		}
		Game.Save ();
	}

	public bool HasUpgradesAvailable () {
		foreach (UpgradeMenu um in upgradeMenus) {
			if(um.CanBuy()) {
				return true;
			}
		}
		return false;
	}
}