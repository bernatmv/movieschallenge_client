using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class PlayButtonScript : FacadeMonoBehaviour {

	public Color[] categoriesColor;

	Image playImage;
	Button playButton;
	bool callingAPI = false;
	float minimumTime = 2.4f;
	float elapsedTime = 0f;
	int iteration = 0;
	float delay = .2f;
	float elapsedDelay = 0f;

	void Awake() {
		playImage = transform.GetComponent<Image> ();
		playButton = transform.GetComponent<Button> ();
	}

	public void play() {
		callingAPI = true;
		playButton.enabled = false;
		// call the API
		HTTPRequest request = new HTTPRequest(new System.Uri(Properties.API + "/game/" + PlayerPrefs.GetString("gameId") + "/play"), endAPICall);
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.Send ();
	}

	void endAPICall(HTTPRequest req, HTTPResponse res) {
		callingAPI = false;
	}

	void FixedUpdate() {
		if (callingAPI || (elapsedTime > 0f && elapsedTime < minimumTime)) {
			elapsedTime += Time.deltaTime;
			elapsedDelay += Time.deltaTime;
			if (elapsedDelay >= delay) {
				elapsedDelay = 0f;
				playImage.color = categoriesColor [iteration];
				iteration = (iteration + 1) % categoriesColor.Length;
			}
		} 
		else {
			iteration = 0;
			elapsedDelay = 0f;
			elapsedTime = 0f;
			playButton.enabled = true;
		}
	}
}
