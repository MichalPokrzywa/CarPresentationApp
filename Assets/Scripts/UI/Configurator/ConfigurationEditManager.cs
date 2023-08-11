using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationEditManager : MonoBehaviour {
	
	ConfigurationSave configurationSave;
	[SerializeField] Button saveButton;
	[SerializeField] Button backButton;
	
	// Start is called before the first frame update
    void Start()
    {
        saveButton.onClick.AddListener(Save);
        backButton.onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadSave() {

    }

    public void Save() {

    }
    public void Back() {

    }
}
