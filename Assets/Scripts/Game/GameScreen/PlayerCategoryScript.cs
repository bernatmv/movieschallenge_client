using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog.movieschallenge;

public class PlayerCategoryScript : FacadeMonoBehaviour {

	public int categoryId = 0;
	public bool challenger = true;
	public Color color;

	void Awake () {
		_dispatcher.AddListener ("update_categories", updateCategories);
	}

	void updateCategories(Object game) {
		Image categoryImage;
		int[] categories;
		if (challenger) {
			categories = ((GameModel) game).players.challenger.categoriesProgress;
		}
		else {
			categories = ((GameModel) game).players.challenged.categoriesProgress;
		}
		if (categories [categoryId - 1] == 4) {
			categoryImage = transform.GetComponent<Image>();
			categoryImage.color = Properties.categoriesColor[categoryId - 1];
		}
	}
}
