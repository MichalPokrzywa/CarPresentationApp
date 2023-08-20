using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
	int currentCamera = 0;
	bool wait = false;
	List<Transform> sceneCameras;

	[SerializeField] FadeScreenAnimation anim;
	// Start is called before the first frame update
	void Start()
    {
		sceneCameras = new List<Transform>();
	    foreach (Transform child in transform) {
		    sceneCameras.Add(child);
	    }
    }

    public void NextCamera() {
	    StartCoroutine(ChangeCamera());

    }

    IEnumerator ChangeCamera() {
	    //FadeScreenAnimation anim = FadeScreenAnimation.instance;
		yield return StartCoroutine(anim.Animation(true));
		sceneCameras[currentCamera].gameObject.SetActive(false);
	    currentCamera++;
	    if (currentCamera >= sceneCameras.Count) {
		    currentCamera = 0;
	    }
	    sceneCameras[currentCamera].gameObject.SetActive(true);

		yield return StartCoroutine(anim.Animation(false));
		yield return null;
    }

}
