using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class ErrorPanelScript : FacadeMonoBehaviour {

	CanvasGroup panel;
	CanvasGroup loading;
	CanvasGroup retryButton;
	CanvasGroup goToMenuButton;

	void Awake () {
		// get elements
		CanvasGroup[] elements = transform.GetComponentsInChildren<CanvasGroup> ();
		panel = elements [0];
		retryButton = elements [1];
		goToMenuButton = elements [2];
		loading = elements [3];
		// bind events
		_dispatcher.AddListener ("api_error_retry_and_back", errorRetryOrBack);
		_dispatcher.AddListener ("api_error_retry", errorRetry);
		_dispatcher.AddListener ("api_error_back", errorBack);
		_dispatcher.AddListener ("api_error_loading", errorLoading);
		_dispatcher.AddListener ("api_error_reset", errorReset);
	}	

	void errorRetryOrBack(Object param) {
		initPanel ();
		show (retryButton);
		show (goToMenuButton);
	}

	void errorRetry(Object param) {
		initPanel ();
		show (retryButton);
	}

	void errorBack(Object param) {
		initPanel ();
		show (goToMenuButton);
	}

	void errorLoading(Object param) {
		hide (retryButton);
		show (loading);
	}
	
	void errorReset(Object param) {
		closePanel ();
	}
	
	void initPanel() {
		show (panel);
		// reset the others just to be sure
		hide (loading);
		hide (retryButton);
		hide (goToMenuButton);
	}

	void closePanel() {
		// reset
		hide (loading);
		hide (retryButton);
		hide (goToMenuButton);
		hide (panel);
	}
	
	void hide(CanvasGroup canvas) {
		canvas.alpha = 0;
		canvas.blocksRaycasts = false;
	}
	
	void show(CanvasGroup canvas) {
		canvas.alpha = 1;
		canvas.blocksRaycasts = true;
	}	
}
