using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class GameWonScript : FacadeMonoBehaviour {

	CanvasGroup canvas;

	void Awake() {
		// get components
		canvas = transform.GetComponent<CanvasGroup> ();
		// bind events
		_dispatcher.AddListener ("game_won", gameWon);
	}

	void gameWon(Object data) {
		canvas.alpha = 1f;
		canvas.blocksRaycasts = true;
	}
}
