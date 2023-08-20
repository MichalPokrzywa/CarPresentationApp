using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingInformation : MonoBehaviour
{
    bool isLoaded;

    void Start() {
	    isLoaded = false;
    }

    public async void SetLoading(bool check) {
	    
		isLoaded = check;
    }
    public bool CheckLoading() {
	    return isLoaded;
    }
}
