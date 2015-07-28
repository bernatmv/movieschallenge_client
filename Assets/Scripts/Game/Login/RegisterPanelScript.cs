using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class RegisterPanelScript : FacadeMonoBehaviour {
	
	CanvasGroup canvas;
	
	void Awake() {
		canvas = transform.GetComponent<CanvasGroup> ();
		_dispatcher.AddListener ("register_hide", hide);
		_dispatcher.AddListener ("register_show", show);
	}
	
	void hide(Object data) {
		Utils.hideCanvas (canvas);
	}
	
	void show(Object data) {
		Utils.showCanvas (canvas);
	}
}
