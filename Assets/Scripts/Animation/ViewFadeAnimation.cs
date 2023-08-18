using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFadeAnimation : ViewAnimation {
	[SerializeField] CanvasGroup canvasGroup;
	[SerializeField] private float duriation = 1f;

	void Start() {
		if (canvasGroup != null) {
			canvasGroup = GetComponent<CanvasGroup>();
		}
	}
	public override IEnumerator MakeAnimationUp() {
		yield return canvasGroup.DOFade(1, duriation).WaitForCompletion();
	}

	public override IEnumerator MakeAnimationDown() {
		yield return canvasGroup.DOFade(0, duriation).WaitForCompletion();
	}
}
