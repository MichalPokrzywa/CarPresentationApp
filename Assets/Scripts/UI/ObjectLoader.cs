using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLoader : MonoBehaviour {
	[SerializeField] List<GameObject> objectsToLoad;
	void Start() {
		StartCoroutine(WaitForGameObjects());
	}

	public IEnumerator WaitForGameObjects() {
		// Wait for all GameObjects to complete their Start function
		foreach (GameObject gameObject in objectsToLoad) {
			gameObject.gameObject.SetActive(true);
			StartCoroutine(gameObject.GetComponent<WaitToLoad>().WaitForGameObjects());
			yield return new WaitUntil(() => gameObject.GetComponent<WaitToLoad>().IsStartComplete);
			gameObject.gameObject.SetActive(false);
		}
	}
}
