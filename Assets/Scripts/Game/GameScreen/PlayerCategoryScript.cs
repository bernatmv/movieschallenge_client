using UnityEngine;
using System.Collections;
using com.lovelydog.movieschallenge;

public class PlayerCategoryScript : FacadeMonoBehaviour {

	void Awake () {
		_dispatcher.AddListener ("update_player_categories", updateCategories);
	}

	void updateCategories(Object categories) {
		Debug.Log (categories);
	}
}
