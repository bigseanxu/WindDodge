using UnityEngine;
using System.Collections;
using System;
using Umeng;

public class ScreenDirector : MonoBehaviour {
	public Transform group1;
	public Transform group2;
	public Transform group3;
	public Transform heroBall;
	public Transform wind;
	public Transform paperEmitter;
	public Transform gameEntrance;
	Game.GameStatus status = Game.GameStatus.Welcome;

	public Transform newBestAudio;
	public Transform fireworks;

	public Transform shareImageGenerator;
	public Color gameOverBgColor = new Color(171, 169, 167, 255);

	string umengAppkey = "5566828d67e58e6ccb0017bf";


	// Use this for initialization
	void Start () {
		heroBall.GetComponent<HeroBall> ().GameOver += OnGameOver;
		group1.gameObject.SetActive(true);
		group2.gameObject.SetActive(false);
		group3.gameObject.SetActive(false);
		heroBall.gameObject.SetActive(true);
		wind.gameObject.SetActive(true);
		paperEmitter.gameObject.SetActive(true);

		GA.StartWithAppKeyAndChannelId(umengAppkey, "App Store");
	}
	
	// Update is called once per frame
	void Update () {
		bool statusChanged = false;

		Game.GameStatus stat = Game.status;
		if (status != Game.status) {
			statusChanged = true;
			status = stat;
		}

		if (!statusChanged) {
			return;
		}

		if (status == Game.GameStatus.Farewell) {
			group1.gameObject.SetActive(false);
			group2.gameObject.SetActive(false);
			group3.gameObject.SetActive(true);
			wind.gameObject.SetActive(false);
			paperEmitter.gameObject.SetActive(false);

			shareImageGenerator.gameObject.SetActive(true);

			int current = PlayerPrefs.GetInt ("current");
			int best = PlayerPrefs.GetInt ("best");

			shareImageGenerator.GetComponent<ShowNumberInCanvas> ().SetNumber(current);
			shareImageGenerator.GetComponent<ShareImageGenerator> ().TakeHiResShot ();
			//	Debug.Log ("current is " + current + " best is " + best);

			if (current > best) {
				PlayerPrefs.SetInt("best", current);
				gameEntrance.GetComponent<GameCenter>().ReportScore(current);
				fireworks.gameObject.SetActive (true);
				newBestAudio.GetComponent<AudioSource>().Play();
			}
			Camera.main.backgroundColor = gameOverBgColor;
			gameEntrance.GetComponent<GameEntrance>().ShowInterstitial();
			GA.FailLevel("level 1");
		}

		if (status == Game.GameStatus.Enjoy) {
			// group1.gameObject.SetActive(false);
			group2.gameObject.SetActive(true);
			group3.gameObject.SetActive(false);
			GA.StartLevel("level 1");
		} 

		if (status == Game.GameStatus.Welcome) {
			group1.gameObject.SetActive(true);
			group2.gameObject.SetActive(false);
			group3.gameObject.SetActive(false);
			heroBall.gameObject.SetActive(true);
			wind.gameObject.SetActive(true);
			paperEmitter.gameObject.SetActive(true);
		}
	}

	void OnGameOver(object sender, EventArgs e) {

		Debug.Log ("on game over 222");
	}
}
