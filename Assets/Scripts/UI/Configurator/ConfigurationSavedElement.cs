using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationSavedElement : MonoBehaviour {
	ConfigurationSave configurationSave;
	[SerializeField] public Button button;
    [SerializeField] TMP_Text saveName;
    public void LoadSavedConfig(ConfigurationSave loadedSave) {
		configurationSave = loadedSave;
		saveName.text = $"{configurationSave.name} ({configurationSave.dateCreation})";
    }

    public void LoadConfigFromFile(string json) {
	    configurationSave = JsonConvert.DeserializeObject<ConfigurationSave>(json);
	    saveName.text = $"{configurationSave.name} ({configurationSave.dateCreation})";
    }

}
