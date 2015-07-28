using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class LoginButtonScript : FacadeMonoBehaviour {
	
	CanvasGroup canvas;
	
	void Awake() {
		canvas = transform.GetComponent<CanvasGroup> ();
		_dispatcher.AddListener ("login_button_hide", hide);
		_dispatcher.AddListener ("login_button_show", show);
	}
	
	void hide(Object data) {
		Utils.hideCanvas (canvas);
	}
	
	void show(Object data) {
		Utils.showCanvas (canvas);
	}

	public void showLogin() {
		_dispatcher.Dispatch ("login_button_hide");
		_dispatcher.Dispatch ("new_user_button_show");
		_dispatcher.Dispatch ("login_show");
		_dispatcher.Dispatch ("register_hide");
	}
}
