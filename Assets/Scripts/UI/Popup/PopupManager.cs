using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour {
	public static PopupManager instance { get; private set; }
	[SerializeField] TMP_Text title;
	[SerializeField] TMP_Text description;
	[SerializeField] Button acceptButton;
	[SerializeField] Button refuseButton;
	[SerializeField] Button exitButton;
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
		this.title.text = title;
		this.description.text = description;
		this.acceptButton.GetComponentInChildren<TMP_Text>().text = acceptInformation;
		this.refuseButton.GetComponentInChildren<TMP_Text>().text = refuseInformation;
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
