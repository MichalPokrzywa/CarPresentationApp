using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
	[SerializeField] Sprite backgroundImage;
	List<PanelToggle> buttons = new List<PanelToggle>();
	PanelToggle activeToggle;
	// Start is called before the first frame update
    void Start() {
	    Color color = new Color(1, 1, 1, 0);
		foreach (Transform child in transform) {
			PanelToggle panelToggle = child.GetComponent<PanelToggle>();
		    buttons.Add(panelToggle);
		    panelToggle.toggle.onValueChanged.AddListener(delegate {
			    StartCoroutine(ChangeView(panelToggle));
		    });
		    panelToggle.toggle.isOn = false;
		    panelToggle.targetImage.sprite = backgroundImage;
			panelToggle.targetImage.color = color;
		}
		activeToggle = buttons[0];
		activeToggle.toggle.Select();
		activeToggle.toggle.isOn = true;
		activeToggle.targetImage.color = Color.white;
    }

    IEnumerator ChangeView(PanelToggle panelToggle) {

		if (panelToggle == activeToggle && panelToggle.toggle.isOn) {
			yield break;
		}
		if (panelToggle == activeToggle && !panelToggle.toggle.isOn) {
			TurnOffButtons();
			yield return StartCoroutine(panelToggle.DoFadeDown());
		}
		else if (panelToggle != activeToggle) {
			ViewGroupManager views = ViewGroupManager.instance; 
			activeToggle = panelToggle;
			yield return StartCoroutine(views.ChangeView(panelToggle.view));
			yield return StartCoroutine(panelToggle.DoFadeUp());
			TurnOnButtons();
		}
    }

    void TurnOffButtons() {
	    foreach (PanelToggle button in buttons) {
		    button.toggle.interactable = false;
	    }
	}

    void TurnOnButtons() {
	    foreach (PanelToggle button in buttons) {
		    button.toggle.interactable = true;
	    }
	}
}
