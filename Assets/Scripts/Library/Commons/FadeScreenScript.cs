using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;


public class FadeScreenScript : FacadeMonoBehaviour {

	public float defaultTime = .1f;
	CanvasGroup panel;

	void Awake () {
		// get elements
		panel = transform.GetComponent<CanvasGroup> ();
		// bind events
		_dispatcher.AddListener ("fade_in_screen", fadeIn);
		_dispatcher.AddListener ("fade_out_screen", fadeOut);
	}

	void hide(CanvasGroup canvas) {
		canvas.alpha = 0;
		canvas.blocksRaycasts = false;
	}
	
	void show(CanvasGroup canvas) {
		canvas.alpha = 1;
		canvas.blocksRaycasts = true;
	}

	void fadeIn(Object data) {
		Utils.fadeInPanel (this, panel, defaultTime, () => {});
	}

	void fadeOut(Object data) {
		Utils.fadeOutPanel (this, panel, defaultTime, () => {});
	}
}
