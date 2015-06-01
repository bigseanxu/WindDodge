using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.SocialPlatforms;
public class GameCenter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Social.localUser.Authenticate (ProcessAuthentication);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// This function gets called when Authenticate completes
	// Note that if the operation is successful, Social.localUser will contain data from the server. 
	void ProcessAuthentication (bool success) {
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
		Social.ShowLeaderboardUI ();
	}

	public void ReportScore(long score) {
		Social.ReportScore (score, "point", success => {
			Debug.Log(success ? "Reported score successfully" : "Failed to report score");
		});
	}

}
