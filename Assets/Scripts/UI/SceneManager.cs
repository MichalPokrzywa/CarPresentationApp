using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {



	[SerializeField] float timeToTimeout = 60.0f;
	public bool idle;
	InputManager inputManager;
	SceneManagerUI sceneManagerUI;
	float timeToIdle;
	float timer;

	void Start() {
		inputManager = InputManager.instance;
		sceneManagerUI = GetComponent<SceneManagerUI>();
		ResetTimer();
	}

	void Update() {

		if (inputManager.DetectMovement()) {
			ResetTimer();
		}
		if (timer > timeToIdle) {
			idle = true;
			sceneManagerUI.DoFadeDown();
			inputManager.isIdle = idle;
		}

		timer += Time.deltaTime;
	}

	private void ResetTimer() {
		timer = Time.time;
		timeToIdle = timer + timeToTimeout;
		if (idle == true) {
			sceneManagerUI.DoFadeUp();
		}
		idle = false;
		inputManager.isIdle = idle;
		
	}
}
