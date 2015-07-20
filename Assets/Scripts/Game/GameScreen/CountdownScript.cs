using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class CountdownScript : FacadeMonoBehaviour {

	Image countdownImage;
	Text countdownText;
	float countdownTime;
	float deltaTime = 0f;
	bool active = false;
	float imageWidth = 640f;
	float imageHeight = 50f;
	float imageWidthStep;

	void Awake() {
		// get components
		countdownImage = transform.GetComponent<Image> ();
		countdownText = transform.GetComponentInChildren<Text> ();
		// init
		resetValues ();
		updateCounter ();
		updateBar ();
		imageWidthStep = imageWidth / countdownTime;
		// bind events
		_dispatcher.AddListener ("stop_countdown", stopCountdown);
		_dispatcher.AddListener ("update_game", endCountdown);
	}

	void Update() {
		if (active) {
			imageWidth -= (Time.deltaTime * imageWidthStep);
			updateBar();
			deltaTime += Time.deltaTime;
			updateTimer();
		}
	}

	void updateTimer() {
		if (deltaTime >= 1f) {
			deltaTime = 0f;
			countdownTime--;
			updateCounter();
		}
	}

	void updateCounter() {
		if (countdownTime < 0) {
			countdownTime = 0;
			active = false;
			failQuestion();
		}
		countdownText.text = countdownTime + " s";
	}

	void updateBar() {
		if (imageWidth <= 0f) {
			imageWidth = 0f;
		}
		countdownImage.rectTransform.sizeDelta = new Vector2( imageWidth, imageHeight);	
	}

	public void startCountdown() {
		active = true;
		resetValues ();
		updateCounter ();
		updateBar ();
	}

	public void stopCountdown(Object data) {
		active = false;
	}

	public void endCountdown(Object data) {
		active = false;
		resetValues ();
	}

	void resetValues () {
		imageWidth = 640f;
		countdownTime = Properties.questionCountdownTime;
	}

	void resetCountdown() {
		active = false;
		resetValues ();
	}

	void failQuestion() {
		int randomAnswer = Random.Range (1, 6);
		PayloadObject payload = new PayloadObject (randomAnswer);
		_dispatcher.Dispatch("question_timeout", payload);
	}
}
