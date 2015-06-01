using UnityEngine;
using System.Collections;

public static class Game
{
	public enum GameStatus {
		Welcome,
		Enjoy,
		Farewell
	};

	public static GameStatus status = GameStatus.Welcome;

	public static void setGameStatus(GameStatus stat) {
		status = stat;
	}

	public static bool soundOn = false;
	public static bool musicOn = false;
}

