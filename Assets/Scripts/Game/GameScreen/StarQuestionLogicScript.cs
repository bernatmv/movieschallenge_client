using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class StarQuestionLogicScript : FacadeMonoBehaviour {
	
	CanvasGroup panel;
	Text title;
	Image hexagon;
	Image icon;
	
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
		initPanel (((PayloadObject)data).intPayload);
	}

	void initPanel(int category) {
		Debug.Log (category);
		// set the title, hexagon's color and icon sprite
		title.text = "Playing to complete the \n" + Properties.categoriesNames[category] + " category";
		icon.sprite = Properties.categoriesIcon[category];
		hexagon.color = Properties.categoriesColor[category];
		// show panel
		show (panel);
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
