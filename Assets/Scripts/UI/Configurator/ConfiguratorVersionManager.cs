using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfiguratorVersionManager : MonoBehaviour {
	[SerializeField] private List<GameObject> carElements;
	Version myVersion = Version.Label;
    // Start is called before the first frame update
    void Start()
    {
	    for (int i = 0; i < carElements.Count; i++) {
			carElements[i].GetComponent<ChangeVersion>().ChangeConfigVersion(myVersion);
	    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
