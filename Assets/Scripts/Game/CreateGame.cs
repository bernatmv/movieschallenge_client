using UnityEngine;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class CreateGame : FacadeMonoBehaviour {

	// create a new game
	public void createGame() {
		string challenger = PlayerPrefs.GetString("username");
		string challenged = (challenger == "berni") ? "gemmins" : "berni";
		API request = new API ();
		request.Post ("/game", (HTTPRequest req, HTTPResponse res) => {
			// get id from response
			NewRegisterModel newGame = JsonMapper.ToObject<NewRegisterModel> (res.DataAsText);
			// save game id to player pref to be retrieved later in the next scene
			PlayerPrefs.SetString ("gameId", newGame.id);
			// load the next scene
			_utils.loadScene("Game");
		});
		request.AddField ("challenger", challenger);
		request.AddField ("challenged", challenged);
		request.AddField ("token", PlayerPrefs.GetString("token"));
		request.showGoBack = false;
		request.Send ();
	}
}