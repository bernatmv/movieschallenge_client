using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class QuoteScript : FacadeMonoBehaviour {

	public void setQuote(string quote) {
		quote = setQuoteColors (quote);
		transform.GetComponentInChildren<Text> ().text = quote;
	}

	string setQuoteColors(string quote) {
		string formatedQuote = "<color=#quoteColor>" + quote.Replace ("\n", "</color>\n<color=#quoteColor>") + "</color>";
		for (int i = 0; i < 5; i++) {
			formatedQuote = Utils.replaceFirst(formatedQuote, "quoteColor", Properties.quoteColor1);
			formatedQuote = Utils.replaceFirst(formatedQuote, "quoteColor", Properties.quoteColor2);
		}
		return formatedQuote;
	}

	public void openQuote() {
		transform.GetComponentInChildren<Animator> ().SetTrigger("open");
	}

	public void closeQuote() {
		transform.GetComponentInChildren<Animator> ().SetTrigger("close");
	}
}
