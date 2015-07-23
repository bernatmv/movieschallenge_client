using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class NewGamePanelScript : FacadeMonoBehaviour {

	CanvasGroup canvas;

	void Awake() {
		// get components
		canvas = transform.GetComponent<CanvasGroup> ();
		// bind events
		_dispatcher.AddListener ("close_new_game_panel", hide);
		_dispatcher.AddListener ("open_new_game_panel", show);
	}

	public void show(Object data) {
		canvas.alpha = 1f;
		canvas.blocksRaycasts = true;
		canvas.interactable = true;
	}

	public void hide(Object data) {
		canvas.alpha = 0f;
		canvas.blocksRaycasts = false;
		canvas.interactable = false;
	}
}