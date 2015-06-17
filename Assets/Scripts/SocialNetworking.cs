using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class SocialNetworking : MonoBehaviour {

	[ DllImport( "__Internal" )]
	private static extern int shareWithFacebook ( string title, string msg, string image);
	[ DllImport( "__Internal" )]
	private static extern int shareWithTwitter ( string title, string msg, string image);

	public Transform shareImageGenerator;
	private enum ShareType{
		Facebook,
		Twitter,
		Weibo
	};
	private ShareType type;

#if UNITY_ANDROID
	private AndroidJavaObject m_activity; 
#endif
	// Use this for initialization
	void Start () {
#if UNITY_ANDROID
//		AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");  
//		m_activity = jc.GetStatic<AndroidJavaObject> ("currentActivity");  
#endif
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate() {
	//	Debug.Log ("lateupdate");
	}

	public void OnFacebookShare() {
		//type = ShareType.Facebook;
		//shareWithFacebook ("title can be modified", "message can be modified");
		//CaptureAPicureAsync ();
		//Debug.Log ("facebook button is clicked.");
		int point = PlayerPrefs.GetInt ("current");
#if UNITY_IPHONE
		shareWithFacebook ("Facebook", " ", Application.persistentDataPath + "/screenshot.png");
#elif UNITY_ANDROID
		using (AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
			using (m_activity = jc.GetStatic<AndroidJavaObject> ("currentActivity")) {
				m_activity.Call("shareToFacebook", "Wind Dodge!  I scored " + point + " points. Can you beat me?");	
			}
		}
#endif
	}

	public void OnTwitterShare() {
		//shareWithTwitter ("title can be modified", "message can be modified");
		//type = ShareType.Twitter;
		//CaptureAPicureAsync ();
		int point = PlayerPrefs.GetInt ("current");
#if UNITY_IPHONE
		shareWithTwitter ("Twitter", "Wind Dodge!  I scored " + point + " points. Can you beat me? #winddodge", Application.persistentDataPath + "/screenshot.png");
#elif UNITY_ANDROID
		using (AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
			using (m_activity = jc.GetStatic<AndroidJavaObject> ("currentActivity")) {
				m_activity.Call("shareToTwitter", "Wind Dodge!  I scored " + point + " points. Can you beat me? #winddodge");	
			}
		}
#endif
	}

	void CaptureAPicureAsync() {
		StartCoroutine (StartCoroutineForShare());
		shareImageGenerator.GetComponent<ShareImageGenerator> ().TakeHiResShot ();
	}

	// Todo
	void DeleteCapturedPicture() {

	}

	IEnumerator StartCoroutineForShare() {
		yield return new WaitForEndOfFrame();
		if (type == ShareType.Facebook) {
			shareWithFacebook ("title can be modified", "message can be modified", Application.persistentDataPath + "/screenshot.png");
		} else if (type == ShareType.Twitter) {
			shareWithTwitter ("title can be modified", "message can be modified", Application.persistentDataPath + "/screenshot.png");
		} else if (type == ShareType.Weibo) {
			Debug.Log ("post to weibo is to be done.");
		} else {
			Debug.Log ("StartCoroutineForShare get error");
		}
	}
	
}