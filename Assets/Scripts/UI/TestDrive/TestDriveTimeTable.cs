using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class TestDriveTimeTable : MonoBehaviour {
	[SerializeField] TMP_Dropdown dateDropdown;
	[SerializeField] TMP_Dropdown timeDropdown;
	[SerializeField] TMP_InputField inputField;
	[SerializeField] Button sendButton;


	ApiRequest api = new ApiRequest();
	DateConfig dateConfig = null;
	DateTime startDate;
	DateTime endDate;

	DateTime starTime;
	DateTime endTime;

	// Start is called before the first frame update
	async void Start() {
		dateConfig = JsonConvert.DeserializeObject<DateConfig>(await Request.GetAsyncText(GlobalVariables.dateConfig));
		List<string> date = new List<string>();
		foreach (DateTime day in EachDay(DateTime.Parse(dateConfig.startDate), DateTime.Parse(dateConfig.endDate))) {
			date.Add(day.ToString("dd'/'MM'/'yyyy"));
		}
		dateDropdown.AddOptions(date);
		date.Clear();
		foreach (DateTime time in EachTime(DateTime.Parse(dateConfig.startDate+" "+ dateConfig.startTime), DateTime.Parse(dateConfig.startDate + " " + dateConfig.endTime))) {
			date.Add(time.ToString("HH:mm"));
		}
		timeDropdown.AddOptions(date);
		sendButton.onClick.AddListener(AddNewRecord);
	}

    IEnumerable<DateTime> EachDay(DateTime from, DateTime thru) {
	    for (DateTime day = from.Date; day.Date <= thru.Date; day = day.AddDays(1)) {
		    yield return day;
	    }
    }
    IEnumerable<DateTime> EachTime(DateTime from, DateTime thru) {
	    for (DateTime time = from.Date + from.TimeOfDay; time <= thru.Date + thru.TimeOfDay; time = time.Add(TimeSpan.FromMinutes(30))) {
		    yield return time;
	    }
	}

    async void AddNewRecord() {

		Debug.Log(dateDropdown.options[dateDropdown.value].text.Replace('/','-'));
		Debug.Log(timeDropdown.options[timeDropdown.value].text.Replace(":",string.Empty));
		Debug.Log(inputField.text);
		await api.AddRecord(inputField.text,timeDropdown.options[timeDropdown.value].text.Replace(":", string.Empty),
			dateDropdown.options[dateDropdown.value].text.Replace('/', '-'));

    }
}
