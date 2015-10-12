using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeInfo : MonoBehaviour {

	[SerializeField] private Text upgradeTitle = null;
	[SerializeField] private Text upgradeInfo = null;
	
	void Start () {
		//Hide ();
	}

	public void ShowInfo(string _upgradeTitle, string _upgradeInfo) {
		Show ();
		upgradeTitle.text = _upgradeTitle + " - Upgrade";
		upgradeInfo.text = _upgradeInfo;
	}

	public void Show() {
		this.gameObject.SetActive(true);
	}

	public void Hide() {
		this.gameObject.SetActive(false);
	}
}
