using UnityEngine;
using System.Collections;

public class ShieldHeroBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		Time.timeScale = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void deactive() {
		gameObject.SetActive (false);
		Color temp = GetComponent<SpriteRenderer> ().color;
		temp.a = 255;
		GetComponent<SpriteRenderer> ().color = temp;
	}
}
