using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSlideAnimation : ViewAnimation
{
	[SerializeField] RectTransform rectTransform;
	[SerializeField] float duriation = 1f;
	[SerializeField] private float startPosition = 480f;
	void Start() {
		if (rectTransform == null) {
			rectTransform = GetComponent<RectTransform>();
		}
	}
	public override IEnumerator MakeAnimationUp() {
		yield return rectTransform.DOAnchorPosX(0, duriation).WaitForCompletion();
	}

	public override IEnumerator MakeAnimationDown() {
		yield return rectTransform.DOAnchorPosX(startPosition, duriation).WaitForCompletion();
	}
}
