using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewGroupManager : MonoBehaviour
{
	private static ViewGroupManager _instance;
	public static ViewGroupManager instance => _instance;

	readonly List<GameObject> views = new List<GameObject>();
	int currentView;
	void Awake() {
		if (_instance != null && _instance != this) {
			Destroy(gameObject);
		}
		else {
			_instance = this;
		}
	}

	void Start() {
		currentView = 0;
		foreach (Transform child in transform) {
			views.Add(child.gameObject);
		}
		views[0].SetActive(true);
	}

	public IEnumerator ChangeView(int newView) {
		views[currentView].SetActive(false);
		StartCoroutine(views[currentView].GetComponent<ViewAnimation>().MakeAnimationDown(currentView));
		views[newView].SetActive(true);
		StartCoroutine(views[newView].GetComponent<ViewAnimation>().MakeAnimationUp(newView));
		currentView = newView;
		yield return null;
	}

}
