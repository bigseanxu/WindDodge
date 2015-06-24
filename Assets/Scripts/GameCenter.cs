using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.SocialPlatforms;
#if UNITY_ANDROID 
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
public class GameCenter : MonoBehaviour {
#if UNITY_ANDROID
	bool isPlayGamesPlayformActivate = false;
#endif
	// Use this for initialization
	void Start () {
#if UNITY_ANDROID
		if (!isPlayGamesPlayformActivate) {
			isPlayGamesPlayformActivate = true;
			PlayGamesPlatform.Activate();
			Debug.Log("PlayGamesPlatform.Activate()");
		}
#endif
		Social.localUser.Authenticate (ProcessAuthentication);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// This function gets called when Authenticate completes
	// Note that if the operation is successful, Social.localUser will contain data from the server. 
	void ProcessAuthentication (bool success) {
		Debug.Log ("mr.x on ProcessAuthentication");
		if (success) {
			Debug.Log ("Authenticated, checking achievements");
			
			// Request loaded achievements, and register a callback for processing them
			Social.LoadAchievements (ProcessLoadedAchievements);
			Social.LoadAchievementDescriptions (descriptions => {
				if (descriptions.Length > 0) {
					Debug.Log ("Got " + descriptions.Length + " achievement descriptions");
					string achievementDescriptions = "Achievement Descriptions:\n";
					foreach (IAchievementDescription ad in descriptions) {
						achievementDescriptions += "\t" +
							ad.id + " " +
								ad.title + " " +
								ad.unachievedDescription + "\n";
					}
					Debug.Log (achievementDescriptions);
				}
				else
					Debug.Log ("Failed to load achievement descriptions");
			});
		}
		else
			Debug.Log ("Failed to authenticate");
	}	
	
	// This function gets called when the LoadAchievement call completes
	void ProcessLoadedAchievements (IAchievement[] achievements) {
		if (achievements.Length == 0)
			Debug.Log ("Error: no achievements found");
		else
			Debug.Log ("Got " + achievements.Length + " achievements");
		
		// You can also call into the functions like this
		Social.ReportProgress ("achieve_1", 0.0, result => {
			if (result)
				Debug.Log ("Successfully reported achievement progress");
			else
				Debug.Log ("Failed to report achievement");
		});
	}
	
	public void LoadLeaderboard() {
#if UNITY_ANDROID
		PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIt5fH8s4EEAIQBw");
#elif UNITY_IPHONE
		Social.ShowLeaderboardUI ();
#endif
#if UNITY_EDITOR
		PlayerPrefs.DeleteAll();
#endif
	}

	public void ReportScore(long score) {
#if UNITY_ANDROID
		string id = "CgkIt5fH8s4EEAIQBw";
#else
		string id = "point";
#endif
		Social.ReportScore (score, id, success => {
			Debug.Log(success ? "Reported score successfully" : "Failed to report score");
		});
	}

}
