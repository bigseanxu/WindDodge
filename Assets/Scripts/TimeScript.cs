using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeScript : MonoBehaviour {

	float time = 0;
	int score = 0;

	public Transform heroBall;
	public Transform scoreUpAudio;
	public Transform gameOverAudio;

	// Use this for initialization
	void Start () {
		heroBall.GetComponent<HeroBall> ().GameOver += OnGameOver;
		heroBall.GetComponent<HeroBall> ().GetScore += OnGetScore;
	}

	void OnEnable() {
		Debug.Log ("time on enable");
	}

	void OnDisable() {
		time = 0;
	}

	// Update is called once per frame
	void Update () {
		HeroBall hero = heroBall.GetComponent<HeroBall>();
		if (Game.status == Game.GameStatus.Enjoy && hero.isActiveAndEnabled) {
			time += Time.deltaTime;		
		}
		Text a = GetComponent<Text> ();

//time mode
//		if (time >= 100) {
//			a.text = ((int)time).ToString ();
//		} else {
//			a.text = time.ToString ();
//		}


		// score mode
		a.text = score.ToString ();
	}

	void OnGameOver(object sender, EventArgs e) {
		PlayerPrefs.SetInt ("current", score);
		gameOverAudio.GetComponent<AudioSource> ().Play ();
	}

	void OnGetScore(object sender, EventArgs e) {
		score++;
		scoreUpAudio.GetComponent<AudioSource> ().Play ();
	}
}
