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
	void Start()
    {
		foreach (string fileName in Directory.GetFiles(Application.persistentDataPath, "*.json")) {
			GameObject newConfig = Instantiate(configurationObject, configurationList.transform);
			using (StreamReader r = new StreamReader(fileName)) {
				string json = r.ReadToEnd();
				ConfigurationSavedElement csElement = newConfig.GetComponent<ConfigurationSavedElement>();
				csElement.LoadConfigFromFile(json);
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
		ceManager.LoadSave(csElement.configurationSave);
		this.gameObject.SetActive(false);
    }
}
