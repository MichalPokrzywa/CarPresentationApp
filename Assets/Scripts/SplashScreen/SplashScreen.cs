using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

	[SerializeField] Image logo;
	[SerializeField] float timeScale = 1.5f;
	[SerializeField] float startTime = 1f;
	CanvasGroup canvasGroup;
	string filePath;
	// Start is called before the first frame update
	async void Start() {
		canvasGroup = GetComponent<CanvasGroup>();
		StartCoroutine(StartLoad());
	}

	IEnumerator StartLoad() {
		LoopLogo();
		yield return StartCoroutine(Check());
		yield return new WaitForSeconds(2f);
		yield return StartCoroutine(TurnOff());
		this.gameObject.SetActive(false);
	}

	void LoopLogo() {
		Sequence mySequence = DOTween.Sequence();
		mySequence.Insert(startTime, logo.transform.DOScale(1.3f, timeScale).SetEase(Ease.InSine));
		mySequence.Insert(startTime + 1, logo.transform.DOScale(1f, timeScale).SetEase(Ease.OutSine));
		mySequence.SetLoops(-1);
		mySequence.Play();
	}

	Texture2D SmollTexture(Texture2D tex) {
		Texture2D result = new Texture2D(385, 250, tex.format, true);
		Color[] rpixels = result.GetPixels(0);
		float incX = (1.0f / (float)385);
		float incY = (1.0f / (float)250);
		for (int px = 0; px < rpixels.Length; px++) {
			rpixels[px] = tex.GetPixelBilinear(incX * ((float)px % 385), incY * ((float)Mathf.Floor(px / 385)));
		}
		result.SetPixels(rpixels, 0);
		result.Apply();
		return result;
	}

	IEnumerator TurnOff() {
		canvasGroup.DOFade(0, timeScale);
		yield return new WaitForSeconds(timeScale);
	}
	IEnumerator Check() {
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
	IEnumerator AddImages(string[] fileNames) {
		if (!System.IO.Directory.Exists(GlobalVariables.dirPathHigh)) {
			System.IO.Directory.CreateDirectory(GlobalVariables.dirPathHigh);
		}
		if (!System.IO.Directory.Exists(GlobalVariables.dirPathLow)) {
			System.IO.Directory.CreateDirectory(GlobalVariables.dirPathLow);
		}
		for (int i = 0; i < fileNames.Length; i++) {
			UnityWebRequest uwr = UnityWebRequestTexture.GetTexture($"http://itsilesia.com/3d/data/PraktykiGaleria/{fileNames[i]}");
			Debug.Log(uwr.url);
			yield return uwr.SendWebRequest();
			if (uwr.result != UnityWebRequest.Result.Success) {
				Debug.Log("Error .. " + uwr.error);
			}
			else {
				if (!System.IO.File.Exists(GlobalVariables.dirPathHigh + "/high" + fileNames[i]) || !System.IO.File.Exists(GlobalVariables.dirPathLow + "/low" + fileNames[i])) {
					yield return StartCoroutine(SaveImage(DownloadHandlerTexture.GetContent(uwr), fileNames[i]));
				}
			}
		}
	}

	IEnumerator SaveImage(Texture2D tex, string filename) {
		byte[] bytes = tex.EncodeToJPG();
		System.IO.File.WriteAllBytes(GlobalVariables.dirPathHigh + "/high" + filename, bytes);
		bytes = SmollTexture(tex).EncodeToJPG();
		System.IO.File.WriteAllBytes(GlobalVariables.dirPathLow + "/low" + filename, bytes);
		yield return new WaitForSeconds(0.1f);
	}

}
