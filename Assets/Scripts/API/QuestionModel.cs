using UnityEngine;
using System.Collections;

public class QuestionModel : Object {
	
	public string _id;
	public int category;
	public int approved;
	public int difficulty;
	public string quote;
	public string correctAnswer;
	public string[] otherAnswers;
}
