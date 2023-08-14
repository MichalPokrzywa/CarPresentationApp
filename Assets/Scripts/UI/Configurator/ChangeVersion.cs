using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVersion : MonoBehaviour {
	ToggleGroup toggleGroup;
	public List<GameObject> toggles;
	public bool load = false;
    // Start is called before the first frame update
    public void UpdateToggles()
	{
		toggles = new List<GameObject>();
		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++) {
			Transform child = transform.GetChild(i);
			if (child != null && child.gameObject != null) {
				toggles.Add(child.gameObject);
			}
		}
		toggles[0].GetComponent<Toggle>().isOn = true;
		load = true;
	}

    public void ChangeConfigVersion(Version version) {
	    bool isActive = false;
	    foreach (GameObject toggle in toggles) {
		    bool flag = toggle.GetComponent<ConfigurationVersion>().versions.Contains(version);
		    if (!flag && toggle.GetComponent<Toggle>().isOn) {
				isActive = true;
				toggle.GetComponent<Toggle>().isOn = false;
		    }
		    toggle.SetActive(flag);
		}

		if (isActive) {
			foreach (GameObject toggle in toggles.Where(toggle => toggle.activeSelf)) {
				toggle.GetComponent<Toggle>().isOn = true;
				break;
			}
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
	}
	public void ChangeActiveToggle(List<int> numbers) {

		foreach (GameObject toggle in toggles) {
			toggle.GetComponent<Toggle>().isOn = false;
		}

		foreach (int number in numbers) {
			toggles[number].GetComponent<Toggle>().isOn = true;
		}
	}
	public void ChangeActiveToggle(int number) {

		foreach (GameObject toggle in toggles) {
			toggle.GetComponent<Toggle>().isOn = false;
		}
		toggles[number].GetComponent<Toggle>().isOn = true;
	}
}