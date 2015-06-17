using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Umeng;

public class GameEntrance : MonoBehaviour {

	private InterstitialAd interstitial;
	public float interval = 60;
	static private float time = 0;

	bool isUIOnclick = false;

	// Use this for initialization
	void Awake() {
		Game.status = Game.GameStatus.Welcome;
		Application.targetFrameRate = 60;
		Debug.Log ("game entrance awake");
	}

	void Start () {
		RequestInterstitial();

		Game.soundOn = PlayerPrefs.GetInt ("sound", 1) > 0 ? true : false;
		Game.musicOn = PlayerPrefs.GetInt ("music", 1) > 0 ? true : false;

		#if UNITY_ANDROID
		GameObject gameCenter = GameObject.Find ("GameCenter");
		gameCenter.SetActive(false);
		#endif
	}

	void Update() {
		time += Time.deltaTime;
//		if (Input.GetMouseButtonUp(0)) {
//
//			Game.status = Game.GameStatus.Enjoy;
//			Debug.Log ("mouse up");
//		}
//
//		if (Input.touchCount > 0) {
//			if (Input.GetTouch(0).phase == TouchPhase.Ended) {
//				Game.status = Game.GameStatus.Enjoy;
//			}
//			Debug.Log ("touchpahse = " + Input.GetTouch(0).phase);
//		}

		if (Game.status == Game.GameStatus.Welcome) {

#if UNITY_EDITOR
			if (Input.GetMouseButtonDown (0) && (!EventSystem.current.IsPointerOverGameObject() || isUIOnclick)) {
#else
			if (Input.GetMouseButtonDown (0) && (!EventSystem.current.IsPointerOverGameObject(0) || isUIOnclick)) {
#endif
				
				
//				Debug.Log ("event system " + EventSystem.current.IsPointerOverGameObject().ToString());
				startGame();
			}
		}
	}

	public void startGame() {
		playGameStartAni ();
		Game.setGameStatus (Game.GameStatus.Enjoy);
	}

	void playGameStartAni() {
		GameObject logo = GameObject.Find ("Logo");
		if (logo == null) {
			Debug.Log ("logo is null");
		}

		logo.GetComponent<Animator> ().Play ("Logo");

		GameObject touchToPlay = GameObject.Find ("TouchToPlay");
		touchToPlay.GetComponent<Animator> ().Play ("TouchToPlay");

//		GameObject rank = GameObject.Find ("Rank");
//		rank.GetComponent<Animator> ().Play ("Rank");
//		rank.GetComponent<Button> ().interactable = false;
//
//		GameObject rate = GameObject.Find ("Rate");
//		rate.GetComponent<Animator> ().Play ("Rate");
//		rate.GetComponent<Button> ().interactable = false;
//
//		GameObject todo = GameObject.Find ("Todo");
//		todo.GetComponent<Animator> ().Play ("Todo");
//		todo.GetComponent<Button> ().interactable = false;

		GameObject music = GameObject.Find ("Music");
		if (music != null) {
			music.GetComponent<Animator> ().Play ("Music");
			music.GetComponent<Toggle> ().interactable = false;
		}
		
		GameObject sound = GameObject.Find ("Sound");
		sound.GetComponent<Animator> ().Play ("Sound");
		sound.GetComponent<Toggle> ().interactable = false;

#if UNITY_ANDROID
		
#else
		GameObject gameCenter = GameObject.Find ("GameCenter");
		gameCenter.GetComponent<Animator> ().Play ("GameCenter");
		gameCenter.GetComponent<Button> ().interactable = false;
#endif
	}

	private void RequestInterstitial()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-7896569660771969/4598629737";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-7896569660771969/9168430136";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create an interstitial.
		interstitial = new InterstitialAd(adUnitId);
		// Register for ad events.
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosing += HandleInterstitialClosing;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
		// Load an interstitial ad.
		interstitial.LoadAd(createAdRequest());
	}

	// Returns an ad request with custom ad targeting.
	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
				.AddKeyword("game")
				.SetGender(Gender.Male)
				.SetBirthday(new DateTime(1985, 1, 1))
				.TagForChildDirectedTreatment(false)
				.AddExtra("color_bg", "9B30FF")
				.Build();
		
	}

	public void ShowInterstitial()
	{
		if (interstitial.IsLoaded())
		{
			if (time > interval) {
				interstitial.Show();
				time = 0;
			}	
		}
		else
		{
			print("Interstitial is not ready yet.");
		}
	}

	#region Interstitial callback handlers
	
	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		print("HandleInterstitialLoaded event received.");
	}
	
	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}
	
	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		print("HandleInterstitialOpened event received");
	}
	
	void HandleInterstitialClosing(object sender, EventArgs args)
	{
		print("HandleInterstitialClosing event received");
	}
	
	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		print("HandleInterstitialClosed event received");
	}
	
	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		print("HandleInterstitialLeftApplication event received");
	}
	
	#endregion

	public void onPointDown() {
		Debug.Log ("onPointDown");
		isUIOnclick = true;
	}

	public void onPointUp() {
		Debug.Log ("onPointUp");
		isUIOnclick = false;
	}
}
