using UnityEngine;
using System.Collections;
using com.lovelydog.movieschallenge;
using com.lovelydog;
using BestHTTP;
using LitJson;
using GameAnalyticsSDK;

public class Authenticate {
	
	protected Dispatcher<Object> _dispatcher = Dispatcher<Object>.Instance;
	protected Utils utils = new Utils();

	public void authenticate() {
		if (string.IsNullOrEmpty (PlayerPrefs.GetString ("token"))) {
			// if the user is not authenticated, load the login
			utils.loadScene("Login");
		} 
		else {
			// if we already have an authenticated token, send event that we finished authentication and a token is ready
			_dispatcher.Dispatch("auth_finished");
		}
	}

	public void login(string username, string password) {
		if (string.IsNullOrEmpty (PlayerPrefs.GetString ("token"))) {
			// if there is no token stored, negotiate a new one
			doAuthentication(username, password);
		} 
		else {
			// if we already have an authenticated token, load the MainMenu
			utils.loadScene("MainMenu");
		}
	}

	void doAuthentication(string username, string password) {
		GameAnalytics.NewDesignEvent ("ui:user:login");
		if (!string.IsNullOrEmpty (username)
		    && !string.IsNullOrEmpty (password)) {
			API req = new API ();
			req.Post ("/authenticate", onAuthenticationFinished);
			req.AddField ("username", username);
			req.AddField ("password", password);
			req.interstitialLoading = true;
			req.Send ();
		}
	}

	public void createUser(string username, string password, string email) {
		GameAnalytics.NewDesignEvent ("ui:user:login");
		if (!string.IsNullOrEmpty (username)
			&& !string.IsNullOrEmpty (password)
			&& !string.IsNullOrEmpty (email)) {
			API req = new API ();
			req.Post ("/user", onAuthenticationFinished);
			req.AddField ("username", username);
			req.AddField ("password", password);
			req.AddField ("email", email);
			req.interstitialLoading = true;
			req.Send ();
		}
	}
	
	void onAuthenticationFinished(HTTPRequest req, HTTPResponse res) {
		JsonReader json = new JsonReader (res.DataAsText);
		json.SkipNonMembers = true;
		// deserialize
		Token token = JsonMapper.ToObject<Token> (json);
		// authentication correct
		if (token != null) {
			// save to player preferences
			PlayerPrefs.SetString ("token", token.token);
			PlayerPrefs.SetString ("username", token.username);
			PlayerPrefs.Save();
			// when we finish the authentication process, load the main menu
			utils.loadScene("MainMenu");
		}
		// authentication erroneous, show error message
		else {
			//TODO: show error message
		}
	}
}