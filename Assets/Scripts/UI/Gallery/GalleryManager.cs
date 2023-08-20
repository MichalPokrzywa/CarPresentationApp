using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class GalleryManager : MonoBehaviour {
	List<Texture2D> photosList;
	GalleryOverview gallery;
	private string[] photoFiles;
	async void Start() {
		photoFiles = System.IO.Directory.GetFiles(GlobalVariables.dirPathLow, "*.jpg");
		gallery = GalleryOverview.instance;
		photosList = new List<Texture2D>();
		//await AddImages(photoFiles);
		StartCoroutine(AddImages(photoFiles));
	}

	public Texture2D GetPhoto(int index) {
		return photosList[index];
	}
	IEnumerator AddImages(string[] fileNames) {
		for (int i = 0; i < fileNames.Length; i++) {
			UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(fileNames[i]);
			yield return uwr.SendWebRequest();
			Debug.Log(photoFiles[i]);
			if (uwr.result == UnityWebRequest.Result.Success) {
				Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
				photosList.Add(texture);
				yield return StartCoroutine(gallery.LoadImage(texture, i));
			}
			else {
				Debug.LogError("Error downloading image: " + uwr.error);
			}
		}
		GetComponent<LoadingInformation>().SetLoading(true);
	}
}
