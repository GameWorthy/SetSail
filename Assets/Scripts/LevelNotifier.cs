using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LevelNotifier : MonoBehaviour {


	[SerializeField] private TextMesh[] text = null;
	
	public void Present(string _text) {
		foreach (TextMesh t in text) {
			t.text = _text;
		}
		StartCoroutine (Present ());
	}

	IEnumerator Present() {
		transform.position = Vector3.left * 10;
		transform.DOMove (Vector3.zero, 0.5f);
		yield return new WaitForSeconds(2.5f);
		transform.DOMove (Vector3.right * 10, 0.5f);
	}
}
