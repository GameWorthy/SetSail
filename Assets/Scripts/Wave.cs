using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	private static float speed = 4;
	public static float Speed {
		get {return speed;}
		set {speed = value;}
	}
	private Animator anim = null;

	void Start() {
		anim = gameObject.GetComponent<Animator> ();
		anim.speed = Random.Range(0.5f,1.5f);
	}

	void Update () {
		float y = transform.position.y;

		y -= speed * Time.deltaTime;

		if (y < -10) {
			y = 10;
		}

		transform.position = new Vector3 (
			transform.position.x,
			y,
			transform.position.z
			);
	}
}
