using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class LoginErrorScript : FacadeMonoBehaviour {
	
	Text usernameText;
	
	void Awake() {
		usernameText = transform.GetComponent<Text> ();
		_dispatcher.AddListener ("show_error_register_login", showError);
	}
	
	void showError(Object data) {
		usernameText.text = "<i><color=#ff0000ff>" + _i18n.get("ERROR_LOGIN") + "</color></i>";
	}
}
