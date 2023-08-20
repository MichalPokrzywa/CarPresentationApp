using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationStartManager : MonoBehaviour {

	[SerializeField] GameObject configurationList;
	[SerializeField] GameObject configurationObject;
	[SerializeField] Button createButton;
	[SerializeField] ConfigurationEditManager ceManager;
	GameObject chosenSave;
	void Start()
    {
		LoadingInformation loadingInformation = GetComponent<LoadingInformation>();
		if (!Directory.Exists(GlobalVariables.configFolder)) {
			Directory.CreateDirectory(GlobalVariables.configFolder);
		}
		foreach (string fileName in Directory.GetFiles(GlobalVariables.configFolder, "*.json")) {
			GameObject newConfig = Instantiate(configurationObject, configurationList.transform);
			using (StreamReader r = new StreamReader(fileName)) {
				string json = r.ReadToEnd();
				ConfigurationSavedElement csElement = newConfig.GetComponent<ConfigurationSavedElement>();
				csElement.LoadConfigFromFile(json);
				csElement.saveFileName = fileName;
				csElement.button.onClick.AddListener(() => LoadConfiguration(csElement));
			}
		}
		createButton.onClick.AddListener(CreateNewConfiguration);
		if (!loadingInformation.CheckLoading()) {
			Debug.Log(loadingInformation.CheckLoading());
			loadingInformation.SetLoading(true);
		}
    }
    void CreateNewConfiguration() {
	    GameObject newConfig = Instantiate(configurationObject, configurationList.transform);
	    ConfigurationSavedElement csElement = newConfig.GetComponent<ConfigurationSavedElement>();
	    csElement.button.onClick.AddListener(() => LoadConfiguration(csElement));
		csElement.CreateSave();
		csElement.SaveJsonFile();
    }

    void LoadConfiguration(ConfigurationSavedElement csElement) {
		
		ceManager.gameObject.SetActive(true);
		chosenSave = csElement.gameObject; 
		ceManager.LoadSave(csElement.configurationSave);
		this.gameObject.SetActive(false);
    }

    public void UpdateConfiguration(ConfigurationSave configSave) {
	    ConfigurationSavedElement csElement = chosenSave.GetComponent<ConfigurationSavedElement>();
	    csElement.LoadSavedConfig(configSave);
		csElement.UpdateJsonFile();
    }
}
