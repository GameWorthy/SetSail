using UnityEngine;
using System.Collections;

public class SideLines : MonoBehaviour {

	private static float speed = 0;
	public static float Speed {
		get { return speed;}
		set { speed = value;}
	}

	void Update() {
	
		float y = transform.position.y - speed * Time.deltaTime;
		if (y <= -13.5f) {
			y = 13.15f;
		}
		
		transform.position = new Vector3 (
			transform.position.x,
			y,
			transform.position.z
			);

	}
}
