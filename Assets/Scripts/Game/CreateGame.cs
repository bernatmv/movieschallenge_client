using UnityEngine;
using System.Collections;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class CreateGame : FacadeMonoBehaviour {

	// create a new game
	public void createGame() {
		string challenger = PlayerPrefs.GetString("username");
		string challenged = (challenger == "berni") ? "gemmins" : "berni";
		Debug.Log (PlayerPrefs.GetString("token"));
		HTTPRequest request = new HTTPRequest (new System.Uri (Properties.API + "/game"), HTTPMethods.Post, (HTTPRequest req, HTTPResponse res) => {
			//TODO: jump to the new game
		});
		request.AddField ("challenger", challenger);
		request.AddField ("challenged", challenged);
		request.AddField ("token", PlayerPrefs.GetString("token"));
		request.Send ();
	}
}