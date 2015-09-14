using UnityEngine;
using System.Collections;

public class InputReader : MonoBehaviour {

	[SerializeField] private Ship ship;

	void Update () {
		if (ship == null) {
			return;
		}

		int dir = 0;
		if (Input.GetAxis ("Horizontal") > 0) {
			dir = 1;
		} else if (Input.GetAxis ("Horizontal") < 0) {
			dir = -1;
		}

		ship.SetDirection (dir);
	}
}
