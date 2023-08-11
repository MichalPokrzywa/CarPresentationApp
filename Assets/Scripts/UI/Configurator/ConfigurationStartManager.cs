using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationStartManager : MonoBehaviour {

	[SerializeField] GameObject configurationList;
	[SerializeField] GameObject configurationObject;
	[SerializeField] Button createButton;
    // Start is called before the first frame update
    void Start()
    {
		foreach (string fileName in Directory.GetFiles(Application.persistentDataPath, "*.json")) {
			GameObject newConfig = Instantiate(configurationObject, configurationList.transform);
			using (StreamReader r = new StreamReader(fileName)) {
				string json = r.ReadToEnd();
				ConfigurationSavedElement csElement = newConfig.GetComponent<ConfigurationSavedElement>();
				csElement.LoadConfigFromFile(json);
				csElement.button.onClick.AddListener(LoadConfiguration);
			}
		}
		createButton.onClick.AddListener(CreateNewConfiguration);
	}

    void CreateNewConfiguration() {

    }

    void LoadConfiguration() {
		
    }
}
