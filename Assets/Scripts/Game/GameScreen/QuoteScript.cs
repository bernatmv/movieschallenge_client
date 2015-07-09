using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog.movieschallenge;

public class QuoteScript : FacadeMonoBehaviour {

	public void setQuote(string quote) {
		transform.GetComponentInChildren<Text> ().text = quote;
	}

	public void openQuote() {
		transform.GetComponentInChildren<Animator> ().SetTrigger("open");
	}
}
