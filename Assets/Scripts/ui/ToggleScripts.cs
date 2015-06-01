using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleScripts : MonoBehaviour {

	public Sprite onImage;
	public Sprite offImage;
	// Use this for initialization
	void Start () {
//		Debug.Log (gameObject.name);
		if (gameObject.name == "Sound") {
			if (!Game.soundOn) {
				GetComponent<Toggle>().isOn = false;
			}
		}

		if (gameObject.name == "Music") {
			if (!Game.musicOn) {
				GetComponent<Toggle>().isOn = false;
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void OnToggleValueChanged() {
		if (GetComponent<Toggle> ().isOn) {
			gameObject.GetComponent<Image> ().sprite = onImage;
			if (gameObject.name == "Sound") {
				PlayerPrefs.SetInt("sound", 1);
			} else if (gameObject.name == "Music") {
				PlayerPrefs.SetInt("music", 1);
			}
		} else {
			gameObject.GetComponent<Image> ().sprite = offImage;
			if (gameObject.name == "Sound") {
				PlayerPrefs.SetInt("sound", 0);
			} else if (gameObject.name == "Music") {
				PlayerPrefs.SetInt("music", 0);
			}
		}
	}
}
