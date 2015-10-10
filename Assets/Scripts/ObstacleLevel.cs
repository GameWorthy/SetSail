using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleLevel : MonoBehaviour {

	private static List<ObstacleLevel>[] obstacleLevels = new List<ObstacleLevel>[10];
	private static Game staticGame = null;
	private static ObstacleLevel transitionLevel = null;

	[SerializeField] private int showInLevel = 1;
	[SerializeField] Game game = null;
	[SerializeField] bool isTransitionLevel = false;
	
	private bool isInLevel = false;
	private Vector3 startingPos = Vector3.zero;
	private List<Pickup> pickups = new List<Pickup> ();

	public int ShowInLevel {
		get { return showInLevel; }
		set { showInLevel = value; }
	}

	public bool IsInLevel {
		get { return isInLevel; }
		set { isInLevel = value; }
	}

	public void AddPickup(Pickup _pickup) {
		pickups.Add (_pickup);
	}

	void Start () {

		//As long as there is reference to game, we should be fine
		if (game != null) {
			staticGame = game;
		}
		startingPos = transform.position;
		
		if (isTransitionLevel) {
			transitionLevel = this;
			return;
		}

		if (obstacleLevels [showInLevel] == null) {
			obstacleLevels [showInLevel] = new List<ObstacleLevel>();
		}
		obstacleLevels[showInLevel].Add (this);
	}

	void Update () {

		if (!Game.GameInProgress) {
			return;
		}

		if (IsInLevel) {
			transform.position = new Vector3 (
				transform.position.x,
				transform.position.y - staticGame.CurrentSpeed * Time.deltaTime,
				transform.position.z
			);
		}
		 
	}

	void OnTriggerEnter2D(Collider2D _other) {
		if (_other.tag == "ship") {
			staticGame.NextObstacle();
			Exit();
		}
	}

	public static ObstacleLevel ActivateRandomObstacle(int _level, float _speed) {
		List<ObstacleLevel> osList = obstacleLevels [_level];
		ObstacleLevel os = osList[Random.Range (0, osList.Count)];
		while (os.IsInLevel) {
			os = osList[Random.Range(0,osList.Count)];
		}
		os.Enter ();
		return os;
	}

	public static ObstacleLevel ActivateTransitionObstacle (float _speed) {
		transitionLevel.Enter ();
		return transitionLevel;
	}

	/// <summary>
	/// Puts obstacles in the level
	/// </summary>
	public void Enter() {
		transform.position = new Vector3 (0,5,0);
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
		IsInLevel = false;
		//body.velocity = Vector3.zero;
		transform.position = startingPos;
	}

	public void OnLevelEnter() {
		ActivatePickups ();
	}

	public void OnLevelExit() {

	}

	private void ActivatePickups() {
		foreach (Pickup p in pickups) {
			p.Reset ();
		}
	}

	public void NextLevel () {
		staticGame.TransitionLevel ();
	}
}


