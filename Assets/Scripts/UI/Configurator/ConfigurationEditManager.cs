using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationEditManager : MonoBehaviour {
	
	ConfigurationSave configurationSave;
	[SerializeField] Button saveButton;
	[SerializeField] Button backButton;
	[SerializeField] ConfiguratorVersionManager configuratorVersionManager;
	[SerializeField] ConfigurationStartManager csManager;
	[SerializeField] TMP_InputField inputField;
	ConfigurationSave tempSave;
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
	    List<int> digits = new List<int>();
		StartCoroutine(WaitForLoading(false));
		for (int i = 1; i < loadedSave.config.Count; i++) {
		    string config = loadedSave.config[i];
		    foreach (char c in config) {
			    if (char.IsDigit(c)) {
				    int digit = (int)char.GetNumericValue(c);
					digits.Add(digit);
			    }
		    }
		    configuratorVersionManager.carElements[i].GetComponent<ChangeVersion>().ChangeActiveToggle(digits);
			digits.Clear();
	    }
		inputField.text = configurationSave.name;
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

		if (inputField.text == string.Empty) {
			inputField.text = "New Configuration";
		}
		newSave.name = inputField.text;
		newSave.config = configList;
		if (newSave.config == configurationSave.config) {
			csManager.UpdateConfiguration(newSave);
			csManager.gameObject.SetActive(true);
			this.gameObject.SetActive(false);
		}
		else {
			tempSave = newSave;
			PopupManager.instance.ShowPopup("PopupTitleWarning", "PopupDescriptionOverride", "PopupRespondYes", "PopupRespondNo", HandleChoiceSave);
		}


    }
    public void Back() {
	    PopupManager.instance.ShowPopup("PopupTitleWarning", "PopupDescriptionNotSaving", "PopupRespondYes", "PopupRespondNo", HandleChoiceBack);
    }

    void HandleChoiceSave(bool isAccepted) {
	    if (isAccepted) {
		    csManager.UpdateConfiguration(tempSave);
		    csManager.gameObject.SetActive(true);
		    this.gameObject.SetActive(false);
			csManager.GetComponent<ConfiguratorRestoreCar>().RestoreCar();
		}
    }

    void HandleChoiceBack(bool isAccepted) {
	    if (isAccepted) {
		    csManager.gameObject.SetActive(true);
		    this.gameObject.SetActive(false);
		    csManager.GetComponent<ConfiguratorRestoreCar>().RestoreCar();
		}
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
