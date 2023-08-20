using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TestDriveList : MonoBehaviour {
	[SerializeField] TMP_Dropdown dateDropdown;
	[SerializeField] Button refreshButton;
	[SerializeField] GameObject listOfDrivesGameObject;
	[SerializeField] GameObject recordPrefab;
	[SerializeField] LoaderAnimation loaderAnimation;
	ApiRequest api = new ApiRequest();
	async void Start() {
		LoadingInformation loadingInformation = GetComponent<LoadingInformation>();
		await CreateRecords();
		dateDropdown.onValueChanged.AddListener(UpdateRecords);
		refreshButton.onClick.AddListener(RefreshRecords);
		UpdateRecords(0);
		await Task.Delay(3000);
		loadingInformation.SetLoading(true);
	}

	async Task CreateRecords()
	{
		List<Entry> entries = await api.GetAllRecordResultsAsc();
		if (entries != null) {
			foreach (Entry entry in entries){
				StartCoroutine(CreateEntry(entry,dateDropdown.value));
			}
		}
	}

	void UpdateRecords(int arg0) {
		StartCoroutine(ShowLoader());
		foreach (Transform child in listOfDrivesGameObject.transform) {
			child.GetComponent<TestDriveRecord>().UpdateRecord(arg0 == 0);
		}
		StartCoroutine(EndLoader());
	}

	async void RefreshRecords() {
		StartCoroutine(ShowLoader());
		foreach (Transform child in listOfDrivesGameObject.transform) {
			Destroy(child.gameObject);
		}
		await CreateRecords();
		UpdateRecords(dateDropdown.value);
		StartCoroutine(EndLoader());
	}

	IEnumerator CreateEntry(Entry entry,int value) {
		Instantiate(recordPrefab, listOfDrivesGameObject.transform);
		recordPrefab.GetComponent<TestDriveRecord>().AddRecord(entry,value==0);
		yield return null;
	}

	IEnumerator ShowLoader() {
		yield return loaderAnimation.Run();
	}

	IEnumerator EndLoader() {
		yield return loaderAnimation.Stop();
	}

}
