using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class LocaleButtonScript : FacadeMonoBehaviour {

	public string locale;
	Button localeButton;
	Shadow buttonShadow;
	string currentLocale;

	void Awake() {
		currentLocale = _i18n.getLocale ();
		localeButton = transform.GetComponent<Button> ();
		buttonShadow = transform.GetComponent<Shadow> ();
		updateLocaleButton ();
	}

	void updateLocaleButton() {
		if (locale == currentLocale) {
			localeButton.interactable = false;
			buttonShadow.enabled = false;
		}
	}

	public void changeLang() {
		_i18n.setLocale (locale);
		_utils.reloadScene ();
	}
}
