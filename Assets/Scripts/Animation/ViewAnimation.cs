using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ViewAnimation : MonoBehaviour
{
	public IEnumerator MakeAnimationUp(int viewIndex) {

		if (viewIndex == 0) {
			yield return null;
		}
		if (viewIndex == 1 || viewIndex == 2) {
			GetComponent<RectTransform>().DOAnchorPosX(0, 1);
			yield return null;
		}
		if (viewIndex == 3) {
			GetComponent<CanvasGroup>().DOFade(1, 1f);
			yield return null;
		}


	}
	public IEnumerator MakeAnimationDown(int viewIndex) {

		if (viewIndex == 0) {
			yield return null;
		}
		if (viewIndex == 1 || viewIndex == 2) {
			GetComponent<RectTransform>().DOAnchorPosX(480, 0.01f);
			yield return null;
		}

		if (viewIndex == 3) {
			GetComponent<CanvasGroup>().DOFade(0, 0.01f);
			yield return null;
		}
	}
}
