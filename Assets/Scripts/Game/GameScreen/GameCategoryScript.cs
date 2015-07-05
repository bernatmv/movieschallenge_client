﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog.movieschallenge;

public class GameCategoryScript : FacadeMonoBehaviour {
	
	public int categoryId = 0;

	void Awake () {
		_dispatcher.AddListener ("update_categories", updateCategories);
	}
	
	void updateCategories(Object gameData) {
		string username = PlayerPrefs.GetString ("username");
		Image[] categoryImages;
		int[] categories;
		GameModel game = ((GameModel)gameData);
		// get categories
		if (game.players.challenger.username == username) {
			categories = game.players.challenger.categoriesProgress;
		}
		else {
			categories = game.players.challenged.categoriesProgress;
		}
		// update game categories progress
		categoryImages = transform.GetComponentsInChildren<Image>();
		if (categories [categoryId - 1] > 0) {
			categoryImages[2].enabled = false;
		}
		if (categories [categoryId - 1] > 1) {
			categoryImages[3].enabled = false;
		}
		if (categories [categoryId - 1] > 2) {
			categoryImages[4].enabled = false;
			categoryImages[1].enabled = true;
		}
	}
}