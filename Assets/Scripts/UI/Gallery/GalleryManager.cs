using System.IO;
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
		photoFiles = Directory.GetFiles(GlobalVariables.dirPathLow, "*.jpg");
		gallery = GalleryOverview.instance;
		photosList = new List<Texture2D>(); 
		StartCoroutine(AddImages(photoFiles));
	}

	public Texture2D GetPhoto(int index) {
		return photosList[index];
	}
	IEnumerator AddImages(string[] fileNames) {
		for (int i = 0; i < fileNames.Length; i++) { ;
			Debug.Log(photoFiles[i]);
			byte [] bytes = File.ReadAllBytes(fileNames[i]);
			Texture2D texture = new Texture2D(2, 2);
			texture.LoadImage(bytes);
			yield return StartCoroutine(gallery.LoadImage(texture, i));
		}
		GetComponent<LoadingInformation>().SetLoading(true);
	}
}
