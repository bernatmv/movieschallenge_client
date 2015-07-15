using UnityEngine;
using System.Collections;
using com.lovelydog.movieschallenge;

public class PauseGameScript : FacadeMonoBehaviour {

	/*
	void OnGUI() {
		if (GUI.Button(new Rect(310,180,200,30), "Restart Level")) {
			_utils.loadScene(Application.loadedLevelName);
		}
	}
	*/

	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			if (Time.timeScale > 0) Time.timeScale = 0;
			else Time.timeScale = 1;
		}
	}
}
