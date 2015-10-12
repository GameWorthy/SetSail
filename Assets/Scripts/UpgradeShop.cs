using UnityEngine;
using System.Collections;

public enum UpgradeTypes {
	TURN,
	FOOD,
	COINS
}

public class UpgradeShop : MonoBehaviour {

	//YEW, FOOD and COINS
	[SerializeField] private UpgradeMenu[] upgradeMenus = null;
	private AudioSource audioSource = null;

	void Start() {
		audioSource = GetComponent<AudioSource> ();
	}

	public void PopulateUpgrades(int _turnLevel, int _foodLevel, int _coinLevel) {
		upgradeMenus [(int)UpgradeTypes.TURN].UpdateUI (_turnLevel);
		upgradeMenus [(int)UpgradeTypes.FOOD].UpdateUI (_foodLevel);
		upgradeMenus [(int)UpgradeTypes.COINS].UpdateUI (_coinLevel);
	}

	public void PurchaseUpgrade(UpgradeTypes _upgradeType, int _price) {
		Game.GameCoins -= _price;
		audioSource.Play ();
		switch (_upgradeType) {
			case UpgradeTypes.TURN:
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