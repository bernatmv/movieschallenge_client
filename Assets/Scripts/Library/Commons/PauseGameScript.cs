using UnityEngine;
using System.Collections;

public class PauseGameScript : MonoBehaviour {

	/*
	void OnGUI() {
		if (GUI.Button(new Rect(310,180,200,30), "Restart Level")) {
			Application.LoadLevel (Application.loadedLevelName);
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
