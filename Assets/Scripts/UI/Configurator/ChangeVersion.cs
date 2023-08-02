using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChangeVersion : MonoBehaviour {
	ToggleGroup toggleGroup;
	List<GameObject> toggles;
    // Start is called before the first frame update
    public void UpdateToggles()
	{
		toggles = new List<GameObject>();
		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++) {
			Transform child = transform.GetChild(i);
			if (child != null && child.gameObject != null) {
				toggles.Add(child.gameObject);
			}
		}
	}

    public void ChangeConfigVersion(Version version) {
	    switch (version) {
		    case Version.Label:
			    break;
		    case Version.Sharp:
			    break;
		    case Version.Unity:
			    break;
		    default:
			    throw new ArgumentOutOfRangeException(nameof(version), version, null);
	    }
	}
}