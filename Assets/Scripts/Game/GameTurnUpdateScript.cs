using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTurnUpdateScript : MonoBehaviour {

	public void setTurn(int currentTurn) {
		transform.GetComponent<Text> ().text = "Turn " + currentTurn;
	}
}
