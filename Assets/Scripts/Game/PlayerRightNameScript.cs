using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerRightNameScript : MonoBehaviour {

	public void setName(string name) {
		transform.GetComponent<Text> ().text = name;
	}
}
