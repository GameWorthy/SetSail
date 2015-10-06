using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

	[SerializeField] private UpgradeTypes menuType = UpgradeTypes.YEW;
	[SerializeField] private Image[] upgradeDots = null; 
	[SerializeField] private Text priceText = null;
	[SerializeField] private Image purchaseBox = null;
	[SerializeField] private UpgradeInfo upgradeInfo = null;
	private UpgradeShop upgradeShop = null;
	private int upgradeLevel = 0;

	void Start() {
		upgradeShop = transform.parent.GetComponent<UpgradeShop> ();
	}

	public void UpdateUI (int _upgradeLevel) {
		int level = _upgradeLevel;
		upgradeLevel = _upgradeLevel;
		foreach (Image i in upgradeDots) {
			if(level > 0 ) {
				i.color = Color.green;
				level--;
			}
			else {
				break;
			}
		}

		int price = -1;
		if (upgradeLevel < UpgradesDB.MAX_UPGRADE_LEVEL) {
			price = GetPrice ();
		}

		if (price > 0) {
			priceText.text = price.ToString ("N0");
		} else {
			priceText.text = "-";
		}

		if (CanBuy()) {
			purchaseBox.color = new Color (0, 0.9f, 0, 1);
		} else {
			purchaseBox.color = new Color (0.7f, 0, 0, 0.8f);
		}
	}

	private bool CanBuy() {
		int price = GetPrice ();
		return Game.GameCoins >= price && price >= 0;
	}

	public void TryPurchase() {
		if (upgradeLevel < UpgradesDB.MAX_UPGRADE_LEVEL) {
			if(CanBuy()) {
				upgradeShop.PurchaseUpgrade(menuType,GetPrice());
			}
		}
	}

	public void ShowInfo() {

		upgradeInfo.ShowInfo (menuType.ToString(),Getdescription());
	}

	private int GetPrice() {
		switch (menuType) {
		case UpgradeTypes.YEW:
			return UpgradesDB.GetTurnPrice(upgradeLevel + 1);
		case UpgradeTypes.FOOD:
			return UpgradesDB.GetFoodPrice(upgradeLevel + 1);
		case UpgradeTypes.COINS:
			return UpgradesDB.GetCoinUpgradePrice(upgradeLevel + 1);
		}
		return 0;
	}

	private string Getdescription() {
		if (upgradeLevel >= UpgradesDB.MAX_UPGRADE_LEVEL) {
			return "Maxed";
		}
		switch (menuType) {
		case UpgradeTypes.YEW:
			return UpgradesDB.GetTurnDescription(upgradeLevel + 1);
		case UpgradeTypes.FOOD:
			return UpgradesDB.GetFoodDescription(upgradeLevel + 1);
		case UpgradeTypes.COINS:
			return UpgradesDB.GetCoinDescription(upgradeLevel + 1);
		}
		return "";
	}
}
