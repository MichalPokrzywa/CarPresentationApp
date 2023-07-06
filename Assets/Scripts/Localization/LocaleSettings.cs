using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocaleSettings : MonoBehaviour {

	private int currentLocale = 0;
	private Image flag;
	private List<Locale> tableOfLocales;
	[SerializeField] List<Sprite> localeFlag;

	void Start() {
		tableOfLocales = LocalizationSettings.AvailableLocales.Locales;
		currentLocale = 0;
		flag = GetComponent<Image>();
		flag.sprite = localeFlag[currentLocale];
		GetComponent<Button>().onClick.AddListener(ChangeLocale);
		StartCoroutine(SetLocale(currentLocale));
	}

	void ChangeLocale() {

		currentLocale++;
		if (tableOfLocales.Count - 1 < currentLocale) {
			currentLocale = 0;
		}
		flag.sprite = localeFlag[currentLocale];
		StartCoroutine(SetLocale(currentLocale));
	}

	IEnumerator SetLocale(int localeID) {

		yield return LocalizationSettings.InitializationOperation;
		LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];

	}
}
