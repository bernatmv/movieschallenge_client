using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCategoryProgressScript : MonoBehaviour {

	public int category = 0;
	Image categoryImage;

	void Awake() {
		// get component
		categoryImage = transform.GetComponent<Image> ();
	}

	public void updateCategory(int[] progress) {
		if (category > 0) {
			int index = category - 1;
			categoryImage.color = Properties.categoriesColor[index];
			categoryImage.enabled = (progress[index] >= 4) ? true : false;
		}
	}
}
