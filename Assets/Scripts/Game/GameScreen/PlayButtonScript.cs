﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class PlayButtonScript : FacadeMonoBehaviour {

	public Color[] categoriesColor;

	Image playImage;
	Image[] questionIcon;
	Text questionText;

	Button playButton;
	QuestionModel question = new QuestionModel();

	bool callingAPI = false;
	bool spinFlag = false;
	float minimumTime = 3.6f;
	float elapsedTime = 0f;
	float delay = 0.2f;
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
	}

	public void play() {
		beginPlay ();
		// call the API
		HTTPRequest request = new HTTPRequest(new System.Uri(Properties.API + "/game/" + PlayerPrefs.GetString("gameId") + "/play"), endAPICall);
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.Send ();
	}

	void beginPlay() {
		// activate icon, deactivate text and button
		questionIcon[1].enabled = true;
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

	void FixedUpdate() {
		if (callingAPI || (elapsedTime > 0f && elapsedTime < minimumTime)) {
			spinFlag = true;
			// to execute while calling the API
			elapsedTime += Time.deltaTime;
			elapsedDelay += Time.deltaTime;
			if (elapsedDelay >= delay) {
				elapsedDelay = 0f;
				playImage.color = categoriesColor [categories[iteration]];
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
					}, 0.6f)
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
			playImage.color = categoriesColor [question.category - 1];
			questionText.text = Properties.categoriesNames[question.category - 1];
		}
	}

	void enableButton(UnityEngine.Object game) {
		if (PlayerPrefs.GetString ("username") == ((GameModel)game).thisTurn) {
			// enable button, disable question icon
			playButton.enabled = true;
			questionIcon [1].enabled = false;
			questionText.enabled = true;
		} 
		else {
			// disable button, enable not your turn text
			playButton.enabled = false;
			questionIcon [1].enabled = false;
			questionText.text = "Not your turn";
		}
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
		minimumTime = categories.Count * 0.6f;
		if (minimumTime < 2.0f) {
			minimumTime = 2.0f;
		}
	}
}
