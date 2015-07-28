using UnityEngine;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;
using GameAnalyticsSDK;

public class LogoutButtonScript : FacadeMonoBehaviour {

	public void logout() {
		GameAnalytics.NewDesignEvent ("ui:user:logout");
		PlayerPrefs.DeleteKey ("token");
		PlayerPrefs.DeleteKey ("username");
		_utils.loadScene ("Login");
	}
}
