using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class Properties {
	// api config
	public static string API = "http://localhost:8080/api";
	public static int connectTimeout = 3;
	public static int timeout = 10;
	// categories config
	public static string[] categoriesNames = new string[] {
		"Family",
		"Romantic",
		"SiFi & fantasy",
		"Humor",
		"History & action",
		"Other"
	};
	public static Color[] categoriesColor = new Color[] {
		newRGBColor(149, 100, 173),
		newRGBColor(244, 111, 110),
		newRGBColor(31, 184, 222),
		newRGBColor(27, 211, 177),
		newRGBColor(250, 187, 59),
		newRGBColor(157, 103, 28)
	};
	public static Color[] categoriesColorAlt = new Color[] {
		newRGBColor(238, 119, 204, 255),
		newRGBColor(255, 238, 0, 255),
		newRGBColor(0, 187, 255, 255),
		newRGBColor(34, 221, 34, 255),
		newRGBColor(255, 102, 51, 255),
		newRGBColor(170, 119, 85, 255)
	};
	public static Sprite[] categoriesIcon = new Sprite[] {
		Resources.Load<Sprite>("categories/category1"),
		Resources.Load<Sprite>("categories/category2"),
		Resources.Load<Sprite>("categories/category3"),
		Resources.Load<Sprite>("categories/category4"),
		Resources.Load<Sprite>("categories/category5"),
		Resources.Load<Sprite>("categories/category6")
	};
	public static Color colorRight = new Color(43/255f, 237/255f, 140/255f, 255/255f);
	public static Color colorWrong = new Color(216/255f, 100/255f, 89/255f, 255/255f);
	// play animation configuration
	public static float spinMinimumTime = .6f;
	public static float spinIteration = .15f;
	public static float delayQuestionStart = .4f;
	// categories completion progress
	public static int starQuestion = 3;
	public static int completedQuestion = 4;

	// methods
	public static Color newRGBColor(int red, int green, int blue, int alpha = 255) {
		return new Color(red/255f, green/255f, blue/255f, alpha/255f);
	}
}