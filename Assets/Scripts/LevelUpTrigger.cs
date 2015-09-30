using UnityEngine;
using System.Collections;

public class LevelUpTrigger : MonoBehaviour {

	private ObstacleLevel level = null;

	void Start () {
		level = transform.parent.GetComponent<ObstacleLevel> ();
	}

	void OnTriggerEnter2D(Collider2D _other) {
		if (level == null) {
			return;
		}

		if (_other.tag == "ship") {
			level.NextLevel();
		}
	}
}
