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
			// if there is no token stored, negotiate a new one
			HTTPRequest req = new HTTPRequest (new System.Uri (Properties.API + "/authenticate"), HTTPMethods.Post, onAuthenticationFinished);
			req.AddField ("username", "berni");
			req.AddField ("password", "hanabi");
			req.Send ();
		} 
		else {
			// send event that we finished authentication and a token is ready
			this._dispatcher.Dispatch(Game.Events.AUTH_FINISHED);
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
			// send event that we finished authentication and a token is ready
			this._dispatcher.Dispatch(Game.Events.AUTH_FINISHED);
		}
		// authentication erroneous, send to login
		else {
		}
	}
}