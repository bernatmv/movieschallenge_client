using UnityEngine;
using System.Collections;
using com.lovelydog.movieschallenge;
using com.lovelydog.events;
using BestHTTP;
using LitJson;

public class Authenticate {
	
	protected Dispatcher<Game.Events> _dispatcher = Dispatcher<Game.Events>.Instance;

	public void authenticate() {
		if (string.IsNullOrEmpty (PlayerPrefs.GetString ("token"))) {
			// if the user is not authenticated, load the login
			Application.LoadLevel("Login");
		} 
		else {
			// if we already have an authenticated token, send event that we finished authentication and a token is ready
			this._dispatcher.Dispatch(Game.Events.AUTH_FINISHED);
		}
	}

	public void login(string username, string password) {
		if (string.IsNullOrEmpty (PlayerPrefs.GetString ("token"))) {
			// if there is no token stored, negotiate a new one
			doAuthentication(username, password);
		} 
		else {
			// if we already have an authenticated token, load the MainMenu
			Application.LoadLevel("MainMenu");
		}
	}

	void doAuthentication(string username, string password) {
		// if there is no token stored, negotiate a new one
		HTTPRequest req = new HTTPRequest (new System.Uri (Properties.API + "/authenticate"), HTTPMethods.Post, onAuthenticationFinished);
		req.AddField ("username", username);
		req.AddField ("password", password);
		req.Send ();
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
			// when we finish the authentication process, load the main menu
			Application.LoadLevel("MainMenu");
		}
		// authentication erroneous, show error message
		else {
			//TODO: show error message
		}
	}
}