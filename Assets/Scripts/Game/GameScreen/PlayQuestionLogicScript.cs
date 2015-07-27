using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class PlayQuestionLogicScript : FacadeMonoBehaviour {

	QuestionModel question = new QuestionModel();
	QuoteScript quote;
	CountdownScript countdown;
	AnswerScript[] answers;
	CanvasGroup canvas;

	void Awake() {
		// get components
		quote = transform.GetComponentInChildren<QuoteScript> ();
		answers = transform.GetComponentsInChildren<AnswerScript> ();
		canvas = transform.GetComponent<CanvasGroup> ();
		countdown = transform.GetComponentInChildren<CountdownScript> ();
		// bind events
		_dispatcher.AddListener ("question_loaded", buildScene);
		_dispatcher.AddListener ("update_game", endPlay);
	}

	public void hide() {
		quote.closeQuote ();
		Utils.fadeOutPanel(this, canvas, 1f, () => {});
		//canvas.alpha = 0;
		canvas.blocksRaycasts = false;
	}

	public void show() {
		// if it's NOT a star or end game question, then make a fadeIn
		if (question.difficulty < 3) {
			Utils.fadeInPanel(this, canvas, .8f, () => {
				Utils.showCanvas(canvas);
			});
		} 
		// if it's a star or end game question, show immediately
		else {
			Utils.showCanvas(canvas);
		}
	}

	void buildScene(Object data) {
		question = (QuestionModel)data;
		// reset question-answers
		_dispatcher.Dispatch ("reset_answers");
		_dispatcher.Dispatch("message_wrong_hide");
		_dispatcher.Dispatch("message_correct_hide");
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
		// start countdown
		startCountdown ();
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

	void startCountdown() {
		countdown.startCountdown ();
	}
	
	void endPlay(Object data) {
		hide ();
	}
}
