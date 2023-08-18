using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewHandler : MonoBehaviour {
	[SerializeField] ViewAnimation viewAnimation;
    // Start is called before the first frame update
    void Start() {
	    if (viewAnimation == null) {
		    viewAnimation = GetComponent<ViewAnimation>();
	    }
    }
    public IEnumerator PlayAnimationUp() {
		gameObject.SetActive(true);
	    yield return StartCoroutine(viewAnimation.MakeAnimationUp());
    }
    public IEnumerator PlayAnimationDown() {
	    yield return StartCoroutine(viewAnimation.MakeAnimationDown());
	    gameObject.SetActive(false);
	}

}
