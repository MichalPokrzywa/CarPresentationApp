using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenAnimation : MonoBehaviour {

	private static FadeScreenAnimation _instance;
	public static FadeScreenAnimation instance => _instance;
	public Image blackScreen;
	[SerializeField] public float timeScale = 3f;
	[SerializeField] float startTime = 0f;

	public void DoFadeUp() {
		blackScreen.DOFade(255, timeScale).SetEase(Ease.InSine);
	}

	public void DoFadeDown() {
		blackScreen.DOFade(0, timeScale).SetEase(Ease.OutSine);
	}

	public void DoFadeToValue(int value) {
		blackScreen.DOFade(value, timeScale);
	}
	public IEnumerator Animation(bool isStart) {
		float timer = 0f;
		float duration = timeScale;

		if (isStart) {
			DoFadeUp();
		}
		else {
			DoFadeDown();
		}

		while (timer < duration) {
			timer += Time.deltaTime;
			yield return null;
		}
	}
}
