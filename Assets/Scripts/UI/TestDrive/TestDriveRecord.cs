using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class TestDriveRecord : MonoBehaviour {
	
	[SerializeField] Button deleteButton;
	[SerializeField] TMP_Text textContainer;

	void Start() {
		deleteButton.onClick.AddListener(DestroyRecord);
	}

	public void AddRecord(Entry entry,bool value) {
		textContainer.text = 
			entry.text.Replace('-', '/')+" " + 
			entry.score.Insert(entry.score.Length - 2, ":") +" "+ entry.name;
		UpdateRecord(value);
	}

	public void UpdateRecord(bool today) {
		string[] getDate = textContainer.text.Split(' ');
		if (today && DateTime.Parse(getDate[0]) != DateTime.Today) {
			this.gameObject.SetActive(false);
		}
		else {
			this.gameObject.SetActive(true);
		}
	}


	async void DestroyRecord() {
		string[] getName = textContainer.text.Split(' ');
		await new ApiRequest().DeleteRecord(getName[2]);
		this.gameObject.SetActive(false);
		Destroy(this.gameObject);
	}

}
