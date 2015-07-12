using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCategoryProgressScript : MonoBehaviour {

	public int category = 0;

	public void updateCategory(int[] progress) {
		if (category > 0) {
			int index = category - 1;
			transform.GetComponent<Image> ().enabled = (progress[index] >= 4) ? true : false;
		}
	}
}
