using UnityEngine;
using System.Collections;

public class Sea : MonoBehaviour {
	private float speed = 15f;
	private Vector2 offset = Vector2.zero;
	public float Speed
	{
		get { return speed; }
		set { speed = value; }
	}
	private Renderer rend = null;

	void Start() {
		rend = gameObject.GetComponent<Renderer>();
	}

	void Update() {
		offset = new Vector2 (0,offset.y + speed * Time.deltaTime / 100);
		rend.sharedMaterial.SetTextureOffset("_MainTex",offset);
	}

}
