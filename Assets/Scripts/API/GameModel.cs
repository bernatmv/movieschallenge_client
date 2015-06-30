using UnityEngine;
using System.Collections;

public class GameModel {

	public class PlayerModel {
		public string id;
		public int[] categoriesProgress;
		public string[] questionsAnswered;
	}

	public class PlayersGroup {
		public PlayerModel challenger;
		public PlayerModel challenged;
	}

	public int turn;
	public PlayersGroup players;
	public string[] plays;
	public string thisTurn;
	public bool ended;
	public string lastPlay;
	public string winner;
}
