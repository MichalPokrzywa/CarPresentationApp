using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryOverview : MonoBehaviour {

	private static GalleryOverview _instance;
	public static GalleryOverview instance => _instance;

	[SerializeField] GameObject imageContainer;
	[SerializeField] GameObject tumbnail;
	[SerializeField] float cellWidth = 385;
	[SerializeField] float cellHight = 250;
	List<Tumbnail> thumbnails = new List<Tumbnail>();
    // Start is called before the first frame update

    void Awake() {
	    if (_instance != null && _instance != this) {
		    Destroy(gameObject);
	    }
	    else {
		    _instance = this;
	    }
    }
	//information: The first sprite created is going missing and can't override by any other image
	public IEnumerator LoadImage(Texture2D photo,int index) {

		Instantiate(tumbnail, imageContainer.transform);
		Sprite sprite = Sprite.Create(photo, new Rect(0, 0, photo.width, photo.height), Vector2.one * 0.5f);
		Tumbnail component = tumbnail.GetComponent<Tumbnail>();
		component.image.sprite = sprite;
		component.index = index;
		component.UpdateSprite(sprite);
		thumbnails.Add(component);
		Debug.Log(thumbnails.Count);
		Debug.Log(component.index);
		yield return null;
    }

	public IEnumerator UpdateImage(Texture2D photo, int index) {
		//Debug.Log(thumbnails[^1].GetComponent<Image>().sprite.name);
		Sprite sprite = Sprite.Create(photo, new Rect(0, 0, photo.width, photo.height), Vector2.one * 0.5f);
		thumbnails[index].image.sprite = sprite;
		//Debug.Log(thumbnails[^1].GetComponent<Image>().sprite.name);
		yield return null;
	}

	public void ClosePhoto(int index) {
		thumbnails[index].gameObject.SetActive(false);
	}
}