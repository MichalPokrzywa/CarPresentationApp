using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewGroupManager : MonoBehaviour
{
	private static ViewGroupManager _instance;
	public static ViewGroupManager instance => _instance;

	public readonly List<GameObject> views = new List<GameObject>();
	GameObject currentView;
	void Awake() {
		if (_instance != null && _instance != this) {
			Destroy(gameObject);
		}
		else {
			_instance = this;
		}
	}

	void Start() {
		foreach (Transform child in transform) {
			views.Add(child.gameObject);
		}
		currentView = views[0];
		views.RemoveAt(views.Count-1);
		currentView.SetActive(true);
	}

	public IEnumerator ChangeView(GameObject view) {
		yield return StartCoroutine(currentView.GetComponent<ViewHandler>().PlayAnimationDown());
		yield return StartCoroutine(view.GetComponent<ViewHandler>().PlayAnimationUp());
		currentView = view;
	}

}
