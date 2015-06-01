using UnityEngine;
using System.Collections;

public class Tornado : MonoBehaviour {

	float angle = -45;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		transform.Translate (0, -0.01f, 0);
	}

	void FixedUpdate() {
//		GameObject[] objects = GameObject.FindGameObjectsWithTag("paper");
//		for (int i = 0; i < objects.Length; i++) {
//			Vector3 dir = transform.position - objects[i].transform.position;
//			float dis = Vector3.Distance(transform.position, objects[i].transform.position);
//			if (dis < 1) dis = 1;
//			if (dis > 10) continue;
//			Vector2 strength = new Vector2(dir.x * k / (dis * dis), dir.y * k / ((dis * dis)));
//		//	Debug.Log ("strength is " + strength.ToString());
//			objects[i].GetComponent<Rigidbody2D>().AddForce(strength);
//		}

		GameObject[] objects = GameObject.FindGameObjectsWithTag("paper");
		for (int i = 0; i < objects.Length; i++) {
			Vector3 dir = transform.position - objects[i].transform.position;
			float dis = Vector3.Distance(transform.position, objects[i].transform.position);
			Quaternion rot = Quaternion.Euler(0, 0, angle);

			Vector3 strength3 = rot * dir;
			Vector2 strength = new Vector2(strength3.x, strength3.y);

			Vector2 dirStr = new Vector2(dir.x, dir.y);
			Vector2 normalDirStr = dirStr.normalized;

			objects[i].GetComponent<Rigidbody2D>().AddForce(strength);
//			objects[i].GetComponent<Rigidbody2D>().AddForce(normalDirStr * 500 / dis);

			Debug.DrawLine(transform.position, strength3 + transform.position);
		}

	}
}
