using UnityEngine;
using System.Collections;

public class GameItemGen : MonoBehaviour {

	int i = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (i == 0) {
			GameObject shield = (GameObject) Instantiate ((GameObject) Resources.Load ("ItemShield"), new Vector3(0, 3, 0), new Quaternion());
//			GameItem gameItem = shield.GetComponent<GameItem>();
//			gameItem.terminal = transform.FindChild("ShieldRef");
			i = 1;
		}


	}
}
