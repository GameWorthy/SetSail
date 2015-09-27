using UnityEngine;
using System.Collections;
using DG.Tweening;


public class Coin : Pickup {
	
	[SerializeField] private SpriteRenderer coin = null;
	[SerializeField] private SpriteRenderer plus = null;
	private float animTime = 0.5f;

	public override void On() {
		plus.transform.localPosition = Vector3.zero;
		plus.color = new Color (1, 1, 1, 0);
	}

	public override void Off() {
		plus.color = Color.white;
		plus.DOColor (new Color(1,1,1,0), animTime * 2);
		plus.transform.DOLocalMove (Vector3.up, animTime);
		coin.color = new Color (1,1,1,0);
		Game.GameCoins++;
	}

	public override void CollidedWithShip(Ship _ship) {
		Game.GameCoins++;
	}
}
