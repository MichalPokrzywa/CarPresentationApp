using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
	List<Button> buttons = new List<Button>();
	
	int currentButton;
	// Start is called before the first frame update
    void Start() {
	    currentButton = 0;
	    foreach (Transform child in transform) {
		    buttons.Add(child.GetComponent<Button>());
		    child.GetComponent<Button>().onClick.AddListener(ChangeView);

		}
		buttons[0].Select();
    }

    void ChangeView() {
	    for (int i = 0; i < buttons.Count; i++) {
		    if (buttons[i].gameObject == EventSystem.current.currentSelectedGameObject) {
				currentButton = i;
				ViewGroupManager views = ViewGroupManager.instance;
				views.ChangeView(i);
				break;
		    }
	    }
    }

}
