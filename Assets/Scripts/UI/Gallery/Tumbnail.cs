using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tumbnail : MonoBehaviour {
	
	public int index;
	void Start() {
		GetComponent<Button>().onClick.AddListener(ShowPhotoChanger);
	}

	void ShowPhotoChanger() {
		PhotoChanger.instance.Show(index);
	}
}
