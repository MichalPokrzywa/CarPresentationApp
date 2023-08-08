using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateDescription : MonoBehaviour {
	TMP_Text description;
	string[] texts;
    // Start is called before the first frame update
    void Start() {
	    description = GetComponent<TMP_Text>();
	    TextAsset textAsset = Resources.Load<TextAsset>("DescriptionsCars");
	    texts = textAsset.text.Split('\n');
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
