using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;

public class PlayerRightNameScript : MonoBehaviour {

	Text name;
	string _rawName;

	void Awake() {
		// get component
		name = transform.GetComponent<Text>();
	}

	public void setName(string newName) {
		_rawName = newName;
		name.text = Utils.limitString(newName, 9);
	}

	public string getName() {
		return name.text;
	}

	public string getNameRaw() {
		return _rawName;
	}
}
