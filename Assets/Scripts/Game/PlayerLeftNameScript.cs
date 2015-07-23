using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.lovelydog;

public class PlayerLeftNameScript : MonoBehaviour {

	public void setName(string name) {
		transform.GetComponent<Text> ().text = Utils.limitString(name, 9);
	}
}
