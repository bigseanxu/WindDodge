using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BestTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnEnable() {
		Debug.Log ("best time on enable");
		GetComponent<Text> ().text = PlayerPrefs.GetInt ("best").ToString ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
