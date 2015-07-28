using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class GamesLoader : FacadeMonoBehaviour {

	public RectTransform activeGamePrefab;
	public RectTransform oldGamePrefab;
	public RectTransform gameParent;
	Authenticate auth = new Authenticate ();

	void Awake() {
		Debug.Log ("GamesLoader awake");
		// bind events
		this._dispatcher.AddListener ("auth_finished", buildScene);
	}

	void Start () {
		Debug.Log ("GamesLoader start");
		// authenticate
		auth.authenticate ();
	}

	void OnApplicationPause(bool paused) {
		if (paused) {
			Debug.Log ("The app has JUST PAUSED");
			PlayerPrefs.Save();
		}
		else {
			Debug.Log ("The app has JUST RESUMED");
			auth.authenticate ();
		}
	}

	// build the scene
	void buildScene(Object param) {
		getGames ();
	}

	// get games from API
	void getGames() {
		// get all the games
		API request = new API("/games", (HTTPRequest req, HTTPResponse res) => {
			// deserialize json
			GameModel[] games = JsonMapper.ToObject<GameModel[]> (res.DataAsText);
			// build list
			buildGamesList(games);
		});
		request.showGoBack = false;
		request.interstitialLoading = true;
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.Send ();
	}

	void buildGamesList(GameModel[] games) {
		// resize scroll content
		_dispatcher.Dispatch ("resize_scroll_content", new PayloadObject(games.Length));
		// build list
		RectTransform gameCanvas;
		for (int i = 0, l = games.Length; i < l; i++) {
			if (games[i].ended) {
				gameCanvas = Instantiate (oldGamePrefab, new Vector2 (-5f, -290 -(190f * i)), Quaternion.Euler(Vector2.zero)) as RectTransform;
			}
			else {
				gameCanvas = Instantiate (activeGamePrefab, new Vector2 (-5f, -290 -(190f * i)), Quaternion.Euler(Vector2.zero)) as RectTransform;
			}
			gameCanvas.transform.SetParent(gameParent.transform, false);
			// set payload
			setPayload(games[i], gameCanvas);
			// set the current turn
			setTurn(games[i], gameCanvas);
			// set players names
			setPlayersNames(games[i], gameCanvas);
			// update categories progress
			setPlayersCategories(games[i], gameCanvas);
			// mark player layer
			markPlayerLayer(games[i], gameCanvas);
		}
	}

	void setPayload(GameModel game, RectTransform gameCanvas) {
		GamePayloadScript gamePayload;
		// set players names
		gamePayload = gameCanvas.GetComponentInChildren<GamePayloadScript>();
		gamePayload.gameId = game._id;
	}
	
	void setTurn(GameModel game, RectTransform gameCanvas) {
		GameTurnUpdateScript turnDisplay;
		// set players names
		turnDisplay = gameCanvas.GetComponentInChildren<GameTurnUpdateScript>();
		turnDisplay.setTurn (game.turn);
	}
	
	void setPlayersNames(GameModel game, RectTransform gameCanvas) {
		PlayerLeftNameScript playerLeftName;
		PlayerRightNameScript playerRightName;
		// set players names
		playerLeftName = gameCanvas.GetComponentInChildren<PlayerLeftNameScript>();
		if (playerLeftName != null) {
			playerLeftName.setName(game.players.challenger.username);
		}
		playerRightName = gameCanvas.GetComponentInChildren<PlayerRightNameScript>();
		if (playerRightName != null) {
			playerRightName.setName(game.players.challenged.username);
		}
	}

	void setPlayersCategories(GameModel game, RectTransform gameCanvas) {
		PlayerLeftCategoryProgressScript[] leftCategoriesProgress;
		PlayerRightCategoryProgressScript[] rightCategoriesProgress;
		// update categories progress
		leftCategoriesProgress = gameCanvas.GetComponentsInChildren<PlayerLeftCategoryProgressScript>();
		foreach (PlayerLeftCategoryProgressScript categoryProgress in leftCategoriesProgress) {
			categoryProgress.updateCategory(game.players.challenger.categoriesProgress);
		}
		rightCategoriesProgress = gameCanvas.GetComponentsInChildren<PlayerRightCategoryProgressScript>();
		foreach (PlayerRightCategoryProgressScript categoryProgress in rightCategoriesProgress) {
			categoryProgress.updateCategory(game.players.challenged.categoriesProgress);
		}
	}

	void markPlayerLayer(GameModel game, RectTransform gameCanvas) {
		if (!game.ended) {
			// get components
			Image[] images = gameCanvas.GetComponentsInChildren<Image> ();
			Image challengerLayer = images[1];
			Image challengedLayer = images[2];
			// activate
			if (game.thisTurn == game.players.challenger.username) {
				challengerLayer.enabled = true;
				challengedLayer.enabled = false;
			} 
			else {
				challengedLayer.enabled = true;
				challengerLayer.enabled = false;
			}
		}
	}
}