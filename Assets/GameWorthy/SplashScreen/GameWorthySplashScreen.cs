﻿using UnityEngine;
using System.Collections;

public class GameWorthySplashScreen : MonoBehaviour {

	[SerializeField] private int sceneToLoad = 1;
	[SerializeField] private Renderer bg = null;

	void Update () {
		float off = Time.time * 0.5f;
		bg.material.SetTextureOffset ("_MainTex", new Vector2(off,off));
	}

	public void LoadNextScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
		bg.gameObject.SetActive (false);
	}
}
