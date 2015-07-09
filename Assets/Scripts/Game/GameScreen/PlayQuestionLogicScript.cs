using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
			}
			else {
				answers[i].setAnswer(question.otherAnswers[i]);
			}
		}
	}

	void openQuote() {
		quote.openQuote ();
	}
}
