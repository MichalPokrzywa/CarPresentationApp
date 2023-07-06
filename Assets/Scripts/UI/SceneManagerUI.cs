using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class SceneManagerUI : MonoBehaviour
{
	[SerializeField] GameObject canvas;
	[SerializeField] float timeScale = 1.0f;
	CanvasGroup canvasGroup;
	
	// Start is called before the first frame update
	void Start()
    {	
		canvasGroup = canvas.GetComponent<CanvasGroup>();
	}

	public void DoFadeUp() {
		canvasGroup.DOFade(255, timeScale);
	}

	public void DoFadeDown() {
		canvasGroup.DOFade(0, timeScale);
	}
}
