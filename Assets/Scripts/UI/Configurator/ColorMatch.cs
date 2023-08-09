using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMatch : MonoBehaviour {
	[SerializeField] ChangeVersion changeVersion;

	private GameObject colorMatchGameObject;
    // Start is called before the first frame update
    void Start() {
	    colorMatchGameObject = changeVersion.toggles[^1];
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
