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
    public IEnumerator LoadImage(Texture2D photo,int index) {

		GameObject newGameObject = Instantiate(tumbnail, imageContainer.transform);
		Sprite sprite = Sprite.Create(photo, new Rect(0, 0, photo.width, photo.height), Vector2.one * 0.5f);
		Tumbnail component = newGameObject.GetComponent<Tumbnail>();
		component.image.sprite = sprite;
		component.index = index;
		component.UpdateSprite(sprite);
		thumbnails.Add(component);
		yield return null;
    }

	public IEnumerator UpdateImage(Texture2D photo, int index) {
		Sprite sprite = Sprite.Create(photo, new Rect(0, 0, photo.width, photo.height), Vector2.one * 0.5f);
		thumbnails[index].image.sprite = sprite;
		yield return null;
	}

	public void ClosePhoto(int index) {
		thumbnails[index].gameObject.SetActive(false);
	}
}