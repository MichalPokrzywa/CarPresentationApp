using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour {
	public static PopupManager instance { get; private set; }
	[SerializeField] TMP_Text title;
	[SerializeField] TMP_Text description;
	[SerializeField] Button acceptButton;
	[SerializeField] Button refuseButton;
	[SerializeField] Button exitButton;
	LocalizeStringEvent localizeStringEvent;
	Action<bool> choiceCallback;
	bool isInitialized = false;
	// Start is called before the first frame update
	void Start()
    {
	    exitButton.onClick.AddListener(ExitPopup);
		acceptButton.onClick.AddListener(OnAcceptButtonClicked);
		refuseButton.onClick.AddListener(OnRefuseButtonClicked);
		gameObject.SetActive(false);
    }
	private void Awake() {
		if (instance == null) {
			instance = this;
			Initialize();
		}
		else {
			Destroy(gameObject);
		}
	}
	private void Initialize() {
		// Additional initialization logic goes here
		isInitialized = true;
	}
	// Update is called once per frame
	public void ShowPopup(string title,string description,string acceptInformation,string refuseInformation,Action<bool> callback) {
		if (!isInitialized) {
			Initialize();
		}
		Locale activeLocale = LocalizationSettings.Instance.GetSelectedLocale();
		LocalizedStringDatabase localized = LocalizationSettings.StringDatabase;
		this.title.text = localized.GetLocalizedString(title, activeLocale);
		this.description.text = localized.GetLocalizedString(description, activeLocale);
		this.acceptButton.GetComponentInChildren<TMP_Text>().text = localized.GetLocalizedString(acceptInformation, activeLocale);
		this.refuseButton.GetComponentInChildren<TMP_Text>().text = localized.GetLocalizedString(refuseInformation, activeLocale);
		choiceCallback = callback;
		this.gameObject.SetActive(true);
	}

	void ExitPopup() {
		choiceCallback?.Invoke(false);
		this.gameObject.SetActive(false);
	}
	public void OnAcceptButtonClicked() {
		choiceCallback?.Invoke(true);
		this.gameObject.SetActive(false);
	}

	public void OnRefuseButtonClicked() {
		choiceCallback?.Invoke(false);
		this.gameObject.SetActive(false);
	}
}
