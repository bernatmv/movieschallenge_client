using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class LanguageTagScript : FacadeMonoBehaviour {

	Text text;
	Language i18n = Language.Instance;

	void Awake() {
		// get component
		text = transform.GetComponent<Text> ();
		// translate tag
		updateText ();
	}

	void updateText() {
		text.text = i18n.get (text.text);
	}
}
