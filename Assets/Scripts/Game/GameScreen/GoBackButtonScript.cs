using UnityEngine;
using System.Collections;
using com.lovelydog.movieschallenge;

public class GoBackButtonScript : FacadeMonoBehaviour {

	public void goBack() {
		// go back to the main menu
		_utils.loadScene("MainMenu");
	}
}
