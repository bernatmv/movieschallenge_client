using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class PlayQuestionLogicScript : FacadeMonoBehaviour {

	QuestionModel question = new QuestionModel();
	QuoteScript quote;
	AnswerScript[] answers;
	CanvasGroup canvas;

	void Awake() {
		// get components
		quote = transform.GetComponentInChildren<QuoteScript> ();
		answers = transform.GetComponentsInChildren<AnswerScript> ();
		canvas = transform.GetComponent<CanvasGroup> ();
		// bind events
		_dispatcher.AddListener ("question_loaded", buildScene);
		_dispatcher.AddListener ("update_game", endPlay);
	}

	public void hide() {
		canvas.alpha = 0;
		canvas.blocksRaycasts = false;
	}

	public void show() {
		canvas.alpha = 1;
		canvas.blocksRaycasts = true;
	}

	void buildScene(Object data) {
		question = (QuestionModel)data;
		// set background color
		setBackgroundColor ();
		// set quote
		setQuote ();
		// set answers
		setAnswers ();
		// show question
		show ();
		// open quote
		openQuote ();
	}

	void setBackgroundColor() {
		transform.GetComponent<Image> ().color = Properties.categoriesColor [question.category - 1];
	}

	void setQuote() {
		quote.setQuote(question.quote);
	}

	void setAnswers() {
		int correct = Random.Range (0, 5);
		ShuffleArray<string> (question.otherAnswers);
		for (int i = 0; i < answers.Length; i++) {
			if (i == correct) {
				answers[i].setAnswer(question.correctAnswer);
				answers[i].markAsCorrect();
			}
			else {
				answers[i].setAnswer(question.otherAnswers[i]);
			}
		}
	}

	void openQuote() {
		Utils.delayAction (this, () => {
			quote.openQuote ();
			// TODO: start countdown
		}, 1.2f);
	}

	void endPlay(Object data) {
		hide ();
	}
}
