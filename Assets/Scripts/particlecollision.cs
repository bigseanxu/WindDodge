using UnityEngine;
using System.Collections;

public class particlecollision : MonoBehaviour {

	public void Start() {
		Debug.Log ("start");

	}
		 
	public void Update() {
		GameObject a = GameObject.Find ("Plane");
		
		if (a != null) {
			Debug.Log ("plane plane");
		}
	}
	void OnParticleCollision(GameObject other) {
		Rigidbody2D rd = GetComponent<Rigidbody2D> ();

		Debug.Log (other.name + this.name);

		GameObject a = GameObject.Find ("Plane");
		
		if (a == null) {
			Debug.Log ("a == null");
		} else if (a.transform == null) {
			Debug.Log ("transform == null");
		} else {
			Debug.Log ("x:" + a.transform.position.x + "y:" + a.transform.position.y + "z:" + a.transform.position.z);
		}

	}
}
