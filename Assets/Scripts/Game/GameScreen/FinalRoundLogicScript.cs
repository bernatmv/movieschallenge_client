﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class FinalRoundLogicScript : FacadeMonoBehaviour {
	
	CanvasGroup panel;
	Text title;
	QuestionModel[] questions;
	int correctAnswers = 0;
	string _gameId;
	int currentQuestion = 0;

	void Awake () {
		// get elements
		_gameId = PlayerPrefs.GetString ("gameId");
		panel = transform.GetComponent<CanvasGroup> ();
		// bind events
		_dispatcher.AddListener ("start_final_round", updateFinalRound);
		_dispatcher.AddListener ("next_final_round_question", nextFinalRoundQuestion);
	}	
	
	void updateFinalRound(Object data) {
		correctAnswers = 0;
		// begin to call the API to retrieve the star question 
		API request = new API("/game/" + _gameId + "/finalRound", questionsReady);
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.Send ();
		// build & show panel
		initPanel ();
	}
	
	void questionsReady(HTTPRequest req, HTTPResponse res) {
		// parse response
		questions = JsonMapper.ToObject<QuestionModel[]> (res.DataAsText);
		// launch first question
		nextQuestion ();
	}

	void nextFinalRoundQuestion(Object data) {
		// check if the previous question was answered correctly
		if (((PayloadObject)data).boolPayload) {
			correctAnswers++;
		}
		// check if the final round finished
		if (currentQuestion == 6) {
			// check if the final round was a success or not
			if (correctAnswers >= 4) {
				sendEndGame(true);
			}
			else {
				sendEndGame(false);
			}
		} 
		else {
			nextQuestion (2.5f);
		}
	}

	void sendEndGame(bool finished) {
		// send status of game to the server
		string uri = (finished) ? "/game/" + PlayerPrefs.GetString ("gameId") + "/finalRoundSuccess" : "/game/" + PlayerPrefs.GetString ("gameId") + "/finalRoundFailed";
		API request = new API();
		request.Post (uri, processGameEnd);
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.Send ();
	}

	void processGameEnd(HTTPRequest req, HTTPResponse res) {
		// deserialize json
		GameModel game = JsonMapper.ToObject<GameModel> (res.DataAsText);
		// update game
		if (!string.IsNullOrEmpty(((GameModel)game)._id)) {
			_dispatcher.Dispatch("update_game", game);
		}
	}

	void nextQuestion(float delay = 3.5f) {
		// save questionId
		PlayerPrefs.SetString ("questionId", questions[currentQuestion]._id);
		// deay action and launch question
		initPanel (1f);
		Utils.delayAction (this, () => {
			resetEnvironment();
			// dispatch event to start question
			_dispatcher.Dispatch ("question_loaded", questions[currentQuestion]);
			// fadeout panel
			Utils.fadeOutPanel (this, panel, .6f, () => {
				currentQuestion++;
			});
		}, 
		delay);
	}

	void resetEnvironment() {
		// reset question-answers
		_dispatcher.Dispatch ("reset_answers");
		_dispatcher.Dispatch("message_wrong_hide");
		_dispatcher.Dispatch("message_correct_hide");
	}

	void initPanel(float delay = .4f) {
		// show panel
		Utils.delayAction (this, () => {
			// fadein panel
			Utils.fadeInPanel (this, panel, .6f, () => {});
		}, 
		delay);
	}
	
	void closePanel() {
		// reset
		hide (panel);
	}
	
	void hide(CanvasGroup canvas) {
		canvas.alpha = 0;
		canvas.blocksRaycasts = false;
	}
	
	void show(CanvasGroup canvas) {
		canvas.alpha = 1;
		canvas.blocksRaycasts = true;
	}	
}