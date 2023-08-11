using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackageCreator : MonoBehaviour
{
	[SerializeField] GameObject uiItem;
	// Start is called before the first frame update
	void Start() {
		StartCoroutine(LoadItems());
	}

	private IEnumerator LoadItems() {
		TextAsset textAsset = Resources.Load<TextAsset>("PackagesCars");
		ToggleGroup toggleComponentGroup = GetComponent<ToggleGroup>();
		string[] lines = textAsset.text.Split('\n');
		for (int i = 0; i < lines.Length; i++) {
			string[] data = lines[i].Split('|');
			if (data.Length >= 2) {
				string[] versions = data[1].Split(' ');
				if (!string.IsNullOrEmpty(data[0])) {
					GameObject newObject = Instantiate(uiItem, transform);
					newObject.GetComponent<Toggle>().group = toggleComponentGroup;
					newObject.GetComponentInChildren<TMP_Text>().text = data[0];
					ConfigurationVersion configVersion = newObject.GetComponent<ConfigurationVersion>();
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
		GetComponent<MultiToggleGroup>().Load();
	}
}
