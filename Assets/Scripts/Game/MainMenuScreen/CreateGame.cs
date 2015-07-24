using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;
using GameAnalyticsSDK;

public class CreateGame : FacadeMonoBehaviour {

	[SerializeField] private InputField _usernameInput;
	Button newGameButton;

	void Awake() {
		// get components
		newGameButton = transform.GetComponent<Button> ();
		// bind events
		_dispatcher.AddListener ("disable_new_game_button", disableButton);
		_dispatcher.AddListener ("enable_new_game_button", enableButton);
	}

	public void createGameFromInput() {
		createGame (_usernameInput.text);
		// send analytics
		GameAnalytics.NewDesignEvent ("ui:game:input");
	}

	public void createGameFromLastPlayer () {
		Text username = transform.GetComponentInChildren<Text> ();
		createGame (username.text);
		// send analytics
		GameAnalytics.NewDesignEvent ("ui:game:lastplayer");
	}

	public void createGameRandom () {
		// send analytics
		GameAnalytics.NewDesignEvent ("ui:game:random");
		// disable buttons
		_dispatcher.Dispatch ("disable_new_game_button");
		// call the API
		API request = new API ();
		request.Post ("/game/random", processResponse);
		request.AddField ("token", PlayerPrefs.GetString("token"));
		request.Send ();
	}

	// create a new game
	public void createGame(string opponentUsername) {
		if (!string.IsNullOrEmpty (opponentUsername)) {
			// disable buttons
			_dispatcher.Dispatch ("disable_new_game_button");
			// create the game
			string challenger = PlayerPrefs.GetString("username");
			string challenged = opponentUsername;
			// call the API
			API request = new API ();
			request.Post ("/game", processResponse);
			request.AddField ("challenger", challenger);
			request.AddField ("challenged", challenged);
			request.AddField ("token", PlayerPrefs.GetString("token"));
			request.Send ();
		}
	}

	void processResponse(HTTPRequest req, HTTPResponse res) {
		// send analytics
		GameAnalytics.NewProgressionEvent(GA_Progression.GAProgressionStatus.GAProgressionStatusStart, "match");
		// enable buttons
		_dispatcher.Dispatch ("enable_new_game_button");
		// get id from response
		NewRegisterModel newGame = JsonMapper.ToObject<NewRegisterModel> (res.DataAsText);
		if (newGame.success) {
			// save game id to player pref to be retrieved later in the next scene
			PlayerPrefs.SetString ("gameId", newGame.id);
			// load the next scene
			_utils.loadScene ("Game");
		} 
		else {
			// if the challenged user didn't exist in the DB, inform the user
			if (newGame.errorCode == 404) {
				_dispatcher.Dispatch("error_new_game_no_exists_challenged");
				GameAnalytics.NewErrorEvent (GA_Error.GAErrorSeverity.GAErrorSeverityWarning, "Create game with non-existing user: " + PlayerPrefs.GetString("username"));
			}
		}
	}

	void disableButton(Object data) {
		newGameButton.enabled = false;
	}

	void enableButton(Object data) {
		newGameButton.enabled = true;
	}
}