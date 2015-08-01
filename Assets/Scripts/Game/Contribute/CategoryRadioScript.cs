using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;
using com.lovelydog.movieschallenge;

public class CategoryRadioScript : FacadeMonoBehaviour {

	public Text label;
	public Image bgImage;
	public CanvasGroup bgCanvas;
	public CanvasGroup check;
	public int category;

	void Awake() {
		label.text = _i18n.get ("CATEGORY_" + category);
		bgImage.color = Properties.categoriesColor [category - 1];
		_dispatcher.AddListener ("category_check", uncheckThis);
	}

	public void checkThis() {
		check.alpha = 1f;
		bgCanvas.alpha = 0f;
		_dispatcher.Dispatch ("category_check", new PayloadObject (category));
	}

	public void uncheckThis(Object data) {
		int checkedCategory = ((PayloadObject)data).intPayload;
		if (category != checkedCategory) {
			bgCanvas.alpha = 1f;
			check.alpha = 0f;
		}
	}
}
