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
		PlayerLeftCategoryProgressScript[] leftCategoriesProgress;
		PlayerRightCategoryProgressScript[] rightCategoriesProgress;
		RectTransform gameCanvas;
		for (int i = 0, l = games.Length; i < l; i++) {
			gameCanvas = Instantiate (activeGamePrefab, new Vector2 (-5f, -100 -(175f * i)), Quaternion.Euler(Vector2.zero)) as RectTransform;
			gameCanvas.transform.SetParent(gameParent.transform, false);
			// set the current turn
			setTurn(games[i], gameCanvas);
			// set players names
			setPlayersNames(games[i], gameCanvas);
			// update categories progress
			setPlayersCategories(games[i], gameCanvas);
		}
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
}