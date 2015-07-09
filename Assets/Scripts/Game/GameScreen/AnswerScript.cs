using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog.movieschallenge;

public class AnswerScript : FacadeMonoBehaviour {

	public int answer;
	public bool correctAnswer = false;
	public string answerText;

	public void setAnswer(string answer) {
		answerText = answer;
		transform.GetComponentInChildren<Text> ().text = answerText;
	}

	public void markAsCorrect() {
		correctAnswer = true;
	}
}
