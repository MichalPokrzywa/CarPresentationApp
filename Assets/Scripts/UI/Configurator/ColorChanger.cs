using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
	[SerializeField] public List<MeshRenderer> materials;
	[SerializeField] ChangeVersion changeGroup;
	ChangeVersion changeVersion;
	public void RegisterToggles() {
		changeVersion = GetComponent<ChangeVersion>();
		ToggleGroup toggleGroup = GetComponent<ToggleGroup>();
		foreach (GameObject toggle in changeVersion.toggles) {
			toggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { ChangeMaterialColor(toggleGroup.GetFirstActiveToggle()); });
			if (changeGroup != null) {
				toggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { ColorMatchUpdate(toggle); });
			}
		}
	}
	void ChangeMaterialColor(Toggle toggle) {
	    Color color = toggle.GetComponent<ConfiguratorColorChange>().color;
	    foreach (MeshRenderer material in materials) {
			material.material.color = color;
		}

	    if (changeGroup != null) {
			
		    GameObject colorMatchGameObject = changeGroup.toggles[^1];
		    if (colorMatchGameObject.GetComponent<Toggle>().isOn) {
				colorMatchGameObject.GetComponentInParent<ColorChanger>().ChangeMaterialColor(toggle);
		    }
		}
    }
	void ColorMatchUpdate(GameObject toggle)
	{
		GameObject colorMatchGameObject = changeGroup.toggles[^1];
		Color color = toggle.GetComponent<ConfiguratorColorChange>().color;
		ConfiguratorColorChange colorChange = colorMatchGameObject.GetComponent<ConfiguratorColorChange>();
		colorChange.UpdateColor(color);
	}

}
