using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class ApiRequest {

	Dreamlo dreamlo;
	string privateUrl = "http://dreamlo.com/lb/r74qg1qSJ0aHRTS0BwmQEAd5cnUWgloEC7eavRsDor9Q";
	string publicUrl = "http://dreamlo.com/lb/64b5236a8f40bb67c4286be3";

	public async UniTask<List<Entry>> GetAllRecordResults() {
		Root apiRoot = JsonConvert.DeserializeObject<Root>(await Request.GetAsyncText(publicUrl+"/json"));
		return apiRoot?.dreamlo.leaderboard?.entry;
	}
	public async UniTask<List<Entry>> GetAllRecordResultsAsc() {
		Root apiRoot = JsonConvert.DeserializeObject<Root>(await Request.GetAsyncText(publicUrl + "/json-score-asc"));
		return apiRoot?.dreamlo.leaderboard?.entry;
	}

	public async UniTask<string> AddRecord(string name,string time,string date) {
		return await Request.PostAsync($"{privateUrl}/add/{name}/{time}/0/{date}","body");
	}

	public async UniTask<string> DeleteRecord(string name) {
		return await Request.PostAsync($"{privateUrl}/delete/{name}","body");
	}

	public async UniTask<string> ClearRecords() {
		return await Request.PostAsync($"{privateUrl}/clear","body");
	}

}
