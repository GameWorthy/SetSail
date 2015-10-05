using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

	[SerializeField] private Image[] upgradeDots = null; 

	public void UpdateUI (int _updateLevel) {
		foreach (Image i in upgradeDots) {
			if(_updateLevel > 0 ) {
				i.color = Color.green;
				_updateLevel--;
			}
			else {
				return;
			}
		}
	}
}
