using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
	List<Toggle> buttons = new List<Toggle>();
	int currentButton;

	[SerializeField] Sprite backgroundImage;
	// Start is called before the first frame update
    void Start() {
	    currentButton = 0;
	    foreach (Transform child in transform) {
		    Toggle toggle = child.GetComponent<Toggle>();
		    buttons.Add(toggle);
		    toggle.onValueChanged.AddListener(delegate {
			    StartCoroutine(ChangeView());
		    });
		    toggle.isOn = false;
		    toggle.targetGraphic.GetComponent<Image>().sprite = backgroundImage;
		    toggle.targetGraphic.GetComponent<Image>().color = new Color(1,1,1,0);
		}
		
	    buttons[0].Select();
	    buttons[0].isOn = true;
		buttons[0].targetGraphic.GetComponent<Image>().color = Color.white;
	}

    IEnumerator ChangeView() {
		bool flag = false;
		for (int i = 0; i < buttons.Count; i++) {
		    if (buttons[i].gameObject == EventSystem.current.currentSelectedGameObject) {
			    if (i == currentButton) {
					buttons[i].isOn = true;
					break;
			    }
			    yield return StartCoroutine(DoFadeDown());
			    yield return StartCoroutine(DoFadeUp(i));
			    currentButton = i;
				ViewGroupManager views = ViewGroupManager.instance;
				yield return StartCoroutine(views.ChangeView(i));
				flag = true;
				break;
		    }
	    }
		if (flag == false) {
		    buttons[currentButton].Select();
		}
    }

    IEnumerator DoFadeUp(int button) {
	    buttons[button].targetGraphic.DOFade(255, 0.03f);
	    yield return null;
    }

    IEnumerator DoFadeDown() {
	    buttons[currentButton].targetGraphic.DOFade(0, 0.03f);
		yield return null;
    }

}
