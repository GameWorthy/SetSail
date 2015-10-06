using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Ship : MonoBehaviour {

	[SerializeField] Game game = null;
	[SerializeField] Fuel fuel = null;
	[SerializeField] Sprite[] shipSprites = null;
	private const float LANE_WIDTH = 2.3f;
	private int direction = 0;
	private float turnSpeed = 3.4f;
	private float fuelConsuptionRate = 3f;
	private Vector3 lastPosition;
	private float previousRotation = 0;
	private bool isDead = false;
	private ParticleSystem[] particles = null;
	private SpriteRenderer sr = null;
	private Sail sail = null;

	public float TurnSpeed {
		get{return turnSpeed;}
		set {turnSpeed = value;}
	}

	public float FuelRate {
		get {return fuelConsuptionRate;}
		set {fuelConsuptionRate = value;}
	}
	
	void Start() {
		particles = GetComponentsInChildren<ParticleSystem> ();
		sr = this.GetComponent<SpriteRenderer> ();
		sail = this.GetComponent<Sail> ();
	}

	public void RandomizeShip() {
		sr.sprite = shipSprites[Random.Range(0,shipSprites.Length)];
		sail.Randomize ();
	}

	void Update() {

		if (isDead) {
			return;
		}

		float maxLaneWidth = LANE_WIDTH;
		float minLaneWidth = -LANE_WIDTH;

		float moveTarget = direction * LANE_WIDTH;

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

		float rotation = (lastPosition.x - xPos) * 200;
		rotation = Mathf.Lerp (previousRotation, rotation, 0.2f);
		previousRotation = rotation;
		transform.rotation = Quaternion.Euler (new Vector3 (0,0,rotation));


		if (Game.GameInProgress) {
			fuel.Percentage -= Time.deltaTime * fuelConsuptionRate;
			if(fuel.Percentage <= 0) {
				game.SetGameOver();
			}
		}
	}

	public void Die() {
		isDead = true;
		foreach (ParticleSystem p in particles) {
			p.Stop();
		}
	}

	public void Live() {
		isDead = false;
		fuel.Percentage = 100;
		foreach (ParticleSystem p in particles) {
			p.Play();
		}
	}

	public void MoveShip(float _to = -3) {
		float y = transform.position.y;
		DOTween.To (() => y, x => y = x, _to, 1f).OnUpdate (()=>{
			transform.position = new Vector3 (transform.position.x, y, transform.position.z);
		});
	}

	public void Refuel(float _percentage) {
		fuel.Percentage += _percentage;
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

	public void TakeHit () {
		game.SetGameOver ();
	}
}
