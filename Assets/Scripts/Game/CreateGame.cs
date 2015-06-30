using UnityEngine;
using System.Collections;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class CreateGame : FacadeMonoBehaviour {

	// create a new game
	public void createGame() {
		string challenger = "5587d455225afbfc3bd4c40f"; // berni
		string challenged = "559282cc6a4b4db443d18f23"; // gemmins
		Debug.Log (PlayerPrefs.GetString("token"));
		HTTPRequest request = new HTTPRequest (new System.Uri (Properties.API + "/game"), HTTPMethods.Post, (HTTPRequest req, HTTPResponse res) => {
			Debug.Log (res.DataAsText);
		});
		request.AddField ("challenger", challenger);
		request.AddField ("challenged", challenged);
		request.AddField ("token", PlayerPrefs.GetString("token"));
		request.Send ();
	}
}
