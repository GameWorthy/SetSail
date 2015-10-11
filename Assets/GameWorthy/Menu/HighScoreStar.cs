using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class HighScoreStar : MonoBehaviour {
	
	[SerializeField] private Image image;

	void Update () {

	}

	public void Activate() {
		Debug.Log ("DEU");
		image.transform.localScale = Vector3.one * 50;
		image.transform.DOScale(Vector3.one,0.4f);
		image.color = new Color(255,0,0,255);
	}

	public void Deactivate() {
		
		Debug.Log ("DEU2");
		image.color = new Color(0,0,0,0);
	}

	public Color GenerateRandomColor() {
		float red = Random.Range(0.3f,1f);
		float green = Random.Range(0.3f,1f);
		float blue = Random.Range(0.3f,1f);
		
		Color color = new Color(red, green, blue);
		return color;
	}
}
