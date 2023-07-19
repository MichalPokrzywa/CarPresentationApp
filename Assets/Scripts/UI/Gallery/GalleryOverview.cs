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

		Instantiate(tumbnail, imageContainer.transform);
		Sprite sprite = Sprite.Create(photo, new Rect(0, 0, photo.width, photo.height), Vector2.one * 0.5f);
		Debug.Log(sprite);
		tumbnail.GetComponent<Image>().sprite = sprite;
		tumbnail.GetComponent<Tumbnail>().index = index;
		
		yield return null;
    }
}