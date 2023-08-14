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
		foreach (string fileName in Directory.GetFiles(Application.persistentDataPath, "*.json")) {
			GameObject newConfig = Instantiate(configurationObject, configurationList.transform);
			using (StreamReader r = new StreamReader(fileName)) {
				string json = r.ReadToEnd();
				ConfigurationSavedElement csElement = newConfig.GetComponent<ConfigurationSavedElement>();
				csElement.LoadConfigFromFile(json);
				csElement.saveFileName = fileName;
				csElement.button.onClick.AddListener(() => LoadConfiguration(csElement));
			}
		}
		Debug.Log(Application.persistentDataPath);
		createButton.onClick.AddListener(CreateNewConfiguration);
	}
    void CreateNewConfiguration() {
	    GameObject newConfig = Instantiate(configurationObject, configurationList.transform);
	    ConfigurationSavedElement csElement = newConfig.GetComponent<ConfigurationSavedElement>();
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
