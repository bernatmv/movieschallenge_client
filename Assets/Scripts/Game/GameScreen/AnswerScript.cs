using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class AnswerScript : FacadeMonoBehaviour {

	public int answer;
	public bool correctAnswer = false;

	Button answerButton;
	Text answerText;
	Image answerImage;

	void Awake() {
		//get components
		answerButton = transform.GetComponent<Button> ();
		answerText = transform.GetComponentInChildren<Text> ();
		answerImage = transform.GetComponent<Image> ();
		// bind events
		_dispatcher.AddListener ("disable_answers", disableAnswer);
		_dispatcher.AddListener ("enable_answers", enableAnswer);
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
		// disable buttons
		_dispatcher.Dispatch ("disable_answers");
		// animate answer while calling the server
		animateAnswer ();
		// send answer to the server
		sendAnswer ();
		// recover updated game and rebuild game scene

		// hide question scene and reset it

	}

	void sendAnswer() {
		HTTPRequest request = new HTTPRequest(new System.Uri(Properties.API + "/game/" + PlayerPrefs.GetString("gameId") + "/answer/" + PlayerPrefs.GetString("questionId")), HTTPMethods.Post, processAnswer);
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.AddField ("answer", answerText.text);
		request.AddField ("correct", correctAnswer.ToString());
		request.AddField ("position", answer.ToString());
		request.Send ();
	}

	void processAnswer(HTTPRequest req, HTTPResponse res) {
		// animate answer
		Debug.Log (res.DataAsText);
	}

	void animateAnswer() {
		// if it's not the correct answer, set color of button to red
		if (!correctAnswer) {
			answerImage.color = Properties.colorWrong;
		}
	}

	void disableAnswer(Object param) {
		// disable button
		answerButton.interactable = false;
		// if it's the correct answer, set color green
		if (correctAnswer) {
			answerImage.color = Properties.colorRight;
		}
	}

	void enableAnswer(Object param) {
		// enable button
		answerButton.interactable = true;
		// reet button to it's former color
		answerImage.color = Color.white;
	}
}
