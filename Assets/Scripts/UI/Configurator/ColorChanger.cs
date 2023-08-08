using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
	[SerializeField] public GameObject mainMaterial;
	[SerializeField] public GameObject sideMaterial;
	ChangeVersion changeVersion;
	// Start is called before the first frame update
	public void RegisterToggles() {
		changeVersion = GetComponent<ChangeVersion>();
		ToggleGroup toggleGroup = GetComponent<ToggleGroup>();
		foreach (GameObject toggle in changeVersion.toggles) {
			toggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { ChangeMaterialColor(toggleGroup.GetFirstActiveToggle()); });
		}
	}

    void ChangeMaterialColor(Toggle toggle) {
	    Color color = toggle.GetComponent<ConfiguratorColorChange>().color;
	    mainMaterial.GetComponent<MeshRenderer>().material.color = color;
	    sideMaterial.GetComponent<MeshRenderer>().material.color = color;
    }

}
