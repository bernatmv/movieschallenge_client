﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class Properties {
	// general
	public static Color bgColor = newRGBColor(82, 81, 98);
	// title
	public static Color titleBgMenu = newRGBColor(255, 255, 255, 20);
	public static Color titleBgChallenger = newRGBColor(244, 111, 110);
	public static Color titleBgChallenged = newRGBColor(27, 211, 177);
	// web site
	public static string webUri = "https://arcane-earth-1212.herokuapp.com";
	// reload scroll offset
	public static float reloadScrollOffset = -55f;
	// api config
	public static string API = "https://arcane-earth-1212.herokuapp.com/api";
	public static int connectTimeout = 4;
	public static int timeout = 12;
	// categories config
	public static string[] categoriesNames = new string[] {
		"CATEGORY_1",
		"CATEGORY_2",
		"CATEGORY_3",
		"CATEGORY_4",
		"CATEGORY_5",
		"CATEGORY_6"
	};
	public static Color[] categoriesColor = new Color[] {
		newRGBColor(241, 226, 147),
		newRGBColor(244, 111, 110),
		newRGBColor(31, 184, 222),
		newRGBColor(27, 211, 177),
		newRGBColor(204, 142, 235),
		newRGBColor(250, 187, 59)
	};
	public static Sprite[] categoriesIcon = new Sprite[] {
		Resources.Load<Sprite>("categories/category_1"),
		Resources.Load<Sprite>("categories/category_2"),
		Resources.Load<Sprite>("categories/category_3"),
		Resources.Load<Sprite>("categories/category_4"),
		Resources.Load<Sprite>("categories/category_5"),
		Resources.Load<Sprite>("categories/category_6")
	};
	public static Color colorRight = new Color(43/255f, 237/255f, 140/255f, 255/255f);
	public static Color colorWrong = new Color(216/255f, 100/255f, 89/255f, 255/255f);
	// times
	public static float questionCountdownTime = 20f;
	public static float answerRightWrongMessageDuration = 1.2f;
	public static float starQuestionDelay = 4.5f;
	public static float starQuestionStartDelay = 0f;
	// play animation configuration
	public static float spinMinimumTime = .6f;
	public static float spinIteration = .15f;
	public static float delayQuestionStart = .2f;
	public static float starQuestionFadeInOutDuration = 1f;
	// categories completion progress
	public static int starQuestion = 3;
	public static int completedQuestion = 4;
	public static Color categoryIconColor = Color.white;
	// quote colors
	public static string quoteColor1 = "000000ff";
	public static string quoteColor2 = "0000a0ff";

	// methods
	public static Color newRGBColor(int red, int green, int blue, int alpha = 255) {
		return new Color(red/255f, green/255f, blue/255f, alpha/255f);
	}
}