using UnityEngine;
using System.Collections;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class GamesLoader : FacadeMonoBehaviour {

	void Awake() {
		// bind events
		this._dispatcher.AddListener (Game.Events.AUTH_FINISHED, buildScene);
	}

	void Start () {
		// authenticate
		Authenticate auth = new Authenticate ();
		auth.authenticate ();
	}

	// build the scene
	void buildScene<Event>(Event e) {
		Debug.Log (PlayerPrefs.GetString("token"));
	}

	//get games from API
	void getGames() {
		HTTPRequest req = new HTTPRequest(new System.Uri(Properties.API + "/authenticate"), (HTTPRequest request, HTTPResponse response) => {
			Debug.Log("Finished");
		}).Send();
	}
}
