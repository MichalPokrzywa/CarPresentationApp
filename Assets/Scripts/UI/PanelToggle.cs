using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelToggle : MonoBehaviour {
	[SerializeField] public Toggle toggle;
	[SerializeField] public Image image;
	[SerializeField] public GameObject view;
	[SerializeField] public Image targetImage;
	void Start() {
		if (toggle == null) {
			toggle = GetComponent<Toggle>();
		}
		targetImage = toggle.targetGraphic.GetComponent<Image>();
    }
	public IEnumerator DoFadeUp() {
		yield return toggle.targetGraphic.DOFade(255, 0.03f);
	}

	public IEnumerator DoFadeDown() {
		yield return toggle.targetGraphic.DOFade(0, 0.03f);
	}
}
