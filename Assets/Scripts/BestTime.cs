using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BestTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnEnable() {
		GetComponent<Text> ().text = PlayerPrefs.GetInt ("best").ToString ();
	}

	// Update is called once per frame
	void Update () {
		//int fps = (int)(1.0f / Time.deltaTime);
		//GetComponent<Text> ().text = fps.ToString ();
	}
}
