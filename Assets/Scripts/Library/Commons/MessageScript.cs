using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class MessageScript : FacadeMonoBehaviour {
	
	[SerializeField] public Text title;
	[SerializeField] public Text message;

	CanvasGroup canvas;

	void Awake() {
		// get components
		canvas = transform.GetComponent<CanvasGroup> ();
		// bind events
		_dispatcher.AddListener ("message_show", show);
		_dispatcher.AddListener ("message_hide", hide);
		_dispatcher.AddListener ("message_update", text);
	}
	
	public void show(Object data) {
		Utils.showCanvas (canvas);
	}
	
	public void hide(Object data) {
		Utils.hideCanvas (canvas);
	}
	
	public void text(Object data) {
		title.text = ((MessageModel)data).title;
		title.color = ((MessageModel)data).color;
		message.text = ((MessageModel)data).text;
		show (data);
		Utils.delayAction (this, () => {
			Utils.fadeOutPanel(this, canvas, .5f, () => {});
		}, 2.5f);
	}
}
