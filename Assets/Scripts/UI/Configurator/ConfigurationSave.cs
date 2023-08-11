using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ConfigurationSave myDeserializedClass = JsonConvert.DeserializeObject<ConfigurationSave>(myJsonResponse);
public class Config {
	public int version { get; set; }
	public int drive { get; set; }
	public int color { get; set; }
	public int rims { get; set; }
	public List<int> package { get; set; }
}

public class ConfigurationSave {
	public string name { get; set; }
	public string dateCreation { get; set; }
	public string lastSave { get; set; }
	public Config config { get; set; }
}
