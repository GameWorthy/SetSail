using UnityEngine;
using System.Collections;
using DG.Tweening;


public class Coin : MonoBehaviour {
	
	[SerializeField] private SpriteRenderer coin = null;
	[SerializeField] private SpriteRenderer plus = null;
	private Collider2D col = null;
	private float animTime = 0.5f;
	private bool findParent = true;

	void Start () {
		col = GetComponent<Collider2D> ();
		On ();
	}

	public void On() {
		col.enabled = true;
		coin.color = Color.white;
		plus.transform.localPosition = Vector3.zero;
		plus.color = new Color (1, 1, 1, 0);
	}

	public void Grab() {
		col.enabled = false;
		plus.color = Color.white;
		plus.DOColor (new Color(1,1,1,0), animTime * 2);
		plus.transform.DOLocalMove (Vector3.up, animTime);
		coin.color = new Color (1,1,1,0);
		Game.GameCoins++;
	}

	void OnTriggerEnter2D(Collider2D _other) {
		if (_other.tag == "ship") {
			LookForParent ();
			Grab ();
		}
	}

	void LookForParent() {
		if (findParent) {
			findParent = false;
			int tries = 3;
			ObstacleLevel levelParent;
			Transform coinParent = transform;
			while (tries > 0) {
				coinParent = coinParent.parent;
				if(coinParent == null) {
					break;
				}
				levelParent = transform.parent.GetComponent<ObstacleLevel>();
				if(levelParent != null) {
					levelParent.AddCoin(this);
					break;
				}
				tries--;
			}
		}
	}
}
