using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class ErrorRetryScript : FacadeMonoBehaviour {

	Action callback;

	void Awake() {
		// bind events
		_dispatcher.AddListener ("api_error_retry_and_back", setCallback);
		_dispatcher.AddListener ("api_error_retry", setCallback);
	}

	public void retry() {
		_dispatcher.Dispatch ("api_error_loading");
		callback ();
	}

	void setCallback(UnityEngine.Object newCallback) {
		callback = ((CallbackObject)newCallback).action;
	}
}
