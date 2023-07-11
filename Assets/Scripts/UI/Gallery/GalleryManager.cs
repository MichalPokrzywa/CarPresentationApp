using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GalleryManager : MonoBehaviour {
	List<Texture2D> photosList;
	GalleryOverview gallery;
	void Start() {
		gallery = GalleryOverview.instance;
		photosList = new List<Texture2D>();
		StartCoroutine(Check());
	}

	public Texture2D GetPhoto(int index) {
		return photosList[index];
	}

	private IEnumerator Check() {
		UnityWebRequest w = UnityWebRequest.Get("http://itsilesia.com/3d/data/PraktykiGaleria/manifest.txt");
		yield return w.SendWebRequest();
		if (w.error != null) {
			Debug.Log("Error .. " + w.error);
		}
		else {
			//Retrieve results as binary data
			byte[] results = w.downloadHandler.data;
			string[] fileNames = System.Text.Encoding.Default.GetString(results).Split('\n');
			for (int i = 0; i < fileNames.Length; i++) {
				Debug.Log(fileNames[i]);
			}
			yield return StartCoroutine(AddImages(fileNames));
		}
	}
	private IEnumerator AddImages(string[] fileNames) {
		for (int i = 0; i < fileNames.Length; i++) {
			UnityWebRequest uwr = UnityWebRequestTexture.GetTexture($"http://itsilesia.com/3d/data/PraktykiGaleria/{fileNames[i]}");
			Debug.Log(uwr.url);
			yield return uwr.SendWebRequest();
			if (uwr.result != UnityWebRequest.Result.Success) {
				Debug.Log("Error .. " + uwr.error);
			}
			else {
				photosList.Add(DownloadHandlerTexture.GetContent(uwr));
				yield return StartCoroutine(gallery.LoadImage(photosList[i]));
			}
		}
	}
}
