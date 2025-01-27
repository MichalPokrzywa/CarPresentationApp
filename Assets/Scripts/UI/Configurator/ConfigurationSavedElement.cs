using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationSavedElement : MonoBehaviour {
	public ConfigurationSave configurationSave;
    public string saveFileName;
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

    public void CreateSave() {
	    configurationSave = new ConfigurationSave();
		configurationSave.name = "New Config";
		configurationSave.dateCreation = DateTime.Today.ToShortDateString();
		configurationSave.lastSave = DateTime.Now.ToLongDateString();
		saveName.text = $"{configurationSave.name} ({configurationSave.dateCreation})";
		configurationSave.config = GlobalVariables.baseConfig;
    }
    public void SaveJsonFile() {
	    string formattedString = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local).ToString("yyyyMMddHHmmss");
		string filePath = Path.Combine(GlobalVariables.configFolder, $"{formattedString}.json");
	    File.WriteAllText(filePath, JsonConvert.SerializeObject(configurationSave));
	}

    public void UpdateJsonFile() {
	    string filePath = Path.Combine(GlobalVariables.configFolder, saveFileName);
	    File.WriteAllText(filePath, JsonConvert.SerializeObject(configurationSave));
	}
}
