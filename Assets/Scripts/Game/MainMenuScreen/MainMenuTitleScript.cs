using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuTitleScript : MonoBehaviour {
	Text title;

	void Awake () {
		title = transform.GetComponent<Text> ();
		title.text = PlayerPrefs.GetString ("username");
	}
}
