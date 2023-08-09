using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfiguratorColorChange : MonoBehaviour {

	public Color color;
	public Image image;

	void Start() {
		image = GetComponent<Image>();
	}
	public void UpdateColor(Color color) {
		this.color = color;
		UpdateImageColor();

	}

	public void UpdateImage(Image image2) {
		image = image2;
	}

	public void UpdateImageColor(Color color) {
		image.color = color;
	}

	public void UpdateImageColor() {
		image.color = color;
	}
}
