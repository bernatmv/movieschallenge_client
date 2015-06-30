using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class GamesLoader : FacadeMonoBehaviour {

	public RectTransform activeGamePrefab;
	public RectTransform oldGamePrefab;
	public RectTransform gameParent;

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
		getGames ();
	}

	// get games from API
	void getGames() {
		// get all the games
		HTTPRequest request = new HTTPRequest(new System.Uri(Properties.API + "/games"), (HTTPRequest req, HTTPResponse res) => {
			// deserialize json
			GameModel[] games = JsonMapper.ToObject<GameModel[]> (res.DataAsText);
			// build list
			buildGamesList(games);
		});
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.Send ();
	}

	void buildGamesList(GameModel[] games) {
		PlayerLeftNameScript playerLeftName;
		PlayerRightNameScript playerRightName;
		RectTransform game;
		for (int i = 0, l = games.Length; i < l; i++) {
			game = Instantiate (activeGamePrefab, new Vector2 (-5f, -100 -(175f * i)), Quaternion.Euler(Vector3.zero)) as RectTransform;
			game.transform.SetParent(gameParent.transform, false);
			playerLeftName = game.GetComponentInChildren<PlayerLeftNameScript>();
			if (playerLeftName != null) {
				playerLeftName.setName("me!");
			}
			playerRightName = game.GetComponentInChildren<PlayerRightNameScript>();
			if (playerRightName != null) {
				playerRightName.setName("yo!");
			}
		}
	}
}