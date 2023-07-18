using Cysharp.Threading.Tasks;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class Request {
	public static async UniTaskVoid GetText(string uri, Action<string> callback) {
		string returnVal = await GetAsyncText(uri);
		callback?.Invoke(returnVal);
	}

	public static async UniTaskVoid GetImage(string uri, Action<Texture2D> callback) {
		Texture2D returnVal = await GetAsyncTexture(uri);
		callback?.Invoke(returnVal);
	}

	public static async UniTask<string> GetAsyncText(string uri) {
		UnityWebRequest wr = UnityWebRequest.Get(uri);
		wr = await GetAsync(wr);
		return wr.downloadHandler.text;
	}

	public static async UniTask<Texture2D> GetAsyncTexture(string uri) {
		UnityWebRequest wr = UnityWebRequestTexture.GetTexture(uri);
		wr = await GetAsync(wr);
		Texture2D tex = DownloadHandlerTexture.GetContent(wr);
		tex.name = uri;
		return tex;
    }

    public static async UniTask<string> PostAsync(string uri, string body) {
        using (UnityWebRequest wr = new UnityWebRequest(uri, "POST")) {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
            wr.uploadHandler = new UploadHandlerRaw(bodyRaw);
            wr.downloadHandler = new DownloadHandlerBuffer();
            wr.SetRequestHeader("Content-Type", "application/json");
            await SendAsync(wr);
            return wr.downloadHandler.text;
        }
    }

    static async UniTask<UnityWebRequest> GetAsync(UnityWebRequest wr) {
		try {
			UnityEngine.Debug.Log($"Start Network Request: {wr.url}");
			await wr.SendWebRequest();
		}
		catch (Exception ex) {
			if (ex is OperationCanceledException) {
				UnityEngine.Debug.Log($"Request Canceled: {wr.url}");
			}
			else if (ex is TimeoutException) {
				UnityEngine.Debug.Log($"Request Timeout: {wr.url}");
			}
			else if (ex is UnityWebRequestException webex) {
				UnityEngine.Debug.Log($"Request web error: {wr.url} Code:{webex.ResponseCode} Message:{webex.Message}");
			}
		}
		return wr;
	}
	static async UniTask<UnityWebRequest> SendAsync(UnityWebRequest wr) {
		try {
			UnityEngine.Debug.Log($"Start Network Request: {wr.url}");
			await wr.SendWebRequest();
		}
		catch (Exception ex) {
			if (ex is OperationCanceledException) {
				UnityEngine.Debug.Log($"Request Canceled: {wr.url}");
			}
			else if (ex is TimeoutException) {
				UnityEngine.Debug.Log($"Request Timeout: {wr.url}");
			}
			else if (ex is UnityWebRequestException webex) {
				UnityEngine.Debug.Log($"Request web error: {wr.url} Code:{webex.ResponseCode} Message:{webex.Message}");
			}
		}
		return wr;
	}
}
