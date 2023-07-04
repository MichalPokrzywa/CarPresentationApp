using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocaleSettings : MonoBehaviour {

	private int currentLocale = 0;
	private List<Locale> tableOfLocales = LocalizationSettings.AvailableLocales.Locales;

	void ChangeLocale() {

		currentLocale++;
		if (tableOfLocales.Count - 1 < currentLocale) {
			currentLocale = 0;
		}

		SetLocale(currentLocale);
	}


	IEnumerator SetLocale(int localeID) {

		yield return LocalizationSettings.InitializationOperation;
		LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];

	}



}
