using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class CoinUI : MonoBehaviour {

	[SerializeField] private Text coinText = null;
	private int currentCoins = 0;
	
	public void UpdateText(int _total) {
		DOTween.To(()=> currentCoins, x=> currentCoins = x, _total, 0.5f).OnUpdate(()=>{
			coinText.text = currentCoins.ToString("N0");
		});
	}
}
