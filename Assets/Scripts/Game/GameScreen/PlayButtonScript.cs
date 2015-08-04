using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using com.lovelydog;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class PlayButtonScript : FacadeMonoBehaviour {

	Image playImage;
	Image[] questionIcon;
	Text questionText;

	Button playButton;
	QuestionModel question = new QuestionModel();

	bool callingAPI = false;
	bool spinFlag = false;
	float minimumTime = Properties.spinMinimumTime;
	float elapsedTime = 0f;
	float delay = Properties.spinIteration;
	float elapsedDelay = 0f;
	int iteration = 0;
	List<int> categories = new List<int> ();

	void Awake() {
		// get components
		questionIcon = transform.GetComponentsInChildren<Image> ();
		questionText = transform.GetComponentInChildren<Text> ();
		playImage = transform.GetComponent<Image> ();
		playButton = transform.GetComponent<Button> ();
		// bind events
		_dispatcher.AddListener ("update_categories", updateCategories);
		_dispatcher.AddListener ("enable_play_button", enableButton);
		_dispatcher.AddListener ("disable_play_button", disableButton);
	}

	public void play() {
		beginPlay ();
		spinFlag = true;
		// call the API
		API request = new API("/game/" + PlayerPrefs.GetString("gameId") + "/play", endAPICall);
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.Send ();
	}

	void beginPlay() {
		// activate icon, deactivate text and button
		enableQuestionIconInterrogation ();
		disableQuestionIconLock ();
		questionText.enabled = false;
		playButton.enabled = false;
		callingAPI = true;
	}

	void endAPICall(HTTPRequest req, HTTPResponse res) {
		// deserialize json
		question = JsonMapper.ToObject<QuestionModel> (res.DataAsText);
		// save questionId
		PlayerPrefs.SetString ("questionId", question._id);
		// end spinning
		callingAPI = false;
	}

	void Update() {
		if (callingAPI || (elapsedTime > 0f && elapsedTime < minimumTime)) {
			spinFlag = true;
			// to execute while calling the API
			elapsedTime += Time.deltaTime;
			elapsedDelay += Time.deltaTime;
			if (elapsedDelay >= delay) {
				elapsedDelay = 0f;
				playImage.color = Properties.categoriesColor [categories[iteration]];
				iteration = (iteration + 1) % categories.Count;
			}
		} 
		else {
			if (spinFlag) {
				spinFlag = false;
				// reset properties
				resetProperties();
				// signal question loaded after 1 second
				StartCoroutine(
					delayAction(() => {
						_dispatcher.Dispatch ("question_loaded", question);
					}, 
					Properties.delayQuestionStart)
				);
			}
		}
	}

	void resetProperties() {
		// reset
		iteration = 0;
		elapsedDelay = 0f;
		elapsedTime = 0f;
		// if a question is stored, set it's color and title
		if (!string.IsNullOrEmpty(question._id)) {
			playImage.color = Properties.categoriesColor [question.category - 1];
			questionText.text = _i18n.get (Properties.categoriesNames[question.category - 1]);
		}
	}

	void enableButton(UnityEngine.Object game) {
		playImage.color = Properties.bgColor;
		questionText.enabled = true;
		disableQuestionIconInterrogation ();
		if (PlayerPrefs.GetString ("username") == ((GameModel)game).thisTurn) {
			// enable button, disable question icon
			playButton.enabled = true;
			questionText.text = _i18n.get ("TAP_ME_BUTTON");
			disableQuestionIconLock();
		} 
		else {
			// disable button, enable not your turn text
			playButton.enabled = false;
			questionText.text = _i18n.get ("NOT_YOUR_TURN_TEXT");
			enableQuestionIconLock();
		}
	}

	void disableButton (UnityEngine.Object game = default(UnityEngine.Object)) {
		playButton.enabled = false;
		disableQuestionIconInterrogation ();
		disableQuestionIconLock ();
		questionText.text = "";
	}

	void updateCategories(UnityEngine.Object game) {
		int[] categoriesProgress;
		// clear categories
		categories.Clear ();
		// check if it's challenger or challenged and fill the categories appropiatedly
		if (PlayerPrefs.GetString ("username") == ((GameModel) game).players.challenger.username) {
			categoriesProgress = ((GameModel) game).players.challenger.categoriesProgress;
		} 
		else {
			categoriesProgress = ((GameModel) game).players.challenged.categoriesProgress;
		}
		// add categories not yet completed
		for (int i = 0; i < categoriesProgress.Length; i++) {
			if (categoriesProgress[i] < 3) {
				categories.Add(i);
			}
		}
		// update minimumTime
		minimumTime = categories.Count * Properties.spinIteration * 2;
		if (minimumTime < Properties.spinMinimumTime) {
			minimumTime = Properties.spinMinimumTime;
		}
	}

	void enableQuestionIconInterrogation() {
		questionIcon [2].enabled = true;
	}

	void enableQuestionIconLock() {
		questionIcon [1].enabled = true;
	}

	void disableQuestionIconInterrogation() {
		questionIcon [2].enabled = false;
	}
	
	void disableQuestionIconLock() {
		questionIcon [1].enabled = false;
	}
}
