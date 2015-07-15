using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog.movieschallenge;

public class LoginScript : FacadeMonoBehaviour {

	[SerializeField] private InputField _username;
	[SerializeField] private InputField _password;

	void Awake () {
		PlayerPrefs.SetString ("token", ""); //TODO: remove this, it-s just for testing (dirty)
		if (!string.IsNullOrEmpty (PlayerPrefs.GetString ("token"))) {
			// if we already have an authenticated token, load the MainMenu
			_utils.loadScene("MainMenu");
		} 
	}

	public void doLogin() {
		// TODO: add loading while processing
		// authenticate
		Authenticate auth = new Authenticate ();
		auth.login (_username.text, _password.text);
		// TODO: show error if failed
	}

	public void setUsername(InputField username) {
		_username = username;
	}

	public void setPassword(InputField password) {
		_password = password;
	}
}