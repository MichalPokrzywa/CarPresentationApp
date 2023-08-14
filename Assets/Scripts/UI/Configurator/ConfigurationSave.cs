using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ConfigurationSave myDeserializedClass = JsonConvert.DeserializeObject<ConfigurationSave>(myJsonResponse);

public class ConfigurationSave {
	public string name { get; set; }
	public string dateCreation { get; set; }
	public string lastSave { get; set; }
	public List<string> config { get; set; }
}
