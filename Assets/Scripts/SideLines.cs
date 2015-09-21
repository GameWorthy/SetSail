using UnityEngine;
using System.Collections;

public class SideLines : MonoBehaviour {
	public float speed = 0;
	private Animator anim = null;
	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void SetSpeed (float _speed) {
		anim.speed = _speed;
	}
}
