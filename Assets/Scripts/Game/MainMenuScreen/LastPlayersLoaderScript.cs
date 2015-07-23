using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class LastPlayersLoaderScript : FacadeMonoBehaviour {

	public RectTransform lastPlayerModel;
	RectTransform[] lastPlayers = new RectTransform[3];
	Text[] names = new Text[3];
	CanvasGroup[] canvases = new CanvasGroup[3];

	void Awake() {
		// bind events
		this._dispatcher.AddListener ("open_new_game_panel", fillLastPlayers);
		// instantiate objects
		for (int i = 0; i < 3; i++) {
			lastPlayers [i] = Instantiate (lastPlayerModel, new Vector2 (0f, 40f - (80f * i)), Quaternion.Euler (Vector2.zero)) as RectTransform;
			lastPlayers [i].transform.SetParent (transform, false);
			names [i] = lastPlayers [i].transform.GetComponentInChildren<Text> ();
			canvases[i] = lastPlayers [i].transform.GetComponent<CanvasGroup> ();
		}
	}
	
	void fillLastPlayers(Object param) {
		resetPlayerNames ();
		getPlayersList ();
		activatePlayersList ();
	}

	void resetPlayerNames() {
		for (int i = 0; i < 3; i++) {
			names [i].text = "";
		}
	}

	void getPlayersList() {
		int count = 0;
		int i = 0;
		// get all challenged users in loaded games (we want to offer those users that the player challenged before)
		PlayerRightNameScript[] namesScripts = transform.root.GetComponentsInChildren<PlayerRightNameScript> ();
		// cycle through all or until we have 3 challenged
		while (count < 3 && i < namesScripts.Length) {
			if (validName(namesScripts[i].getNameRaw())) {
				names[count].text = namesScripts[i].getNameRaw();
				count++;
			}
			i++;
		}
	}

	bool validName(string name) {
		bool isValid = false;
		string username = PlayerPrefs.GetString ("username");
		if (name != username && !string.IsNullOrEmpty(name)) {
			if (name != names[0].text
			    && name != names[1].text
			    && name != names[2].text) {
				isValid = true;
			}
		}
		return isValid;
	}
	
	void activatePlayersList() {
		for (int i = 0; i < 3; i++) {
			if (string.IsNullOrEmpty(names [i].text)) {
				Utils.hideCanvas(canvases [i]);
			}
			else {
				Utils.showCanvas(canvases [i]);
			}
		}
	}
}
