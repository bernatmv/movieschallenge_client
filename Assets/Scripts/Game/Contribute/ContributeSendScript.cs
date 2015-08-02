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
		request.AddField ("otherAnswers", JsonMapper.ToJson(new string[] {
			_wrongAnswer1.text,
			_wrongAnswer2.text,
			_wrongAnswer3.text,
			_wrongAnswer4.text,
			_wrongAnswer5.text
		}));
		request.Send ();
	}

	void processResponse(HTTPRequest req, HTTPResponse res) {
		Debug.Log (res.DataAsText);
		JsonReader json = new JsonReader (res.DataAsText);
		json.SkipNonMembers = true;
		NewQuestionModel response = JsonMapper.ToObject<NewQuestionModel> (json);
		Debug.Log (response.success);
		if (response.success != null) {
			MessageModel message = new MessageModel();
			if (response.success) {
				message.title = _i18n.get("MESSAGE_TITLE_OK");
				message.text = _i18n.get("MESSAGE_NEW_CHALLENGE_OK");
				message.color = Properties.colorRight;
			}
			else {
				message.title = _i18n.get("MESSAGE_TITLE_ERROR");
				message.text = _i18n.get("MESSAGE_NEW_CHALLENGE_ERROR");
				message.color = Properties.colorWrong;
			}
			_dispatcher.Dispatch("message_update", message);
		}
	}
}
