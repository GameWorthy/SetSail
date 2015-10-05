using UnityEngine;
using System.Collections;

public enum UpgradeTypes {
	YEW,
	FOOD,
	COINS
}

public class UpgradeShop : MonoBehaviour {

	//YEW, FOOD and COINS
	[SerializeField] UpgradeMenu[] upgradeMenus = null;

	public void PopulateUpgrades(int _turnLevel, int _foodLevel, int _coinLevel) {
		upgradeMenus [0].UpdateUI (_turnLevel);
		upgradeMenus [1].UpdateUI (_foodLevel);
		upgradeMenus [2].UpdateUI (_coinLevel);
	}

	void Purchaseupgrade(UpgradeTypes upgradeType) {

	}

	void Upgrade() {
	}

	void GetPricebyUpgrade() {
	}
}
