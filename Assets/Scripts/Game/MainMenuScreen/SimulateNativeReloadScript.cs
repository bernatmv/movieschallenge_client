using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class SimulateNativeReloadScript : FacadeMonoBehaviour {

	RectTransform content;

	void Awake() {
		content = (RectTransform)transform;
	}

	void FixedUpdate () {
		if (content.offsetMax.y < Properties.reloadScrollOffset) {
			//_utils.reloadScene();
		}
	}
}
