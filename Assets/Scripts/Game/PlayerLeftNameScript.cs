using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLeftNameScript : MonoBehaviour {

	public void setName(string name) {
		transform.GetComponent<Text> ().text = name;
	}
}
