using UnityEngine;
using System.Collections;
using com.lovelydog;

public class ContributeButtonScript : MonoBehaviour {

	public void openWebSite() {
		Utils.openWebSite (Properties.webUri);
	}
}
