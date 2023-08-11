using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateDescription : MonoBehaviour {
	TMP_Text description;
	string[] texts;
	public bool flag = false;
    // Start is called before the first frame update
    public void LoadText() {
	    description = GetComponent<TMP_Text>();
	    TextAsset textAsset = Resources.Load<TextAsset>("DescriptionsCars");
	    texts = textAsset.text.Split('\n');
	    flag = true;
    }
    public void ChangeVersion(Version version)
    {
	    switch (version) {
		    case Version.Label:
			    description.text = texts[0];
			    break;
			case Version.Sharp:
				description.text = texts[1];
				break;
			case Version.Unity:
				description.text = texts[2];
				break;
	    }
    }
}
