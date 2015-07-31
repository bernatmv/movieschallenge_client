using UnityEngine;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class ContributeButtonScript : FacadeMonoBehaviour {

	public void openWebSite() {
		Utils.openWebSite (Properties.webUri);
	}

	public void contribute() {
		_utils.loadScene ("Contribute");
	}
}
