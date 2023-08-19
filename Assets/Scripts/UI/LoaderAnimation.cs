using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderAnimation : MonoBehaviour
{
	[SerializeField] float animationTime = 1.0f;
	[SerializeField] float stopTime = 0.5f;
	[SerializeField] Image loaderImage;

	void Start() {
		RectTransform rect = loaderImage.GetComponent<RectTransform>();
		Sequence mySequence = DOTween.Sequence();
		mySequence.Append(rect.DORotate(new Vector3(0, 0, -180), animationTime).SetEase(Ease.InSine));
		mySequence.Append(rect.DORotate(new Vector3(0, 0, 0), animationTime).SetEase(Ease.OutSine));
		mySequence.PrependInterval(stopTime);
		mySequence.SetLoops(-1);
		
	}
	public IEnumerator Run() {
		gameObject.SetActive(true);
		yield return new WaitForSeconds(1.0f);
	}

	public IEnumerator Stop() {
		gameObject.SetActive(false);
		yield return null;
	}
}
