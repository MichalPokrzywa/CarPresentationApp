using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ColorCreator : MonoBehaviour {
	
	[SerializeField] GameObject uiItem;

	// Start is called before the first frame update
	void Start() {
		TextAsset textAsset = Resources.Load<TextAsset>("ColorCars");
		string[] lines = textAsset.text.Split('\n');
		Color newCol = Color.white;
		for (int i = 0; i < lines.Length; i++) {
			// Remove any leading or trailing white spaces
			string colorName = lines[i].Trim();
			if (!string.IsNullOrEmpty(colorName)) {
				GameObject newObject = Instantiate(uiItem,this.transform);
				if (ColorUtility.TryParseHtmlString(colorName, out newCol)) {
					newObject.GetComponent<Image>().color = newCol;
					newObject.GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
					GetComponent<ToggleGroup>().RegisterToggle(newObject.GetComponent<Toggle>());
					if (i > 1) {
						newObject.transform.GetChild(1).gameObject.SetActive(true);
					}
				}
			}
		}
		GetComponent<ChangeVersion>().UpdateToggles();
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
