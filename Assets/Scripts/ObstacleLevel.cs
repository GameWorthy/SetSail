using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleLevel : MonoBehaviour {

	private static List<ObstacleLevel> obstacleLeves = new List<ObstacleLevel>();
	private static Game staticGame = null;

	[SerializeField] private int showInLevel = 1;
	[SerializeField] Game game = null;

	private float speed = 0f;
	private bool isInLevel = false;
	private Vector3 startingPos = Vector3.zero;

	public int ShowInLevel {
		get { return showInLevel; }
		set { showInLevel = value; }
	}
	
	public float Speed {
		get { return speed; }
		set { speed = value; }
	}

	public bool IsInLevel {
		get { return isInLevel; }
		set { isInLevel = value; }
	}

	void Start () {
		startingPos = transform.position;
		obstacleLeves.Add (this);
		//As long as there is reference to game, we should be fine
		if (game != null) {
			staticGame = game;
		}
	}

	void Update () {

		if (!staticGame.GameInProgress) {
			return;
		}

		transform.position = new Vector3 (
			transform.position.x,
			transform.position.y - speed * Time.deltaTime,
			transform.position.z
		);
	}

	void OnTriggerEnter2D(Collider2D _other) {
		if (_other.tag == "ship") {
			staticGame.NextObstacle();
			Exit();
		}
	}

	public static ObstacleLevel ActivateRandomObstacle(int _level, float _speed) {
		ObstacleLevel os = obstacleLeves [Random.Range (0, obstacleLeves.Count)];
		while (os.IsInLevel) {
			os = obstacleLeves[Random.Range(0,obstacleLeves.Count)];
		}
		os.Enter (_speed);
		return os;
	}

	/// <summary>
	/// Puts obstacles in the level
	/// </summary>
	/// <param name="_speed">_speed to which obstacle will move down</param>
	public void Enter(float _speed) {
		transform.position = new Vector3 (0,5,0);
		Speed = _speed;
		IsInLevel = true;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ObstacleLevel"/> class.
	/// </summary>
	public void Exit() {
		StartCoroutine (EExit ());
	}

	private IEnumerator EExit() {
		yield return new WaitForSeconds(2);
		ForceExit ();
	}

	public void ForceExit () {
		Speed = 0;
		IsInLevel = false;
		transform.position = startingPos;
	}
}


