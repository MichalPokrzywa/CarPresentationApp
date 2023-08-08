using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ColorCreator : MonoBehaviour {
	
	[SerializeField] GameObject uiItem;

	// Start is called before the first frame update
	void Start() {
		StartCoroutine(LoadItems());
	}

	public IEnumerator LoadItems()
	{
		TextAsset textAsset = Resources.Load<TextAsset>("ColorCars");
		ToggleGroup toggleComponentGroup = GetComponent<ToggleGroup>();
		string[] lines = textAsset.text.Split('\n');
		Color newCol = Color.white;
		for (int i = 0; i < lines.Length; i++) {
			string[] data = lines[i].Split('|');
			if (data.Length >= 2) {
				string[] versions = data[1].Split(' ');
				if (!string.IsNullOrEmpty(data[0])) {
					GameObject newObject = Instantiate(uiItem, transform);
					ConfiguratorColorChange ccc = newObject.GetComponent<ConfiguratorColorChange>();
					Toggle toggleComponent = newObject.GetComponent<Toggle>();
					ConfigurationVersion configVersion = newObject.GetComponent<ConfigurationVersion>();
					if (ColorUtility.TryParseHtmlString(data[0].Trim(), out newCol)) {
						ccc.UpdateColor(newCol);
						ccc.image = newObject.GetComponent<Image>();
						ccc.UpdateImageColor();
					}
					else {
						Debug.LogWarning("Invalid color format: " + data[0]);
					}
					toggleComponent.group = toggleComponentGroup;
					//toggleComponentGroup.RegisterToggle(toggleComponent);
					if (i > 1) {
						newObject.transform.GetChild(1).gameObject.SetActive(true);
					}
					foreach (string versionName in versions) {
						if (Enum.TryParse<Version>(versionName, out Version parsedVersion)) {
							configVersion.AddVersion(parsedVersion);
						}
						else {
							Debug.LogWarning("Unknown version: " + versionName);
						}
					}
				}
			}
		}
		yield return null;
		GetComponent<ChangeVersion>().UpdateToggles();
		GetComponent<ColorChanger>().RegisterToggles();
	}


}
