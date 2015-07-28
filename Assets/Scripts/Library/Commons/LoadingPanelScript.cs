using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class LoadingPanelScript : FacadeMonoBehaviour {
	
	CanvasGroup panel;

	void Awake () {
		// get elements
		panel = transform.GetComponent<CanvasGroup> ();
		// bind events
		_dispatcher.AddListener ("loading_interstitial_open", open);
		_dispatcher.AddListener ("loading_interstitial_close", close);
		Debug.Log ("==============");
		Debug.Log (panel);
	}

	void open(Object data) {
		Debug.Log ("++++++++++++");
		Debug.Log (panel);
		show (panel);
	}

	void close(Object data) {
		Debug.Log ("------------");
		Debug.Log (panel);
		hide (panel);
	}
	
	void hide(CanvasGroup canvas) {
		Utils.hideCanvas (canvas);
	}
	
	void show(CanvasGroup canvas) {
		Utils.showCanvas (canvas);
	}	
}
