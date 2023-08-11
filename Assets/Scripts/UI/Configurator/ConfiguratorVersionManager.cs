using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfiguratorVersionManager : MonoBehaviour {
	[SerializeField] private List<GameObject> carElements;
	[SerializeField] private TMP_Text descriptionElements;
	Version myVersion = Version.Label;
	ToggleGroup myToggleGroup; 
    // Start is called before the first frame update
    void Start()
    {
		myToggleGroup = GetComponent<ToggleGroup>();
		for (int i = 0; i < carElements.Count; i++) {
			StartCoroutine(LoadObjectCoroutine(carElements[i]));
	    }
	    foreach (Transform child in transform) {
		    child.GetComponent<Toggle>().onValueChanged.AddListener(delegate {
			    ChangeVersion();
		    });
		    child.GetComponent<Toggle>().group = myToggleGroup;
		}
		this.GetComponent<ChangeVersion>().UpdateToggles();
	    StartCoroutine(LoadDescriptionCoroutine());

    }
    IEnumerator LoadObjectCoroutine(GameObject objectToLoad) {
	    while (!objectToLoad.GetComponent<ChangeVersion>().load) {
			yield return null;
		}
	    objectToLoad.GetComponent<ChangeVersion>().ChangeConfigVersion(myVersion);

    }
    IEnumerator LoadDescriptionCoroutine() {
	    UpdateDescription description = descriptionElements.GetComponent<UpdateDescription>();
	    description.LoadText();
	    while (!description.flag) {
		    yield return null;
	    }
		description.ChangeVersion(myVersion);

	}

	void ChangeVersion() {
	    myVersion = Enum.Parse<Version>(myToggleGroup.GetFirstActiveToggle().GetComponentInChildren<TMP_Text>().text);
	    for (int i = 0; i < carElements.Count; i++) {
		    carElements[i].GetComponent<ChangeVersion>().ChangeConfigVersion(myVersion);
		}
	    descriptionElements.GetComponent<UpdateDescription>().ChangeVersion(myVersion);
	}

	public void ChangeVersion(int version) {
		myVersion = Enum.Parse<Version>(myToggleGroup.GetFirstActiveToggle().GetComponentInChildren<TMP_Text>().text);
		for (int i = 0; i < carElements.Count; i++) {
			carElements[i].GetComponent<ChangeVersion>().ChangeConfigVersion(myVersion);
		}
		descriptionElements.GetComponent<UpdateDescription>().ChangeVersion(myVersion);
	}

}
