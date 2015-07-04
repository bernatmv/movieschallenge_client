using UnityEngine;
using System.Collections;
using com.lovelydog.movieschallenge;

public class LoadGameScript : FacadeMonoBehaviour {

	public void loadGame(GameObject gamePayload) {
		string gameId = gamePayload.GetComponent<GamePayloadScript> ().gameId;
		// save game id to player pref to be retrieved later in the next scene
		PlayerPrefs.SetString ("gameId", gameId);
		// load the next scene
		Application.LoadLevel ("Game");
	}
}
