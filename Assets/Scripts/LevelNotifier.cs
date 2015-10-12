using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LevelNotifier : MonoBehaviour {


	[SerializeField] private TextMesh[] text = null;
	[SerializeField] private AudioClip levelUpClip = null;
	[SerializeField] private AudioClip hellClip = null;
	private AudioSource audioSource = null;

	void Start() {
		audioSource = GetComponent<AudioSource> ();
	}

	public void Present(string _text) {
		foreach (TextMesh t in text) {
			t.text = _text;
		}
		StartCoroutine (Present ());
	}

	public void PlayLevelUp() {
		audioSource.clip = levelUpClip;
		audioSource.loop = false;
		audioSource.Play ();
	}

	public void PlayHell() {
		audioSource.clip = hellClip;
		audioSource.loop = true;
		audioSource.Play ();
	}

	public void StopHell() {
		audioSource.Stop ();
	}

	IEnumerator Present() {
		transform.position = Vector3.left * 10;
		transform.DOMove (Vector3.zero, 0.5f);
		yield return new WaitForSeconds(2.5f);
		transform.DOMove (Vector3.right * 10, 0.5f);
	}
}
