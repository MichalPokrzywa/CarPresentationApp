using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GalleryManager : MonoBehaviour {
	List<Texture2D> photosList;
	GalleryOverview gallery;
	private string[] photoFiles;
	void Start() {
		photoFiles = System.IO.Directory.GetFiles(GlobalVariables.dirPathLow, "*.jpg");
		gallery = GalleryOverview.instance;
		photosList = new List<Texture2D>();
		AddImages(photoFiles);
	}

	public Texture2D GetPhoto(int index) {
		return photosList[index];
	}
	async void AddImages(string[] fileNames) {
		for (int i = 0; i < fileNames.Length; i++) {
			UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(fileNames[i]);
			photosList.Add(await Request.GetAsyncTexture(fileNames[i]));
			StartCoroutine(gallery.LoadImage(photosList[i],i));
		}
	}
	//private IEnumerator Check() {
	//	UnityWebRequest w = UnityWebRequest.Get("http://itsilesia.com/3d/data/PraktykiGaleria/manifest.txt");
	//	yield return w.SendWebRequest();
	//	if (w.error != null) {
	//		Debug.Log("Error .. " + w.error);
	//	}
	//	else {
	//		//Retrieve results as binary data
	//		byte[] results = w.downloadHandler.data;
	//		string[] fileNames = System.Text.Encoding.Default.GetString(results).Split('\n');
	//		for (int i = 0; i < fileNames.Length; i++) {
	//			Debug.Log(fileNames[i]);
	//		}
	//		yield return StartCoroutine(AddImages(fileNames));
	//	}
	//}

}
