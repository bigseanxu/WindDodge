using UnityEngine;
using System.Collections;

public class ScoreParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!GetComponent<ParticleSystem> ().isPlaying) {
			gameObject.SetActive(false);
		}
	}
}
