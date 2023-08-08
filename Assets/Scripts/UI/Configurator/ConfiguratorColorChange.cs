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
    }

	public void UpdateImage(Image image) {
		this.image = image;
	}

	public void UpdateImageColor(Color color) {
		this.image.color = color;
	}

	public void UpdateImageColor() {
		this.image.color = color;
	}
}
