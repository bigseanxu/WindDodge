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

//	public class Level {
//		int paperPerSec;
//		float velocity;
//		float wind;
//	}	

//	public static Level[] level = new Level [] {
//		{6, 100, 0.5f},
//		{7, 150, 1},
//		{8, 200, 1.5f},
//		{10, 180, 1.5f},
//		{10, 180, 1.5f},
//	};


}

