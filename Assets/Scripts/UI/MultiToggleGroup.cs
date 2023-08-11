using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class MultiToggleGroup : MonoBehaviour
{
	public List<Toggle>toggles;

	public void Load() {

		foreach (Transform child in transform) {
			toggles.Add(child.GetComponent<Toggle>());
		}
		foreach (Toggle toggle in toggles) {
			toggle.onValueChanged.AddListener((isOn) => OnToggleValueChanged(toggle, isOn));
		}
	}

	private void OnToggleValueChanged(Toggle changedToggle, bool isOn) {
		// Handle the changed toggle
		if (isOn) {
			// Toggle was turned on, keep all toggles on
			foreach (Toggle toggle in toggles) {
				toggle.isOn = true;
				// Handle appearance or other behavior based on the new selection state
				// For example, you can change the color, sprite, or do anything you need
			}
		}
		else {
			// Toggle was turned off, do any necessary handling here
		}
	}
}
