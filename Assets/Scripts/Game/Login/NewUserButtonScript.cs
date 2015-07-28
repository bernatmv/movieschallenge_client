using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class NewUserButtonScript : FacadeMonoBehaviour {

	CanvasGroup canvas;

	void Awake() {
		canvas = transform.GetComponent<CanvasGroup> ();
		_dispatcher.AddListener ("new_user_button_hide", hide);
		_dispatcher.AddListener ("new_user_button_show", show);
	}

	void hide(Object data) {
		Utils.hideCanvas (canvas);
	}

	void show(Object data) {
		Utils.showCanvas (canvas);
	}
	
	public void showRegister() {
		_dispatcher.Dispatch ("new_user_button_hide");
		_dispatcher.Dispatch ("login_button_show");
		_dispatcher.Dispatch ("login_hide");
		_dispatcher.Dispatch ("register_show");
	}
}
