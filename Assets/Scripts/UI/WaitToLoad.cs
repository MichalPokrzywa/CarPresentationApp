using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToLoad : MonoBehaviour {
	bool startComplete { get; set; } = false ;
	[SerializeField] List<LoadingInformation> objectsToLoad;
    // Start is called before the first frame update
    void Start()
    {

	}
    public IEnumerator WaitForGameObjects() {
	    // Wait for all GameObjects to complete their Start function
		
	    foreach (LoadingInformation information in objectsToLoad) {
		    while (!information.CheckLoading()) {
			    yield return new WaitForSeconds(0.1f);
			}
		    //yield return new WaitUntil(() => information.CheckLoading());
	    }
	    startComplete = true;
    }
	public bool IsStartComplete => startComplete;
}
