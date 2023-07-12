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
		Debug.Log("ujonqefo");
		if (currentIndice >= picturePath.Length) {
			currentIndice = 0;
		}
		StartCoroutine(LoadImage(currentIndice));
		GC.Collect();
	}
	public void MoveBackward() {
		currentIndice--;
		Debug.Log("jnkk");
		if (currentIndice < 0) {
			currentIndice = picturePath.Length-1;
		}
		StartCoroutine(LoadImage(currentIndice));
		GC.Collect();
	}

	public void Show(int index) {
		this.gameObject.SetActive(true);
		currentIndice = index;
		StartCoroutine(LoadImage(index));
	}

	public void Close() {
		Destroy(photo.sprite);
		this.gameObject.SetActive(false);
	}

	//IEnumerator LoadAllImages() {
	//	picturePath = Directory.GetFiles(GlobalVariables.dirPathHigh, "*.jpg");
	//}

	IEnumerator LoadImage(int indexFromFile) {
		Image item = photo;
		using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(picturePath[indexFromFile])) {
			yield return uwr.SendWebRequest();
			if (item.sprite != null) {
				Destroy(item.sprite);
			}
			if (uwr.result != UnityWebRequest.Result.Success) {
				Debug.Log(uwr.error);
			}
			else {
				texture = DownloadHandlerTexture.GetContent(uwr);
			}
		}
		int originalWidth = texture.width;
		int originalHeight = texture.height;
		Sprite sprite = Sprite.Create(texture, new Rect(0, 0, originalWidth, originalHeight), Vector2.one * 0.5f);
		item.sprite = sprite;
		yield return new WaitForSeconds(0.2f);
	}
}
