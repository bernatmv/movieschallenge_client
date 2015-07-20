using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class AnswerScript : FacadeMonoBehaviour {

	public int answer;
	public bool correctAnswer = false;

	GameModel game;
	Button answerButton;
	Text answerText;
	Image answerImage;
	bool responseRdy = false;
	bool waitRdy = false;
	bool finalRoundActive = false;

	void Awake() {
		//get components
		answerButton = transform.GetComponent<Button> ();
		answerText = transform.GetComponentInChildren<Text> ();
		answerImage = transform.GetComponent<Image> ();
		// bind events
		_dispatcher.AddListener ("disable_answers", disableAnswer);
		_dispatcher.AddListener ("enable_answers", enableAnswer);
		_dispatcher.AddListener ("reset_answers", resetAnswer);
		_dispatcher.AddListener ("question_timeout", questionTimeout);
		_dispatcher.AddListener ("start_final_round", startFinalRound);
	}

	public void setAnswer(string answer) {
		answerText.text = answer;
	}

	public void markAsCorrect() {
		correctAnswer = true;
	}

	public void markAsOther() {
		correctAnswer = false;
	}
	
	public void reset() {
		markAsOther ();
		setAnswer ("");
	}

	public void chooseThis() {
		responseRdy = false;
		waitRdy = false;
		// disable buttons
		_dispatcher.Dispatch ("disable_answers");
		_dispatcher.Dispatch ("stop_countdown");
		// animate answer while calling the server
		animateAnswer ();
		// if we are in the final round, notify action
		if (finalRoundActive) {
			_dispatcher.Dispatch("next_final_round_question", new PayloadObject(correctAnswer));
		} 
		else {
			// send answer to the server
			sendAnswer ();
			StartCoroutine(delayAction (answerWait, .4f));
		}
	}

	void sendAnswer() {
		API request = new API();
		request.Post ("/game/" + PlayerPrefs.GetString ("gameId") + "/answer/" + PlayerPrefs.GetString ("questionId"), responseReady);
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.AddField ("answer", answerText.text);
		request.AddField ("correct", correctAnswer.ToString());
		request.AddField ("position", answer.ToString());
		request.Send ();
	}

	void responseReady(HTTPRequest req, HTTPResponse res) {
		responseRdy = true;
		// deserialize json
		game = JsonMapper.ToObject<GameModel> (res.DataAsText);
		// call process response to sync with wait
		processResponse ();
	}

	void answerWait() {
		waitRdy = true;
		// call process response to sync with api call
		processResponse ();
	}

	void processResponse() {
		if (responseRdy && waitRdy) {
			// if response is OK, reset question and update game with the new data
			if (!string.IsNullOrEmpty(((GameModel)game)._id)) {
				_dispatcher.Dispatch("update_game", game);
			}
			// if response is not OK, reset question and set game blocked with update pending
			//TODO: better error handling
		}
	}

	void animateAnswer() {
		// if it's not the correct answer, set color of button to red and show the appropiate message
		if (!correctAnswer) {
			answerImage.color = Properties.colorWrong;
			_dispatcher.Dispatch("message_wrong_show");
		} 
		else {
			_dispatcher.Dispatch("message_correct_show");
		}
	}

	void disableAnswer(Object param) {
		// disable button
		answerButton.interactable = false;
		// if it's the correct answer, set color green
		if (correctAnswer) {
			// disabled to make it more challenging
			//answerImage.color = Properties.colorRight; 
		}
	}

	void enableAnswer(Object param) {
		// enable button
		answerButton.interactable = true;
		// reet button to it's former color
		answerImage.color = Color.white;
	}

	void resetAnswer(Object param) {
		// reset values
		reset();
		// enable answer
		enableAnswer (param);
	}

	void questionTimeout(Object data) {
		int randomAnswer = ((PayloadObject)data).intPayload;
		if (answer == randomAnswer) {
			if (correctAnswer) {
				randomAnswer++;
				randomAnswer = (randomAnswer == 6) ? 1 : randomAnswer;
				PayloadObject payload = new PayloadObject (randomAnswer);
				_dispatcher.Dispatch("question_timeout", payload);
			}
			else {
				chooseThis();
			}
		}
	}

	void startFinalRound(Object data) {
		finalRoundActive = true;
	}
}
