using UnityEngine;
using System.Collections;

public class InputReader : MonoBehaviour {

	[SerializeField] private Ship ship = null;

	void Update () {
		if (ship == null) {
			return;
		}

		int dir = 0;
		//Touch control
		foreach (Touch t in Input.touches) {
			if(t.position.x >= Screen.width / 2) {
				dir = 1;
			}
			else {
				dir = -1;
			}
		}


		//Keyboard control
		if (Input.GetAxis ("Horizontal") > 0) {
			dir = 1;
		} else if (Input.GetAxis ("Horizontal") < 0) {
			dir = -1;
		}

		ship.SetDirection (dir);
	}
}
