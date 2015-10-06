using UnityEngine;
using System.Collections;

public class Sail : MonoBehaviour {
	
	[SerializeField] private SpriteRenderer topSail = null;
	[SerializeField] private SpriteRenderer botSail = null;
	[SerializeField] private Sprite[] topSails = null;
	[SerializeField] private Sprite[] botSails = null;

	void Start () {
		int rand = Random.Range (0, topSails.Length);
		topSail.sprite = topSails [rand];
		botSail.sprite = botSails [rand];
	}
}
