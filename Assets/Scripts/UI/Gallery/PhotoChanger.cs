using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PhotoChanger : MonoBehaviour
{
	private static PhotoChanger _instance;
	public static PhotoChanger instance => _instance;

	private string[] picturePath;
	private int currentIndice;
	private Texture2D texture;
	[SerializeField] Button nextButton;
	[SerializeField] Button previousButton;
	[SerializeField] Button backButton;
	[SerializeField] Image photo;
	void Awake() {
		if (_instance != null && _instance != this) {
			Destroy(gameObject);
		}
		else {
			_instance = this;
		}
	}
	void Start() {
		picturePath = Directory.GetFiles(GlobalVariables.dirPathHigh, "*.jpg");	
		Debug.Log(picturePath.Length);
		nextButton.onClick.AddListener(MoveForward);
		previousButton.onClick.AddListener(MoveBackward);
		backButton.onClick.AddListener(Close);
		this.gameObject.SetActive(false);
	}

	public void MoveForward() {
		currentIndice++;
		if (currentIndice >= picturePath.Length) {
			currentIndice = 0;
		}
		LoadImage(currentIndice);
		GC.Collect();
	}
	public void MoveBackward() {
		currentIndice--;
		if (currentIndice < 0) {
			currentIndice = picturePath.Length-1;
		}
		LoadImage(currentIndice);
		GC.Collect();
	}

	public void Show(int index) {
		this.gameObject.SetActive(true);
		currentIndice = index;
		LoadImage(index);
	}

	public void Close() {
		Destroy(photo.sprite);
		this.gameObject.SetActive(false);
	}

	//IEnumerator LoadAllImages() {
	//	picturePath = Directory.GetFiles(GlobalVariables.dirPathHigh, "*.jpg");
	//}

	async void LoadImage(int indexFromFile) {
		Image item = photo;
		if (item.sprite != null) {
			Destroy(item.sprite);
		}
		texture = await Request.GetAsyncTexture(picturePath[indexFromFile]);
		int originalWidth = texture.width;
		int originalHeight = texture.height;
		Sprite sprite = Sprite.Create(texture, new Rect(0, 0, originalWidth, originalHeight), Vector2.one * 0.5f);
		item.sprite = sprite;
	}
}
