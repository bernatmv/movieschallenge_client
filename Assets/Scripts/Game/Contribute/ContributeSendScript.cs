using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;
using GameAnalyticsSDK;

public class ContributeSendScript : FacadeMonoBehaviour {

	[SerializeField] private InputField _quote;
	[SerializeField] private InputField _category;
	[SerializeField] private InputField _correctAnswer;
	[SerializeField] private InputField _wrongAnswer1;
	[SerializeField] private InputField _wrongAnswer2;
	[SerializeField] private InputField _wrongAnswer3;
	[SerializeField] private InputField _wrongAnswer4;
	[SerializeField] private InputField _wrongAnswer5;
	string _difficulty = "1";

	void Awake () {
	
	}

	public void sendQuestion() {
		API request = new API ();
		request.Post ("/question", processResponse);
		request.AddField ("token", PlayerPrefs.GetString("token"));
		request.AddField ("quote", _quote.text);
		request.AddField ("category", _category.text);
		request.AddField ("difficulty", _difficulty);
		request.AddField ("correctAnswer", _correctAnswer.text);
		/*
		request.AddField ("otherAnswers", new string[] {
			_wrongAnswer1,
			_wrongAnswer2,
			_wrongAnswer3,
			_wrongAnswer4,
			_wrongAnswer5
		});*/

		request.Send ();
	}

	void processResponse(HTTPRequest req, HTTPResponse res) {

	}
}
