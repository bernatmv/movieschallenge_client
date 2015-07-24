using UnityEngine;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class CloseOptionsPanelScript : FacadeMonoBehaviour {

	public void closePanel() {
		_dispatcher.Dispatch ("close_options_panel");
	}
}
