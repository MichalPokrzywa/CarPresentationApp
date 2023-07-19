using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToBasicView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GetComponent<Button>().onClick.AddListener(Back);
    }

    void Back() {
	    StartCoroutine(ViewGroupManager.instance.ChangeView(0));
    }
}
