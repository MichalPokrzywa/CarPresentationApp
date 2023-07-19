using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderAnimation : MonoBehaviour
{
	[SerializeField] float animationTime = 1.0f;
	[SerializeField] float stopTime = 0.5f;
	public void Run() {
		Sequence mySequence = DOTween.Sequence();
		mySequence.Append(transform.DORotate(new Vector3(0, 0, -180), animationTime).SetEase(Ease.InSine));
		mySequence.Append(transform.DORotate(new Vector3(0, 0, 0), animationTime).SetEase(Ease.OutSine));
		mySequence.PrependInterval(stopTime);
		mySequence.SetLoops(-1);
	}
}
