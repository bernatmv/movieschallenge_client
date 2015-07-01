using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleTextScript : MonoBehaviour {

	public void setTitle(string title) {
		transform.GetComponent<Text> ().text = title;
	}
}
