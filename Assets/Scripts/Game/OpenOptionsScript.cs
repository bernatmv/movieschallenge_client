using UnityEngine;
using System.Collections;
using com.lovelydog.movieschallenge;

public class OpenOptionsScript : FacadeMonoBehaviour {

	public void openOptions() {
		_dispatcher.Dispatch ("open_options_panel");
	}
}
