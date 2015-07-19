using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class ScrollViewContentScript : FacadeMonoBehaviour {

	RectTransform rect;

	void Awake() {
		// get components
		rect = transform.GetComponent<RectTransform> ();
		// bind events
		_dispatcher.AddListener ("resize_scroll_content", resize);
	}

	void resize(Object data) {
		int numGames = ((PayloadObject)data).intPayload;
		int height = (numGames * 185) + 100;
		rect.sizeDelta = new Vector2 (0, height);
	}
}
