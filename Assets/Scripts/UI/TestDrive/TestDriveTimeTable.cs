using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	// Start is called before the first frame update
	async void Start() {
		dateConfig = JsonConvert.DeserializeObject<DateConfig>(await Request.GetAsyncText(GlobalVariables.dateConfig));
		List<string> date = new List<string>();
		List<string> date2 = new List<string>();
		foreach (DateTime day in EachDay(DateTime.Parse(dateConfig.startDate), DateTime.Parse(dateConfig.endDate))) {
			date.Add(day.ToString("dd'/'MM'/'yyyy"));
		}
		dateDropdown.AddOptions(date);
		foreach (DateTime time in EachTime(DateTime.Parse(dateConfig.startDate+" "+ dateConfig.startTime), DateTime.Parse(dateConfig.startDate + " " + dateConfig.endTime))) {
			date2.Add(time.ToString("HH:mm"));
		}


		dateDropdown.onValueChanged.AddListener(UpdateHours);
		timeDropdown.AddOptions(date2);
		sendButton.onClick.AddListener(AddNewRecord);
	}

	private async void UpdateHours(int arg0) {
		List<Entry> entries = await api.GetAllRecordResults();
		List<string> optionsToKeep = new List<string>();

		foreach (DateTime time in EachTime(DateTime.Parse(dateConfig.startDate + " " + dateConfig.startTime), DateTime.Parse(dateConfig.startDate + " " + dateConfig.endTime))) {
			optionsToKeep.Add(time.ToString("HH:mm"));
		}
		foreach (Entry en in entries) {
			if (optionsToKeep.Contains(en.score.Insert(en.score.Length - 2, ":")) && dateDropdown.options[arg0].text == en.text.Replace('-', '/')) {
				optionsToKeep.Remove(en.score.Insert(en.score.Length - 2, ":"));
			}
		}
		timeDropdown.ClearOptions();
		timeDropdown.AddOptions(optionsToKeep);

	}

	IEnumerable<DateTime> EachDay(DateTime from, DateTime thru) {
		if (from < DateTime.Today) {
			from = DateTime.Today;
		}
	    for (DateTime day = from.Date; day.Date <= thru.Date; day = day.AddDays(1)) {
		    yield return day;
	    }
    }

    IEnumerable<DateTime> EachTime(DateTime from, DateTime thru) {
	    if (from < DateTime.Today) {
		    from = DateTime.Today + from.TimeOfDay;
		    thru = DateTime.Today + thru.TimeOfDay;
	    }
	    for (DateTime time = from.Date + from.TimeOfDay ; time <= thru.Date + thru.TimeOfDay; time = time.Add(TimeSpan.FromMinutes(30))) {
			if (time > DateTime.Now) {
				yield return time;
			}
	    }
	}

    async void AddNewRecord() {

		Debug.Log(dateDropdown.options[dateDropdown.value].text.Replace('/','-'));
		Debug.Log(timeDropdown.options[timeDropdown.value].text.Replace(":",string.Empty));
		Debug.Log(inputField.text);
		if (inputField.text == String.Empty) {
			await api.AddRecord($"{dateDropdown.options[dateDropdown.value].text.Replace('/', '-')}{timeDropdown.options[timeDropdown.value].text.Replace(":", string.Empty)}", timeDropdown.options[timeDropdown.value].text.Replace(":", string.Empty),
				dateDropdown.options[dateDropdown.value].text.Replace('/', '-'));
		}
		else { 
			await api.AddRecord(inputField.text,timeDropdown.options[timeDropdown.value].text.Replace(":", string.Empty),
			dateDropdown.options[dateDropdown.value].text.Replace('/', '-'));
		}
    }
}
