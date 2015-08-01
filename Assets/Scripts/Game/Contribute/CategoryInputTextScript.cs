using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class CategoryInputTextScript : FacadeMonoBehaviour {
	
	public InputField inputText;

	void Awake() {
		inputText.text = "0";
		_dispatcher.AddListener ("category_check", updateInput);
	}

	public void updateInput(Object data) {
		int checkedCategory = ((PayloadObject)data).intPayload;
		inputText.text = checkedCategory + "";
	}
}
