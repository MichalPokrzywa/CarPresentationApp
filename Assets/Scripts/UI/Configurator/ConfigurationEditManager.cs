using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationEditManager : MonoBehaviour {
	
	ConfigurationSave configurationSave;
	[SerializeField] Button saveButton;
	[SerializeField] Button backButton;
	[SerializeField] ConfiguratorVersionManager configuratorVersionManager;
	[SerializeField] ConfigurationStartManager csManager;
	// Start is called before the first frame update
    void Start()
    {
        saveButton.onClick.AddListener(Save);
        backButton.onClick.AddListener(Back);
        StartCoroutine(WaitForLoading(true));
    }
    public void LoadSave(ConfigurationSave loadedSave) {
		configurationSave = loadedSave;
	    int numericValue = (int)char.GetNumericValue(loadedSave.config[0][0]);
	    configuratorVersionManager.ChangeVersion(numericValue);
	    StartCoroutine(WaitForLoading(false));
		for (int i = 1; i < loadedSave.config.Count; i++) {
		    string config = loadedSave.config[i];
		    List<int> digits = new List<int>();
		    foreach (char c in config) {
			    if (char.IsDigit(c)) {
				    int digit = (int)Char.GetNumericValue(c);
				    digits.Add(digit);
					Debug.Log(digit);
			    }
		    }
		    configuratorVersionManager.carElements[i-1].GetComponent<ChangeVersion>().ChangeActiveToggle(digits);
	    }
    }

    public void Save() {
	    ConfigurationSave newSave = new ConfigurationSave();
	    newSave.name = configurationSave.name;
		newSave.lastSave = DateTime.Now.ToLongDateString();
		newSave.dateCreation = configurationSave.dateCreation;
		List<string> configList = new List<string>();
		foreach (GameObject carElement in configuratorVersionManager.carElements) {
			configList.Add(carElement.GetComponent<ChangeVersion>().ReturnActiveToggleNumber());
		}
		newSave.config = configList;
		csManager.UpdateConfiguration(newSave);
		csManager.gameObject.SetActive(true);
		this.gameObject.SetActive(false);

    }
    public void Back() {
	    csManager.gameObject.SetActive(true);
	    this.gameObject.SetActive(false);
	}
    IEnumerator WaitForLoading(bool turnOff) {
	    while (!configuratorVersionManager.loadingInformation.CheckLoading()) {
		    yield return null;
	    }
	    if (turnOff) {
		    gameObject.SetActive(false);
	    }
    }
}
