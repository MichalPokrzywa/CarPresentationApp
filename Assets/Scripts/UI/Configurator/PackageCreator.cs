using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageCreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		foreach (Transform child in transform) {
			GetComponent<ToggleGroup>().RegisterToggle(child.GetComponent<Toggle>());
		}
		GetComponent<ChangeVersion>().UpdateToggles();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
