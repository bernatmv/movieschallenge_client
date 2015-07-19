using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class GameLostScript : FacadeMonoBehaviour {
	
	CanvasGroup canvas;
	
	void Awake() {
		// get components
		canvas = transform.GetComponent<CanvasGroup> ();
		// bind events
		_dispatcher.AddListener ("game_lost", gameLost);
	}
	
	void gameLost(Object data) {
		canvas.alpha = 1f;
		canvas.blocksRaycasts = true;
	}
}
