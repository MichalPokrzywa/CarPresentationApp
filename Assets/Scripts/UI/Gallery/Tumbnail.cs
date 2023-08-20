using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tumbnail : MonoBehaviour {
	
	public int index;
	public Image image;
	void Start() {
		GetComponent<Button>().onClick.AddListener(ShowPhotoChanger);
		image = GetComponent<Image>();
	}

	public void UpdateSprite(Sprite sprite) {

	}

	void ShowPhotoChanger() {
		PhotoChanger.instance.Show(index);
	}
}
