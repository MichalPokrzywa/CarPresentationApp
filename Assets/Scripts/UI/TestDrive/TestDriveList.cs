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

	ApiRequest api = new ApiRequest();
	async void Start() {
		await CreateRecords();
		dateDropdown.onValueChanged.AddListener(UpdateRecords);
		refreshButton.onClick.AddListener(RefreshRecords);
	}

	async Task CreateRecords()
	{
		List<Entry> entries = await api.GetAllRecordResultsAsc();
		
		foreach (Entry entry in entries)
		{
			StartCoroutine(CreateEntry(entry,dateDropdown.value));
		}
	}

	void UpdateRecords(int arg0) {
		foreach (Transform child in listOfDrivesGameObject.transform) {
			child.GetComponent<TestDriveRecord>().UpdateRecord(arg0 == 0);
		}
	}

	async void RefreshRecords() {
		foreach (Transform child in listOfDrivesGameObject.transform) {
			Destroy(child.gameObject);
		}
		await CreateRecords();
	}

	IEnumerator CreateEntry(Entry entry,int value) {
		Instantiate(recordPrefab, listOfDrivesGameObject.transform);
		recordPrefab.GetComponent<TestDriveRecord>().AddRecord(entry,value==0);
		yield return null;
	}

}
