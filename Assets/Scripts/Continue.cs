using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Continue : MonoBehaviour {

	[SerializeField] private Text text = null;
	private Game game = null;

	void Start () {
		game = Game.GetSelf ();
	}

	public void UpdateInfo () {
		int price = CalculatePrice ((int)game.CurrentMiles);
		if (price >= Game.GameCoins) {
			this.gameObject.SetActive (false);
		} else {
			this.gameObject.SetActive (true);
		}

		text.text = "Continue For: " + price.ToString ("N0");
	}

	public void Purchase (){
		int price = CalculatePrice ((int)game.CurrentMiles);
		if (price <= Game.GameCoins) {
			Game.GameCoins -= price;
			Game.Save();
			game.ContinueGame();
		}
	}

	int CalculatePrice(int _miles) {
		return _miles / 2;
	}
}
