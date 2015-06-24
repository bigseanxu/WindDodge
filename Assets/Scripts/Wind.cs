using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {
	// Use this for initialization
	private float defaultStrength = -5;
	private float offset = 10;

	public Vector2 strength;

	public float minStrNeg = -10;
	public float maxStrNeg = -5;
	public float minStrPos = 5;
	public float maxStrPos = 10;

	public float time2ChangeStength = 5;
	private float timePast = 0;

	public float timeToBeMoreDifficult = 10;
	public float strenthMovement = 1;

	public float maxStr;

	private Game.GameStatus status;
	void Start () {
		strength = new Vector2 (defaultStrength, 0);
		status = Game.status;

	}

	void Awake() {
	}

	void FixedUpdate() {
		bool statusChanged = false;
		if (Game.status != status) {
			statusChanged = true;
			status = Game.status;
		}

		if (statusChanged) {
			if (status == Game.GameStatus.Enjoy) {
				Invoke ("invodeDifficultAddition", timeToBeMoreDifficult);
			}
		}

		timePast += Time.deltaTime;
		
		if (timePast >= time2ChangeStength) {
			float x1 = Random.Range (minStrNeg, maxStrNeg);
			float x2 = Random.Range (minStrPos, maxStrPos);
			strength.x = Random.value > 0.5f ? x1 : x2;
			timePast -= time2ChangeStength;
		}
		
		GameObject[] objects = GameObject.FindGameObjectsWithTag("paper");
		
		//		Debug.Log ("length = " + objects.Length);
		for (int i = 0; i < objects.Length; i++) {
			objects[i].GetComponent<Rigidbody2D>().AddForce(strength);
		}
	}

	// Update is called once per frame
	void Update () {

	}

	void invodeDifficultAddition() {
		minStrNeg -= strenthMovement;
		maxStrNeg -= strenthMovement;
		minStrPos += strenthMovement;
		maxStrPos += strenthMovement;
		Invoke ("invodeDifficultAddition", timeToBeMoreDifficult);
	}
}
