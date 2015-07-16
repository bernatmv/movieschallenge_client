using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class ErrorGoBackScript : FacadeMonoBehaviour {

	public void goToMainMenu() {
		_utils.loadScene("MainMenu");
	}
}
