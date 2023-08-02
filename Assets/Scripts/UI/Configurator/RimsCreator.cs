using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RimsCreator : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] GameObject uiItem;

	// Start is called before the first frame update
	void Start() {
		TextAsset textAsset = Resources.Load<TextAsset>("ColorCars");
		string[] lines = textAsset.text.Split('\n');
		Color newCol = Color.white;
		for (int i = 0; i < 3; i++) {
			// Remove any leading or trailing white spaces
			string colorName = lines[i].Trim();
			if (!string.IsNullOrEmpty(colorName)) {
				GameObject newObject = Instantiate(uiItem, this.transform);
				newObject.GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
				GetComponent<ToggleGroup>().RegisterToggle(newObject.GetComponent<Toggle>());
				if (ColorUtility.TryParseHtmlString(colorName, out newCol)) {
					newObject.GetComponent<Image>().color = newCol;
				}
			}
		}
		GetComponent<ChangeVersion>().UpdateToggles();
	}

}
