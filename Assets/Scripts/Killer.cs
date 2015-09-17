using UnityEngine;
using System.Collections;

public class Killer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D _other) {
		Debug.Log (_other);
		if (_other.tag == "ship") {
			_other.GetComponent<Ship>().TakeHit();
		}
	}
}
