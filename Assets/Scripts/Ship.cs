using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	private const float LANE_WIDTH = 2.3f;
	private int direction = 0;
	private float turnSpeed = 1.4f;
	private Vector3 lastPosition;
	private float previousRotation = 0;

	void Update() {

		float maxLaneWidth = LANE_WIDTH;
		float minLaneWidth = -LANE_WIDTH;

		float moveTarget = direction * LANE_WIDTH;


		if (direction == 0) {
			moveTarget = 0;

			if(transform.position.x > 0) {
				direction = -1;
				minLaneWidth = 0;
			}
			else if (transform.position.x < 0) {
				direction = 1;
				maxLaneWidth = 0;
			}
		}

		float distanceToTarget = Mathf.Abs (transform.position.x - moveTarget);
		float ease = 1;
		if (distanceToTarget < 0.5f && distanceToTarget != 0) {
			ease = (distanceToTarget / 0.5f);
		}

		float xPos = transform.position.x + (direction * Time.deltaTime * turnSpeed * ease);

		xPos = Mathf.Clamp (xPos, minLaneWidth, maxLaneWidth);

		lastPosition = transform.position;

		transform.position = new Vector3 (
			xPos,
			transform.position.y,
			transform.position.z);

		float rotation = (lastPosition.x - xPos) * 100;
		rotation = Mathf.Lerp (previousRotation, rotation, 0.2f);
		previousRotation = rotation;
		transform.rotation = Quaternion.Euler (new Vector3 (0,0,rotation));
	}

	/// <summary>
	/// -1:Left
	/// 0:No Input
	/// 1: Right
	/// </summary>
	/// <param name="_dir">_dir.</param>
	public void SetDirection(int _dir) {
		direction = _dir;
	}
}
