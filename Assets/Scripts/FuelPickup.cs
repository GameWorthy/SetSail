using UnityEngine;
using System.Collections;
using DG.Tweening;


public class FuelPickup : Pickup {
	
	[SerializeField] private SpriteRenderer wing = null;
	private float animTime = 0.5f;

	public override void On() {
		wing.color = Color.white;
	}

	public override void Off() {
		wing.color = new Color (1,1,1,0);
		Game.GameCoins++;
	}
	
	public override void CollidedWithShip(Ship _ship) {
		_ship.Refuel (20);
	}
}
