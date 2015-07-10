using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog.movieschallenge;

public class MessagePanelScript : FacadeMonoBehaviour {

	public string messageId;

	CanvasGroup canvas;
	Text message;

	void Awake() {
		// get components
		canvas = transform.GetComponent<CanvasGroup> ();
		message = transform.GetComponentInChildren<Text> ();
		// bind events
		_dispatcher.AddListener ("message_" + messageId + "_show", show);
		_dispatcher.AddListener ("message_" + messageId + "_hide", hide);
		_dispatcher.AddListener ("message_" + messageId + "_text", text);
	}

	public void show(Object data) {
		canvas.alpha = 1f;
	}

	public void hide(Object data) {
		canvas.alpha = 0f;
	}

	public void text(Object data) {
		message.text = ((MessageModel)data).text;
	}
}
