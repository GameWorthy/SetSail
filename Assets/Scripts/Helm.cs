using UnityEngine;
using System.Collections;
using DG.Tweening;


public class Helm : MonoBehaviour {

	void Start() {
		StartCoroutine(IRotate());
	}

	IEnumerator IRotate() {
		yield return new WaitForSeconds (Random.Range (2f, 5f));
		transform.DORotate (new Vector3 (0, 0, Random.Range (0, 360)), Random.Range (2f, 5f));
		StartCoroutine(IRotate());
	}
}
