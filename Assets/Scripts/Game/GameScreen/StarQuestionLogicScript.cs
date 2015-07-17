using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

public class StarQuestionLogicScript : FacadeMonoBehaviour {
	
	CanvasGroup panel;
	Text title;
	Image hexagon;
	Image icon;
	float startTime;
	QuestionModel question;
	
	void Awake () {
		// get elements
		panel = transform.GetComponent<CanvasGroup> ();
		Image[] images = transform.GetComponentsInChildren<Image> ();
		hexagon = images [1];
		icon = images [2];
		title = transform.GetComponentsInChildren<Text>() [0];
		// bind events
		_dispatcher.AddListener ("start_star_question", updateStarQuestion);
	}	
	
	void updateStarQuestion(Object data) {
		int category = ((PayloadObject)data).intPayload;
		startTime = Time.time;
		// begin to call the API to retrieve the star question 
		API request = new API("/question/category/" + (category + 1), questionReady);
		request.AddField ("token", PlayerPrefs.GetString ("token"));
		request.Send ();
		// build & show panel
		initPanel (category);
	}

	void questionReady(HTTPRequest req, HTTPResponse res) {
		float delay = ((Time.time - startTime) < (Properties.starQuestionDelay - Properties.delayQuestionStart)) ? Properties.starQuestionDelay : Properties.delayQuestionStart;
		// parse response
		question = JsonMapper.ToObject<QuestionModel> (res.DataAsText);
		// save questionId
		PlayerPrefs.SetString ("questionId", question._id);
		// deay action and launch question
		Utils.delayAction (this, () => {
			// dispatch event to start question
			_dispatcher.Dispatch ("question_loaded", question);
			// fadeout panel
			Utils.fadeOutPanel (this, panel, 1f, executeQuestion);
		}, 
		delay);
	}

	void executeQuestion() {
		// hide star question panel
		closePanel();
	}

	void initPanel(int category) {
		// set the title, hexagon's color and icon sprite
		title.text = "Playing to complete the \n" + Properties.categoriesNames[category] + " category";
		icon.sprite = Properties.categoriesIcon[category];
		hexagon.color = Properties.categoriesColor[category];
		// show panel
		Utils.delayAction (this, () => {
			// fadein panel
			Utils.fadeInPanel (this, panel, 1f, () => {});
		}, 
		1f);
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
