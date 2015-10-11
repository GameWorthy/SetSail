using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	
	private bool findParent = true;
	private Collider2D col = null;
	private AudioSource audioSource = null;

	public virtual void CollidedWithShip(Ship _ship) {}
	public virtual void On() {}
	public virtual void Off() {}

	public void Reset() {
		col.enabled = true;
		On ();
	}

	void Start () {
		col = GetComponent<Collider2D> ();
		audioSource = GetComponent<AudioSource> ();
		LookForParent ();
	}

	void OnTriggerEnter2D(Collider2D _other) {
		if (_other.tag == "ship") {
			col.enabled = false;
			CollidedWithShip(_other.GetComponent<Ship>());
			Off ();
			if(audioSource) {
				audioSource.pitch = Random.Range(0.95f,1.05f);
				audioSource.Play();
			}
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
					levelParent.AddPickup(this);
					break;
				}
				tries--;
			}
		}
	}
}
