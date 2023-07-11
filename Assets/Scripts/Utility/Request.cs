using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

public static class Request {
	public static Task<string[]> GetFileNames(string uri, Action<string> callback) {
		UnityWebRequest uwr = UnityWebRequest.Get("http://itsilesia.com/3d/data/PraktykiGaleria/manifest.txt");
		byte[] results = uwr.downloadHandler.data;
		string[] fileNames = Encoding.Default.GetString(results).Split('\n');
		return Task.FromResult(fileNames);
	}

	public static Task<Sprite> GetSprite(Texture2D image) {
		Sprite s = Sprite.Create(
			image,
			new Rect(0.0f, 0.0f, image.width, image.height),
			new Vector2(0.5f, 0.5f));
		s.name = image.name;
		return Task.FromResult(s);
	}

	public static Task<List<Texture2D>> GetImages(string[] fileNames) {
		List<Texture2D> images = new List<Texture2D>();
		for (int i = 0; i < fileNames.Length; i++) {
			UnityWebRequest uwr = UnityWebRequestTexture.GetTexture($"http://itsilesia.com/3d/data/PraktykiGaleria/{fileNames[i]}");
			if (uwr.result != UnityWebRequest.Result.Success) {
				Debug.Log("Error .. " + uwr.error);
			}
			else {
				images.Add(DownloadHandlerTexture.GetContent(uwr));
			}
		}
		return Task.FromResult(images);
	}
}